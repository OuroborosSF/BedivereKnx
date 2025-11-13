namespace BedivereKnx.GUI.Forms
{
    partial class FrmMainPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainPanel));
            tvArea = new TreeView();
            tlpBlock = new TableLayoutPanel();
            lstGroup = new ListBox();
            tlpMain = new TableLayoutPanel();
            tlpTop = new TableLayoutPanel();
            chkLight = new CheckBox();
            chkCurtain = new CheckBox();
            chkValue = new CheckBox();
            chkEnable = new CheckBox();
            chkScene = new CheckBox();
            tlpMain.SuspendLayout();
            tlpTop.SuspendLayout();
            SuspendLayout();
            // 
            // tvArea
            // 
            resources.ApplyResources(tvArea, "tvArea");
            tvArea.Name = "tvArea";
            tvArea.AfterSelect += tvArea_AfterSelect;
            // 
            // tlpBlock
            // 
            resources.ApplyResources(tlpBlock, "tlpBlock");
            tlpBlock.Name = "tlpBlock";
            tlpBlock.SizeChanged += tlpBlock_SizeChanged;
            // 
            // lstGroup
            // 
            resources.ApplyResources(lstGroup, "lstGroup");
            lstGroup.FormattingEnabled = true;
            lstGroup.Name = "lstGroup";
            lstGroup.SelectedIndexChanged += lstGroup_SelectedIndexChanged;
            // 
            // tlpMain
            // 
            resources.ApplyResources(tlpMain, "tlpMain");
            tlpMain.Controls.Add(tlpBlock, 1, 0);
            tlpMain.Controls.Add(lstGroup, 0, 0);
            tlpMain.Name = "tlpMain";
            // 
            // tlpTop
            // 
            tlpTop.BackColor = Color.Gainsboro;
            resources.ApplyResources(tlpTop, "tlpTop");
            tlpTop.Controls.Add(chkLight, 0, 0);
            tlpTop.Controls.Add(chkCurtain, 1, 0);
            tlpTop.Controls.Add(chkValue, 2, 0);
            tlpTop.Controls.Add(chkEnable, 3, 0);
            tlpTop.Controls.Add(chkScene, 4, 0);
            tlpTop.Name = "tlpTop";
            // 
            // chkLight
            // 
            chkLight.Checked = true;
            chkLight.CheckState = CheckState.Checked;
            resources.ApplyResources(chkLight, "chkLight");
            chkLight.Name = "chkLight";
            chkLight.UseVisualStyleBackColor = true;
            chkLight.CheckedChanged += chkLight_CheckedChanged;
            // 
            // chkCurtain
            // 
            chkCurtain.Checked = true;
            chkCurtain.CheckState = CheckState.Checked;
            resources.ApplyResources(chkCurtain, "chkCurtain");
            chkCurtain.Name = "chkCurtain";
            chkCurtain.UseVisualStyleBackColor = true;
            chkCurtain.CheckedChanged += chkCurtain_CheckedChanged;
            // 
            // chkValue
            // 
            chkValue.Checked = true;
            chkValue.CheckState = CheckState.Checked;
            resources.ApplyResources(chkValue, "chkValue");
            chkValue.Name = "chkValue";
            chkValue.UseVisualStyleBackColor = true;
            chkValue.CheckedChanged += chkValue_CheckedChanged;
            // 
            // chkEnable
            // 
            chkEnable.Checked = true;
            chkEnable.CheckState = CheckState.Checked;
            resources.ApplyResources(chkEnable, "chkEnable");
            chkEnable.Name = "chkEnable";
            chkEnable.UseVisualStyleBackColor = true;
            chkEnable.CheckedChanged += chkEnable_CheckedChanged;
            // 
            // chkScene
            // 
            chkScene.Checked = true;
            chkScene.CheckState = CheckState.Checked;
            resources.ApplyResources(chkScene, "chkScene");
            chkScene.Name = "chkScene";
            chkScene.UseVisualStyleBackColor = true;
            chkScene.CheckedChanged += chkScene_CheckedChanged;
            // 
            // FrmMainPanel
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            Controls.Add(tlpTop);
            Controls.Add(tvArea);
            DoubleBuffered = true;
            Name = "FrmMainPanel";
            tlpMain.ResumeLayout(false);
            tlpTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        internal TreeView tvArea;
        private TableLayoutPanel tlpBlock;
        private ListBox lstGroup;
        private TableLayoutPanel tlpMain;
        private TableLayoutPanel tlpTop;
        private CheckBox chkLight;
        private CheckBox chkCurtain;
        private CheckBox chkValue;
        private CheckBox chkEnable;
        private CheckBox chkScene;
    }
}