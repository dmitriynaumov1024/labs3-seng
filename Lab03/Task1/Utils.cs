using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;

static class Utils
{
    static Regex RE_XML_TAGS = new Regex(@"\<[^\>]+\>");
    static string[] Whitespaces = new string[]{" ", "\t", "\n", "\r"};
    static StringSplitOptions NoEmpty = StringSplitOptions.RemoveEmptyEntries;

    public static string RemoveXmlTags (string source)
    {
        string[] words = RE_XML_TAGS
            .Replace(source, " ")
            .HtmlDecode()
            .Split(Whitespaces, NoEmpty);
        return String.Join(" ", words);
    }

    static string HtmlDecode (this string source)
    {
        return WebUtility.HtmlDecode(source);
    }
}
