using System;
using System.Windows.Forms;
using System.Drawing;

public class PaintWorkspaceView : Panel
{
    private PaintWorkspace workspace;

    private PaintCanvasView screenCanvas;

    public PaintWorkspaceView()
    {
        this.Dock = DockStyle.Fill;

        this.Resize += (sender, args) => {
            if (this.screenCanvas != null) {
                this.screenCanvas.Location = new Point (
                    (this.Width - this.workspace.CanvasSize)/2, 
                    (this.Height - this.workspace.CanvasSize)/2
                );
            }
        };
    }

    public void AttachWorkspace (PaintWorkspace workspace) 
    {
        this.workspace = workspace;
        var color = workspace.BackColor;
        Console.Write("canvas size: {0}\n", workspace.CanvasSize);
        this.screenCanvas = new PaintCanvasView (workspace) {
            Location = new Point (
                (this.Width - workspace.CanvasSize)/2, 
                (this.Height - workspace.CanvasSize)/2 ),
            BackColor = Color.FromArgb(color.R, color.G, color.B)
        };

        this.Controls.Add(this.screenCanvas);
    }

    public void Init(PaintCanvas canvas) 
    {
        this.screenCanvas.Init();
    }
}
