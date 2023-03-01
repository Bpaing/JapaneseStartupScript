# Routine Manager

### UPDATE 12/14/2022

So I lied about updating the Python scripts. I have in fact been using it everyday to monitor my files, 
but the backup and interface plans were promptly forgotten (until now).

My current plans are to rebuild the original program logic in a WPF application using the MVVM design pattern.
After that I want to follow through with adding additional features.

Here are the current feature plans:
1. Startup - Open browser links and processes (from original program)
2. Monitor - Track the runtimes of specified file extensions (from original program, but generic to work with any file extension)\
3. Calendar - Display visual data from Monitor feature

~~Backup - Primarily an interface to specify commands for [restic](https://restic.net/) and [rclone](https://rclone.org/) which I have been using for my cloud backups.~~  
**2/28/23 Removing this feature since other GUI programs exist for these tools.**


## Previous Description

The main motivation for this was for my Japanese study routine.

I got tired of opening files and applications everytime I started my computer, so I made these Python scripts.

It uses the runtime of open files to determine what to open on next startup.

### UPDATE 04/25/2022

At this point, the original logic I wanted to create is finished. Below are some things I would like to implement in the future.

- Perform backup routine after monitoring program is terminated
- Optimize monitoring code
- Modularize code to work with any type of viewable file
- Implement a system that checks if a file is closed on the last page, add the next sequential file in the directory to the list instead
- Implement GUI application so I can change the number of URLs and files to open whenever I feel like it. Also for additional visual confirmation. 
