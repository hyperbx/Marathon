param([String]$projectPath, [String]$version, [String]$commitID, [Boolean]$useFullCommitID)

$project = [System.IO.File]::ReadAllLines($projectPath)

if (!$useFullCommitID -and $commitID.Length -ne 0)
{
    $commitID = $commitID.Substring(0, 7)
}

$i = 0
foreach ($line in $project)
{
    $lineSplit = $line.Split(">")

    if ($lineSplit[0].Contains("<Version"))
    {
        $lineSplit[1] = "${version}</Version"
    }
    elseif ($lineSplit[0].Contains("<InformationalVersion"))
    {
        $lineSplit[1] = "${version}-${commitID}</InformationalVersion"
    }

    $project[$i] = [System.String]::Join(">", $lineSplit)

    $i++
}

[System.IO.File]::WriteAllLines($projectPath, $project)