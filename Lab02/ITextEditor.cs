using System;

interface ITextEditor
{
    string Text { get; set; }
    bool TrySave();
    bool TryOpen();
    void ShowStats();
    event Action TextChanged;
}
