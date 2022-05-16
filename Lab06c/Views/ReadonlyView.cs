using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

class ReadonlyView : Form
{
    private string windowName = "Bloknot";

    private ITextEditor editor;

    private TextBox textArea;
    private MenuStrip menuStrip;

    public ReadonlyView(ITextEditor editor) : base()
    {
        this.editor = editor;

        this.ClientSize = new Size(800, 600);
        this.FormBorderStyle = FormBorderStyle.Sizable;

        // Set up text area
        this.textArea = new TextBox {
            Multiline = true,
            ReadOnly = true,
            AcceptsTab = true,
            AcceptsReturn = true,
            Dock = DockStyle.Fill,
            Font = Styles.MonospaceFont,
            BackColor = Styles.BackgroundColor,
            ScrollBars = ScrollBars.Vertical
        };
        this.textArea.TextChanged += (sender, args) => {
            this.editor.Text = this.textArea.Text;
        };
        this.editor.TextChanged += () => {
            this.textArea.Text = this.editor.Text;
        };
        this.editor.FileNameChanged += () => {
            this.Text = String.Format("{0} - {1}", this.editor.FileName, this.windowName); 
        };
        
        this.Closed += (sender, args) => {
            this.editor.TryClose();
        };

        // Set up menu strip
        this.menuStrip = new MenuStrip {
            Dock = DockStyle.Top
        };
        var fileItem = new ToolStripMenuItem { Text = "File" };
        var saveAsItem = fileItem.DropDownItems.Add("Save as", null, (sender, args) => {
            editor.TrySave();
        });

        var toolItem = new ToolStripMenuItem { Text = "Tools" };
        var searchItem = toolItem.DropDownItems.Add("Search", null, (sender, args) => {
            editor.ShowSearch();
        });
        var statItem = toolItem.DropDownItems.Add("Statistics", null, (sender, args) => {
            editor.ShowStats();
        });
        var selectLangItem = toolItem.DropDownItems.Add("Select language", null, (sender, args) => {
            Actions.SelectLanguage();
        });

        this.menuStrip.Items.Add(fileItem);
        this.menuStrip.Items.Add(toolItem);

        // Add top-level controls to this view  
        this.Controls.Add(this.textArea);
        this.Controls.Add(this.menuStrip);

        this.UseLocale(locale => {
            this.windowName = locale["windowReadonlyName"];
            this.Text = String.Format("{0} - {1}", this.editor.FileName, this.windowName);
            
            fileItem.Text = locale["menu.file"];
            saveAsItem.Text = locale["menu.file.saveAs"];

            toolItem.Text = locale["menu.tools"];
            searchItem.Text = locale["menu.tools.search"];
            statItem.Text = locale["menu.tools.statistics"];
            selectLangItem.Text = locale["menu.tools.language"];
        });
    }
}
