namespace BedivereKnx
{

    /// <summary>
    /// KNX组类型
    /// </summary>
    public enum KnxObjectType
    {
        None = 0,
        Light = 1,
        Curtain,
        Value,
        Enablement,
        Scene
    }

    public enum KnxEnablementType
    {
        General = 0,
        Enable,
        Lock
    }

    public enum KnxObjectPart
    {
        None,
        /// <summary>
        /// 开关控制
        /// </summary>
        SwitchControl,
        /// <summary>
        /// 开关反馈
        /// </summary>
        SwitchFeedback,
        /// <summary>
        /// 数值控制
        /// </summary>
        ValueControl,
        /// <summary>
        /// 数值反馈
        /// </summary>
        ValueFeedback,
        /// <summary>
        /// 调光控制
        /// </summary>
        DimmingControl,
        /// <summary>
        /// 场景控制
        /// </summary>
        SceneControl
    }

    /// <summary>
    /// KNX定时器状态
    /// </summary>
    public enum KnxScheduleTimerState
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stoped = 0,
        /// <summary>
        /// 启动中
        /// </summary>
        Starting = 2,
        /// <summary>
        /// 运行
        /// </summary>
        Running = 1
    }

    /// <summary>
    /// KNX报文类型
    /// </summary>
    public enum KnxMessageType
    {
        Unknown = -1,
        System = 0,
        FromBus = 1,
        ToBus = 2
    }

    /// <summary>
    /// KNX设备状态
    /// </summary>
    public enum KnxDeviceState
    {
        BusError = -2,
        Unknown = -1,
        Offline = 0,
        Online = 1
    }

}
