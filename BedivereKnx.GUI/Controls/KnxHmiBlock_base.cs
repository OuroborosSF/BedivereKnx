using BedivereKnx.Models;

namespace BedivereKnx.GUI.Controls
{

    public partial class KnxHmiBlockBase : UserControl, IDefaultSize
    {

        public static int DefaultWidth => 200;

        public static int DefaultHeight => 200;

        public KnxHmiBlockBase()
        {
            InitializeComponent();
        }

        protected KnxHmiBlockBase(KnxObject obj)
        {
            InitializeComponent();
            if (string.IsNullOrWhiteSpace(obj?.Name))
            {
                lblName.Text = obj?.Code;
            }
            else
            {
                lblName.Text = obj?.Name;
            }
            //调整字体大小确保完全显示：
            float fontSize = lblName.Font.Size;
            while (TextRenderer.MeasureText(lblName.Text, new Font(lblName.Font.FontFamily, fontSize)).Width > lblName.Width)
            {
                fontSize -= 0.5f;
                lblName.Font = new Font(lblName.Font.FontFamily, fontSize);
            }
            //Size textSize = TextRenderer.MeasureText(lblName.Text, lblName.Font);
            //if (textSize.Width > lblName.Width)
            //{
            //    float newSize = lblName.Font.Size * (float)lblName.Width / textSize.Width;
            //    lblName.Font = new Font(lblName.Font.FontFamily, newSize);
            //}
        }

    }

}
