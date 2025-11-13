using BedivereKnx.GUI.Controls;
using BedivereKnx.Models;

namespace BedivereKnx.GUI.Forms
{
    public partial class FrmMainPanel : Form
    {

        private readonly KnxSystem knx = Globals.KnxSys!;
        private Dictionary<string, List<KnxObject>> currentDic = [];
        //private readonly List<KnxHmiLightBlock> listSwitch = [];
        private List<KnxHmiBlockBase> currentControls = []; //当前显示的控件数组

        public FrmMainPanel()
        {
            InitializeComponent();
            AreaTreeInit();
            this.HorizontalScroll.Visible = false;
            tlpMain.HorizontalScroll.Visible = false;
            tlpBlock.HorizontalScroll.Visible = false;
        }

        /// <summary>
        /// 初始化树形结构
        /// </summary>
        private void AreaTreeInit()
        {
            tvArea.Nodes.Clear();
            TreeNode root = tvArea.Nodes.Add(Resources.Strings.Ui_RootNode); //添加根节点
            root.Expand(); //展开根节点
            root.Nodes.AddRange(knx.Areas.MainAreas.ToTreeNodes()); //添加主区域
            foreach (TreeNode mainNode in root.Nodes) //遍历主区域节点
            {
                mainNode.Expand(); //展开主节点
                mainNode.Nodes.AddRange(knx.Areas[mainNode.Name].ChildrenAreas.ToTreeNodes()); //向主区域下添加中区域
                foreach (TreeNode midNode in mainNode.Nodes)
                {
                    midNode.Nodes.AddRange(knx.Areas[midNode.Name].ChildrenAreas.ToTreeNodes()); //向中区域下添加子区域
                }
            }
        }

        /// <summary>
        /// 区域选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvArea_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is null) return;
            this.SuspendLayout();
            if (e.Node.Level >= 2)
            {
                currentDic = knx.Objects.GetGroupDic<KnxObject>(e.Node.Name);
                lstGroup.DataSource = currentDic.Keys.ToList(); //填充分组ListBox
                IEnumerable<KnxScene> listScn = knx.Scenes
                    .Where(scn => scn.AreaCode == e.Node.Name); //筛选出属于选择区域的KNX场景
                currentControls = ObjToControl(listScn); //控件列表此时只有场景
                IEnumerable<KnxObject> listObj = knx.Objects
                    .Where(obj => obj.AreaCode == e.Node.Name); //筛选出属于选择区域的KNX对象
                currentControls.AddRange(ObjToControl(listObj)); //控件列表中加入KNX对象部分
                PanelInit(tlpBlock, currentControls);
            }
            this.ResumeLayout();
        }

