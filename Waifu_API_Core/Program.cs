using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using WaifuSharp;
using Waifu_API;
using System.Reflection;
using System.Web;

namespace Waifu_API_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            WaifuClient client = new WaifuClient();

            //Type myType = endpoints.GetType();
            //IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            //
            //foreach (PropertyInfo prop in props)
            //{
            //    object propValue = prop.GetValue(endpoints, null);
            //
            //    Console.WriteLine(propValue);
            //}


            var listOfFieldNames = typeof(Endpoints).GetMembers().ToList();

            var listTypeNames = new List<string>();

            for (int i = 5; i < 7; i++)
                listTypeNames.Add(listOfFieldNames[i].Name);

            foreach(var t in listTypeNames)
                Console.WriteLine(t);

            string type = Console.ReadLine();

            List<string> listCategoryNames = new List<string>();


            foreach (var t in listTypeNames)
                if (t == type)
                {

                    MethodInfo mi = typeof(Endpoints).GetType().GetMethod(type);

                    Array enumValueArray = Enum.GetValues(typeof(Endpoints.Sfw));
                    foreach (int enumValue in enumValueArray)
                    {
                        listCategoryNames.Add(Enum.GetName(typeof(Endpoints.Sfw), enumValue));
                    }
                    break;
                }
            foreach (var c in listCategoryNames)
                Console.Write($"{c} ");

            var category = Console.ReadLine();

            Console.WriteLine(client.GetSfwImage(Endpoints.Sfw.Waifu));
            Console.ReadKey();
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
