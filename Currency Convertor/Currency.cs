using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using System.IO.IsolatedStorage;
using System.Globalization;
using System.Threading;
using System.Net.NetworkInformation;

namespace Currency_Convertor
{
    public class Currency
    {
        private IsolatedStorageFile isf;
        private List<Decimal?> rates = new List<Decimal?>() { 1 };

        public Currency()
        {
            isf = IsolatedStorageFile.GetUserStoreForApplication();
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                DownloadFile();
            }
            else
            {
                ParseData();
            }
        }

        private void DownloadFile()
        {
            Uri uri = new Uri("http://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.txt");
            
            var wc = new WebClient();            
            wc.OpenReadCompleted += new OpenReadCompletedEventHandler(wcOpenReadCompleted);
            wc.OpenReadAsync(uri);
        }

        private void wcOpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            String sheet = "cur.txt";            

            var str = e.Result;

            using (IsolatedStorageFileStream fileStream = isf.CreateFile(sheet))
            {
                byte[] buffer = new byte[8 * 1024];
                int len;
                while ((len = str.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, len);                    
                }                
            }
            this.ParseData();            
        }

        private void ParseData()
        {
            try
            {
                using (IsolatedStorageFileStream stream = isf.OpenFile("cur.txt", FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("cs-CZ");
                        reader.ReadLine(); //date #num
                        reader.ReadLine(); //header
                        for (int i = 1; i < 35; i++)
                        {
                            var line = reader.ReadLine();
                            if (i != 22) //MFF
                            {
                                System.Diagnostics.Debug.WriteLine(line);
                                rates.Add(setExchange(line.Split('|')));
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {

            }
        }

        private Decimal? setExchange(String[] parts)
        {
            int num;
            Decimal ex;            
            try
            {
                num = int.Parse(parts[2]);
                ex = Decimal.Parse(parts[4]);
            }
            catch (FormatException)
            {
                return null;
            }
            System.Diagnostics.Debug.WriteLine(num + " " + ex + " " + ex/num);
            return ex/num;
        }

        public Decimal? getRate(int from, int to)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(rates[to]);
                System.Diagnostics.Debug.WriteLine(rates[from]);
                System.Diagnostics.Debug.WriteLine(rates[to] / rates[from]);
                return rates[from] / rates[to];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        public String Refresh()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                DownloadFile();
                return "Refreshed.";
            }
            return "Network connection not found.";
        }
    }
}
