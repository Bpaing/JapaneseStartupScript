from doctest import testfile
import os
import subprocess as sp
import webbrowser as wb

# TO DO LIST
# When computer starts up:
# Open Anki
# Open 4 cbz files currently reading (use pickling?)
# Start podcast audio and make sure shuffle is on

# How to determine 4 cbz files I'm reading?
# Check for open cbz files
# Check how long each file is open
# If closed on the last page, remove from queue and add next volume (if it exists)

def main():    
    path = r'E:\Users\Brendan\Downloads\Japanese\Content\読む\妹さえいればいい。\妹さえいればいい。 第14巻.cbz'
    #sp.Popen([path], shell=True)
    wb.open_new(path)

if __name__ == '__main__':
    main()
#os.chdir(path)
#os.startfile("妹さえいればいい。 第14巻.cbz")
#os.startfile("CECS277MidtermTwo.pdf")
        
