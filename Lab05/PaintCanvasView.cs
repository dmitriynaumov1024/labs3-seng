using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

public class PaintCanvasView : Control
{
    private Graphics bitmapGraphics;
    private bool shouldDraw;
    private PaintWorkspace workspace;

    public PaintCanvasView (PaintWorkspace workspace) 
    {
        this.workspace = workspace;
        this.shouldDraw = false;
        this.bitmapGraphics = Graphics.FromImage(workspace.Canvas.Bitmap);

        this.Width = workspace.CanvasSize;
        this.Height = workspace.CanvasSize;

        this.Region = workspace.Canvas.Region;

        this.MouseDown += (sender, args) => {
            if (args.Button == MouseButtons.Left) {
                this.shouldDraw = true;
                this.workspace.Draw(args.X, args.Y);
                Console.Write("Start drawing\n");
            }
        };

        this.MouseUp += (sender, args) => {
            if (args.Button == MouseButtons.Left) {
                this.shouldDraw = false;
                Console.Write("Stop drawing\n");
            }
        };

        this.MouseMove += (sender, args) => {
            if (this.shouldDraw && this.workspace!=null) {
                this.workspace.Draw(args.X, args.Y);
            }
        };

        workspace.Canvas.Changed += (canvas) => {
            this.OnPaint();
        };
    }

    public void Init () 
    {
        this.OnPaint();
    }

    protected override void OnPaint (PaintEventArgs eArgs = null) 
    {  
        Graphics target;
        if (eArgs == null) {
            if (gBackup == null) gBackup = this.CreateGraphics();
            target = gBackup;
        }
        else {
            base.OnPaint(eArgs);
            gBackup = null;
            target = eArgs.Graphics;
        }
        target.DrawImage(this.workspace.Canvas.Bitmap, new Rectangle(0, 0, this.Width, this.Height));
    }

    private Graphics gBackup;
    private PaintColor colorBackup;
    private Brush brushBackup = null;
}
