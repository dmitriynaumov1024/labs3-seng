using System;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;

// The same as Task 0, but with lazy singleton

static class Program
{
    static int count = 4;

    [STAThread]
    public static void Main (string[] args)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        var bag = ApplicationBag.Instance.Value;
        for (int i=0; i<count; i++) {
            var form = new Form {
                Text = "Example "+i.ToString(),
                ClientSize = new System.Drawing.Size(260, 200)
            };
            form.Controls.Add(new NumberView(NumberHolder.Instance.Value));
            bag.Add(form);
            form.Closed += (sender, eargs) => {
                bag.Remove(form);
            };
            form.Show();
        }
        Application.Run();
    }
}
