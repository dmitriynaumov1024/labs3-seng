using System;
using System.Net;
using System.Text;

public static class Utils
{
    public static string HtmlDecoded (this string source)
    {
        return WebUtility.HtmlDecode(source);
    }

    public static string HtmlEncoded (this string source)
    {
        return WebUtility.HtmlEncode(source);
    }

    public static string RtfEncoded (this string source)
    {
        StringBuilder sb = new StringBuilder(source);
        sb.Replace("\\", "\\\\");
        sb.Replace("{", "\\{");
        sb.Replace("}", "\\}");
        return sb.ToString();
    }
}
