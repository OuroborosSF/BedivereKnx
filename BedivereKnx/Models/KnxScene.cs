using System.ComponentModel;
using Knx.Falcon;

namespace BedivereKnx.Models
{

    /// <summary>
    /// KNX场景组
    /// </summary>
    public class KnxScene : KnxObject
    {

        ///// <summary>
        ///// 场景组地址
        ///// </summary>
        //public required GroupAddress SceneAddress
        //{
        //    get => this[KnxObjectPart.SceneControl].Address;
        //    set => this[KnxObjectPart.SceneControl] = new(value, [18,1]);
        //}

        /// <summary>
        /// 场景地址名称
        /// </summary>
        [Browsable(false)]
        public string[] Names { get; set; } = new string[64];

        /// <summary>
        /// 场景数量
        /// </summary>
        [Browsable(false)]
        public int SceneCount { get; private set; }

        /// <summary>
        /// 新建KNX场景对象
        /// </summary>
        /// <param name="id">对象ID</param>
        /// <param name="code">对象编号</param>
        /// <param name="name">对象名称</param>
        /// <param name="ifCode">接口编号</param>
        /// <param name="areaCode">区域编号</param>
        public KnxScene(int id, string code, string? name, string? ifCode, string? areaCode, string sceneValues)
            : base(KnxObjectType.Scene, id, code, name, ifCode, areaCode)
        {
            Names = new string[64]; //重置场景名数组
            string[] valuePairs = Convertor.ToArray(sceneValues, ','); //场景值数组
            SceneCount = valuePairs.Length;
            foreach (string pairText in valuePairs) //遍历每个场景值
            {
                if (string.IsNullOrWhiteSpace(pairText)) continue; //跳过空项
                string[] pair = Convertor.ToArray(pairText, '='); //{场景地址, 场景名}
                if (byte.TryParse(pair[0], out byte num)) //场景编号是数字
                {
                    if (num < 64) //0~63的情况
                    {
                        Names[num] = pair[1].Trim(); //设置对应场景号的名称
                    }
                    else //场景号超过大于的情况
                    {
                        throw new ArgumentOutOfRangeException(string.Format(ResString.ExMsg_KnxSceneNumberInvalid, num));
                    }
                }
                else //场景号不是数字
                {
                    throw new FormatException(string.Format(ResString.ExMsg_KnxSceneNumberInvalid, pair[0]));
                }
            }
        }

        ///// <summary>
        ///// 新建KNX场景组对象
        ///// </summary>
        ///// <param name="id">场景组ID</param>
        ///// <param name="code">场景组编号</param>
        ///// <param name="name">场景组名称</param>
        ///// <param name="address">场景组地址</param>
        ///// <param name="ifCode">接口编号</param>
        //public KnxScene(int id, string code, string? name, GroupAddress address, string? ifCode, string? areaCode)
        //    : base(KnxObjectType.Scene, id, code, name, ifCode, areaCode)
        //{
        //    this[KnxObjectPart.SceneControl] = new KnxGroup(address, 18, 1); //新建DPST18.001的KNX组对象
        //    Names = new string[64]; //重置场景名数组
        //}

        /// <summary>
        /// 场景控制
        /// </summary>
        /// <param name="sceneNumber">场景编号（0~63）</param>
        /// <param name="isLearn">是否学习场景</param>
        /// <param name="priority">优先级</param>
        public void SceneControl(byte sceneNumber, bool isLearn = false, MessagePriority priority = MessagePriority.Low)
        {
            if (sceneNumber <= 63)
            {
                if (isLearn) sceneNumber += 128; //场景学习的值=控制值+128
                WriteValue(KnxObjectPart.SceneControl, sceneNumber, priority);
            }
            else //场景编号大于63报错
            {
                throw new ArgumentOutOfRangeException(string.Format(ResString.ExMsg_KnxSceneNumberInvalid, sceneNumber));
            }
        }

    }

}
