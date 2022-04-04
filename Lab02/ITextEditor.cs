using System;

interface ITextEditor
{
    string Text { get; set; }
    bool TrySave();
    bool TryOpen();
    void ShowStats();
    void ShowRemovedSpaces();
    event Action TextChanged;
}
