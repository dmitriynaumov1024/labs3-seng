using System;
using System.Collections.Generic;
using System.IO;

public abstract class TextParser
{
    protected string source;

    public TextParser (string source)
    {
        this.source = source;
    }

    public TextParser (TextReader reader) : this(reader.ReadToEnd()) { }

    public abstract void ParseTo (TextBuilder builder);
}
