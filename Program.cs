using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace RedisCache
{
    class Program
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = "<redisCacheConnectionString>";
            return ConnectionMultiplexer.Connect(cacheConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get {
                return lazyConnection.Value;
            }
        }
        static void Main(string[] args)
        {


            IDatabase cache = Connection.GetDatabase();

            cache.StringSet("Message", "This is a message from redis cacahe");
            Console.WriteLine(cache.StringGet("Message"));


            Customer obj = new Customer()
            {
                Name = "Ashok Kumar",
                Age = 18

            };

            cache.StringSet("Ashok", JsonConvert.SerializeObject(obj));
            Console.WriteLine(JsonConvert.DeserializeObject(cache.StringGet("Ashok")));

            Console.ReadLine();
        }
    }

    public class Customer
    {
        public string  Name { get; set; }
        public int  Age { get; set; }
    }
}
