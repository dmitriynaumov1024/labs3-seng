using System;
using System.Windows.Forms;
using System.Drawing;

public class LanguageView : Form
{
    public LanguageView()
    {
        this.ClientSize = new Size(300, 300);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;

        var knownLocales = LocaleProvider.LocaleKeysAndNames;

        var listBox = new ListBox {
            Dock = DockStyle.Fill
        };

        foreach (var kvPair in knownLocales) {
            listBox.Items.Add(kvPair.Value);
        }

        this.Controls.Add(listBox);

        this.UseLocale (locale => {
            this.Text = locale["languageViewName"];
        });

        listBox.SelectedIndexChanged += (sender, args) => {
            this.SetLocale(knownLocales[listBox.SelectedIndex].Key);
        };
    }

    static LanguageView instance = new LanguageView(); 

    public static void Show () {
        instance.ShowDialog();
    }
}