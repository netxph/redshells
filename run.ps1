$project_path = $pwd.path
powershell -NoProfile -NoExit -NoLogo -Command "Import-Module $project_path\artifacts\RedShells.PowerShell.dll"
