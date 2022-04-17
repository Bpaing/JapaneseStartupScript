import pickle
import time
import asyncio
import psutil

#I want the scripts to be able to keep track of files I'm currently reading.
#Read from CBZ pickle list. Open every file.
#Every time I open a new cbz file, add to the list.
#Track time on file open, track time on file close.
#If I close a cbz file on the last page, I'm done reading it. I don't need to see it again on next startup.
#Instead, remove from list and substitute with the next volume if it exists.
#Sort by longest time open, take the first 4 (or less) and discard the rest.
#Pickle those 4 files for the startup script to read from next time.


class CBZ:
    def __init__(self, directory, filename):
        self.directory = directory
        self.filename = filename
        self.runtime = 0
    
    def __eq__(self, other):
        return self.filename == other.filename
    
    def __hash__(self):
        return hash((self.directory, self.filename))

    def __str__(self):
        return self.filename

    def getPath(self):
        return self.directory + "\\" + self.filename

    async def monitorRuntime(self):
        print("monitoring runtime of " + self.filename)
        s = await asyncio.create_subprocess_shell(self.getPath())
        opened = time.time()
        await s.wait()
        closed = time.time()
        print (closed - opened)
        self.runtime = closed - opened

def readCBZList():
    try:
        pickle_in = open("cbz.pickle", "rb")
        list = pickle.load(pickle_in)
        pickle_in.close()
    except Exception:
        list = []
    return list

def writeCBZList(data):
    pickle_out = open("cbz.pickle", "wb")
    pickle.dump(data, pickle_out)
    pickle_out.close()

async def trackCBZ():
    #list = readCBZList()
    list = [CBZ(r'E:\Users\Brendan\Downloads\Japanese\Content\読む\妹さえいればいい。\妹さえいればいい。 第14巻.cbz'), CBZ(r'E:\Users\Brendan\Downloads\Japanese\Content\読む\妹さえいればいい。\妹さえいればいい。 第13巻.cbz')]
    runtimeList = await asyncio.gather(list[0].trackRuntime())

async def ankiStartup():
    a = await asyncio.create_subprocess_shell(r'E:\Anki\anki.exe')
    totalOpened = set()
    while (a.returncode != 0):
        await asyncio.sleep(7)
        currentlyOpen = set()

        for process in psutil.process_iter():
            if process.name() == 'CDisplayEx.exe':
                # cmdline() returns a list containing executable path and file path.
                filePath = process.cmdline()[1]
                # split file path from the right to get a list containing directory and file name.
                directoryFile = filePath.rsplit('\\', 1)
                currentlyOpen.add(CBZ(directoryFile[0],directoryFile[1]))
        print(a.returncode)

        newlyOpened = currentlyOpen.difference(totalOpened)
        await asyncio.gather(*map(CBZ.monitorRuntime, list(newlyOpened)))
        totalOpened.update(newlyOpened)
        
    print('anki was closed.')


#cmdline shows the path. can insert directly into CBZ().
#cwd shows the directory the file is in.
#By storing processes in a set, it is less computationally-intensive to monitor new processes.
#difference() returns a new set
#difference_update() removes items that exist in both sets

# old_set and new_set
# if new elements are found in new_set
#   new_set.difference_update(old_set)
#   then add new_set elements to old_set
asyncio.run(ankiStartup())