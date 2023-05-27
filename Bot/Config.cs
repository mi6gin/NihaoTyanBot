using System;
using System.IO;

namespace NihaoTyan.Bot
{
    static class Config
    {
        public static readonly string Token;

        static Config()
        {
            string filePath = @"C:\Users\mi6gun\Documents\my token\Nihao.txt";
            if (File.Exists(filePath))
            {
                Token = File.ReadAllText(filePath);
            }
            else
            {
                Console.WriteLine("Token file not found.");
            }
        }
    }
}
