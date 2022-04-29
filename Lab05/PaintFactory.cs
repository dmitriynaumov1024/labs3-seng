using System;

public delegate PaintBrush PaintBrushCreator (PaintColor color, int size);
public delegate PaintCanvas PaintCanvasCreator (PaintColor color, int size);

public class PaintFactory
{
    public PaintBrushCreator GetBrush { get; set; }
    public PaintCanvasCreator GetCanvas { get; set; }
} 
