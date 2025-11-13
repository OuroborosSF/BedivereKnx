using BedivereKnx.Models;

namespace BedivereKnx.GUI.Controls
{

    internal partial class KnxHmiValueBlock : KnxHmiBlockBase
    {

        private readonly KnxValue? knxValue; //KNX对象

        internal KnxHmiValueBlock(KnxValue val)
            : base(val)
        {
            knxValue = val;
            InitializeComponent();
        }

    }

}
