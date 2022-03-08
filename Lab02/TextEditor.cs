using System;
using System.IO;
using System.Text;

class TextEditor: ITextEditor
{
    public virtual string Text { get; set; }
    public Func<Stream> SaveSelector { get; set; }
    public Func<Stream> OpenSelector { get; set; }
    public Action<TextStats> ShowStatsPopup { get; set; }
    public event Action TextChanged;

    public bool TryOpen() 
    {
        try {
            var reader = new StreamReader(this.OpenSelector());
            this.Text = reader.ReadToEnd();
            this.TextChanged.Invoke();
            return true;
        }
        catch (Exception ex) {
            return false;
        }
    }

    public bool TrySave()
    {
        try {
            var writer = new StreamWriter(this.SaveSelector());
            writer.Write(this.Text);
            writer.Close();
            return true;
        }
        catch (Exception ex) {
            return false;
        }
    }

    public void ShowStats()
    {
        this.ShowStatsPopup(new TextStats(this.Text, Encoding.UTF8));
    }
}
