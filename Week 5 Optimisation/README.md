# CMP5402 Week 5: Optimisation Techniques scripts

Unity 6.3 LTS (6000.3). Drag the whole `CMP5402_Week5_Scripts` folder into your project's `Assets` folder.
Everything is in the `CMP5402.Week5` namespace. Nothing extra to install: `UnityEngine.Pool` and
`Unity.Profiling` are part of the engine.

These are the fixed counterparts to the Week 4 Inefficient Game. In the lab, write your own fix and measure it
first; then compare with these.

## Fixes

| Script | Week 4 module it fixes | Slide | What it shows |
|---|---|---|---|
| `Fixes/FastEnemy.cs` | Slow enemies | Cache it once, use it every frame | Cached Rigidbody, a static list instead of `FindObjectsByType`, a loop instead of LINQ, no per-frame strings, AI that thinks 4 times a second. |
| `Pooling/BulletPool.cs` + `Pooling/PooledBullet.cs` | Bullet spam | Object pooling with UnityEngine.Pool | `ObjectPool<T>` with pre-warm, reset on `Fire`, release instead of destroy. |
| `Tools/Log.cs` | Log spam | Debug.Log is not free | `[Conditional]` logging that the compiler removes from builds unless `ENABLE_LOGS` is defined. Errors stay. |
| `Fixes/NonAllocSensor.cs` | (per-frame garbage) | Where per-frame garbage comes from | `Physics.OverlapSphereNonAlloc` into an array you keep, on a timer, with a LayerMask. |
| `Fixes/UpdateManager.cs` | (many Updates) | Do less, less often | One `Update` that ticks a list of `ITicked` objects. |

The Overdraw, Memory hog, Physics storm and Light storm modules are fixed with settings, not scripts:
texture import (ASTC, Max Size), audio import (Load Type), Project Settings > Time and Physics, and the URP asset.
The slides list each setting.

## Evidence

`Tools/BeforeAfterLog.cs`: an on-screen RECORD button that takes a 30 second sample (after a 5 second warm-up)
and appends average frame interval, worst frame and system memory to `before_after.csv` in
`Application.persistentDataPath`. Change the `Label` field before each run ("baseline", "cached components",
"pooled bullets", ...). It works in release builds, so your final numbers can come from a release build.
GC Alloc and draw calls still come from the Profiler, which needs a development build.

## Suggested lab order

1. Baseline: Week 4 build, your phone, `BeforeAfterLog` sample, Profiler screenshot.
2. Biggest cost first (from your Week 4 report). One change. Build. Sample again.
3. Next two costs, one at a time.
4. Settings pass: Time, Physics, URP asset, texture and audio import. Sample again.
5. Before and after table with the CSV and screenshots. Note anything that got worse.

## How these were checked

The scripts compile against hand-written stand-ins for the Unity API (with and without the legacy Input Manager
define). They have not yet been compiled inside Unity 6000.3 or run on a phone. Sources for each technique are
on the last two slides of the Week 5 lecture and in SOURCES.md.
