using System;
using System.Drawing;
using System.Drawing.Imaging;

public abstract class PaintBrush
{
    public ShapeType Shape { get; set; }
    public PaintColor Color { get; set; }
    public int Size { get; set; }

    public void Draw (PaintCanvas canvas, int x, int y) 
    {
        Action<Brush, Rectangle> drawAction = this.DrawAction(canvas);

        Color color = System.Drawing.Color.FromArgb 
            (this.Color.R, this.Color.G, this.Color.B);

        Rectangle drawingRect = new Rectangle 
            (x - Size/2, y - Size/2, Size, Size);

        drawAction(new SolidBrush(color), drawingRect);

        canvas.Update();
    }

    protected abstract Action<Brush, Rectangle> DrawAction (PaintCanvas canvas);
}

public class PaintBrushRound : PaintBrush
{
    protected override Action<Brush, Rectangle> DrawAction(PaintCanvas canvas) 
    {
        return canvas.Graphics.FillEllipse;
    }

    public PaintBrushRound (PaintColor color, int size)
    {
        this.Shape = ShapeType.Circle;
        this.Color = color;
        this.Size = size;
    }
}

public class PaintBrushSquare : PaintBrush
{
    protected override Action<Brush, Rectangle> DrawAction(PaintCanvas canvas) 
    {
        return canvas.Graphics.FillRectangle;
    }

    public PaintBrushSquare (PaintColor color, int size)
    {
        this.Shape = ShapeType.Square;
        this.Color = color;
        this.Size = size;
    }
}
