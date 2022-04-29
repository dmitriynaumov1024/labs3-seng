using System;
using System.Drawing;
using System.Windows.Forms;

public class PaintProgramViewSidePanel : Panel
{
    private PaintWorkspace workspace;

    private TextBox colorHexInput;
    private TextBox brushSizeInput;

    private TableLayoutPanel panel;

    public PaintProgramViewSidePanel ()
    {
        var colorHexLabel = new Label {
            Dock = DockStyle.Fill,
            Text = "Color (hex)",
            TextAlign = ContentAlignment.MiddleLeft
        };
        this.colorHexInput = new TextBox {
            Dock = DockStyle.Fill,
            Text = "",
            TextAlign = HorizontalAlignment.Center,
            Font = Styles.MonospaceFont
        };
        this.colorHexInput.MouseLeave += (sender, args) => {
            if (this.workspace != null) {
                this.workspace.BrushColor = new PaintColor(this.colorHexInput.Text);
                this.colorHexInput.Text = this.workspace.BrushColor.ToHexString();
            }
        };

        var brushSizeLabel = new Label {
            Dock = DockStyle.Fill,
            Text = "Brush size",
            TextAlign = ContentAlignment.MiddleLeft
        };
        this.brushSizeInput = new TextBox {
            Dock = DockStyle.Fill,
            Text = "",
            TextAlign = HorizontalAlignment.Center,
            Font = Styles.MonospaceFont
        };
        this.brushSizeInput.MouseLeave += (sender, args) => {
            if (this.workspace != null) {
                int size = 0;
                if (!int.TryParse(this.brushSizeInput.Text, out size)) return; 
                this.workspace.BrushSize = size;
                this.brushSizeInput.Text = this.workspace.BrushSize.ToString();
            }
        };

        this.panel = new TableLayoutPanel {
            Dock = DockStyle.Top,
            ColumnCount = 2,
            Padding = new Padding(4)
        };

        for (int i=0; i<this.panel.ColumnCount; i++) {
            this.panel.ColumnStyles.Add ( 
                new ColumnStyle(SizeType.Percent, 100F/this.panel.ColumnCount)
            );
        }

        this.panel.Controls.Add(colorHexLabel);
        this.panel.Controls.Add(this.colorHexInput);
        this.panel.Controls.Add(brushSizeLabel);
        this.panel.Controls.Add(this.brushSizeInput);
        this.panel.Controls.Add(new Label());

        this.Controls.Add(this.panel);
        this.panel.Visible = false;
    }

    public void AttachWorkspace (PaintWorkspace workspace)
    {
        this.workspace = workspace;
        this.colorHexInput.Text = this.workspace.BrushColor.ToHexString();
        this.brushSizeInput.Text = this.workspace.BrushSize.ToString();
        this.panel.Visible = true;
    }

    public void DetachWorkspace ()
    {
        this.workspace = null;
        this.panel.Visible = false;
    }
}
