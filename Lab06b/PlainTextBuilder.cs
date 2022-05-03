using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class PlainTextBuilder : TextBuilder
{
    public PlainTextBuilder () : base () { }

    public override void Paragraph ()
    {
        writer.Write("\r\n");
    }

    public override void AddTextChunk (TextChunk chunk)
    {
        writer.Write(chunk.Text);
    }

    public override void BeginText ()
    {
        // Do nothing
    }

    public override void EndText ()
    {
        // Do nothing
    }
}
