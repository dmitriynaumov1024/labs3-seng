using System;
using System.Windows.Forms;
using System.Drawing;

class MainView : Form
{
    private NumberModel model;

    private TextBox 
        numberInput1,
        numberInput2;

    private Label 
        outputLabel;

    private Button 
        submitButton;

    public MainView(NumberModel model): base()
    {
        this.model = model;
        this.model.Change += this.UpdateView;

        this.Text = "Lab 1 : Task 1";
        this.ClientSize = new Size(320, 320);

        this.numberInput1 = new TextBox { 
            TextAlign = HorizontalAlignment.Right,
            Dock = DockStyle.Fill,
            MaxLength = 10
        };
        this.numberInput2 = new TextBox { 
            TextAlign = HorizontalAlignment.Right,
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
            this.model.UseAtomic(mo => {
                mo.Number1 = this.numberInput1.Text;
                mo.Number2 = this.numberInput2.Text;
            });
        };

        var layoutPanel = new TableLayoutPanel {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            Padding = new Padding(4)
        };
        var pan = layoutPanel.Controls;
        pan.Add(MakeLabel("Number #1"), row: 0, column: 0);
        pan.Add(MakeLabel("Number #2"), row: 1, column: 0);
        pan.Add(this.numberInput1, row: 0, column: 1);
        pan.Add(this.numberInput2, row: 1, column: 1);
        pan.Add(this.submitButton, row: 2, column: 0);
        layoutPanel.SetColumnSpan(this.submitButton, 2);
        pan.Add(this.outputLabel, row: 3, column: 0);
        layoutPanel.SetColumnSpan(this.outputLabel, 2);
        this.Controls.Add(layoutPanel);
    }

    private void UpdateView()
    {
        this.outputLabel.Text = this.model.OutputMessage;
    }

    static Label MakeLabel(string text) {
        return new Label { 
            Text = text, 
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        };
    }
}
