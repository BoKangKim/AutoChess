using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ExcelScriptable
{
    internal class ReadFile
    {
        private string path = null;
        private StreamReader sr = null;
        public List<string> read = null;

        public ReadFile(string path)
        {
            read = new List<string>();
            this.path = path;
            FileInfo excel = new FileInfo(path);
            if (excel.Exists == false)
            {
                Console.WriteLine(path + " is not exists");
                return;
            }
            else
            {
                sr = new StreamReader(path);
                Console.WriteLine("Checked Path : " + path + " is exists...");
            }
           
        }

        public bool Run()
        {
            string temp = null;
            try
            {
                while ((temp = sr.ReadLine()) != null)
                {
                     read.Add(temp);
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                sr.Close();
                return false;
            }


            sr.Close();
            return true;
        }

        

    }
}
