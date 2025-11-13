using Knx.Falcon;

namespace BedivereKnx.Models
{

    public class KnxCurtain : KnxObject
    {

        /// <summary>
        /// 可调节
        /// </summary>
        public bool Adjustable { get; private set; }

        /// <summary>
        /// 开关反馈
        /// </summary>
        public GroupValue? StatusFeedback
        {
            get
            {
                _ = groups.TryGetValue(KnxObjectPart.SwitchFeedback, out KnxGroup? group);
                return group?.Value;
            }
        }

        /// <summary>
        /// 开度反馈
        /// </summary>
        public GroupValue? ValueFeedback
        {
            get
            {
                _ = groups.TryGetValue(KnxObjectPart.ValueFeedback, out KnxGroup? group);
                return group?.Value;
            }
        }

        public KnxCurtain(int id, string code, string? name, string? ifCode, string? areaCode)
            : base(KnxObjectType.Curtain, id, code, name, ifCode, areaCode)
        {
            Adjustable = groups.ContainsKey(KnxObjectPart.ValueControl)
                      && groups.ContainsKey(KnxObjectPart.ValueFeedback);
        }

    }

    /// <summary>
    /// 窗帘类型
    /// </summary>
    public enum CurtainType
    {
        /// <summary>
        /// 横拉窗帘
        /// </summary>
        Curtain,
        /// <summary>
        /// 竖向窗帘
        /// </summary>
        Blind
    }

    /// <summary>
    /// 窗帘开关状态
    /// </summary>
    public enum CurtainStatus
    {
        Unknown = -1,
        Open = 0,
        Close = 1
    }

}
