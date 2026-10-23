using LiteDB;
using System.Linq.Expressions;

namespace BedivereKnx
{

    public static class LiteDb
    {
        /// <summary>
        /// 当前数据库
        /// </summary>
        private static LiteDatabase? currentDb;

        /// <summary>
        /// 当前数据库的文件路径
        /// </summary>
        private static string? currentFilePath;

        /// <summary>
        /// 用于线程安全的锁对象
        /// </summary>
        private static readonly Lock lockObj = new();

        /// <summary>
        /// 创建或切换数据库
        /// </summary>
        /// <param name="filePath"></param>
        public static void SwitchDb(string filePath)
        {
            if (!File.Exists(filePath)) // 无效路径的情况
            {
                throw new Exception(string.Format(ResString.ExMsg_DbNotFound, filePath));
            }
            string fullPath = Path.GetFullPath(filePath); // 完整路径

            // 路径不变的情况下，不进行任何操作：
            if ((currentDb != null) && (currentFilePath == fullPath)) return;

            lock (lockObj)
            {
                // 关闭并释放旧的数据库连接：
                if (currentDb != null)
                {
                    try
                    {
                        currentDb.Dispose(); // 关闭数据库连接
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ResString.ExMsg_DbCloseFailed, ex);
                    }
                    currentDb = null;
                }

                // 创建新的数据库连接：
                var connectionString = new ConnectionString
                {
                    Filename = fullPath,
                    Connection = ConnectionType.Shared // Share模式适合多进程访问
                };

                currentDb = new LiteDatabase(connectionString);
                currentFilePath = fullPath;
            }
        }

        /// <summary>
        /// 确保数据库是否初始化
        /// </summary>
        private static void EnsureInit()
        {
            if (currentDb == null)
            {
                throw new InvalidOperationException(ResString.ExMsg_DbNotInit);
                // 也可以直接用默认参数初始化
            }
        }

        /// <summary>
        /// 获取指定类型的集合引用
        /// </summary>
        private static ILiteCollection<T> GetCollection<T>()
        {
            EnsureInit();
            return currentDb!.GetCollection<T>();
        }

        #region 查询

        /// <summary>
        /// 根据ID查询单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="id">ID</param>
        /// <returns>实体对象</returns>
        public static T FindById<T>(ObjectId id)
        {
            var collection = GetCollection<T>();
            return collection.FindById(id);
        }

        /// <summary>
        /// 根据条件查询单个实体（LINQ表达式）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">LINQ条件表达式</param>
        /// <returns>符合条件的实体对象</returns>
        public static T FindOne<T>(Expression<Func<T, bool>> predicate)
        {
            var collection = GetCollection<T>();
            return collection.FindOne(predicate);
        }

        /// <summary>
        /// 根据条件查询对象列表（LINQ表达式）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">LINQ条件表达式</param>
        /// <returns>符合条件的实体列表</returns>
        public static List<T> FindList<T>(Expression<Func<T, bool>> predicate)
        {
            var collection = GetCollection<T>();
            return collection.Find(predicate).ToList();
        }

        /// <summary>
        /// 根据条件查询对象列表（SQL语句）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <returns>符合条件的实体列表</returns>
        //public static List<T> FindList<T>(string sql)
        //{
        //    // 注意：ExecuteReader 返回的是 BsonDocument，需要手动映射或反序列化
        //    // 这里为了通用性，使用简单的 SQL 执行并反序列化
        //    var docs = db.Execute(sql, args).ToEnumerable().ToList();

        //    var mapper = BsonMapper.Global;
        //    var result = new List<T>();
        //    foreach (var doc in docs)
        //    {
        //        // 如果查询结果包含多个集合的数据，可能需要更复杂的逻辑，这里假设返回结构匹配 T
        //        result.Add(mapper.Deserialize<T>(doc));
        //    }
        //    return result;
        //}

        /// <summary>
        /// 查询所有实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>实体列表</returns>
        public static List<T> FindAll<T>()
        {
            var collection = GetCollection<T>();
            return collection.FindAll().ToList();
        }

        #endregion

        #region 插入

        /// <summary>
        /// 插入单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>插入后的ID</returns>
        public static ObjectId Insert<T>(T entity)
        {
            var collection = GetCollection<T>();
            return collection.Insert(entity);
        }

