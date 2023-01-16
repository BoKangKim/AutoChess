using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelScriptable
{
    internal class FileManageMent
    {
        private ReadFile rf = null;
        private WriteFile wf = null;
        private string writePath = null;
        private string GUID = null;
        private string fileName = null;

        public FileManageMent(string readPath,string writePath,string GUID,string fileName)
        {
            rf  = new ReadFile(readPath);
            this.writePath = writePath;
            this.GUID = GUID;
            this.fileName = fileName;
        }

        public void Run()
        {
            if(rf.Run() == true)
            {
                Console.WriteLine("Excel File Read And Copy Complete...");
                wf = new WriteFile(rf.read,writePath,GUID,fileName);
                if(wf.Run() == true)
                {
                    Console.WriteLine("Complete...");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Excel File Read And Copy Failed...");
                return;
            }
        }
    }
}
