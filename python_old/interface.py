from PyQt5.QtWidgets import QApplication, QMainWindow
import sys

class Interface(QMainWindow):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Daily Routine Automation")

        minWidth = 200
        minHeight = 200
        self.setMinimumSize(minWidth, minHeight)

if __name__ == '__main__':
    app = QApplication(sys.argv)
    frame = Interface()
    frame.show()
    app.exec_()
