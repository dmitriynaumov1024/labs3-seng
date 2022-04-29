using System;
using System.Windows.Forms;
using System.Drawing;

public class PaintProgramView: Form
{
    private PaintProgramWinforms program;

    private TabControl tabPanel;
    private PaintProgramViewSidePanel sidePanel;

    public PaintProgramView (PaintProgramWinforms program)
    {
        this.program = program;
        this.InitializeView();

        this.program.WorkspaceAdded += (workspace, index) => {
            TabPage page = new TabPage(workspace.Name);
            page.Controls.Add(this.program.GetView(workspace));
            this.tabPanel.TabPages.Add(page);
            this.program.SelectWorkspace(index);
        };

        this.program.WorkspaceSelected += (workspace, index) => {
            this.SelectWorkspace(workspace, index);
        };

        this.program.WorkspaceRemoved += (workspace, index) => {
            this.sidePanel.DetachWorkspace();
            this.tabPanel.TabPages.RemoveAt(index);
            this.program.SelectWorkspace(0);
        };

        this.tabPanel.SelectedIndexChanged += (sender, args) => {
            this.program.SelectWorkspace(this.tabPanel.SelectedIndex);
        };

        this.ResizeBegin += (sender, eArgs) => {
            this.SuspendLayout();
        };

        this.ResizeEnd += (sender, eArgs) => {
            this.ResumeLayout();
        };
    }

    private void InitializeView ()
    {
        this.Text = "Paint";
        this.MinimumSize = new Size(720, 520);
        this.ClientSize = new Size(920, 640);
        this.StartPosition = FormStartPosition.CenterScreen;

        this.tabPanel = new TabControl {
            Dock = DockStyle.Fill
        };
        this.Controls.Add(this.tabPanel);

        this.sidePanel = new PaintProgramViewSidePanel() {
            BackColor = Color.FromArgb(245, 245, 245),
            Dock = DockStyle.Left,
            Width = 280
        };
        this.Controls.Add(this.sidePanel);

        var menuStrip = new MenuStrip {
            Dock = DockStyle.Top
        };
        var fileItem = new ToolStripMenuItem { Text = "File" };
        foreach (string key in program.Factories.Keys) {
            fileItem.DropDownItems.Add("Create "+key, null, (sender, args) => {
                program.TryNew(key);
            });
        }
        
        // fileItem.DropDownItems.Add("Open file", null, (sender, args) => {
            
        // });
        fileItem.DropDownItems.Add("Save as", null, (sender, args) => {
            this.program.TrySave();
        });
        menuStrip.Items.Add(fileItem);
        this.Controls.Add(menuStrip);
    }

    private void SelectWorkspace (PaintWorkspace workspace, int index)
    {
        this.tabPanel.SelectedIndex = index;
        this.sidePanel.AttachWorkspace(workspace);
    }
}
