param
(
    [String]$ProjectPath,
    [String]$Version,
    [String]$CommitID,
    [Switch]$UseFullCommitID
)

$project = [System.IO.File]::ReadAllLines($ProjectPath)

if (!$UseFullCommitID -and $CommitID.Length -ne 0)
{
    $CommitID = $CommitID.Substring(0, 7)
}

$i = 0
foreach ($line in $project)
{
    $lineSplit = $line.Split(">")

    if ($lineSplit[0].Contains("<Version"))
    {
        $lineSplit[1] = "${Version}</Version"
    }
    elseif ($lineSplit[0].Contains("<InformationalVersion"))
    {
        $lineSplit[1] = "${Version}-${CommitID}</InformationalVersion"
    }

    $project[$i] = [System.String]::Join(">", $lineSplit)

    $i++
}

[System.IO.File]::WriteAllLines($ProjectPath, $project)