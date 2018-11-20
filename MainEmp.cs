using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;

namespace RDP_FramWork
{
    public class MainEmp
    {
        public static void Main(string[] arg)
        {
            UtilityTool utlTool = new UtilityTool();
            utlTool.readFile();
        }
    }

    public class UtilityTool
    {
        private bool validInt(string ln)
        {
            int i;
            bool chkValid = int.TryParse(ln, out i);
            return chkValid;
        }
        private string chkAmount(string ln)
        {
            ln = ln.Substring(20, 5);
            int length = ln.Length;
            if((ln[0]=='1'|| ln[0] == '2')&& (ln[1] == '0' || ln[1] == '2'))
            {
                return ln;
            }
            return ln="Invalid";
        }
        private DateTime chkDate(string ln)
        {
            ln = ln.Substring(10, 10);
            string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy" };
            DateTime outdt;
            bool chkdt = DateTime.TryParseExact(ln, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out outdt);
            return outdt;
        }
        private void dtbetwwen(string ln)
        {
            DateTime td = DateTime.Now;
            TimeSpan tmspn;
            string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy" };
            
                  DateTime outdt;
            bool chkdt = DateTime.TryParseExact(ln, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out outdt);
            tmspn = td - outdt;
        }
        
        public void readFile()
        {
            try
            {
                using (FileStream fs = new FileStream(@"E:\DownLoadStudy\CSharp\RDP_FramWork\RDPFile.txt", FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string alltext = sr.ReadToEnd();
                        string[] linearray = alltext.Split('\n');
                        string line;
                        List<RdpEmployee> validlist = new List<RdpEmployee>();
                        List<RdpEmployee> Invalidlist = new List<RdpEmployee>();

                        for (int i=0;i< linearray.Length;i++)
                        {
                            line = linearray[i];
                          //  line= chkAmount(line);
                         //   int amount;
                            chkDate(line);
                         //  bool  chkamount = int.TryParse(line, out amount);

                        //    string[] lineArray = line.Replace('\r'.ToString(), "").Split(',');
                        //   RdpEmployee rdpEmployee = new RdpEmployee();
                        //   if(validInt(line.Substring(0, 4))==true)
                        //   { rdpEmployee.RDPID = line.Substring(0, 4); }


                        }
                    }
                     
                }
            }
           catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void xmlWrite()
        {
            List<RdpEmployee> lst = new List<RdpEmployee>();
            FileStream fileStream = new FileStream("", FileMode.OpenOrCreate, FileAccess.Write);
            XmlSerializer seri = new XmlSerializer(typeof(List<RdpEmployee>));
            seri.Serialize(fileStream,lst );
        }
        public void XmlReadFile()
        {
            FileStream fileStream = new FileStream("", FileMode.Open, FileAccess.Read);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<RdpEmployee>));
            List<RdpEmployee> lst = new List<RdpEmployee>();
            lst =(List<RdpEmployee>) xmlSerializer.Deserialize(fileStream);
            foreach(var item in lst)
            {

            }
        }
    }
}
