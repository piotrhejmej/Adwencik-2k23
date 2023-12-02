namespace Adwencik_2k23.Utils
{
    public class InputLoader
    {
        public string[] Load(string fileName) => System.IO.File.ReadAllLines($"Resources\\{fileName}.input");
    }
}
