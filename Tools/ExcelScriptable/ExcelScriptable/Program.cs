namespace ExcelScriptable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileManageMent fmm = new FileManageMent(args[0], args[1], args[2], args[3]);
            fmm.Run();
        }
    }
}
