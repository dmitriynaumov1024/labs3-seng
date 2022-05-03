using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class HtmlTextBuilder : TextBuilder
{
    protected const string 
        HTML_BEGIN = "<!DOCTYPE html>\r\n<html>\r\n<head></head>\r\n<body>\r\n",
        HTML_END = "</body>\r\n</html>\r\n",
        P_BEGIN = "<p>",
        P_END = "</p>\r\n";

    protected static IDictionary<TextStyles, string> 
        StylesAndTagNames = new SortedDictionary<TextStyles, string> {
        { TextStyles.Bold, "b" },
        { TextStyles.Italic, "i" },
        { TextStyles.Underline, "u" },
        { TextStyles.Strikethru, "s" }
    };

    public HtmlTextBuilder () : base () { }

    bool paragraphOpen = false;

    public override void Paragraph ()
    {
        if (paragraphOpen) {
            writer.Write(P_END);
            paragraphOpen = false;
        }
    }

    public override void AddTextChunk (TextChunk chunk)
    {
        if (!paragraphOpen) {
            paragraphOpen = true;
            writer.Write(P_BEGIN);
        }
        Stack<string> tags = new Stack<string>();
        foreach (var kvPair in StylesAndTagNames) {
            if (chunk.Style.HasFlag(kvPair.Key)) {
                writer.Write("<{0}>", kvPair.Value);
                tags.Push(kvPair.Value);
            }
        }
        writer.Write(chunk.Text.HtmlEncoded());
        foreach (string tag in tags) {
            writer.Write("</{0}>", tag);
        }
    }

    public override void BeginText ()
    {
        writer.Write(HTML_BEGIN);
    }

    public override void EndText ()
    {
        if (paragraphOpen) {
            writer.Write(P_END);
            paragraphOpen = false;
        }   
        writer.Write(HTML_END);
    }
}
