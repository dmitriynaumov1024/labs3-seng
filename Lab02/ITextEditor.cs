using System;

interface ITextEditor
{
    string Text { get; set; }
    bool TrySave();
    bool TryOpen();
    event Action TextChanged;
}
