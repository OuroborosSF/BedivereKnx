using BedivereKnx.Models;

namespace BedivereKnx.GUI.Controls
{

    internal partial class KnxHmiCurtainBlock : KnxHmiBlockBase
    {

        private readonly KnxCurtain? knxCurtain; //KNX对象

        internal KnxHmiCurtainBlock(KnxCurtain curtain)
            : base(curtain)
        {
            knxCurtain = curtain;
            InitializeComponent();
        }

    }

}
