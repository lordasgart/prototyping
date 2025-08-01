param (
    [int]$taskNumber
)

# Get the task info
$taskInfo = task $taskNumber info

# Extract the UUID from the task info output
$uuid = $taskInfo | Select-String -Pattern 'UUID\s+([a-f0-9-]+)' | ForEach-Object { $_.Matches.Groups[1].Value }

if ($uuid) {
    $homeDir = [System.Environment]::GetFolderPath('MyDocuments')
    $taskDir = Join-Path $homeDir '.task'
    $markdownFile = Join-Path $taskDir "$uuid.md"

    if (-Not (Test-Path $taskDir)) {
        New-Item -ItemType Directory -Path $taskDir
    }

    if (-Not (Test-Path $markdownFile)) {
        New-Item -ItemType File -Path $markdownFile
    }

    # Open the markdown file
    Start-Process $markdownFile
} else {
    Write-Error "UUID not found for task number $taskNumber"
}

# note() {
#     local task_number=$1
#     if [ -z "$task_number" ]; then
#         echo "Usage: note <task_number>"
#         return 1
#     fi

#     pwsh -File /path/to/get-task-uuid.ps1 -taskNumber $task_number
# }
