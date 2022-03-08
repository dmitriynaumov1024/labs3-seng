using System;
using System.Linq;
using System.Text;

class TextStats
{
    public int KbCount { get; private set; }
    public int CharCount { get; private set; }
    public int LineCount { get; private set; }
    public int PageCount { get; private set; }
    public int EmptyLineCount { get; private set; }

    public TextStats (string text, Encoding encoding)
    {
        if (text==null) text = string.Empty;
        this.KbCount = encoding.GetByteCount(text) / 1024 + 1;
        this.CharCount = text.Length;
        this.LineCount = text.Count(c => c == '\n');
        if (!text.EndsWith("\n")) this.LineCount += 1;
        this.PageCount = this.CharCount / PageCharCount + 1;
        this.EmptyLineCount = CountEmptyLines(text);
    }

    // there are approx. 1800 chars in one page
    static int PageCharCount = 1800;

    // helper method to count empty lines
    static int CountEmptyLines (string text)
    {
        int count = 0;
        int length = text.Length - 1;
        for (int i=0; i<length; i++) {
            if (text[i]=='\n' && (text[i+1]=='\n' || text[i+1]=='\r')) {
                count++;
            } 
        }
        return count;
    }
}
