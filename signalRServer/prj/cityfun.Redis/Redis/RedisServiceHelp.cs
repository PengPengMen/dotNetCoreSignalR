#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：cityfun.Redis.Redis
* 项目描述 ：
* 类 名 称 ：RedisServiceHelp
* 类 描 述 ：
* 所在的域 ：CF-415PZN2
* 命名空间 ：cityfun.Redis.Redis
* 机器名称 ：CF-415PZN2 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：门鹏鹏
* 创建时间 ：2020/5/21 10:43:13
* 更新时间 ：2020/5/21 10:43:13
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 门鹏鹏 2020. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace cityfun.Redis.Redis
{
    public class RedisServiceHelp : IRedisService
    {
        private string  _redisConnection {get;set;}

        public RedisServiceHelp()
        {
            _redisConnection = "192.168.2.37:6379,password=,allowadmin=true,ConnectTimeout=65535,syncTimeout=65535";
        }
        public dynamic GetRedisStringValue(string key)
        {
            dynamic ret = null;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase();
                    var newobj = db.StringGet(key, CommandFlags.None);
                    ret = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(newobj);
                    
                }

            }
            catch (Exception ex)
            {
                ret = null;
            }
            return ret;
        }

        public dynamic GetRedisValue(string key)
        {
            dynamic ret = null;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase();
                    var newobj = db.StringGet(key, CommandFlags.None);
                    ret = (dynamic)(newobj);
                }

            }
            catch (Exception ex)
            {
                ret = null;
            }
            return ret;
        }

        public bool RemoveRedisValue(string key)
        {
            var ret = false;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase();
                    ret = db.KeyDelete(key);
                }

            }
            catch (Exception ex)
            {

            }
            return ret;
        }

        public bool SetRedisStringValue(string key, string value, TimeSpan time)
        {
            var ret = false;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase();
                    ret = db.StringSet(key, value, time);
                }

            }
            catch (Exception ex)
            {

            }
            return ret;
        }

        public bool SetRedisValue(dynamic key, dynamic value)
        {
            var ret = false;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase();
                    ret = db.StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
                }

            }
            catch (Exception ex)
            {

            }
            return ret;
        }

        public bool SetRedisValue(dynamic key, dynamic value, TimeSpan time)
        {
            var ret = false;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase();
                    ret = db.StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(value), time);
                }

            }
            catch (Exception ex)
            {

            }
            return ret;
        }

        
        public bool SetRedisStringValueToSelectDb(string key, string value, int dbNumber = -1)
        {
            var ret = false;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase(dbNumber);
                    ret = db.StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ret;
        }

        public bool SetRedisStringValueTimeSpanToSelectDb(string key, string value, TimeSpan time, int dbNumber = -1)
        {
            var ret = false;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase(dbNumber);
                    ret = db.StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(value), time);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ret;
        }

        public dynamic GetRedisValueSelectDb(string key, int dbNumber = -1)
        {
            dynamic ret = null;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase(dbNumber);
                    var newobj = db.StringGet(key, CommandFlags.None);
                    ret = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(newobj);
                }
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;
        }

        public bool DeleteRedisValueSelectDb(string key, int dbNumber = -1)
        {
            var ret = false;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase(dbNumber);
                    ret = db.KeyDelete(key);
                }

            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        public bool EditRedisStringValueSelectDb(string key,string value, int dbNumber = -1)
        {
            var ret = false;
            try
            {
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_redisConnection))
                {
                    IDatabase db = redis.GetDatabase(dbNumber);
                    ret = db.StringSet(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ret;
        }
    }
}