        /// <summary>
        /// 插入多个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体对象列表</param>
        /// <param name="batchSize">每批插入的数量（默认5000）</param>
        /// <returns>成功插入的数量</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int InsertBulk<T>(IEnumerable<T> entities, int batchSize = 5000)
        {
            ArgumentNullException.ThrowIfNull(entities); // 列表为空的情况

            var collection = GetCollection<T>();
            // LiteDB的InsertBulk接受IEnumerable和可选的batchSize
            return collection.InsertBulk(entities, batchSize);
        }

        #endregion

        #region 更新

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>是否更新成功</returns>
        public static bool Update<T>(T entity)
        {
            var collection = GetCollection<T>();
            return collection.Update(entity);
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        /// <returns>更新成功的数量</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int UpdateMany<T>(IEnumerable<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities); // 列表为空的情况

            var collection = GetCollection<T>();
            //int count = 0; // 更新成功计数
            //foreach (var entity in entities)
            //{
            //    if (collection.Update(entity)) count++; // 更新成功时计数+1
            //}
            //return count;
            return collection.Update(entities);
        }

        ///// <summary>
        ///// 部分更新 (Patch) - 仅更新指定字段
        ///// </summary>
        ///// <typeparam name="T">实体类型</typeparam>
        ///// <param name="id">主键ID</param>
        ///// <param name="updateAction">更新动作，例如: x => x.Name = "NewName"</param>
        ///// <param name="collectionName">集合名称</param>
        //public static void UpdateField<T>(object id, Action<T> updateAction, string collectionName) where T : new()
        //{
        //    using (var db = GetDatabase())
        //    {
        //        var col = GetCollection<T>(db, collectionName);
        //        var entity = col.FindById(new BsonValue(id));
        //        if (entity != null)
        //        {
        //            updateAction(entity);
        //            col.Update(entity);
        //        }
        //    }
        //}

        #endregion

        #region 删除

        /// <summary>
        /// 根据ID删除单个实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="id">要删除的实体ID</param>
        /// <returns></returns>
        public static bool DeleteById<T>(ObjectId id)
        {
            var collection = GetCollection<T>();
            return collection.Delete(id);
        }

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="predicate">LINQ条件表达式</param>
        /// <returns>成功删除的数量</returns>
        public static int DeleteMany<T>(Expression<Func<T, bool>> predicate)
        {
            var collection = GetCollection<T>();
            // 注意：LiteDB 的 DeleteMany 通常接受 BsonExpression 或 Predicate
            // 这里使用更通用的 BsonExpression 方式可能性能更好，但 Predicate 更易用
            return collection.DeleteMany(predicate);
        }

        /// <summary>
        /// 删除全部实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>成功删除的数量</returns>
        public static int DeleteAll<T>()
        {
            var collection = GetCollection<T>();
            return collection.DeleteAll();
        }

        #endregion

        #region 文件

        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="fileId">文件唯一标识ID</param>
        /// <param name="filePath">本地文件路径</param>
        public static void UploadFile(string fileId, string filePath)
        {
            using var stream = File.OpenRead(filePath);
            currentDb.FileStorage.Upload(fileId, Path.GetFileName(filePath), stream);
        }

