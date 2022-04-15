import pickle
from signal import SIG_DFL
import subprocess
import time
import asyncio
import threading

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
        self.runtime = 0
        self.opened = 0
        self.closed = 0
        threading.Thread.__init__(self)

    def run(self):
        test = 0

    def openCBZ(self):
        self.s = subprocess.Popen([self.path], shell = True)
        self.opened = time.time()
        print(self.opened)

    async def trackRuntime(self):
        s = await asyncio.create_subprocess_shell(r'E:\Users\Brendan\Downloads\Japanese\Content\読む\妹さえいればいい。\妹さえいればいい。 第14巻.cbz')
        opened = time.time()
        await s.communicate()
        closed = time.time()
        print (closed - opened)
    
    def resetRuntime(self):
        self.runtime = 0

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
    c = await asyncio.gather(list[0].trackRuntime(), list[1].trackRuntime())

#s = subprocess.Popen([r'E:\Users\Brendan\Downloads\Japanese\Content\読む\妹さえいればいい。\妹さえいればいい。 第14巻.cbz'], shell = True)
asyncio.run(trackCBZ())
#path = r'E:\Users\Brendan\Downloads\Japanese\Content\読む\妹さえいればいい。\妹さえいればいい。 第14巻.cbz'
#for proc in psutil.process_iter(attrs=['pid', 'name']):
 #   if proc.name() == 'CDisplayEx.exe':
  #      print(proc.open_files())