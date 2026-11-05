# CMP5402 Week 7: Saving, Cloud Data \& Online scripts

Unity 6.3 LTS (6000.3). Drag the whole `CMP5402\_Week7\_Scripts` folder into your project's `Assets` folder. Everything is in
the `CMP5402.Week7` namespace. Nothing here depends on the Week 3 to 6 packs, but `SettingsPrefs` is written to sit
beside the Week 6 `QualityTiers` PlayerPrefs keys without touching them.

Use these scripts as the starter implementation for your Lab 6 game. Adapt the progress fields, settings and checkpoint
calls to your game. Complete local saving first, then cloud sync and offline play. Account recovery and controlled races
are extensions. The live UGS adapter needs Cloud Save and Authentication. Local saving and the development test store
work without those packages.

## Set-up

1. Put an empty GameObject called `Systems` in your first scene and add `SaveManager`, `NetProbe` and (when you begin cloud sync)
`CloudSync`. They survive scene loads.
2. In a test scene, add `SaveTestMenu` to any object. It only exists in the Editor and in development builds.
3. Game code: read `SaveManager.Data`, change it, call `SaveManager.MarkDirty()` (or use
`SaveManager.Change(d => d.coins += 10)`), and call `SaveManager.SaveNow("checkpoint")` at checkpoints.
The manager saves by itself on pause and on focus loss.

## Scripts

|Script|Slide|What it does|
|-|-|-|
|`Save/SaveData.cs`|One class holds everything worth keeping|The `\[Serializable]` save shape: `version`, `saveCounter` (never reset, for the conflict dialog), `saveId` (a random ID per save: the identity CloudSync compares), progress fields, a `List` instead of a dictionary. `CurrentVersion` is bumped when the shape changes.|
|`Save/SaveFile.cs`|Write it so a crash can't corrupt it|JSON in `persistentDataPath`. Writes to a unique `.tmp` then `File.Replace` into place, keeping the previous file as `.bak`; every write carries a revision and an older write finishing late is discarded. Reads main, then backup, then a fresh `SaveData`. A save from a newer build is neither loaded nor overwritten: saving pauses until `DiscardNewerSave`. `Normalise` is the single validation and migration path for local and cloud copies (null lists, out-of-range values, old versions). `persistentDataPath` is cached on the main thread for background writes.|
|`Save/SaveManager.cs`|Save on pause. Never trust quit.|The one owner of the file. `Revision` goes up on every change and `Dirty` means "newer than the last revision written", so edits made during a background write stay dirty. `SaveNow(reason)`, `Replace`, `NewGame`, `Saved` (carries the snapshot actually written) and `Loaded` events. Saves in `OnApplicationPause(true)` and `OnApplicationFocus(false)`, always synchronously; `OnApplicationQuit` is kept only for Editor and desktop. `SaveNowAsync` (save performance extension) serialises on the main thread and writes on a background thread, queuing one follow-up after a successful write if more changed meanwhile; a failed write stays dirty for the next trigger rather than retrying in a loop. `NewGame` carries the save counter on. A `ProfilerMarker` and `LastSaveMs` let you measure the write.|
|`Save/SettingsPrefs.cs`|PlayerPrefs for settings, files for progress|The settings that belong in PlayerPrefs: volumes, haptics, language, the cloud-sync opt-in. Saves on every change because `OnApplicationQuit` may never run on a phone.|
|`Cloud/CloudSync.cs`|Cloud Save in twenty lines; When the phone and the cloud disagree|Anonymous sign-in and ONE serialised sync operation that every trigger (local save, launch, reconnection, settings toggle) requests. Three-way comparison against the remembered ancestor (the save ID of the cloud copy at the last sync): only local moved, upload with the cloud write lock (`SaveItem`); only cloud moved, restore; both moved, or unsaved local edits, suspend uploads and raise `Conflict`. Decisions and uploads work on an immutable snapshot of the local data, and a restore records the downloaded copy's ID before the local save stamps a new one. A write-lock rejection re-reads and re-decides. A failed download is "unknown", never "absent", and a late download cannot overwrite progress made meanwhile. The launch wait is bounded at two seconds and the sync continues in the background. Opt-in checked on every sync; a sync that is still running when the player changes the setting applies nothing (generation check after every await); unsaved edits are saved before any upload, including the first upload to an empty cloud; `DeleteCloudCopyAsync` suspends uploads first and turns sync off; `DeleteAccountAndDataAsync` also deletes the Unity player account and local files. Needs the packages and define below.|
|`Net/NetProbe.cs`|A request, with every way it can fail; Design for the connection you actually have|`GetAsync` with a timeout and the full `UnityWebRequest.Result` switch, `ProbeAsync` with four attempts and doubling waits, automatic re-probing (15 s doubling to 5 min while offline, restarting at 15 s for each new offline episode; every 5 min while online; and once when the app returns from the background) with a `ReachabilityChanged` event that CloudSync uses to re-sync on reconnection, and `ServerNow` anchored to monotonic `realtimeSinceStartupAsDouble` so changing the phone's clock does not move it (samples expire after 24 h). `internetReachability` is used only as the cheap pre-check, as Unity's docs advise. Reaching the probe endpoint says nothing about Cloud Save's availability, and rewards that matter are still validated server-side.|
|`Tools/SaveTestMenu.cs`|The save-system test matrix|On-screen buttons: change and save, corrupt, delete, future version, reload, cloud on/off, sync now, delete cloud copy, network probe. Shows where the data was loaded from (main / backup / new / blocked / cloud), revision, the last save time, cloud and network state. It is also the fallback conflict dialog (Keep this phone's / Keep the cloud's), reading `CloudSync.PendingCloudCopy` each frame so it works whatever order the objects initialise in, and the 'discard newer save' prompt. Three-finger tap or F3 hides it.|

