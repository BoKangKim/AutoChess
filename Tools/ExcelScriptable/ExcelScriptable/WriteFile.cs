using System;
using System.Collections.Generic;
using System.IO;

namespace ExcelScriptable
{
    internal class WriteFile
    {
        private string path = null;
        private string[] subject = null;
        private string GUID = null;
        private string[] name = null;
        private List<string[]> splitDatas = null;
        StreamWriter[] sw = null;

        public WriteFile(List<string> data,string path, string GUID,string fileName)
        {
            Console.WriteLine("Start Write Asset File..");
            
            this.path = path;
            this.GUID = GUID;

            subject = data[0].Split(',');

            sw = new StreamWriter[data.Count - 1];
            name = new string[data.Count - 1];
            splitDatas = new List<string[]>();

            for(int i = 0; i < sw.Length; i++)
            {
                name[i] = fileName + "_" + i.ToString();
                sw[i] = new StreamWriter(path + "\\" + name[i] + ".asset");
            }

            for(int i = 1; i < data.Count; i++)
            {
                splitDatas.Add(data[i].Split(','));
            }
        }

        public bool Run()
        {
            int count = 0;
            try
            {
                for(int i = 0; i < sw.Length; i++)
                {
                    sw[i].WriteLine("%YAML 1.1");
                    sw[i].WriteLine("%TAG !u! tag:unity3d.com,2011:");
                    sw[i].WriteLine("--- !u!114 &11400000");
                    sw[i].WriteLine("MonoBehaviour:");
                    sw[i].WriteLine("  m_ObjectHideFlags: 0");
                    sw[i].WriteLine("  m_CorrespondingSourceObject: {fileID: 0}");
                    sw[i].WriteLine("  m_PrefabInstance: {fileID: 0}");
                    sw[i].WriteLine("  m_PrefabAsset: {fileID: 0}");
                    sw[i].WriteLine("  m_GameObject: {fileID: 0}");
                    sw[i].WriteLine("  m_Enabled: 1");
                    sw[i].WriteLine("  m_EditorHideFlags: 0");
                    sw[i].WriteLine("  m_Script: {fileID: 11500000, guid: " + GUID + ", type: 3}");
                    sw[i].WriteLine("  m_Name: " + name[i]);
                    sw[i].WriteLine("  m_EditorClassIdentifier:");

                    for(int j = 0; j < subject.Length; j++)
                    {
                        sw[i].Write("  " + subject[j] + ": ");
                        sw[i].WriteLine(splitDatas[count][j]);
                    }

                    count++;
                    sw[i].Close();
                }
                Console.WriteLine("Complete Write...");

                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
