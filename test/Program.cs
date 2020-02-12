using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace test
{
    class Program
    {
        static string[] readfileString(string path)
        {
            StreamReader sr = new StreamReader(path);
            string line;
            List<string> file = new List<string>();

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                file.Add(line);
            }
            sr.Close();
            string[] mas = file.ToArray();
            return mas;
        }
        static int[] readfileINT(string path)
        {

            StreamReader sr = new StreamReader(path);
            string line;
            List<int> file = new List<int>();

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                Int32.TryParse(line, out int res);
                file.Add(res);
            }
            sr.Close();
            int[] mas = file.ToArray();
            return mas;
        }
        static string[] mergeString(string path1, string path2, bool sort)//sort == true сортировка по возрастанию, false-наоборот
        {
            string[] m1 = readfileString(path1);
            string[] m2 = readfileString(path2);
            string[] m3 = new string[m1.Length + m2.Length];

            if (sort == false)
            {
                Array.Reverse(m1);
                Array.Reverse(m2);
            }

            int i = 0, j = 0, k = 0, err = 0;

            while (i < m1.Length && j < m2.Length)
            {
                string x = m1[i];
                string y = m2[j];

                int result = String.Compare(x, y);

                if (result > 0)
                {
                    m3[k] = y;
                    j++;
                }
                else if (result < 0)
                {
                    m3[k] = x;
                    i++;
                }
                k++;
            }
            if (j <= m2.Length - 1)
            {
                Array.Copy(m2, j, m3, k, m3.Length - k);
            }
            else if (i <= m1.Length - 1) Array.Copy(m1, i, m3, k, m3.Length - k);

            if (sort == false)
            {
                string[] m4 = new string[m1.Length + m2.Length - err];
                Array.Copy(m3, 0, m4, 0, m1.Length + m2.Length - err);
                Array.Reverse(m4);
                return m4;
            }
            else return m3;
        }
        static int[] mergeINT(string path1, string path2, bool sort)
        {
            int[] m1 = readfileINT(path1);
            int[] m2 = readfileINT(path2);
            int[] m3 = new int[m1.Length + m2.Length];

            if (sort == false)
            {
                Array.Reverse(m1);
                Array.Reverse(m2);
            }

            int i = 0, j = 0, k = 0, err = 0;
            while (i < m1.Length && j < m2.Length)
            {
                int x = m1[i];
                int y = m2[j];

                if (x >= y)
                {
                    m3[k] = y;
                    j++;
                }
                else
                {
                    m3[k] = x;
                    i++;
                }
                k++;

            }
            if (j <= m2.Length - 1)
            {
                Array.Copy(m2, j, m3, k, m3.Length - k);
            }
            else if (i <= m1.Length - 1) Array.Copy(m1, i, m3, k, m3.Length - k);

            if (sort == false)
            {
                int[] m4 = new int[m1.Length + m2.Length - err];
                Array.Copy(m3, 0, m4, 0, m1.Length + m2.Length - err);
                Array.Reverse(m4);
                return m4;
            }
            else return m3;
        }
        static void Main(string[] args)
        {
            string path = @"D:\цфт\test\";//необходимо указать путь с входными файлами 

            try
            {
                if ((args[0] == "-a" && args[1] == "-i") || (args[0] == "-i" && args[1] == "-a"))
                {
                    using (StreamWriter w = new StreamWriter(path + args[2], false, Encoding.Default))

                        foreach (int a in mergeINT(path + args[3], path + args[4], true))
                        {
                            w.WriteLine(a);
                            Console.WriteLine(a + "");
                        }
                }
                else if ((args[0] == "-a" && args[1] == "-s") || (args[0] == "-s" && args[1] == "-a"))
                {
                    using (StreamWriter w = new StreamWriter(path + args[2], false, Encoding.Default))

                        foreach (string a in mergeString(path + args[3], path + args[4], true))
                        {
                            w.WriteLine(a);
                            //Console.WriteLine(a + "");
                        }
                }

                else if ((args[0] == "-d" && args[1] == "-i") || (args[0] == "-i" && args[1] == "-d"))
                {
                    using (StreamWriter w = new StreamWriter(args[2], false, Encoding.Default))

                        foreach (int a in mergeINT(args[3], args[4], false))
                        {
                            w.WriteLine(a);
                            //  Console.WriteLine(a + "");
                        }

                }
                else if ((args[0] == "-d" && args[1] == "-s") || (args[0] == "-s" && args[1] == "-d"))
                {
                    using (StreamWriter w = new StreamWriter(args[2], false, Encoding.Default))

                        foreach (string a in mergeString(args[3], args[4], false))
                        {
                            w.WriteLine(a);
                            //Console.WriteLine(a + "");
                        }

                }
                else
                {
                    throw new Exception("Неверно указаны аргументы консоли");
                }


            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Отсутсвует один или несколько файлов");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Нажмите любую клавишу, чтобы завершить работу программы");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}

