using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TradeBot
{
    class Data
    {
        public string ItemNameEng;
        public string ItemNameRu;
        public string PricePurchase;
        public string PriceSell;

        public static void CreateRequiredFiles()
        {
            //if (!File.Exists(@"Data\Log.txt")) File.Create(@"Data\Log.txt").Close();
            if (!File.Exists(@"Data\SavedLoginAndPassword.txt")) File.Create(@"Data\SavedLoginAndPassword.txt").Close();
            if (!File.Exists(@"Data\MainList.txt")) File.Create(@"Data\MainList.txt").Close();
            if (!File.Exists(@"Data\OldBoughtItems.txt")) File.Create(@"Data\OldBoughtItems.txt").Close();
            if (!File.Exists(@"Data\Items.txt"))
            {
                File.Create(@"Data\Items.txt").Close();
                MessageBox.Show("Внимаение!\r\nСписок предметов пуст!", "Внимаение!");
            }
            if (!File.Exists(@"Data\SavedConfiguration.txt"))
            {
                File.Create(@"Data\SavedConfiguration.txt").Close();
                using StreamWriter sw = new StreamWriter(@"Data\SavedConfiguration.txt");
                for (int i = 0; i < 12; i++) sw.WriteLine("");
                sw.Write("");
            }
        }

        public static void ReadList(List<string> SomeList, string filename)
        {
            SomeList.Clear();
            using StreamReader sr = new StreamReader(@"Data\" + filename + ".txt");
            while (!sr.EndOfStream) SomeList.Add(sr.ReadLine());
        }

        public static void WriteList(List<string> SomeList, string filename)
        {
            if (SomeList.Count == 0) return;
            using StreamWriter sw = new StreamWriter(@"Data\" + filename + ".txt", false, Encoding.Default);
            for (int i = 0; i < SomeList.Count - 1; i++) sw.WriteLine(SomeList[i]);
            sw.Write(SomeList[^1]);
        }

        public static List<string> ReadConfiguration()
        {
            List<string> Configuration = new List<string>();
            using (StreamReader sr = new StreamReader(@"Data\SavedConfiguration.txt"))
            {
                while (!sr.EndOfStream) Configuration.Add(sr.ReadLine());
            }

            if (Configuration.Count != 14)
            {
                Configuration.Clear();
                using StreamWriter sw = new StreamWriter(@"Data\SavedConfiguration.txt", false, Encoding.Default);
                for (int i = 0; i < 13; i++)
                {
                    sw.WriteLine("");
                    Configuration.Add("");
                }
                Configuration.Add("");
                sw.Write("");
            }
            return Configuration;
        }

        public static void WriteConfiguration(List<string> Configuration)
        {
            using StreamWriter sw = new StreamWriter(@"Data\SavedConfiguration.txt", false, Encoding.Default);
            for (int i = 0; i < Configuration.Count; i++) sw.WriteLine(Configuration[i]);
        }

        public static string ReadLoginAndPassword(int key)
        {
            string login;
            string password;
            string path;


            using StreamReader sr = new StreamReader(@"Data\SavedLoginAndPassword.txt");
            var fi = new FileInfo(@"Data\SavedLoginAndPassword.txt");
            if (fi.Length != 0)
            {
                login = sr.ReadLine();
                password = sr.ReadLine();
                path = sr.ReadLine();
                if (key == 1) return login;
                if (key == 2) return password;
                if (key == 3) return path;
            }
            return "";
        }

        public static string ReadMaFile(string path)
        {
            string maFileText;
            using (FileStream fstream = File.OpenRead($"{path}"))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                maFileText = Encoding.Default.GetString(array);
            }
            return maFileText;
        }

        public static void WriteLoginAndPassword(string login, string password, string path)
        {
            using StreamWriter sw = new StreamWriter(@"Data\SavedLoginAndPassword.txt", false, Encoding.Default);
            sw.WriteLine(login);
            sw.WriteLine(password);
            if (path != "") sw.WriteLine(path);
        }

        public static void LoadData(List<Data> MainList)
        {
            string Read;
            MainList.Clear();
            using StreamReader sr = new StreamReader(@"Data\MainList.txt");
            while (!sr.EndOfStream)
            {
                Read = sr.ReadLine();
                string[] blocks = Read.Split('$');
                MainList.Add(new Data() { ItemNameEng = blocks[1], ItemNameRu = blocks[2], PricePurchase = blocks[3], PriceSell = blocks[4] });
            }
        }

        public static void UploadData(List<Data> MainList)
        {
            if (MainList.Count != 0)
            {
                using StreamWriter sw = new StreamWriter(@"Data\MainList.txt", false, Encoding.Default);
                for (int i = 0; i < MainList.Count - 1; i++) sw.WriteLine("$" + MainList[i].ItemNameEng + "$" + MainList[i].ItemNameRu + "$" + MainList[i].PricePurchase + "$" + MainList[i].PriceSell + "$");
                sw.Write("$" + MainList[^1].ItemNameEng + "$" + MainList[^1].ItemNameRu + "$" + MainList[^1].PricePurchase + "$" + MainList[^1].PriceSell + "$");
            }
            else
            {
                File.Delete(@"Data\MainList.txt");
                File.Create(@"Data\MainList.txt").Close();
            }
        }
    }
}
