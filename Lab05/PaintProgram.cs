using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public abstract class PaintProgram
{
    const int WORKSPACE_LIMIT = 4;

    public Dictionary<string, PaintFactory> Factories = 
        new Dictionary<string, PaintFactory> {
        { "Rectangular", new PaintFactory {
            GetBrush = (color, size) => 
                new PaintBrushSquare(color, size),
            GetCanvas = (color, size) => 
                new PaintCanvasSquare(color, size)
        }},
        { "Round", new PaintFactory {
            GetBrush = (color, size) => 
                new PaintBrushRound(color, size),
            GetCanvas = (color, size) => 
                new PaintCanvasRound(color, size)
        }}
    };

    protected List<PaintWorkspace> workspaces;

    protected PaintWorkspace ActiveWorkspace;

    public void TryNew(string shapeName)
    {
        if (this.workspaces.Count < WORKSPACE_LIMIT) {
            int index = this.workspaces.Count;
            PaintWorkspace newWorkspace = this.TryNewImpl(shapeName);
            this.workspaces.Add(newWorkspace);
            this.WorkspaceAdded.Invoke(newWorkspace, index);
        }
        else {
            this.ShowInfoImpl("Can not create more workspaces.");
        }
    }

    public void TryOpen() 
    {
        
    }

    public void TrySave()
    {
        var workspace = this.ActiveWorkspace;
        string filename = this.SaveDialogImpl(workspace.Name);
        if (filename != null) {
            workspace.Canvas.Bitmap.Save (File.OpenWrite(filename), ImageFormat.Png);
        }
    }

    public void Run()
    {
        this.RunImpl();
    }

    public PaintWorkspace GetWorkspace(int index)
    {
        try {
            return this.workspaces[index];
        }
        catch (Exception ex) {
            return null;
        }
    }

    public void RemoveWorkspace(int index)
    {
        try {
            this.workspaces.RemoveAt(index);
        }
        catch (Exception ex) {
            return;
        }
    }

    public void SelectWorkspace(int index)
    {
        Console.Write("Selected {0}\n", index);
        this.ActiveWorkspace = this.workspaces[index];
        this.WorkspaceSelected.Invoke(this.ActiveWorkspace, index);
    }

    // To be implemented
    protected abstract PaintWorkspace TryNewImpl(string name);
    protected abstract string OpenDialogImpl();
    protected abstract string SaveDialogImpl(string workspaceName);
    protected abstract void RunImpl();
    protected abstract void ShowInfoImpl(string message);

    public event Action<PaintWorkspace, int> WorkspaceSelected;
    public event Action<PaintWorkspace, int> WorkspaceAdded;
    public event Action<PaintWorkspace, int> WorkspaceRemoved;

    protected PaintProgram()
    {
        this.workspaces = new List<PaintWorkspace>();
    }
}
