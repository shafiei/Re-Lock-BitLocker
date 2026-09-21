# Changelog

## 1.0.1

First release with the Windows app and installer.

- New Windows 11-style app: lists BitLocker drives with their state, locks one drive or all of them
- Windows installer (`ReLockBitLocker-Setup.exe`) with Start menu shortcut, optional desktop shortcut and clean uninstall
- New logo, used for the app, the installer and this repository
- The batch script now lists your drives and lets you pick by number, with **Lock all**; the Windows drive and unencrypted drives are no longer offered
- The batch script no longer loops if administrator rights can't be obtained
- Persian (right-to-left) and English interface, chosen from the Windows display language
- Clear messages when BitLocker isn't available or a drive can't be locked

## 1.0.0

- Original batch script that asks for a drive letter and locks it