### Cloud Save and offline play

Window > Package Manager > Unity Registry > Cloud Save (Authentication is installed with it). Edit > Project Settings >
Services: sign in with your Unity ID and link the project to a Unity Gaming Services project (create one if you need to).
Then add `UGS\_CLOUDSAVE\_INSTALLED` to Project Settings > Player > Other Settings > Scripting Define Symbols and wire
the opt-in toggle to `CloudSync.Instance.SetEnabled(value)`. Start with the toggle off. Use a disposable test player.

Anonymous sign-in supports the required same-install restore test. Preserve the cached Authentication session when
removing local progress. For the optional reinstall and two-device tests, implement a supported identity provider and
link it to the existing player. The starter pack includes anonymous sign-in. Provider integration is part of the extension.

## Section 8 extension: test the cloud code in Unity

`CloudSync` uses `ICloudStore`. With the UGS define, `UgsCloudStore` is selected by default. In the Editor or a
development build, a test driver can create a `TestCloudStore` and install it with `CloudSync.UseTestStore` before
starting sync. The store lives in memory and its ancestor preference is separate from UGS. Use a disposable test project
or a build with a separate application identifier, because the game still writes its ordinary local save files and settings.

In a test component, assign the scene's CloudSync in the Inspector, then install the store in that component's `Awake`
before any script requests sync or changes progress. Omit the UGS define for a fully offline test build. Call
`SetEnabled(true)` in `Start` or from a test button, and await `CurrentSync` before preparing the next case.
Keep the driver behind `#if UNITY\_EDITOR || DEVELOPMENT\_BUILD`.

|Test|How to drive it|Expected observation|
|-|-|-|
|Failed read|Set `FailNextRead`, then request sync with newer local progress.|`WriteCount` stays unchanged until a successful read.|
|Delayed read|Call `HoldNextRead()`, request sync, await the gate's `Entered`, then disable sync or edit local progress. Call `Release()` and await `CurrentSync`.|A late response cannot overwrite the local progress. Edits during a read of a divergent remote copy trigger a conflict.|
|Overlapping saves|Start from a synced state. Queue two `HoldNextWrite()` gates, save once, wait for the first gate, then save again. Release the first and await the second gate.|`LastSyncedSaveId` equals the first captured payload ID while the newer local save remains available. Release the second gate and check convergence.|
|Stale write lock|Pause a locked upload and call `ExternalWrite(CloudSync.SaveKey, json)` with a distinct valid save. Release the upload.|The write is rejected, `ReadCount` increases again and the remote copy is preserved while the conflict is pending.|

`WriteCalls` records attempted payloads and locks. Use `PeekJson` to inspect the remote copy and `FailNextWrite` to
inject a write failure. Release all armed gates before leaving a test scene. See `DesktopHarness/SyncScenarios.cs.txt`
for complete gated examples with assertions.

## 

