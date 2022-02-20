using System;
using System.Windows.Forms;
using System.Drawing;

class MainView : Form
{
    private PasswordGeneratorModel model;

    private TextBox numberInput;

    private CheckBox useNumbersInput, 
                     usePunctuationInput;

    private TextBox outputTextArea;

    private Button submitButton;

    public MainView(PasswordGeneratorModel model): base()
    {
        this.model = model;

        this.Text = "Lab 1 : Task 4";
        this.ClientSize = new Size(240, 200);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        this.numberInput = new TextBox { 
            TextAlign = HorizontalAlignment.Center,
            Dock = DockStyle.Fill,
            MaxLength = 10
        };

        this.useNumbersInput = new CheckBox {
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Fill,
            Text = "Numbers"
        };

        this.usePunctuationInput = new CheckBox {
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Fill,
            Text = "Punctuation"
        };

        this.outputTextArea = new TextBox { 
            Dock = DockStyle.Fill, 
            Multiline = true,
            ReadOnly = true,
            Font = Styles.MonospaceFont
        };

        this.submitButton = new Button {
            Dock = DockStyle.Fill,
            Text = "Generate password"
        };

        this.submitButton.Click += (sender, eArgs) => {
            int number;
            if (int.TryParse(this.numberInput.Text, out number)) {
                bool allowNumbers = this.useNumbersInput.Checked,
                     allowPunctuation = this.usePunctuationInput.Checked;
                this.outputTextArea.Text = this.model.Password(number, 
                    allowNumbers, allowPunctuation);
            }
        };

        var layoutPanel = new TableLayoutPanel {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            Padding = new Padding(4)
        };
        var pan = layoutPanel.Controls;
        pan.Add(MakeLabel("Length:"), row: 0, column: 0);
        pan.Add(this.numberInput, row: 0, column: 1);
        pan.Add(this.useNumbersInput, row: 1, column: 0);
        pan.Add(this.usePunctuationInput, row: 1, column: 1);
        pan.Add(this.submitButton, row: 2, column: 0);
        layoutPanel.SetColumnSpan(this.submitButton, 2);
        pan.Add(this.outputTextArea, row: 3, column: 0);
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
