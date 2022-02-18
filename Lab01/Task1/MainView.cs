using System;
using System.Windows.Forms;
using System.Drawing;

class MainView : Form
{
    private AgeCalculatorModel model;

    private TextBox 
        nameInput, 
        ageInput,
        multInput;

    private Label 
        outputLabel;


    public MainView(AgeCalculatorModel model): base()
    {
        this.model = model;
        this.model.Change += this.UpdateView;

        this.Text = "Lab 1 : Task 1";
        this.ClientSize = new Size(320, 320);

        this.nameInput = new TextBox();
        this.ageInput = new TextBox();
        this.multInput = new TextBox();
        this.outputLabel = new Label { 
            Dock = DockStyle.Fill, 
            TextAlign = ContentAlignment.MiddleLeft 
        };

        this.nameInput.TextChanged += (sender, eArgs) => {
            model.UserNameString = this.nameInput.Text;
        };
        this.ageInput.TextChanged += (sender, eArgs) => {
            model.UserAgeString = this.ageInput.Text;
        };
        this.multInput.TextChanged += (sender, eArgs) => {
            model.MultiplierString = this.multInput.Text;
        };

        var layoutPanel = new TableLayoutPanel {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            Padding = new Padding(4)
        };
        var pan = layoutPanel.Controls;
        pan.Add(MakeLabel("Your name:"), row: 0, column: 0);
        pan.Add(MakeLabel("Your age:"), row: 1, column: 0);
        pan.Add(MakeLabel("Multiplier:"), row: 2, column: 0);
        pan.Add(this.nameInput, row: 0, column: 1);
        pan.Add(this.ageInput, row: 1, column: 1);
        pan.Add(this.multInput, row: 2, column: 1);
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
