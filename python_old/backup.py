import os
import anki
# This is the python program for backing up my files.
# 'anki' module is used specifically for creating backups of my Anki flashcard decks for studying.

# To do list:
# get anki to export collection on sync
# copy paste specific files (firefox bookmarks, qbittorrent, etc)
# run rclone commands


# rclone back up
# 1. Get most up to date files (bookmarks, anki export, qbittorrent, etc)
# 2. determine which directories to backup
# 3. for each directory, find total size of files and subfolders
# 4. sort by size (smallest to largest)
# 5. execute rclone commands