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


# A CBZ object represents a viewable file.
# This class was primarily created for viewing .cbz files.
# A .cbz file is a renamed file archive (.zip, .rar, etc.) and
# is primarily used with a comic book viewer application to read digital images.
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
        return self.filename + " " + str(self.runtime)

    def getPath(self):
        return self.directory + "\\" + self.filename
    
    def getRuntime(self):
        return self.runtime

    def hasProcess(self):
        return self.associatedProcess != None
    
    def isRunning(self):
        try:
            return self.associatedProcess.is_running()
        except Exception:
            print("This object does not have an associated process.")


    # Takes time of process termination to calculate total runtime.
    # Also removes associated process to denote termination.
    def calculateRuntime(self, time):
        try:
            self.runtime += (time - self.associatedProcess.create_time())
            self.associatedProcess = None
        except Exception:
            print("This object does not have an associated process.")
    

    # Updates the CBZ with a new Process
    # This is to account for the case that we reopen a cbz file
    # and want to start tracking runtime again.
    def updateProcess(self, process):
        self.associatedProcess = process


    # A running process is needed to calculate runtime correctly.
    # Searches for a CDisplayEx.exe process with the same file name
    # and instantiates Process field with the running process.
    def __grabProcess(self):
        for process in psutil.process_iter():
            if process.name() == 'CDisplayEx.exe':
                processFileName = process.cmdline()[1].rsplit('\\', 1)[1]
                if processFileName == self.filename:
                    self.associatedProcess = process
    
    # Opens the file and grabs the running process.
    async def open(self):
        program = await asyncio.create_subprocess_shell(self.getPath(), shell=True)
        while (self.associatedProcess == None):
            await asyncio.sleep(1)
            self.__grabProcess()

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

def processMonitor(cbzSet):
    for process in psutil.process_iter():
        if process.name() == 'CDisplayEx.exe':
            # cmdline() returns a list containing executable path and file path.
            filePath = process.cmdline()[1]
            # split file path from the right to get a list containing directory and file name.
            directoryFile = filePath.rsplit('\\', 1)
            
            cbz = CBZ(directoryFile[0], directoryFile[1], process)

            if cbz in cbzSet:
                # grabs the original cbz in the set and updates its associated process.
                # this addresses the case where files are reopened and we want to track runtime again.
                original = {cbz}.intersection(cbzSet).pop()
                if original.associatedProcess == None:
                    original.updateProcess(process)
            else:
                cbzSet.add(cbz)
    
    for cbz in cbzSet:
        if cbz.hasProcess() and not cbz.isRunning():
            cbz.calculateRuntime(time.time())
    
# Starts a program using its executable path as an argument. 
# Monitor runtime of opened cbz files while program is running.
# After program is terminated, return list of CBZ objects sorted by runtime descending.
async def trackCBZWhileOpen(executablePath):
    program = await asyncio.create_subprocess_shell(executablePath)
    allOpenedCBZs = set()
    await asyncio.sleep(7)
    while (program.returncode != 0):
        await asyncio.sleep(1)
        processMonitor(allOpenedCBZs)
    return allOpenedCBZs


# instead of running a separate asyncio instance, use psutil to monitor running instead?
# if I close a file then open it again, start monitoring from its current runtime value.
# set comparison, if runtime > 0, start monitoring again?
# when dumping pickle, get rid of currently running process

