param(
    [Parameter(Mandatory=$true)]
    [string]$FilePath,
    
    [Parameter(Mandatory=$true)]
    [string]$Password
)

# Convert to absolute path and validate
$absolutePath = if ([System.IO.Path]::IsPathRooted($FilePath)) {
    $FilePath
} else {
    Join-Path (Get-Location) $FilePath
}

if (-not (Test-Path $absolutePath)) {
    Write-Error "File not found: $absolutePath"
    Write-Host "Current directory: $(Get-Location)" -ForegroundColor Yellow
    exit 1
}

$FilePath = $absolutePath

# Generate output filename
$fileInfo = Get-Item $FilePath
$outputPath = Join-Path $fileInfo.Directory "$($fileInfo.BaseName).encrypted"

try {
    Write-Host "Encrypting: $FilePath"
    
    # Generate key and IV from password
    $sha256 = [System.Security.Cryptography.SHA256]::Create()
    $passwordBytes = [System.Text.Encoding]::UTF8.GetBytes($Password)
    $hash = $sha256.ComputeHash($passwordBytes)
    
    # Use first 32 bytes for key, first 16 bytes for IV
    $key = $hash[0..31]
    $iv = $hash[0..15]
    
    # Create AES encryptor
    $aes = [System.Security.Cryptography.Aes]::Create()
    $aes.Key = $key
    $aes.IV = $iv
    
    # Read input file
    $inputBytes = [System.IO.File]::ReadAllBytes($FilePath)
    
    # Encrypt data
    $encryptor = $aes.CreateEncryptor()
    $encryptedBytes = $encryptor.TransformFinalBlock($inputBytes, 0, $inputBytes.Length)
    
    # Write encrypted file
    [System.IO.File]::WriteAllBytes($outputPath, $encryptedBytes)
    
    Write-Host "Successfully encrypted to: $outputPath" -ForegroundColor Green
    
    # Clean up
    $aes.Dispose()
    $encryptor.Dispose()
    $sha256.Dispose()
}
catch {
    Write-Error "Encryption failed: $($_.Exception.Message)"
    exit 1
}