$version = Get-Content VERSION;

# Move Resources to Release dir
if(Test-Path -Path "$PSScriptRoot/bin/Release/Resources" ) {
    move $PSScriptRoot/bin/Release/Resources/* $PSScriptRoot/bin/Release/
    rmdir -r $PSScriptRoot/bin/Release/Resources/
}

# Create temporary Output dir
mkdir $PSScriptRoot/bin/Output

# Pt1 Create output.zip
$required_files = @(
    # WebMCam
    "WebMCam.exe",
    "license.md",

    # NAudio
    "NAudio.dll",
    "NAudio-license.txt"
)

foreach ($file in $required_files) {
    copy $PSScriptRoot/bin/Release/$file $PSScriptRoot/bin/Output/$file
}

Add-Type -A System.IO.Compression.FileSystem
[IO.Compression.ZipFile]::CreateFromDirectory("$PSScriptRoot/bin/Output", "$PSScriptRoot/bin/WebMCam-$version.zip")

# Pt2 Create output-ffmpeg.zip
$ffmpeg_files = @(
    # FFmpeg
    "ffmpeg.exe",
    "ffmpeg-license.txt"
);

foreach ($file in $ffmpeg_files) {
    copy $PSScriptRoot/bin/Release/$file $PSScriptRoot/bin/Output/$file
}

[IO.Compression.ZipFile]::CreateFromDirectory("$PSScriptRoot/bin/Output", "$PSScriptRoot/bin/WebMCam-$version-FFmpeg.zip")

# Cleanup
rmdir -r bin/Output