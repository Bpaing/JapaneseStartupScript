import asyncio
import webbrowser as wb
import time

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
    print("Opening " + link)
    print('start firefox /new-tab ' + link)
    await asyncio.create_subprocess_shell('start firefox /new-tab ' + link)

async def browserStartup():
    url = (
        r'https://www.youtube.com/',
        r'https://genshin-center.com/planner',
        r'https://webstatic-sea.mihoyo.com/ys/event/signin-sea/index.html?act_id=e202102251931481&lang=en-us'
    )
    await asyncio.gather(*map(openURL, url))


async def ankiStartup():
    a = await asyncio.create_subprocess_shell(r'E:\Anki\anki.exe')
    await a.wait()
    print('anki was closed.')

def main():
    asyncio.run(browserStartup())
    asyncio.run(ankiStartup())

if __name__ == '__main__':
    main()
