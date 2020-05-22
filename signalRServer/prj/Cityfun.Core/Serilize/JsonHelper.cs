#region << 版 本 注 释 >>
/*----------------------------------------------------------------
* 项目名称 ：Cityfun.Core.Serilize
* 项目描述 ：
* 类 名 称 ：JsonHelper
* 类 描 述 ：
* 所在的域 ：CF-415PZN2
* 命名空间 ：Cityfun.Core.Serilize
* 机器名称 ：CF-415PZN2 
* CLR 版本 ：4.0.30319.42000
* 作    者 ：门鹏鹏
* 创建时间 ：2020/5/21 10:53:16
* 更新时间 ：2020/5/21 10:53:16
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ 门鹏鹏 2020. All rights reserved.
*******************************************************************
//----------------------------------------------------------------*/
#endregion
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using NJJ = Newtonsoft.Json.JsonConvert;

namespace Cityfun.Core.Serilize
{
    /// <summary>
    /// Newtonsoft.Json拓展类
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 转为JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string AsJson(this object obj)
        {
            string result = string.Empty;
            try
            {
                result = NJJ.SerializeObject(obj);
            }
            catch
            {

            }

            return result;
        }

        /// <summary>
        /// 转为实体类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T To<T>(this string json) where T : class
        {
            T t = default(T);
            try
            {
                t = NJJ.DeserializeObject<T>(json);
            }
            catch
            {

            }

            return t;
        }

        ///<summary>
        /// 实现序列化进行驼峰命名（第一个首字母小写)
        /// </summary>
        /// 
        public static dynamic CamelCasePropertyNames(this object obj)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                // 设置为驼峰命名
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var userStr = JsonConvert.SerializeObject(obj, serializerSettings);
            var data = JsonConvert.DeserializeObject(userStr);
            return data;
        }
    }
}
