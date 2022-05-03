using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class PlainTextParser : TextParser
{
    static string[] LineSeparators = new string[]{"\r\n", "\n"};

    public PlainTextParser (string source) : base (source) { }

    public PlainTextParser (TextReader reader) : base(reader) { }

    public override void ParseTo (TextBuilder builder)
    {
        string[] lines = source.Split(LineSeparators, StringSplitOptions.None);
        foreach (string line in lines) {
            builder.AddText(line, TextStyles.Plain);
            builder.Paragraph();
        }
    }
}
