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


    def __init__(self, directory, filename, associatedProcess=None):
        self.directory = directory
        self.filename = filename
        self.associatedProcess = associatedProcess
        self.runtime = 0
    
    def __eq__(self, other):
        return self.filename == other.filename
    
    def __hash__(self):
        return hash((self.directory, self.filename))

    def __str__(self):
        return self.filename

    def getPath(self):
        return self.directory + "\\" + self.filename

    def setRuntime(self, timeClosed):
        self.runtime = timeClosed - self.associatedProcess.create_time()

    def addRuntime(self, time):
        self.runtime += time

    # A running process is needed for monitorRuntime() to work.
    # Searches for a CDisplayEx.exe process with the same file name.
    def grabProcess(self):
        for process in psutil.process_iter():
            if process.name() == 'CDisplayEx.exe':
                processFileName = process.cmdline()[1].rsplit('\\', 1)[1]
                if processFileName == self.filename:
                    self.associatedProcess = process
                    print("grabbed process of " + self.filename)
    
    async def open(self):
        startProcess = await asyncio.create_subprocess_shell(self.getPath(), shell=True)
        while (self.associatedProcess == None):
            await asyncio.sleep(1)
            self.grabProcess()
        await startProcess.wait()
        

def readCBZList():
    try:
        pickle_in = open("cbz.pickle", "rb")
        list = pickle.load(pickle_in)
        pickle_in.close()
    except Exception:
        list = []
    return list

def writeCBZList(data):
    for cbz in data:
        cbz.associatedProcess = None
    pickle_out = open("cbz.pickle", "wb")
    pickle.dump(data, pickle_out)
    pickle_out.close()

async def cbzStartup():
    await asyncio.gather(*map(CBZ.open, readCBZList()))

# Starts Anki. Track runtime of opened cbz files while Anki is running.
async def ankiMonitor():
    a = await asyncio.create_subprocess_shell(r'E:\Anki\anki.exe')
    totalCBZs = set()
    while (a.returncode != 0):
        await asyncio.sleep(7)
        currentCBZs = set()
        for process in psutil.process_iter():
            if process.name() == 'CDisplayEx.exe':
                # cmdline() returns a list containing executable path and file path.
                filePath = process.cmdline()[1]
                # split file path from the right to get a list containing directory and file name.
                directoryFile = filePath.rsplit('\\', 1)

                currentCBZs.add(CBZ(directoryFile[0], directoryFile[1], process))

        newCBZs = currentCBZs.difference(totalCBZs)
        totalCBZs.update(newCBZs)

        closedCBZs = totalCBZs.difference(currentCBZs)
        for cbz in closedCBZs:
            #if its in totalCBZs, add to time, else set time
            cbz.setRuntime(time.time())
       

    print('anki was closed.')
    return list(totalCBZs).sort(key = lambda x: x.runtime)

# instead of running a separate asyncio instance, use psutil to monitor running instead?
# if I close a file then open it again, start monitoring from its current runtime value.
# set comparison, if runtime > 0, start monitoring again?
# when dumping pickle, get rid of currently running process
asyncio.run(ankiMonitor())

