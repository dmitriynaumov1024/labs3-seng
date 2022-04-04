using System;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;

class Program
{
    [STAThread]
    static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var editor = new TextEditor {
            OpenSelector = Actions.SelectFileToOpen,
            SaveSelector = Actions.SelectFileToSave,
            ShowStatsPopup = Actions.ShowStatsPopup,
            ShowRemovedSpacesPopup = Actions.ShowProposedTextPopup
        };
        var view = new MainView(editor);
        Application.Run(view);
    }    
}
