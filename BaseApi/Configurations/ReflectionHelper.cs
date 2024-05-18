using System.Reflection;

namespace BaseApi.Configurations
{
    public class ReflectionHelper
    {
        public static void PrintPropertyNames<T>()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (var property in properties)
            {
                Console.WriteLine(property.Name);
            }
        }
    }
}
