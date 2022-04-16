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
    def __init__(self, path):
        self.path = path

    async def trackRuntime(self):
        s = await asyncio.create_subprocess_shell(self.path)
        opened = time.time()
        await s.communicate()
        closed = time.time()
        print (closed - opened)
        return closed - opened

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


#s = subprocess.Popen([r'E:\Users\Brendan\Downloads\Japanese\Content\読む\妹さえいればいい。\妹さえいればいい。 第14巻.cbz'], shell = True)
#asyncio.run(trackCBZ())
#path = r'E:\Users\Brendan\Downloads\Japanese\Content\読む\妹さえいればいい。\妹さえいればいい。 第14巻.cbz'


#cmdline shows the path. can insert directly into CBZ().
#cwd shows the directory the file is in.
#By storing processes in a set, it is less computationally-intensive to monitor new processes.
for process in psutil.process_iter():
    if process.name() == 'CDisplayEx.exe':
        # cmdline() returns a list containing executable path and file path.
        filePath = process.cmdline()[1]

        # split file path from the right to get a list containing directory and file name.
        filePath = process.cmdline()[1]
        directoryAndFile = filePath.rsplit('\\', 1)
        print(directoryAndFile)
