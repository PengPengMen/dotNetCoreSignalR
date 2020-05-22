using System;
using System.Collections.Generic;
using System.Text;

namespace cityfun.Redis.Redis
{
    /// <summary>
    /// Redis 接口
    /// </summary>
    public interface IRedisService
    {
        /// <summary>
        /// Redis get命令
        /// </summary>
        /// <param name="key"> 键</param>
        /// <returns></returns>
        dynamic GetRedisValue(string key);

        /// <summary>
        /// Redis set 命令
        /// </summary>
        /// <param name="key"> 键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SetRedisValue(dynamic key, dynamic value);

        /// <summary>
        /// Redis set 命令 设置过期时间 ttl 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        bool SetRedisValue(dynamic key, dynamic value, TimeSpan time);

        /// <summary>
        /// Redis 获取字符串类型的key
        /// </summary>
        /// <param name="key"> 字符串类型的key</param>
        /// <returns></returns>
        dynamic GetRedisStringValue(string key);

        /// <summary>
        /// Redis 设置字符串类型的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        bool SetRedisStringValue(string key, string value, TimeSpan time);

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool RemoveRedisValue(string key);

        /// <summary>
        /// 存储字符串key-value到指定的redis数据库中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dbNumber"></param>
        /// <returns></returns>
        bool SetRedisStringValueToSelectDb(string key, string value, int dbNumber = -1);

        /// <summary>
        /// 存储字符串key-value到指定的redis数据库中
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="dbNumber">数据库编号</param>
        /// <param name="time">过期时间</param>
        /// <returns></returns>
        bool SetRedisStringValueTimeSpanToSelectDb(string key, string value, TimeSpan time, int dbNumber = -1);

        /// <summary>
        /// 指定数据库中获取指定key的value
        /// </summary>
        /// <param name="key">字符串类型键</param>
        /// <param name="dbNumber">数据库编码 默认-1</param>
        /// <returns></returns>
        dynamic GetRedisValueSelectDb(string key,int dbNumber = -1);

        /// <summary>
        /// 指定数据库中删除指定key的value
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="dbNumber">数据库编码 默认-1</param>
        /// <returns></returns>
        bool DeleteRedisValueSelectDb(string key, int dbNumber = -1);

        /// <summary>
        /// 指定Redis数据库修改key的value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dbNumber"></param>
        /// <returns></returns>
        bool EditRedisStringValueSelectDb(string key, string value, int dbNumber = -1);
    }
}
