using System;
using System.IO;
using System.Windows.Forms;

class Actions 
{
    public static  Stream SelectFileToOpen()
    {
        var dialog = new OpenFileDialog {
            RestoreDirectory = true,
            Title = "Select a file to open",
            Filter = "All files | *.*"
        };
        if (dialog.ShowDialog() == DialogResult.OK) {
            return dialog.OpenFile();
        }
        else {
            return null;
        }
    }

    public static Stream SelectFileToSave()
    {
        var dialog = new SaveFileDialog {
            RestoreDirectory = true,
            Title = "Save as",
            Filter = "All files | *.*"
        };
        if (dialog.ShowDialog() == DialogResult.OK) {
            return dialog.OpenFile();
        }
        else {
            return null;
        }
    }
}
