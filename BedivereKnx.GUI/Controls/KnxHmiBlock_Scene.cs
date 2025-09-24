using BedivereKnx.Models;

namespace BedivereKnx.GUI.Controls
{

    public partial class KnxHmiSceneBlock : KnxHmiBlockBase
    {

        private readonly KnxScene? knxScn;
        private readonly Button[] btns = [];

        public KnxHmiSceneBlock(KnxScene scn)
            : base(scn)
        {
            knxScn = scn;
            InitializeComponent();
            btns = [btnScn0, btnScn1, btnScn2, btnScn3]; //场景按钮的数组
            int i = 0;
            for (byte n = 0; n < knxScn.Names.Length; n++)
            {
                string? scnName = knxScn.Names[n]?.Trim();
                if (string.IsNullOrWhiteSpace(scnName)) continue; //跳过空项
                if (i < btns.Length - 1)
                {
                    btns[i].Text = knxScn.Names[n]; //按钮文本设置为对应场景名称
                    btns[i].Tag = n; //Tag设置为场景号
                    btns[i].Visible = true;
                    btns[i].Click += btnScn_Click; //绑定按钮点击事件
                    i++;
                }
                else
                {
                    break;
                }
            }
            if (knxScn.SceneCount <= 2)
            {
                tlpBtns.RowStyles[1].SizeType = SizeType.Absolute;
                tlpBtns.RowStyles[1].Height = 0;
            }
        }

        private void btnScn_Click(object? sender, EventArgs e)
        {
            if (knxScn is null) return;
            Button btn = (Button)sender!;
            byte scnNum = (byte)btn.Tag!;
            knxScn.SceneControl(scnNum);
        }

        private void btnAllScenes_Click(object sender, EventArgs e)
        {
            if (knxScn is null) return;
            Forms.FrmSceneCtl frmSceneCtl = new(knxScn);
            if (frmSceneCtl.ShowDialog() == DialogResult.OK)
            {
                knxScn.SceneControl(frmSceneCtl.SelectedNum);
            }
        }

    }

}
