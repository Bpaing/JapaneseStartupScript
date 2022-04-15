import subprocess as sp
import webbrowser as wb

# TO DO LIST
# When computer starts up:
# Open Anki
# Open 4 cbz files currently reading (use pickling?)
# Start podcast audio and make sure shuffle is on
# open firefox tabs

# How to determine 4 cbz files I'm reading?
# Check for open cbz files
# Check how long each file is open
# If closed on the last page, remove from queue and add next volume (if it exists)

def browserStartup():
    wb.open_new_tab(r'https://www.youtube.com/')
    wb.open_new_tab(r'https://genshin-center.com/planner')
    wb.open_new_tab(r'https://webstatic-sea.mihoyo.com/ys/event/signin-sea/index.html?act_id=e202102251931481&lang=en-us')

def ankiStartup():
    sp.Popen([r'E:\Anki\anki.exe'], shell=True)

def main():
    browserStartup()
    ankiStartup()

if __name__ == '__main__':
    main()
