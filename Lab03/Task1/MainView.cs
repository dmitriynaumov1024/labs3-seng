using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;

class MainView : Form
{
    private TextBox urlInput;
    private TextBox outputTextArea;
    private Button submitButton;

    public MainView(RssLoader loader): base()
    {
        this.Text = "RSS Viewer";
        this.ClientSize = new Size(560, 560);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        this.urlInput = new TextBox { 
            Dock = DockStyle.Fill,
            MaxLength = 250
        };

        this.outputTextArea = new TextBox { 
            Dock = DockStyle.Fill, 
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true,
            Font = Styles.MonospaceFont,
            Text = "Data will appear here"
        };

        this.submitButton = new Button {
            Dock = DockStyle.Fill,
            Text = "Load"
        };

        this.submitButton.Click += (sender, eArgs) => {
            this.outputTextArea.Text = "loading..."; 
            loader
                .GetTextAsync (this.urlInput.Text)
                .ContinueWith ((task)=> {
                    this.outputTextArea.Text = task.Result;
                });
        };

        var layoutPanel = new TableLayoutPanel {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            Padding = new Padding(4)
        };
        var pan = layoutPanel.Controls;
        pan.Add(MakeLabel("Address:"), row: 0, column: 0);
        pan.Add(this.urlInput, row: 0, column: 1);
        pan.Add(this.submitButton, row: 1, column: 0);
        layoutPanel.SetColumnSpan(this.submitButton, 2);
        pan.Add(this.outputTextArea, row: 2, column: 0);
        layoutPanel.SetColumnSpan(this.outputTextArea, 2);
        this.Controls.Add(layoutPanel);
    }

    static Label MakeLabel(string text) {
        return new Label { 
            Text = text, 
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };
    }
}

