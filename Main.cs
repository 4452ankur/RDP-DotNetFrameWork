using System;
using System.Collections.Generic;
using System.Text;
using RDP_FramWork;
using System.IO;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;



namespace RDP_FramWork
{
    public class MainRDP
    {
        RdpEmployee rdpEmployee = new RdpEmployee();
        private static void chkDobrt(DateTime dob)
        {
            var today = DateTime.Today;
            DateTime age = new DateTime(today.Subtract(dob).Ticks);
            var iz = age.Year- 1;
           

        }

        static void Main(string [] arg)
        {
            DateTime dt = Convert.ToDateTime("10/27/2008");
            chkDobrt(dt);
            List<RdpEmployee> MailList = new List<RdpEmployee>();
            DatabaseTools dbTool = new DatabaseTools();
            
            XmlUtilites xmlUtilites = new XmlUtilites();
            utilities utilities = new utilities();
            Console.WriteLine("I am Ankur");
          
            MailList=utilities.Fileread();
            dbTool.ReadData();
            dbTool.UpdateTable();
            dbTool.DataTableFill();

            dbTool.InsertData(MailList);
            xmlUtilites.XmlReader();
            Console.ReadLine();
        }
      
    }

    public class utilities
    {
        // text file read and insert into Raw list
        XmlUtilites xmlUtilites = new XmlUtilites();

        public List<RdpEmployee> Fileread()
        {
            List<RdpEmployee> lstRawEmp = new List<RdpEmployee>();
            try
            {
               
                FileStream fileStream = new FileStream(@"E:\DownLoadStudy\CSharp\RDP_FramWork\RDP_File.txt", FileMode.Open, FileAccess.Read);
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    string FileContent = streamReader.ReadToEnd();
                    string[] FileContentArray = FileContent.Split('\n'.ToString());


                    
                    for (int i = 0; i < FileContentArray.Length; i++)
                    {
                        RdpEmployee objEmp = new RdpEmployee();
                        string line = FileContentArray[i];
                        string[] lineArray = line.Replace('\r'.ToString(), "").Split(',');
                        objEmp.RDPID = lineArray[0];
                        objEmp.RdpName = lineArray[1];
                        objEmp.RDPDOB = Convert.ToDateTime(lineArray[2]);
                        objEmp.RDSalary = Convert.ToInt32(lineArray[3]);
                        objEmp.RDPAddress = lineArray[4];
                        objEmp.RDPProject = lineArray[5];
                        lstRawEmp.Add(objEmp);

                    }
                    //xmlUtilites.XmlWriter(lstRawEmp);
                }                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstRawEmp;
        }

