using System;
using System.IO;

public class Program
{
    [STAThread]
    public static void Main (string[] args)
    {
        if (!Directory.Exists("./example/")) {
            Console.Write("Directory example does not exist. \n");
            return;
        }

        TextParser p1 = new RtfTextParser(File.OpenText("./example/Helloworld2.rtf"));

        var htmlBuilder = new HtmlTextBuilder();
        htmlBuilder.Attach(File.CreateText("./example/rtf-to-html.html"));
        p1.ParseTo(htmlBuilder);
        htmlBuilder.Detach();

        var plainBuilder = new PlainTextBuilder();
        plainBuilder.Attach(File.CreateText("./example/rtf-to-plain.txt"));
        p1.ParseTo(plainBuilder);
        plainBuilder.Detach();

        var rtfBuilder = new RtfTextBuilder();
        rtfBuilder.Attach(File.CreateText("./example/rtf-to-rtf.rtf"));
        p1.ParseTo(rtfBuilder);
        rtfBuilder.Detach();
    }
}
