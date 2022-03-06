param
(
    [Alias("a")][Switch]$BuildAll,
    [Alias("c")][String]$Configuration = "Release",
    [Alias("d")][Switch]$Clean,
    [Alias("h")][Switch]$Help,
    [Alias("p")][String]$Profile
)

$profileData = ".profiles"

if ($Help)
{
    echo "Marathon Build Script"
    echo ""
    echo "All your platforms are belong to us."
    echo ""
    echo "Usage:"
    echo "-a|-BuildAll - build Marathon with all available profiles."
    echo "-c|-Configuration [name] - build Marathon with a specific configuration."
    echo "-d|-Clean - cleans the solution before building Marathon."
    echo "-h|-Help - display help."
    echo "-p|-Profile [name] - build Marathon with a specific profile."
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

if (![System.String]::IsNullOrEmpty($Profile))
{
    if ([System.Array]::IndexOf([System.IO.File]::ReadAllLines($profileData), $Profile) -eq -1)
    {
        echo "No such profile exists: $Profile"
        exit
    }

    dotnet publish /p:Configuration="$Configuration" /p:PublishProfile="$Profile"
    
    if (!$BuildAll)
    {
        exit
    }
}

if ($BuildAll)
{
    foreach ($line in [System.IO.File]::ReadAllLines($profileData))
    {
        # Skip profile if it was already specified and built.
        if ($line -eq $Profile)
        {
            continue
        }
        
        dotnet publish /p:Configuration="$Configuration" /p:PublishProfile="$line"
    }
}
else
{
    dotnet publish /p:Configuration="$Configuration" /p:PublishProfile="win-x86"
    dotnet publish /p:Configuration="$Configuration" /p:PublishProfile="win-x64"
}