        /// <summary>
        /// 编组选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            return;
            //if (lstGroup.SelectedItem is null) return;
            //string? grpName = lstGroup.SelectedItem.ToString();
            //if (string.IsNullOrWhiteSpace(grpName)) return;
            //List<KnxObject> list = currentDic[grpName];
            //listSwitch.Clear();
            //foreach (KnxObject obj in list)
            //{
            //    switch (obj.Type)
            //    {
            //        case KnxObjectType.Light:
            //            //【区分调光】
            //            listSwitch.Add(new((KnxLight)obj) { Dock = DockStyle.Fill });
            //            break;
            //        case KnxObjectType.Curtain:
            //            break;
            //        case KnxObjectType.Value:
            //            break;
            //        case KnxObjectType.Enablement:
            //            break;
            //        default:
            //            continue;
            //    }
            //}
            ////MessageBox.Show(listSwitch.Count.ToString());
            ////pnlSwitch_Resize(pnlSwitch, EventArgs.Empty);
            //PanelInit(tlpBlock, listSwitch);
        }

        /// <summary>
        /// KNX对象列表转控件列表
        /// </summary>
        /// <param name="objList"></param>
        /// <returns></returns>
        private static List<KnxHmiBlockBase> ObjToControl(IEnumerable<KnxObject> objList)
        {
            if (objList is null) return [];
            List<KnxHmiBlockBase> ctlList = []; //输出用的列表
            foreach (KnxObject obj in objList)
            {
                switch (obj.Type)
                {
                    case KnxObjectType.Light: //灯光
                        //【区分调光】
                        KnxHmiLightBlock lgt = new((KnxLight)obj)
                        {
                            Dock = DockStyle.Fill,
                        };
                        ctlList.Add(lgt);
                        break;
                    case KnxObjectType.Curtain: //窗帘
                        KnxHmiCurtainBlock ctn = new((KnxCurtain)obj)
                        {
                            Dock = DockStyle.Fill,
                        };
                        ctlList.Add(ctn);
                        break;
                    case KnxObjectType.Value: //数值
                        KnxHmiValueBlock val = new((KnxValue)obj)
                        {
                            Dock = DockStyle.Fill,
                        };
                        ctlList.Add(val);
                        break;
                    case KnxObjectType.Enablement: //使能
                        KnxHmiEnableBlock en = new((KnxEnablement)obj)
                        {
                            Dock = DockStyle.Fill,
                        };
                        ctlList.Add(en);
                        break;
                    case KnxObjectType.Scene: //场景
                        KnxHmiSceneBlock scn = new((KnxScene)obj)
                        {
                            Dock = DockStyle.Fill,
                        };
                        ctlList.Add(scn);
                        break;
                    default:
                        continue;
                }
            }
            return ctlList;
        }

        private static List<KnxHmiBlockBase> ObjToControl(IEnumerable<KnxScene> scnList)
        {
            if (scnList is null) return [];
            List<KnxHmiBlockBase> ctlList = []; //输出用的列表
            foreach (KnxScene scn in scnList)
            {
                KnxHmiSceneBlock ctl = new(scn)
                {
                    Dock = DockStyle.Fill,
                };
                ctlList.Add(ctl);
            }
            return ctlList;
        }

        /// <summary>
        /// 加载控件列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tlp"></param>
        /// <param name="list"></param>
        private void PanelInit<T>(TableLayoutPanel tlp, List<T> list)
            where T : KnxHmiBlockBase, IDefaultSize
        {
            tlp.SuspendLayout(); //停止控件刷新
            tlp.Controls.Clear(); //清除原有控件
            tlp.RowStyles.Clear();
            tlp.ColumnStyles.Clear();
            tlp.AutoScroll = false; //禁用自动滚动
            //tlp.AutoScrollMinSize = Size.Empty;
            List<T> listV = list.Where(x => x.Visible == true).ToList(); //只包含需要显示的控件
            tlp.ColumnCount = (int)Math.Floor((double)tlpMain.Width / T.DefaultWidth); //列的数量
            for (int c = 0; c < tlp.ColumnCount; c++)
            {
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); //设定列的样式
            }
            tlp.RowCount = (int)Math.Ceiling((double)listV.Count / tlp.ColumnCount); //行的数量
            for (int r = 0; r < tlp.RowCount; r++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, T.DefaultHeight)); //设定行的样式
            }
            tlp.RowCount += 1; //额外添加一行，防止最后一行被拉高
            //tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            //Debug.Print($"RowCount:{tlp.RowCount}, ColumnCount: {tlp.ColumnCount}");
            for (int i = 0; i < listV.Count; i++)
            {
                int rId = i / tlp.ColumnCount;
                int cId = i % tlp.ColumnCount;
                tlp.Controls.Add(listV[i], cId, rId);
                //Debug.Print($"row={tlp.GetRow(listV[i])}, col={tlp.GetColumn(listV[i])}");
            }
            tlp.ResumeLayout(); //恢复控件刷新
            tlp.AutoScroll = true; //启用自动滚动
        }

        private void PanelFilter<T>(bool show) where T : KnxHmiBlockBase
        {
            foreach (var b in currentControls.OfType<T>())
            {
                b.Visible = show;
            }
            PanelInit(tlpBlock, currentControls);
        }

        private void tlpBlock_SizeChanged(object sender, EventArgs e)
        {
            PanelInit(tlpBlock, currentControls);
        }

        private void chkLight_CheckedChanged(object sender, EventArgs e)
        {
            PanelFilter<KnxHmiLightBlock>(chkLight.Checked);
        }

        private void chkCurtain_CheckedChanged(object sender, EventArgs e)
        {
            PanelFilter<KnxHmiCurtainBlock>(chkCurtain.Checked);
        }

        private void chkValue_CheckedChanged(object sender, EventArgs e)
        {
            PanelFilter<KnxHmiValueBlock>(chkValue.Checked);
        }

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            PanelFilter<KnxHmiEnableBlock>(chkEnable.Checked);
        }

        private void chkScene_CheckedChanged(object sender, EventArgs e)
        {
            PanelFilter<KnxHmiSceneBlock>(chkScene.Checked);
        }

    }

}
