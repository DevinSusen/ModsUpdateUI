using Microsoft.Win32;

namespace AppUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", @"InstallPath", "Nothing") as string;
            System.Console.WriteLine(path);
        }
    }
}
