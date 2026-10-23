using LiteDB;
using System.Net;

namespace BedivereKnx.Database
{

    internal static class BsonMappers
    {

        public static void Initialize()
        {
            BsonMapper.Global.RegisterType<IPAddress>(
                ip => new BsonValue(ip.ToString()),
                bson => IPAddress.Parse(bson.AsString)
            );
        }

    }
}
