using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SeekAndArchive
{
    class Program
    {
        static void Main(string[] args)
        {
            string nameOfTheDirectory = args[1];

            string sourceDirectory = @nameOfTheDirectory;

            string nameOfTheFile = args[0];

            string filePath = searchFile();

            string[] tokens = filePath.Split('\\');

            string destinationPath = "C:\\Users\\lugos\\Desktop" + "\\" + "backup_" + tokens.Last();

            string text = File.ReadAllText(@filePath);

            string searchFile()
            {
                var fileList = new DirectoryInfo(sourceDirectory).GetFiles(nameOfTheFile, SearchOption.AllDirectories);
                string searchedFileName = "";
                foreach (var file in fileList)
                {
                    if (file.ToString() == nameOfTheFile)
                    {
                        searchedFileName = file.FullName;
                    }
                }
                return searchedFileName;
            }


            byte[] GetFileHash(string fileName)
            {
                HashAlgorithm sha1 = HashAlgorithm.Create();
                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    return sha1.ComputeHash(stream);
            }

            string hash = System.Text.Encoding.UTF8.GetString(GetFileHash(filePath));


            while (true)
            {
                System.Threading.Thread.Sleep(1000);

                if (hash != System.Text.Encoding.UTF8.GetString(GetFileHash(filePath)))
                {
                    hash = System.Text.Encoding.UTF8.GetString(GetFileHash(filePath));
                    File.WriteAllText(destinationPath, text);
                    text = File.ReadAllText(filePath);
                }
            }
        }
    }
}