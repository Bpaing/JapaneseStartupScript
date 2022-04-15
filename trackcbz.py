import pickle
import subprocess
import time

#I want the scripts to be able to keep track of files I'm currently reading.
#Every time I open a cbz file, add to a list.
#Track time on file open, track time on file close.
#If I close a cbz file on the last page, I'm done reading it. I don't need to see it again on next startup.
#Instead, remove from list and substitute with the next volume if it exists.
#Sort by longest time open, take the first 4 (or less) and discard the rest.
#Pickle those 4 files for the startup script to read from next time.

class CBZ:
    def __init__(self, path):
        self.path = path
        self.runtime = 0

    def trackRuntime(self):
        s = subprocess.Popen([self.path], shell = True)
        opened = time.time()
        s.wait()
        closed = time.time()
        self.runtime = closed - opened
    
    def resetRuntime(self):
        self.runtime = 0

def readCBZ():
    try:
        pickle_in = open("cbz.pickle", "rb")
        list = pickle.load(pickle_in)
        pickle_in.close()
    except Exception:
        list = []
    return list

def writeCBZ(data):
    pickle_out = open("cbz.pickle", "wb")
    pickle.dump(data, pickle_out)
    pickle_out.close()

def trackCBZ():
    list = readCBZ()
        
    print(s.stdout)
    s.wait()
    print(s.stdout) 


#s = subprocess.Popen([r'E:\Users\Brendan\Downloads\Japanese\Content\読む\妹さえいればいい。\妹さえいればいい。 第14巻.cbz'], shell = True)