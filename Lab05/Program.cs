using System;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    public static void Main (string[] args)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var pProgram = Singleton<PaintProgramWinforms>.Instance;
        pProgram.Run();
    }
}