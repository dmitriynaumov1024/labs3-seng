using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

class Actions 
{
    public static Stream SelectFileToOpen()
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

    public static void ShowStatsPopup (TextStats stats)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Size: {0} Kb\n", stats.KbCount);
        sb.AppendFormat("{0} chars\n", stats.CharCount);
        sb.AppendFormat("{0} lines\n", stats.LineCount);
        sb.AppendFormat("{0} pages\n", stats.PageCount);
        sb.AppendFormat("{0} empty lines\n", stats.EmptyLineCount);
        MessageBox.Show(sb.ToString());
    }
}
