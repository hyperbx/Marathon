param
(
    [String]$CommitID,
    [String]$ProjectPath,
    [Switch]$UseFullCommitID,
    [String]$Version
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
        if ([System.String]::IsNullOrEmpty($CommitID))
        {
            $lineSplit[1] = "${Version}</InformationalVersion"
        }
        else
        {
            $lineSplit[1] = "${Version}-${CommitID}</InformationalVersion"
        }
    }

    $project[$i] = [System.String]::Join(">", $lineSplit)

    $i++
}

[System.IO.File]::WriteAllLines($ProjectPath, $project)