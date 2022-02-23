using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

class MainView : Form
{
    private TextBox textArea;
    private MenuStrip menuStrip;

    public MainView() : base()
    {
        this.Text = "Bloknot";
        this.ClientSize = new Size(600, 480);
        this.FormBorderStyle = FormBorderStyle.Sizable;

        // Set up text area
        this.textArea = new TextBox {
            Multiline = true,
            AcceptsReturn = true,
            Dock = DockStyle.Fill,
            Font = Styles.MonospaceFont,
            BackColor = Styles.BackgroundColor,
            ScrollBars = ScrollBars.Vertical
        };

        // Set up menu strip
        this.menuStrip = new MenuStrip {
            Dock = DockStyle.Top
        };
        var fileItem = new ToolStripMenuItem { Text = "File" };
        fileItem.DropDownItems.Add("Open file...", null, (sender, args) => {
            MessageBox.Show("Sorry, you can not open files yet...");
        });
        fileItem.DropDownItems.Add("Save as...", null, (sender, args) => {
            MessageBox.Show("Sorry, you can not save files yet...");
        });
        this.menuStrip.Items.Add(fileItem);

        // Add top-level controls to this view  
        this.Controls.Add(this.textArea);
        this.Controls.Add(this.menuStrip);
    }
}
