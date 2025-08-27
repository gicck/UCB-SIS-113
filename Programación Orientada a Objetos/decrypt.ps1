param(
    [Parameter(Mandatory=$true)]
    [string]$EncryptedFilePath,
    
    [Parameter(Mandatory=$true)]
    [string]$Password,
    
    [Parameter(Mandatory=$false)]
    [string]$OutputPath
)

# Convert to absolute path and validate
$absolutePath = if ([System.IO.Path]::IsPathRooted($EncryptedFilePath)) {
    $EncryptedFilePath
} else {
    Join-Path (Get-Location) $EncryptedFilePath
}

if (-not (Test-Path $absolutePath)) {
    Write-Error "Encrypted file not found: $absolutePath"
    Write-Host "Current directory: $(Get-Location)" -ForegroundColor Yellow
    exit 1
}

$EncryptedFilePath = $absolutePath

# Generate output filename if not provided
if (-not $OutputPath) {
    $fileInfo = Get-Item $EncryptedFilePath
    $OutputPath = Join-Path $fileInfo.Directory "$($fileInfo.BaseName -replace '\.encrypted$', '.decrypted')"
}

try {
    Write-Host "Decrypting: $EncryptedFilePath"
    
    # Generate key and IV from password (must match encryption)
    $sha256 = [System.Security.Cryptography.SHA256]::Create()
    $passwordBytes = [System.Text.Encoding]::UTF8.GetBytes($Password)
    $hash = $sha256.ComputeHash($passwordBytes)
    
    # Use first 32 bytes for key, first 16 bytes for IV
    $key = $hash[0..31]
    $iv = $hash[0..15]
    
    # Create AES decryptor
    $aes = [System.Security.Cryptography.Aes]::Create()
    $aes.Key = $key
    $aes.IV = $iv
    
    # Read encrypted file
    $encryptedBytes = [System.IO.File]::ReadAllBytes($EncryptedFilePath)
    
    # Decrypt data
    $decryptor = $aes.CreateDecryptor()
    $decryptedBytes = $decryptor.TransformFinalBlock($encryptedBytes, 0, $encryptedBytes.Length)
    
    # Write decrypted file
    [System.IO.File]::WriteAllBytes($OutputPath, $decryptedBytes)
    
    Write-Host "Successfully decrypted to: $OutputPath" -ForegroundColor Green
    
    # Clean up
    $aes.Dispose()
    $decryptor.Dispose()
    $sha256.Dispose()
}
catch {
    Write-Error "Decryption failed: $($_.Exception.Message)"
    Write-Host "Make sure you're using the correct password" -ForegroundColor Yellow
    exit 1
}