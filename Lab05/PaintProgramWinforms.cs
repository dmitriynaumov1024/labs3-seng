using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class PaintProgramWinforms: PaintProgram
{
    protected Dictionary<PaintWorkspace, PaintWorkspaceView> workspacesAndViews;

    protected PaintProgramWinforms () : base () 
    { 
        this.workspacesAndViews = 
            new Dictionary<PaintWorkspace, PaintWorkspaceView>();
    }

    protected override PaintWorkspace TryNewImpl (string shapeName) 
    { 
        PaintWorkspaceView view = new PaintWorkspaceView();
        PaintWorkspace workspace = new PaintWorkspace(Factories[shapeName]);
        // workspace.OnDraw = view.Draw;
        // workspace.OnInit = view.Init;
        view.AttachWorkspace(workspace);
        this.workspacesAndViews[workspace] = view;
        return workspace;
    }

    protected override string OpenDialogImpl () 
    { 
        var dialog = new OpenFileDialog {
            RestoreDirectory = true,
            Title = "Save as",
            Filter = "All files | *.*"
        };
        if (dialog.ShowDialog() == DialogResult.OK) {
            return dialog.FileName;
        }
        else {
            return null;
        }
    }

    protected override string SaveDialogImpl (string workspaceName)
    {
        var dialog = new SaveFileDialog {
            RestoreDirectory = true,
            Title = "Save as",
            Filter = "All files | *.*"
        };
        if (dialog.ShowDialog() == DialogResult.OK) {
            return dialog.FileName;
        }
        else {
            return null;
        }
    }

    protected override void RunImpl ()
    {
        // Application.EnableVisualStyles();
        Application.Run (new PaintProgramView(this));
    }

    protected override void ShowInfoImpl (string message)
    {
        MessageBox.Show (message);
    }

    public PaintWorkspaceView GetView (PaintWorkspace workspace)
    {
        return this.workspacesAndViews[workspace];
    }
}