        /// <summary>
        /// 下载单个文件
        /// </summary>
        /// <param name="fileId">文件唯一标识ID</param>
        /// <param name="savePath">保存的本地路径</param>
        public static void DownloadFile(string fileId, string savePath)
        {
            using var stream = File.Create(savePath);
            currentDb.FileStorage.Download(fileId, stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public static bool FileExists(string fileId)
        {
            return currentDb.FileStorage.Exists(fileId);
        }

        #endregion

        /// <summary>
        /// 显式关闭当前数据库连接
        /// </summary>
        public static void Close()
        {
            lock (lockObj)
            {
                if (currentDb != null)
                {
                    currentDb.Dispose();
                    currentDb = null;
                    currentFilePath = null;
                }
            }
        }

    }

}

//#region 文件存取 (File Storage)

///// <summary>
///// 上传单个文件 (从文件路径)
///// </summary>
///// <param name="fileId">文件唯一标识ID</param>
///// <param name="filePath">本地文件路径</param>
///// <param name="fileName">存储在数据库中的文件名(可选)</param>
//public static void UploadFile(string fileId, string filePath, string fileName = null)
//{
//    if (!File.Exists(filePath))
//        throw new FileNotFoundException("文件不存在", filePath);

//    using (var db = GetDatabase())
//    {
//        var storage = GetFileStorage(db);
//        using (var stream = File.OpenRead(filePath))
//        {
//            // 如果未提供文件名，使用原始文件名
//            string finalFileName = fileName ?? Path.GetFileName(filePath);
//            storage.Upload(fileId, finalFileName, stream);
//        }
//    }
//}

///// <summary>
///// 上传单个文件 (从字节数组)
///// </summary>
///// <param name="fileId">文件唯一标识ID</param>
///// <param name="data">文件字节数据</param>
///// <param name="fileName">文件名</param>
//public static void UploadFileBytes(string fileId, byte[] data, string fileName)
//{
//    using (var db = GetDatabase())
//    {
//        var storage = GetFileStorage(db);
//        using (var stream = new MemoryStream(data))
//        {
//            storage.Upload(fileId, fileName, stream);
//        }
//    }
//}

///// <summary>
///// 批量上传文件
///// </summary>
///// <param name="files">Key为FileId, Value为本地文件路径的字典</param>
//public static void UploadManyFiles(Dictionary<string, string> files)
//{
//    if (files == null || files.Count == 0) return;

//    using (var db = GetDatabase())
//    {
//        var storage = GetFileStorage(db);
//        foreach (var kvp in files)
//        {
//            if (File.Exists(kvp.Value))
//            {
//                using (var stream = File.OpenRead(kvp.Value))
//                {
//                    storage.Upload(kvp.Key, Path.GetFileName(kvp.Value), stream);
//                }
//            }
//        }
//    }
//}

///// <summary>
///// 下载单个文件到本地路径
///// </summary>
///// <param name="fileId">文件ID</param>
///// <param name="savePath">保存路径</param>
//public static void DownloadFile(string fileId, string savePath)
//{
//    using (var db = GetDatabase())
//    {
//        var storage = GetFileStorage(db);
//        // 确保目录存在
//        var dir = Path.GetDirectoryName(savePath);
//        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
//        {
//            Directory.CreateDirectory(dir);
//        }

//        storage.Download(fileId, savePath);
//    }
//}

///// <summary>
///// 获取文件字节数组
///// </summary>
///// <param name="fileId">文件ID</param>
///// <returns>文件字节数组</returns>
//public static byte[] GetFileBytes(string fileId)
//{
//    using (var db = GetDatabase())
//    {
//        var storage = GetFileStorage(db);
//        var fileInfo = storage.FindById(fileId);
//        if (fileInfo == null) return null;

//        using (var stream = new MemoryStream())
//        {
//            storage.Download(fileId, stream);
//            return stream.ToArray();
//        }
//    }
//}

///// <summary>
///// 删除单个文件
///// </summary>
///// <param name="fileId">文件ID</param>
///// <returns>是否删除成功</returns>
//public static bool DeleteFile(string fileId)
//{
//    using (var db = GetDatabase())
//    {
//        var storage = GetFileStorage(db);
//        return storage.Delete(fileId);
//    }
//}

///// <summary>
///// 批量删除文件
///// </summary>
///// <param name="fileIds">文件ID列表</param>
///// <returns>删除成功的数量</returns>
//public static int DeleteManyFiles(IEnumerable<string> fileIds)
//{
//    if (fileIds == null) return 0;

//    int count = 0;
//    using (var db = GetDatabase())
//    {
//        var storage = GetFileStorage(db);
//        foreach (var id in fileIds)
//        {
//            if (storage.Delete(id))
//                count++;
//        }
//    }
//    return count;
//}

///// <summary>
///// 获取文件信息
///// </summary>
///// <param name="fileId">文件ID</param>
///// <returns>LiteFileInfo</returns>
//public static LiteFileInfo<string> GetFileInfo(string fileId)
//{
//    using (var db = GetDatabase())
//    {
//        var storage = GetFileStorage(db);
//        return storage.FindById(fileId);
//    }
//}

//        #endregion
