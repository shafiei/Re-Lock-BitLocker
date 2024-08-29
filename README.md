# Re-Lock-BitLocker

a .bat file for easy Re-Lock Bitlocker drive after unlock without restarting the windows

Re-Lock-BitLocker
Overview
Re-Lock-BitLocker is a simple batch file utility designed to re-lock a BitLocker-encrypted drive after it has been unlocked, without requiring a system restart. This is useful for scenarios where you temporarily need to access a drive and then want to re-secure it without rebooting your system.

Features
Re-lock BitLocker Drive: Easily re-locks an unlocked BitLocker drive.
No Restart Required: Re-locks the drive without needing to restart your Windows machine.
Prerequisites
Windows OS: This script is intended for Windows environments where BitLocker is used.
Administrator Privileges: You will need administrative rights to execute this script successfully.
Usage
Download the .bat File: Download the Re-Lock-BitLocker.bat file from the repository.

Run the Script:

Right-click the .bat file and select Run as administrator.
Follow the on-screen prompts to select the drive you wish to re-lock.
Verify the Drive Status:

Ensure the drive is re-locked by checking its status in the BitLocker Management interface.
Example
To use the script, simply run it from an elevated command prompt:

batch
Copy code
Re-Lock-BitLocker.bat
Follow the prompts to select the drive you wish to re-lock. The script will handle the rest.

Script Details
The Re-Lock-BitLocker.bat script uses the manage-bde command to lock the drive. It requires the drive letter to be specified when prompted. The script is designed to be easy to use and integrates seamlessly with Windows' BitLocker management tools.

Troubleshooting
Permission Issues: Ensure you are running the script with administrative privileges.
Drive Letter Input: Make sure to input the correct drive letter in the format (e.g., C:).
Contributing
If you have suggestions or improvements, please submit a pull request or open an issue in this repository.

License
This project is licensed under the MIT License. See the LICENSE file for details.

Contact
For questions or support, please contact the repository maintainer via the GitHub Issues page.
