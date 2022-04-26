import webbrowser
from trackcbz import *
from backup import *

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

async def openURL(link):
    webbrowser.open_new_tab(link)

async def browserStartup(urlList):
    await asyncio.gather(*map(openURL, urlList))

async def startProcess(executablePath):
    await asyncio.create_subprocess_shell(executablePath)

async def main():
    urlList = [
        r'https://www.youtube.com/',
        r'https://www.reddit.com/',
        r'https://genshin-center.com/planner',
        r'https://webstatic-sea.mihoyo.com/ys/event/signin-sea/index.html?act_id=e202102251931481&lang=en-us'
    ]
    #await browserStartup(urlList)
    await cbzStartup()
    #Also start podcast playlist

    #Sort list by runtime, pickle top 4
    n = 4
    files = sorted(await trackCBZWhileOpen(r'E:\Anki\anki.exe'), key=lambda x: x.runtime, reverse=True)
    filesToPickle = files[:n]
    for cbz in filesToPickle:
        print(cbz)
    writeCBZList(filesToPickle)

    #backup.py functions here

if __name__ == '__main__':
    asyncio.run(main())
