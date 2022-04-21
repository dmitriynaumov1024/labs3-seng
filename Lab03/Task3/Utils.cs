using System;
using System.IO;
using System.Text;

class Utils
{
    public static readonly string PlainTextHttpHeader = 
        "HTTP/1.1 200 OK\r\n" + 
        "Connection: keep-alive\r\n" + 
        "Content-Type: text/plain\r\n" +
        "Server: STFU\r\n\r\n";

    public static string GetRelativePath (DirectoryInfo directory, DirectoryInfo root)
    {
        string rootPath = root.FullName.Replace('\\', '/');
        string dirPath = directory.FullName.Replace('\\', '/');
        if (rootPath.Length > dirPath.Length) {
            return "";
        }
        else {
            return dirPath.Replace(rootPath, "");
        }
    }

    public static string GetFsEntriesText (string relPath, string rootPath) {
        string path = rootPath + relPath;
        var baseDir = new DirectoryInfo(path);
        StringBuilder sb = new StringBuilder();
        if (Directory.Exists(path)) {
            sb.AppendFormat("Contents of {0}:\r\n", relPath);
            foreach (DirectoryInfo dir in baseDir.EnumerateDirectories()) {
                sb.AppendFormat("{0}/\r\n", dir.Name);
            }
            foreach (FileInfo file in baseDir.EnumerateFiles()) {
                sb.AppendFormat("{0} \r\n", file.Name);
            }
            return sb.ToString();
        }
        else if (File.Exists(path)) {
            return String.Format("{0} is a file.\r\n", relPath);
        }
        else {
            return String.Format("Directory {0} does not exist.\r\n", relPath);
        }
    }
}
