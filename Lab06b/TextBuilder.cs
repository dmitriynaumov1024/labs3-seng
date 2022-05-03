using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

public abstract class TextBuilder
{
    protected TextWriter writer;

    public bool HasWriter {
        get {
            return this.writer != null;
        }
    }

    public TextBuilder () { }

    public void Attach (TextWriter writer)
    {
        this.writer = writer;
        this.BeginText();
    }

    public void AddText (string text, TextStyles style = TextStyles.Plain) 
    {
        this.AddTextChunk (new TextChunk {
            Text = text,
            Style = style
        });
    }

    public abstract void Paragraph ();

    public abstract void AddTextChunk (TextChunk chunk);

    public abstract void BeginText ();

    public abstract void EndText ();

    public void Detach () 
    {
        this.EndText();
        this.writer.Flush();
        this.writer = null;
    }
}
