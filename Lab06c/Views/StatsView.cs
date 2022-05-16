using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

class StatsView : Form
{
    public StatsView (TextStats stats) : base()
    {
        this.Text = "Statistics";
        this.ClientSize = new Size(240, 280);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Padding = new Padding(5);
        this.BackColor = Styles.BackgroundColor;

        var label = new Label {
            Dock = DockStyle.Fill,
            Font = Styles.MonospaceFont
        };

        this.Controls.Add (label);

        this.UseLocale(locale => {

            this.Text = locale["statsViewName"];

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{2}: {0:F2} {1}\n", stats.KbCount, locale["stats.kbCount"], locale["stats.fileSize"]);
            sb.AppendFormat("{0} {1}\n", stats.PageCount, locale["stats.pageCount"]);
            sb.AppendFormat("{0} {1}\n", stats.LineCount, locale["stats.lineCount"]);
            sb.AppendFormat(" └ {0} {1}\n", stats.EmptyLineCount, locale["stats.emptyLineCount"]);
            sb.AppendFormat("{0} {1}\n", stats.CharCount, locale["stats.charCount"]);
            sb.AppendFormat(" ├ {0} {1}\n", stats.LetterCount, locale["stats.letterCount"]);
            sb.AppendFormat(" │  ├ {0} {1}\n", stats.VowelCount, locale["stats.vowelCount"]);
            sb.AppendFormat(" │  └ {0} {1}\n", stats.ConsCount, locale["stats.consCount"]);
            sb.AppendFormat(" ├ {0} {1}\n", stats.DigitCount, locale["stats.digitCount"]);
            sb.AppendFormat(" ├ {0} {1}\n", stats.PunctCount, locale["stats.punctCount"]);
            sb.AppendFormat(" └ {0} {1}\n", stats.OtherCount, locale["stats.otherCount"]);

            label.Text = sb.ToString();
        });
    }
}
