using Knx.Falcon;
using System.Data;

namespace BedivereKnx
{

    /// <summary>
    /// KNX报文日志记录器
    /// </summary>
    public class KnxMessageLogger
    {

        /// <summary>
        /// 报文日志使能
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 报文日志表
        /// </summary>
        public DataTable Table { get; private set; } = new();

        /// <summary>
        /// 初始化KNX报文日志记录器
        /// </summary>
        /// <param name="enable">是否启用日志</param>
        public KnxMessageLogger(bool enable = false)
        {
            TableInit();
            Enable = enable;
        }

        /// <summary>
        /// 初始化报文日志表
        /// </summary>
        private void TableInit()
        {
            Table = new DataTable();
            Table.Columns.Add("DateTime", "DateTime", typeof(DateTime)); //报文时间
            Table.Columns.Add("MessageType", "MessageType", typeof(KnxMessageType)); //报文类型
            Table.Columns.Add("EventType", "EventType", typeof(GroupEventType)); //事件类型
            Table.Columns.Add("SourceAddress", "SourceAddress", typeof(IndividualAddress)); //源地址
            Table.Columns.Add("DestinationAddress", "DestinationAddress", typeof(GroupAddress)); //目标地址
            Table.Columns.Add("MessagePriority", "MessagePriority", typeof(MessagePriority)); //优先级
            Table.Columns.Add("Value", "Value", typeof(GroupValue)); //值
            Table.Columns.Add("HopCount", "HopCount", typeof(byte)); //路由计数
            Table.Columns.Add("IsSecure", "IsSecure", typeof(bool)); //安全性
            Table.Columns.Add("Info", "Info", typeof(string)); //额外信息
        }

        /// <summary>
        /// 报文传输事件
        /// </summary>
        /// <param name="e">KNX报文事件参数</param>
        /// <param name="info">额外信息</param>
        internal void OnMessageTransmission(KnxMsgEventArgs e, string? info)
        {
            DataRow dr = Table.NewRow();
            dr["DateTime"] = DateTime.Now;
            dr["MessageType"] = e.MessageType;
            dr["EventType"] = e.EventType;
            dr["SourceAddress"] = e.SourceAddress;
            dr["DestinationAddress"] = e.DestinationAddress;
            dr["MessagePriority"] = e.MessagePriority;
            if (e.Value is not null) dr["Value"] = e.Value;
            dr["HopCount"] = e.HopCount;
            dr["IsSecure"] = e.IsSecure;
            dr["Info"] = info;
            Table.Rows.Add(dr);
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        public void Clear()
        {
            Table.Rows.Clear();
        }

    }

}
