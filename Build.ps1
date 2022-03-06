param
(
    [Alias("a")][Switch]$all,
    [Alias("c")][String]$config = "Release",
    [Alias("d")][Switch]$clean,
    [Alias("h")][Switch]$help,
    [Alias("p")][String]$profile
)

$profileData = ".profiles"

if ($help)
{
    echo "Marathon Build Script"
    echo ""
    echo "All your platforms are belong to us."
    echo ""
    echo "Usage:"
    echo "-a|-all - build Marathon with all available profiles."
    echo "-c|-config [name] - build Marathon with a specific configuration."
    echo "-d|-clean - cleans the solution before building Marathon."
    echo "-h|-help - display help."
    echo "-p|-profile [name] - build Marathon with a specific profile."
    exit
}

# Check if the .NET SDK is installed.
if (!(Get-Command -Name dotnet -ErrorAction SilentlyContinue))
{
    echo ".NET SDK is required to build Marathon."
    echo "You can install the required .NET SDK for Windows from here: https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-6.0.200-windows-x64-installer"
    exit
}

if ($clean)
{
    dotnet clean
}

if (![System.String]::IsNullOrEmpty($profile))
{
    if ([System.Array]::IndexOf([System.IO.File]::ReadAllLines($profileData), $profile) -eq -1)
    {
        echo "No such profile exists: $profile"
        exit
    }

    dotnet publish /p:Configuration="$config" /p:PublishProfile="$profile"
    
    if (!$buildAll)
    {
        exit
    }
}

if ($buildAll)
{
    foreach ($line in [System.IO.File]::ReadAllLines($profileData))
    {
        # Skip profile if it was already specified and built.
        if ($line -eq $profile)
        {
            continue
        }
        
        dotnet publish /p:Configuration="$config" /p:PublishProfile="$line"
    }
}
else
{
    dotnet publish /p:Configuration="$config" /p:PublishProfile="win-x86"
    dotnet publish /p:Configuration="$config" /p:PublishProfile="win-x64"
}