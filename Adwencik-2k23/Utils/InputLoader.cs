namespace Adwencik_2k23.Utils
{
    public static class InputLoader
    {
        public static string[] Load(string fileName) => System.IO.File.ReadAllLines($"Resources\\{fileName}.input");
    }
}
