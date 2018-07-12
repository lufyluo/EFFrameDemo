using System;
using System.IO;
using System.Linq;

namespace LufyAssembly.Extension
{
    public static class AssemblyExtension
    {
        public static System.Reflection.Assembly[]  GetAllAssemblies(this System.Reflection.Assembly assembly)
        {
            LoadAllSamePathDll(assembly);
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        private static void LoadAllSamePathDll(System.Reflection.Assembly assembly)
        {
            var path = FindPath(assembly);
            LoadDll(path);
        }

        private static void LoadDll(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles("*.dll");
            foreach (FileInfo f in fil)
            {
                System.Reflection.Assembly.LoadFile(f.FullName);
            }
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (DirectoryInfo d in dii)
            {
                LoadDll(d.FullName);
            }
        }

        public static string FindPath(System.Reflection.Assembly assembly)
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            if(string.IsNullOrEmpty(path))
                throw new FileNotFoundException($"无法找到{assembly.FullName}文件");
            var uri = new Uri(path.Remove(path.LastIndexOf("/", StringComparison.Ordinal)));
            return uri.LocalPath;
        }
    }
}
