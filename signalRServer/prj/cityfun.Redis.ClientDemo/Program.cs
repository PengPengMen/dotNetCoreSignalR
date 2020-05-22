using cityfun.Redis.Redis;
using System;

namespace cityfun.Redis.ClientDemo
{
    public class Program
    {
        static RedisServiceHelp redisService = new RedisServiceHelp();
        static bool redisOperRst = false;
        public static void Main(string[] args)
        {
            Console.WriteLine("请选择要使用的Redis数据库编号(-1~14):");
            System.Console.WriteLine(" 按ESC 退出");
            int dbNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("--------------操作的Redis数据库编号{0}--------", dbNumber);
            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.WriteLine("请选择操作：1.新增 2.查询 3.编辑 4.删除");
                    int operNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("请选择输入操作的key名称:");
                    string key = Console.ReadLine();
                    switch (operNumber)
                    {
                        case 1:
                            Console.WriteLine("请选择输入新增key值:");
                            string addvalue = Console.ReadLine();
                            redisOperRst = redisService.SetRedisStringValueToSelectDb(key, addvalue, dbNumber);
                            if (redisOperRst)
                            {
                                string currentValue = redisService.GetRedisValueSelectDb(key, dbNumber);
                                Console.WriteLine("当前key:{0} value新增成功，当前value:{1}", key, currentValue);
                            }
                            else
                            {
                                Console.WriteLine("当前key:{0} value新增失败");
                            }
                            break;
                        case 2:
                            string queryValue = redisService.GetRedisValueSelectDb(key, dbNumber);
                            Console.WriteLine("当前key:{0} 当前value:{1}", key, queryValue);
                            break;
                        case 3:
                            Console.WriteLine("请选择输入要修改key值:");
                            string newValue = Console.ReadLine();
                            redisOperRst = redisService.EditRedisStringValueSelectDb(key, newValue, dbNumber);
                            if (redisOperRst)
                            {
                                string currentValue = redisService.GetRedisValueSelectDb(key, dbNumber);
                                Console.WriteLine("当前key:{0} value修改成功，当前value:{1}", key, currentValue);
                            }
                            else
                            {
                                Console.WriteLine("当前key:{0} value修改失败");
                            }
                            break;
                        case 4:
                            redisOperRst = redisService.DeleteRedisValueSelectDb(key, dbNumber);
                            if (redisOperRst)
                            {
                                Console.WriteLine("当前key:{0} 删除成功", key);
                            }
                            else
                            {
                                Console.WriteLine("当前key:{0} 删除失败");
                            }
                            break;
                        default:
                            break;
                    }
                }
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);
            Console.WriteLine("Hello World!");
        }
    }
}