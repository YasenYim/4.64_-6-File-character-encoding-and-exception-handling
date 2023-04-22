using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace _4_6文件_字符编码与异常处理
{
    class Program
    {
        // 异常处理：
        // 1、把有可能出现异常的代码，用try块包裹
        // 2、catch捕获异常。 catch（异常类型）。异常是按照继承规则，从特殊到一般逐级捕获
        // 3、把必须要执行的代码（关闭，清理，善后工作），放到finally中

      
        static void WriteFile()
        {
            FileStream file = null;
            try
            {
                file = new FileStream("I:\\a.txt", FileMode.Create);

                string s = "dfdgasg\n34";

                // 字符串s转byte[]
                byte[] bytes = Encoding.UTF8.GetBytes(s);

                // 写入byte[]到文件
                file.Write(bytes, 0, bytes.Length);
            }
            catch(UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("基类异常" + e.Message);
            }
            finally
            {
                // 关闭文件
                if(file != null)
                {
                    file.Close();
                    file = null;
                }
            }
        }
        static void ReadFile()
        {
            // 另一种：使用using简化文件读写的操作
            using (FileStream file = new FileStream("a.txt", FileMode.Open))
            {
                byte[] bytes = new byte[10240]; 

                // 这里的大小是我们人为设定的，大小不能超过10k
                // realReadLen是实际读取的字节数
                int realReadLen = file.Read(bytes, 0, 10240);    // file.Read(字节数组，开始下标，最多读取的字节数）

                Console.WriteLine(bytes);

                string s = Encoding.UTF8.GetString(bytes, 0, realReadLen);

                Console.WriteLine("s:" + s);
            }
            // using 会自动释放file变量
     
        } 
        static void WriteFileBin()
        {
            using(FileStream file = new FileStream("b",FileMode.Create))
            {
                // 创建二进制写入器，绑定于file上
                BinaryWriter writer = new BinaryWriter(file);
                // 借助二进制写入器进行写入
                writer.Write(123);
                writer.Write("你好");
                writer.Write(false);
                writer.Write(3.14f);
            }
        }
        static void ReadFileBin()
        {
            using (FileStream file = new FileStream("b", FileMode.Open))
            {
                // 创建二进制读取器
                BinaryReader reader = new BinaryReader(file);

                int a = reader.ReadInt32();
                string b = reader.ReadString();
                bool c = reader.ReadBoolean();
                float d = reader.ReadSingle();

                Console.WriteLine($" {a} {b} {c} {d}");
            }
        }
        static void Main(string[] args)
        {
            WriteFile();
            ReadFile();

            WriteFileBin();
            ReadFileBin();

            Console.ReadLine();
        }
    }
}
