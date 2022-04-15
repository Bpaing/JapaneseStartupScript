import pickle

#I want the scripts to be able to keep track of files I'm currently reading.
#Every time I open a cbz file, add to a list.
#Track time on file open, track time on file close.
#If I close a cbz file on the last page, I'm done reading it. I don't need to see it again on next startup.
#Instead, remove from list and substitute with the next volume if it exists.
#Sort by longest time open, take the first 4 (or less) and discard the rest.
#Pickle those 4 files for the startup script to read from next time.

class CBZ:
    filename = ''
    directory= ''
    runtime = 0

def readCBZ():
    i = 0

def writeCBZ():
    i = 0

def main():
    print("Hello world!")

if __name__ = '__main__':
    main()