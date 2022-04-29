using System;

public class PaintColor
{
    private int r, g, b;

    public int R {
        get { return this.r; }
        set { this.r = value.Clamp(0, 255); }
    }
    public int G {
        get { return this.g; }
        set { this.g = value.Clamp(0, 255); }
    }
    public int B {
        get { return this.b; }
        set { this.b = value.Clamp(0, 255); }
    }

    public PaintColor (string hexRGB)
    {
        try {
            hexRGB = hexRGB.Substring(hexRGB.Length-6);
            int rgb = Convert.ToInt32(hexRGB, 16);
            this.B = rgb & 255;
            this.G = (rgb >> 8) & 255;
            this.R = (rgb >> 16) & 255;
            return;
        }
        catch (Exception ex) {
            // Fallback
            this.R = 0;
            this.G = 0;
            this.B = 0;
        }
    }

    public PaintColor (int r, int g, int b) 
    {
        this.R = r;
        this.G = g;
        this.B = b;
    }

    public string ToHexString() 
    {
        return String.Format("#{0:x2}{1:x2}{2:x2}", this.r, this.g, this.b);
    }
}