        public List<RdpEmployee> Valid_InvalidList(List<RdpEmployee>objlst)
        {
            return objlst;
        }
        public void filemove()
        {
            try
            {
                File.Move(@"D\a.txt", "D\b.txt");
            }
            catch(Exception ex)
            {
               Console.WriteLine(ex.Message);
            }           
        }

    }
    /// <summary>
    ///  class read write from xmal file
    /// </summary>
    public class XmlUtilites
    {
        /// <summary>
        /// 
        /// </summary>
        public void XmlWriter(List<RdpEmployee>lstEmp)
        {
            using (FileStream XMLfileStream = new FileStream(@"E:\DownLoadStudy\CSharp\RDP_FramWork\XML_RDP_File.xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<RdpEmployee>));
                xmlSerializer.Serialize(XMLfileStream, lstEmp);
                XMLfileStream.Close();
            }              

        }
        /// <summary>
        /// 
        /// </summary>
        public void XmlReader()
         {
            FileStream XmlfileStreamRd = new FileStream(@"E:\DownLoadStudy\CSharp\RDP_FramWork\XML_RDP_File.xml", FileMode.Open, FileAccess.Read);
            List<RdpEmployee> lstemp = new List<RdpEmployee>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<RdpEmployee>));
            lstemp = (List<RdpEmployee>)xmlSerializer.Deserialize(XmlfileStreamRd);


        }
     
        }

    public class DatabaseTools
    {

        string constr =@"Data Source=ANKUR-PC\SQLEXPRESS;Initial Catalog=RDP;Persist Security Info=True;User ID=sa;Password=P@ssw0rd";
       public void InsertData(List<RdpEmployee>lst)
        {
            using (SqlConnection sqlCon = new SqlConnection(constr))
            {
                sqlCon.Open();
                string InsertQuery = "Insert into rdp_emp(EmpID,EmpName,EmpProject,EmpAddress,EmpDOB,EmpSalary) values (@EmpID,@EmpName,@EmpProject,@EmpAddress,@EmpDOB,@EmpSalary)";

                using (SqlCommand sqlCmd = new SqlCommand(InsertQuery, sqlCon))
                {
                    foreach(var item in lst)
                    {
                        sqlCmd.Parameters.Clear();
                        sqlCmd.Parameters.AddWithValue("@EmpID", item.RDPID);
                        sqlCmd.Parameters.AddWithValue("@EmpName", item.RdpName);
                        sqlCmd.Parameters.AddWithValue("@EmpProject", item.RDPProject);
                        sqlCmd.Parameters.AddWithValue("@EmpAddress", item.RDPAddress);
                        sqlCmd.Parameters.AddWithValue("@EmpDOB", item.RDPDOB);
                        sqlCmd.Parameters.AddWithValue("@EmpSalary", item.RDSalary);
                        sqlCmd.ExecuteNonQuery();
                    }

                }
            }

        }

   
        public void ReadData()
        {
            List<RdpEmployee> listReder = new List<RdpEmployee>();
            string constr = @"Data Source=ANKUR-PC\SQLEXPRESS;Initial Catalog=RDP;Persist Security Info=True;User ID=sa;Password=P@ssw0rd";
            using (SqlConnection sqlCon = new SqlConnection(constr))
            {
                string queryString = "Select * from rdp_emp";
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand(queryString, sqlCon))

                    
                using (SqlDataReader reader = sqlCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RdpEmployee emp = new RdpEmployee();
                        emp.RDPID = reader.GetString(0);
                        emp.RdpName = reader.GetString(1);
                        emp.RDPProject = reader.GetString(2);
                        emp.RDPAddress = reader.GetString(3);
                        emp.RDPDOB = reader.GetDateTime(4);
                        emp.RDSalary = reader.GetInt32(5);

                        listReder.Add(emp);

                    }
                }


            }
        }

        public void DataTableFill()
        {
            using (SqlConnection sqlConn = new SqlConnection(constr))
            {
                string comtext = "Select * from rdp_emp";
                sqlConn.Open();
                DataTable dt = new DataTable();
                using (SqlCommand sqlCom = new SqlCommand(comtext, sqlConn))
                using (SqlDataAdapter sqlAdp = new SqlDataAdapter(sqlCom))
                {
                    dt.Clear();
                    sqlAdp.Fill(dt);

                }

                using (FileStream fstream = new FileStream(@"E:\DownLoadStudy\CSharp\RDP_FramWork\RDP_wrtTbl_data.txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fstream))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            streamWriter.WriteLine(dr[0].ToString() + "," + dr[1].ToString() + "," + dr[2].ToString() + "," + dr[3].ToString() + "," + dr[4].ToString() + "," + dr[5].ToString());
                        }
                    }
                }
                
            }
        }

        public void UpdateTable()
        {
            try
            {
        
                string constr = @"Data Source=Ankur-PC\SQLEXPRESS;Initial Catalog=RDP;Persist Security Info=True;User ID=sa;Password=P@ssw0rd";
                using (SqlConnection sqlconn = new SqlConnection(constr))
                {
                    sqlconn.Open();
                    string cmdText = "update Emp_PF set PF_Amout=a.Pf_Persent*b.EmpSalary From RDP_Emp B,Emp_PF A  where a.Emp_Id = b.EmpID";
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlconn))
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
    }

    //public class ValidationCheck
    //{
    //    private bool chkDOB(DateTime dob)
    //    {
    //        // Save today's date.
    //        var today = DateTime.Today;
    //        // Calculate the age.
    //        var age = today.Year - dob.Year;
    //        // Go back to the year the person was born in case of a leap year
    //        if (dob > today.AddYears(-age)) age--;
    //        return true;
    //    }
    //    private bool chkSalary()
    //    {

    //    }
    //    private bool duplicateRow()
    //    {

    //    }
    //    List<RdpEmployee> lstValid = new List<RdpEmployee>();
    //    List<RdpEmployee> lstInValid = new List<RdpEmployee>();

    //    public void ReturnList(out List<RdpEmployee>lstval,out List<RdpEmployee>lstInval,List<RdpEmployee>lstRaw)
    //    {
    //        foreach(var item in lstRaw)
    //        {

    //        }
    //    }
    //}
         
    }


