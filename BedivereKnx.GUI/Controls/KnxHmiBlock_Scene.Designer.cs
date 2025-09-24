namespace BedivereKnx.GUI.Controls
{
    partial class KnxHmiSceneBlock
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            btnAllScenes = new Button();
            tlpBtns = new TableLayoutPanel();
            btnScn3 = new Button();
            btnScn2 = new Button();
            btnScn1 = new Button();
            btnScn0 = new Button();
            tlpBtns.SuspendLayout();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.BackColor = SystemColors.Window;
            lblName.Location = new Point(0, 156);
            lblName.Size = new Size(196, 40);
            // 
            // btnAllScenes
            // 
            btnAllScenes.Dock = DockStyle.Bottom;
            btnAllScenes.Location = new Point(0, 131);
            btnAllScenes.Name = "btnAllScenes";
            btnAllScenes.Size = new Size(196, 25);
            btnAllScenes.TabIndex = 3;
            btnAllScenes.Text = "...";
            btnAllScenes.UseVisualStyleBackColor = true;
            btnAllScenes.Click += btnAllScenes_Click;
            // 
            // tlpBtns
            // 
            tlpBtns.ColumnCount = 2;
            tlpBtns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBtns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpBtns.Controls.Add(btnScn3, 1, 1);
            tlpBtns.Controls.Add(btnScn2, 0, 1);
            tlpBtns.Controls.Add(btnScn1, 1, 0);
            tlpBtns.Controls.Add(btnScn0, 0, 0);
            tlpBtns.Dock = DockStyle.Fill;
            tlpBtns.Location = new Point(0, 0);
            tlpBtns.Name = "tlpBtns";
            tlpBtns.RowCount = 2;
            tlpBtns.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpBtns.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpBtns.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpBtns.Size = new Size(196, 131);
            tlpBtns.TabIndex = 4;
            // 
            // btnScn3
            // 
            btnScn3.Dock = DockStyle.Fill;
            btnScn3.Location = new Point(101, 68);
            btnScn3.Name = "btnScn3";
            btnScn3.Size = new Size(92, 60);
            btnScn3.TabIndex = 3;
            btnScn3.Tag = "3";
            btnScn3.UseVisualStyleBackColor = true;
            btnScn3.Visible = false;
            // 
            // btnScn2
            // 
            btnScn2.Dock = DockStyle.Fill;
            btnScn2.Location = new Point(3, 68);
            btnScn2.Name = "btnScn2";
            btnScn2.Size = new Size(92, 60);
            btnScn2.TabIndex = 2;
            btnScn2.Tag = "2";
            btnScn2.UseVisualStyleBackColor = true;
            btnScn2.Visible = false;
            // 
            // btnScn1
            // 
            btnScn1.Dock = DockStyle.Fill;
            btnScn1.Location = new Point(101, 3);
            btnScn1.Name = "btnScn1";
            btnScn1.Size = new Size(92, 59);
            btnScn1.TabIndex = 1;
            btnScn1.Tag = "1";
            btnScn1.UseVisualStyleBackColor = true;
            btnScn1.Visible = false;
            // 
            // btnScn0
            // 
            btnScn0.Dock = DockStyle.Fill;
            btnScn0.Location = new Point(3, 3);
            btnScn0.Name = "btnScn0";
            btnScn0.Size = new Size(92, 59);
            btnScn0.TabIndex = 0;
            btnScn0.Tag = "0";
            btnScn0.UseVisualStyleBackColor = true;
            btnScn0.Visible = false;
            // 
            // KnxHmiSceneBlock
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(tlpBtns);
            Controls.Add(btnAllScenes);
            Name = "KnxHmiSceneBlock";
            Size = new Size(196, 196);
            Controls.SetChildIndex(lblName, 0);
            Controls.SetChildIndex(btnAllScenes, 0);
            Controls.SetChildIndex(tlpBtns, 0);
            tlpBtns.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnAllScenes;
        private TableLayoutPanel tlpBtns;
        private Button btnScn0;
        private Button btnScn3;
        private Button btnScn2;
        private Button btnScn1;
    }
}
