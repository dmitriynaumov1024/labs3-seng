using System;

interface ITextEditor
{
    string Text { get; set; }
    bool TrySave();
    bool TryOpen();
    void TryUndo();
    void TryRedo();
    void ShowStats();
    void ShowSearch();
    void ShowRemovedSpaces();
    event Action TextChanged;
}
