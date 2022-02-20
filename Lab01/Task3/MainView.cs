using System;
using System.Windows.Forms;
using System.Drawing;

class MainView : Form
{
    private NumberModel model;

    private TextBox numberInput;

    private Label outputLabel;

    private Button submitButton;

    public MainView(NumberModel model): base()
    {
        this.model = model;

        this.Text = "Lab 1 : Task 3";
        this.ClientSize = new Size(220, 200);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;

        this.numberInput = new TextBox { 
            TextAlign = HorizontalAlignment.Center,
            Dock = DockStyle.Fill,
            MaxLength = 10
        };

        this.outputLabel = new Label { 
            Dock = DockStyle.Fill, 
            TextAlign = ContentAlignment.TopLeft
        };

        this.submitButton = new Button {
            Dock = DockStyle.Fill,
            Text = "Submit"
        };

        this.submitButton.Click += (sender, eArgs) => {
            int number;
            if (int.TryParse(this.numberInput.Text, out number)) {
                this.outputLabel.Text = String.Format("Divisors of {0}:\n{1}", 
                    number, String.Join(", ", this.model.GetDivisors(number)));
            }
        };

        var layoutPanel = new TableLayoutPanel {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            Padding = new Padding(4)
        };
        var pan = layoutPanel.Controls;
        pan.Add(MakeLabel("Number"), row: 0, column: 0);
        pan.Add(this.numberInput, row: 0, column: 1);
        pan.Add(this.submitButton, row: 1, column: 0);
        layoutPanel.SetColumnSpan(this.submitButton, 2);
        pan.Add(this.outputLabel, row: 2, column: 0);
        layoutPanel.SetColumnSpan(this.outputLabel, 2);
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
