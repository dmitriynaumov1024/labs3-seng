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
            Directory.CreateDirectory("./example/");
        }

        TextBuilder b1 = new PlainTextBuilder();
        DirectTheBuild(b1);
        b1.SaveTo(File.Create("./example/result.txt"));

        TextBuilder b2 = new HtmlTextBuilder();
        DirectTheBuild(b2);
        b2.SaveTo(File.Create("./example/result.html"));

        TextBuilder b3 = new RtfTextBuilder(b2);
        b3.SaveTo(File.Create("./example/result.rtf"));
    }
}
