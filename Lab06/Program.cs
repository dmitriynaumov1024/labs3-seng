using System;
using System.IO;

public class Program
{
    public static void DirectTheBuild (TextBuilder builder)
    {
        if (builder == null) return;
        builder.AddText("Hello!", TextStyles.Bold);
        builder.Paragraph();
        builder.AddText("This is ");
        builder.AddText("bold italic", TextStyles.Bold | TextStyles.Italic);
        builder.Paragraph();
        builder.AddText("#include <stdio.h>");
        builder.Paragraph();
        builder.AddText("#include <stdlib.h>");
        builder.Paragraph();
        builder.AddText("int main() { return 0; }");
        builder.Paragraph();
        builder.AddText("This is underline", TextStyles.Underline);
        builder.Paragraph();
        builder.AddText("This is strikethrough { }", TextStyles.Strikethru);
        builder.Paragraph();
    }

    [STAThread]
    public static void Main (string[] args)
    {
        if (!Directory.Exists("./example/")) {
            Console.Write("Directory example does not exist. \n");
            return;
        }

        TextParser p1 = new RtfTextParser(File.OpenText("./example/Helloworld2.rtf"));
        TextParser p2 = new PlainTextParser(File.OpenText("./example/ApplicationBag.cs"));

        new PlainTextBuilder(p1.Chunks).SaveTo(File.Create("./example/rtf-to-plain.txt"));
        new HtmlTextBuilder(p1.Chunks).SaveTo(File.Create("./example/rtf-to-html.html"));
        new RtfTextBuilder(p1.Chunks).SaveTo(File.Create("./example/rtf-to-rtf.rtf"));

        new RtfTextBuilder(p2.Chunks).SaveTo(File.Create("./example/txt-to-rtf.rtf"));
        new HtmlTextBuilder(p2.Chunks).SaveTo(File.Create("./example/txt-to-html.html"));
    }
}
