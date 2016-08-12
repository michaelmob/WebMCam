$version = Get-Content VERSION;

# Move Resources to Release dir
if(Test-Path -Path "bin/Release/Resources" ) {
    move -f bin/Release/Resources/* bin/Release/
    rmdir bin/Release/Resources/
}

# Create temporary Output dir
mkdir bin/Output

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
    copy bin/Release/$file bin/Output/$file
}

Add-Type -A System.IO.Compression.FileSystem
[IO.Compression.ZipFile]::CreateFromDirectory("bin/Output", "bin/WebMCam-$version.zip")

# Pt2 Create output-ffmpeg.zip
$ffmpeg_files = @(
    # FFmpeg
    "ffmpeg.exe",
    "ffmpeg-license.txt"
);

foreach ($file in $ffmpeg_files) {
    copy bin/Release/$file bin/Output/$file
}

[IO.Compression.ZipFile]::CreateFromDirectory("bin/Output", "bin/WebMCam-$version-FFmpeg.zip")

# Cleanup
rmdir -r bin/Output