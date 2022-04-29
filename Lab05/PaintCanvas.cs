using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public abstract class PaintCanvas 
{
    private Bitmap bitmap;
    private Region region;
    private Graphics graphics;

    public PaintColor BackColor { get; set; }
    public ShapeType Shape { get; set; }
    public int Size { get; set; }

    public Bitmap Bitmap { 
        get { 
            return this.bitmap; 
        } 
        protected set {
            this.bitmap = value;
            this.graphics = Graphics.FromImage(this.bitmap);
        }
    }

    public Region Region {
        get { 
            return this.region; 
        }
        protected set {
            this.region = value;
        }
    }

    public Graphics Graphics {
        get { 
            return this.graphics; 
        }
    }

    public PaintCanvas (ShapeType shape, PaintColor color, int size) 
    {
        this.Bitmap = new Bitmap(size, size);
        this.Shape = shape;
        var sdcolor = Color.FromArgb(color.R, color.G, color.B);
        this.graphics.FillRectangle(new SolidBrush(sdcolor), 0, 0, size, size);
    }

    public event Action<PaintCanvas> Changed;

    public void Update()
    {
        this.Changed.Invoke(this);
    }
}

public class PaintCanvasRound : PaintCanvas
{
    public PaintCanvasRound (PaintColor color, int size)
    : base (ShapeType.Circle, color, size)
    {
        var gpath = new GraphicsPath();
        gpath.AddEllipse(new Rectangle(0, 0, size, size));
        this.Region = new Region(gpath);
        this.Graphics.Clip = new Region(gpath);
    }
}

public class PaintCanvasSquare : PaintCanvas
{
    public PaintCanvasSquare (PaintColor color, int size)
    : base (ShapeType.Square, color, size)
    {
        var gpath = new GraphicsPath();
        gpath.AddRectangle(new Rectangle(0, 0, size, size));
        this.Region = new Region(gpath);
        this.Graphics.Clip = new Region(gpath);
    }
}
