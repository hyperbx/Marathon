param
(
    [Switch]$BuildAll,
    [String]$CommitID,
    [String]$Configuration = "Release",
    [Switch]$Clean,
    [Switch]$Help,
    [String]$Profile,
    [Switch]$UseFullCommitID,
    [String]$Version
)

$profiles = @("win-x86", "win-x64", "linux-x64", "osx-x64")
$versionPatcher = ".github\workflows\VersionPatcher.ps1"

if ($Help)
{
    echo "Marathon Build Script"
    echo ""
    echo "All your platforms are belong to us."
    echo ""
    echo "Usage:"
    echo "-BuildAll - build Marathon with all available profiles."
    echo "-CommitID [id] - set the commit ID to use from GitHub for the version number."
    echo "-Configuration [name] - build Marathon with a specific configuration."
    echo "-Clean - cleans the solution before building Marathon."
    echo "-Help - display help."
    echo "-Profile [name] - build Marathon with a specific profile."
    echo "-UseFullCommitID - use the full 40 character commit ID for the version number."
    echo "-Version [major].[minor].[revision] - set the version number for this build of Marathon."
    exit
}

# Check if the .NET SDK is installed.
if (!(Get-Command -Name dotnet -ErrorAction SilentlyContinue))
{
    echo ".NET SDK is required to build Marathon."
    echo "You can install the required .NET SDK for Windows from here: https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-6.0.200-windows-x64-installer"
    exit
}

if ($Clean)
{
    dotnet clean
}

function PatchVersionInformation([String]$commitID, [Boolean]$useFullCommitID, [String]$version)
{
    # Patch the version number for all projects.
    if (![System.String]::IsNullOrEmpty($version))
    {
        foreach ($project in [System.IO.Directory]::EnumerateFiles(".", "*.csproj", [System.IO.SearchOption]::AllDirectories))
        {
            & "${versionPatcher}" -CommitID $commitID -ProjectPath "${project}" -Version $version
        }
    }
}

function Build([String]$configuration, [String]$profile)
{
    # Patch version number before building.
    PatchVersionInformation $CommitID $UseFullCommitID $Version

    dotnet publish /p:Configuration="${configuration}" /p:PublishProfile="${profile}"

    # Restore default version number.
    PatchVersionInformation "" $false "1.0.0"
}

if (![System.String]::IsNullOrEmpty($Profile))
{
    if ([System.Array]::IndexOf($profiles, $Profile) -eq -1)
    {
        echo "No such profile exists: ${Profile}"
        exit
    }

    Build $Configuration $Profile
    
    if (!$BuildAll)
    {
        exit
    }
}

if ($BuildAll)
{
    foreach ($currentProfile in $profiles)
    {
        # Skip profile if it was already specified and built.
        if ($currentProfile -eq $Profile)
        {
            continue
        }
        
        Build $Configuration $currentProfile
    }
}
else
{
    Build $Configuration "win-x86"
    Build $Configuration "win-x64"
}