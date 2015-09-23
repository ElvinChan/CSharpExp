using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ObjectStore
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<Product> products = new List<Product>();
                products.Add(new Product(1, "Spiky Pung", 1000.0, "Good stuff."));
                products.Add(new Product(2, "Gloop Galloop Soup", 25.0, "Tasty."));
                products.Add(new Product(4, "Hat Sauce", 12.0, "One for the kids."));
                Console.WriteLine("Products to save:");
                foreach (Product product in products)
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine();

                //创建序列化器
                IFormatter serializer = new BinaryFormatter();

                //创建写入流
                FileStream saveFile = new FileStream("Products.bin", FileMode.Create, FileAccess.Write);

                //将Product对象序列化保存到saveFile流中
                serializer.Serialize(saveFile, products);
                saveFile.Close();

                //反序列化，读取文件流
                FileStream loadFile = new FileStream("Products.bin", FileMode.Open, FileAccess.Read);
                List<Product> savedProducts = serializer.Deserialize(loadFile) as List<Product>;
                loadFile.Close();

                Console.WriteLine("Products loaded:");
                foreach (Product product in savedProducts)
                {
                    Console.WriteLine(product);
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine("A serialization exception has been thrown!");
                Console.WriteLine(e.Message);
            }
            catch(IOException e)
            {
                Console.WriteLine("An IO exception has been thrown!");
                Console.WriteLine(e.Message);
            }
 
            Console.ReadKey();
        }
    }
}
