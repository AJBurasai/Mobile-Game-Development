# Desktop harness for the Week 7 save and sync logic

This is NOT part of the Unity project and Unity ignores it (the C# files are named `.cs.txt`). It compiles the real
`Scripts/Save`, `Scripts/Cloud` and `Scripts/Net` code against hand-written stand-ins for the Unity and Unity Gaming
Services APIs, with a real JSON serialiser (System.Text.Json with fields), an in-memory PlayerPrefs, and a fake Cloud Save
service that stores one value per key with a write lock and throws `CloudSaveConflictException` on a lock mismatch.

Run it with the .NET 8 SDK installed:

    cd DesktopHarness
    dotnet run
    dotnet run -- --gated

Scenarios (all must print PASS):

A. restore from the cloud does not loop (one upload, then "in sync")
B. unsaved local edits turn a cloud change into a conflict, edits intact
C. resolving "keep local" uploads the local copy with the write lock
D. the recorded ancestor matches the normal upload payload
E. a failed download uploads nothing; the next successful read does
F. two devices diverging is a conflict; "keep cloud" adopts the cloud copy
G. a new game is never mistaken for "in sync"
H. corrupt file loads from backup; missing file starts fresh; a newer-build file is preserved and saving refused
I. another device writing between our read and our upload: lock rejected, re-read, conflict raised
J. no cloud copy plus unsaved edits: the edits are saved first and uploaded under their new ID
K. turning cloud sync off during a download stops the restore from being applied

The gated suite uses the pack’s `TestCloudStore` implementation:

L. hold two uploads separately and check the recorded ancestor between them
M. fail a read and verify the number of attempted writes stays unchanged
N. disable sync while a download is held
O. edit local progress while a divergent download is held
P. simulate another device changing the key while a locked upload is held

Both commands return a non-zero exit code if an assertion fails. A desktop pass does not establish Unity compilation,
real device behaviour or live UGS behaviour. Scenario I uses a synchronous hook; scenario P holds the write pending.
