using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

class StatsView : Form
{
    public StatsView (TextStats stats) : base()
    {
        this.Text = "Statistics";
        this.ClientSize = new Size(240, 200);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Padding = new Padding(5);
        this.BackColor = Styles.BackgroundColor;

        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("Size: {0:F2} Kb\n", stats.KbCount);
        sb.AppendFormat("{0} chars\n", stats.CharCount);
        sb.AppendFormat("{0} lines\n", stats.LineCount);
        sb.AppendFormat("{0} pages\n", stats.PageCount);
        sb.AppendFormat("{0} empty lines\n", stats.EmptyLineCount);

        this.Controls.Add (new Label {
            Dock = DockStyle.Fill,
            Text = sb.ToString(),
            Font = Styles.MonospaceFont
        });
    }
}
