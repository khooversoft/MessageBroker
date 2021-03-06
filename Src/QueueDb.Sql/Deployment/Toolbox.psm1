<# 
.NOTES 
    Name: Tool box for PowerShell
    Author: Kelvin Hoover
    Requires: PowerShell v4 or higher

#> 
 
function Get-SqlPackage
{
    $path = "${env:ProgramFiles(x86)}\Microsoft SQL Server\140\DAC\bin\SqlPackage.exe";

    if (-not (Test-Path $path))
    {
        $errorMsg = "Cannot find SqlPackage at $path";
        Write-Error $errorMsg;
        throw $errorMsg;
    }

    return $path;
}

function Get-SqlCmd
{
    $paths = & where.exe sqlcmd.exe

    if( !$paths -or $paths.Count -eq 0 )
    {
        $errorMsg = "No paths were returned for sSQLCMD";
        Write-Host $errorMsg -ForegroundColor Yellow;
        throw $errorMsg;
    }

    $sqlCmdPath = $paths[0];

    if( !(Test-Path -Path $sqlCmdPath))
    {
        $errorMsg = "Cannot find SQLCMD.EXE from the paths, path = $sqlCmdPath";
        Write-Host $errorMsg -ForegroundColor Yellow;
        throw $errorMsg;
    }

    return $sqlCmdPath;
}

Function Invoke-Command
{
    Param ( 
        [Parameter(Mandatory=$True)]
        [string] $CommandPath,

        [Parameter(Mandatory=$True)]
        [string[]] $CommandArguments
    )

    $commandLine = $CommandArguments -join " "

    Write-Verbose "Executing: $CommandPath $commandLine"

    try {
        $pinfo = New-Object System.Diagnostics.ProcessStartInfo
        $pinfo.FileName = $CommandPath
        $pinfo.RedirectStandardError = $true
        $pinfo.RedirectStandardOutput = $true
        $pinfo.UseShellExecute = $false
        $pinfo.Arguments = $commandLine

        $p = New-Object System.Diagnostics.Process
        $p.StartInfo = $pinfo
        $p.Start() | Out-Null
        $rtn = [pscustomobject]@{
            stdout = $p.StandardOutput.ReadToEnd()
            stderr = $p.StandardError.ReadToEnd()
            ExitCode = $p.ExitCode  
        }
        $p.WaitForExit()
      }
      catch {
         exit
      }

    if( $rtn.ExitCode -ne 0)
    {
        Write-Host "Exit code: $($rtn.ExitCode)" -ForegroundColor Yellow
        Write-Host "Standard Out: $($rtn.Stdout)" -ForegroundColor Cyan
        Write-Host "Standard Error: $($rtn.Stderr)" -ForegroundColor Yellow
    }
    else
    {
        Write-Host "Standard Out: $($rtn.Stdout)"
        Write-Verbose "Exit code: $($rtn.ExitCode)"
        Write-Verbose "Standard Error: $($rtn.Stderr)"
    }
}

Export-ModuleMember -Function '*'
