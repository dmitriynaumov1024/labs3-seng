using System;

public class PaintWorkspace
{
    // Constants
    const int CANVAS_SIZE_MIN = 100;
    const int CANVAS_SIZE_MAX = 640;
    const int BRUSH_SIZE_MIN = 1;
    const int BRUSH_SIZE_MAX = 48;

    // Private fields
    private PaintFactory factory;
    private PaintCanvas canvas;
    private PaintBrush brush;
    private PaintColor brushColor;
    private PaintColor backColor;
    private int brushSize;
    private int canvasSize;
    private string name;

    public Action<PaintCanvas> OnInit { get; set; }
    public Action<PaintCanvas, PaintBrush, int, int> OnDraw { get; set; }

    // Public properties
    public PaintColor BrushColor {
        get { return this.brushColor; }
        set { 
            this.brushColor = value; 
            this.brush = this.factory.GetBrush (this.brushColor, this.brushSize);
        }
    }

    public PaintColor BackColor {
        get { return this.backColor; }
    }

    public int BrushSize { 
        get { return this.brushSize; }
        set { 
            this.brushSize = value.Clamp(BRUSH_SIZE_MIN, BRUSH_SIZE_MAX); 
            this.brush = this.factory.GetBrush (this.brushColor, this.brushSize);
        }
    }

    public int CanvasSize {
        get { return this.canvasSize; }
    }

    public string Name {
        get { return this.name; }
        set {
            if (value.Length > 30) {
                value = value.Substring(0, 30);
            }
            this.name = value;
        }
    }

    public bool IsInitialized {
        get {
            return this.canvas != null;
        }
    }

    public PaintCanvas Canvas {
        get {
            return this.canvas;
        }
    }

    public PaintWorkspace (PaintFactory factory)
    {
        this.factory = factory;

        this.name = "Untitled";

        this.canvasSize = 300;
        this.brushSize = 10;

        this.backColor = new PaintColor("ffffff");
        this.brushColor = new PaintColor("000000");
        
        this.canvas = this.factory.GetCanvas (this.backColor, this.canvasSize);
        this.brush = this.factory.GetBrush (this.brushColor, this.brushSize);

        // this.OnInit(this.canvas);
    }

    public virtual void Draw (int x, int y) 
    {
        this.brush.Draw(this.canvas, x, y);
    }
}
