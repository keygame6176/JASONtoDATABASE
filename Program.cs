using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using IBM.Data.DB2;
using Newtonsoft.Json.Linq;

namespace Transfer2Raccoon
{
	// Token: 0x02000004 RID: 4
	internal class Program
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002890 File Offset: 0x00000A90
		/*定時刪除Log 模組*/
		public static void DeleteFile(string fileDirect,int saveDay,string filetype)

		{
			if (filetype == "")
			{
				filetype = "*.*";
			}
			else
			{
				filetype = "*." + filetype;
			}
				DateTime nowTime = DateTime.Now;

				string[] files = Directory.GetFiles(fileDirect, filetype, SearchOption.AllDirectories);  //獲取該目錄下所有 .txt文件
				foreach (string file in files)
				{
					FileInfo fileInfo = new FileInfo(file);
					TimeSpan t = nowTime - fileInfo.CreationTime;  //當前時間  減去 文件創建時間
					int day = t.Days;
					if (day > saveDay)   //保存的時間 ；  單位：天
					{
						File.Delete(file);  //刪除超過時間的文件
					}
				}
			

		}
		/*主程式*/
		public static void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			if (Program.tags == null)
			{
				Program.tags = new Dictionary<string, string>();
				foreach (string tagPair in ConfigurationManager.AppSettings["Tags"].Split(new char[]
				{
					';'
				}))
				{
					Program.tags.Add(tagPair.Split(new char[]
					{
						':'
					})[0], tagPair.Split(new char[]
					{
						':'
					})[1]);
				}
			}
			DB2Connection con = new DB2Connection(ConfigurationManager.AppSettings["DB2ConnectionString"]);
			Program.aTimer.Stop();
			string INIOPEN = ConfigurationManager.AppSettings["INIOPEN"];
			string RespiratoryORHemodialysis = ConfigurationManager.AppSettings["RespiratoryORHemodialysis"];
			string sourceFile = ConfigurationManager.AppSettings["sourceFile"];
			string DataTableName = ConfigurationManager.AppSettings["DataTableName"];
			string HID = ConfigurationManager.AppSettings["HID"];
			IEnumerable<string> txtFiles = Directory.EnumerateFiles(sourceFile, "*.txt");
			string ProgrmPath = System.IO.Directory.GetCurrentDirectory();
			string Data = DateTime.Now.ToString("yyyyMMddHH");
			string Datalog = DateTime.Now.ToString("yyyyMMddHH:mm ss tt");
			if (INIOPEN == "Y")
			{
				StreamReader path = new StreamReader(ConfigurationManager.AppSettings["Pathstr"]);
				if (RespiratoryORHemodialysis == "Respiratory")
				{
					string str;
					do
					{
						str = path.ReadLine();
					}
					while (!str.Contains("PumpPath="));
					string pathresult = str.Split(new char[]
					{
						'='
					})[1];
					txtFiles = Directory.EnumerateFiles(pathresult, "*.txt");
					Console.WriteLine("Pumps路徑:" + pathresult);
				}
				else if (RespiratoryORHemodialysis == "Hemodialysis")
				{
					while (!path.ReadLine().Contains("[Hemodialysis]"))
					{
					}
					string pathresult = path.ReadLine().Split(new char[]
					{
						'='
					})[1];
					txtFiles = Directory.EnumerateFiles(pathresult, "*.txt");
					Console.WriteLine("資料夾路徑:" + pathresult);
				}
			}
			else if (INIOPEN == "N")
			{
				txtFiles = Directory.EnumerateFiles(sourceFile, "*.txt");
			}
			foreach (string file in txtFiles)
			{
				Console.WriteLine("正在轉:" + file);
				if (!File.Exists(file.Split(new char[]
				{
					'.'
				})[0] + ".transferRed"))
				{
					StreamReader afile = new StreamReader(file);
					con.Open();
					string line;
					while ((line = afile.ReadLine()) != null && !(line == ""))
					{
						JToken jsonstring = JObject.Parse(line);
						List<string> strColumnList = new List<string>();
						List<string> strValueList = new List<string>();
						string columnstr = "";
						string valuestr = "";
						foreach (JToken jtoken in ((IEnumerable<JToken>)jsonstring))
						{
							JProperty item = (JProperty)jtoken;
							string tagId = item.Name;
							string value = item.Value.ToString();
							string tagname = (from m in Program.tags
							where m.Key == tagId
							select m.Value).FirstOrDefault<string>();
							if (tagname != null)
							{
								strColumnList.Add(tagname);
								strValueList.Add(value);
							}
						}
						foreach (JToken jtoken2 in ((IEnumerable<JToken>)jsonstring.SelectToken("measureResult")[0]))
						{
							JProperty item2 = (JProperty)jtoken2;
							string tagId = item2.Name;
							string value2 = item2.Value.ToString();
							string tagname2 = (from m in Program.tags
							where m.Key == tagId
							select m.Value).FirstOrDefault<string>();
							if (tagname2 != null)
							{
								strColumnList.Add(tagname2);
								strValueList.Add(value2);
							}
						}
						foreach (JToken item3 in ((IEnumerable<JToken>)jsonstring.SelectToken("measureResult")[0].SelectToken("itemValue")))
						{
							string tagId = item3.SelectToken("valueType").ToString();
							string value3 = item3.SelectToken("value").ToString();
							string tagname3 = (from m in Program.tags
							where m.Key == tagId
							select m.Value).FirstOrDefault<string>();
							if (tagname3 != null)
							{
								strColumnList.Add(tagname3);
								strValueList.Add(value3);
							}
						}
						foreach (string column in strColumnList)
						{
							columnstr = columnstr + ",\"" + column + "\"";
						}
						columnstr = columnstr.Substring(1, columnstr.Length - 1);
						foreach (string value4 in strValueList)
						{
							valuestr = valuestr + ",'" + value4 + "'";
						}
						valuestr = valuestr.Substring(1, valuestr.Length - 1);
						DB2Command cmd = new DB2Command();
						cmd.CommandType = CommandType.Text;
						string strQuery = string.Concat(new string[]
						{
							"INSERT INTO ",
							DataTableName, 
							" ( ",
							columnstr, 
							", \"HID\"",
							" ) VALUES(",
							valuestr,
							", '" ,
							HID,
							"'",
							")"
						});
						try
						{
							cmd.CommandText = strQuery;
							cmd.Connection = con;
							cmd.ExecuteNonQuery();
							/*File.AppendAllText(ConfigurationManager.AppSettings["destinationFile"] + "\\\\out.txt",  strQuery + "\r\n");*/
							File.AppendAllText(ProgrmPath + "\\Log" + "\\out"+Data+".txt", Datalog + strQuery + "\r\n");
							break;
						}
						catch (Exception ex)
						{
							/*File.AppendAllText(ConfigurationManager.AppSettings["destinationFile"] + "\\\\out.txt", strQuery+ "\r\n" + columnstr + "\r\n" + valuestr + "\r\n" + ex.Message + "\r\n");*/
							File.AppendAllText(ProgrmPath + "\\Log" + "\\out" + Data + ".txt", Datalog + strQuery + "\r\n" + columnstr + "\r\n" + valuestr + "\r\n" + ex.Message + "\r\n");
							break;
						}
						finally
						{
							con.Close();
						}
					}
					Console.WriteLine("已轉完一筆資料");
					afile.Close();
				}
				else
				{
					Console.WriteLine("這個檔案已經轉過，開始移動檔案");
				}
				try
				{
					string Filename = Path.GetFileName(file);
					sourceFile = file;
					string destinationFile = ConfigurationManager.AppSettings["destinationFile"];
					if (File.Exists(destinationFile + "\\" + Filename))/*true 偵測檔案是否存在，如存在刪除移動*/
					{						
						Console.WriteLine("檔案存在");
						/*Console.WriteLine("檔案已移動到"+destinationFile + "\\" + Filename );*/
						File.Delete(destinationFile + "\\" + Filename);
						File.Move(sourceFile, destinationFile + "\\" + Filename);
					}
					else/*檔案不存在移動*/
					{
						Console.WriteLine("檔案不存在");
						File.Move(sourceFile, destinationFile + "\\" + Filename);
						Console.WriteLine(Filename + ",一筆檔案已移動!");
					}
				}
				catch (Exception ex2)
				{
					Console.WriteLine(ex2.Message);
				}
			}
			DeleteFile(ProgrmPath + "\\Log\\",1,"txt");
			Console.WriteLine(ProgrmPath + "\\Log\\");
			Console.WriteLine("等待倒數..");
			Program.aTimer.Start();
		}
		/*main*/
		// Token: 0x0600000E RID: 14 RVA: 0x0000306C File Offset: 0x0000126C
		private static void Main(string[] args)
		{
			if (args.Count<string>() > 0 && args[0] == "-ui")
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Form1());
				return;
			}
			int Timmer = int.Parse(ConfigurationManager.AppSettings["Timmer"]);
			Program.aTimer = new System.Timers.Timer();
			Program.aTimer.Interval = (double)Timmer;
			Program.aTimer.Elapsed += Program.OnTimedEvent;
			Program.aTimer.AutoReset = true;
			Program.aTimer.Enabled = true;
			string ProgrmPath = System.IO.Directory.GetCurrentDirectory();
			string Data= DateTime.Now.ToString("yyyyMMddHH");
			Console.WriteLine("Press <Enter> to exit...");
			Console.WriteLine("程式路徑"+ProgrmPath  );
			Console.ReadLine();
			while (Console.ReadKey().Key != ConsoleKey.Enter) { }
		}




		// Token: 0x0400000C RID: 12
		private static System.Timers.Timer aTimer;

		// Token: 0x0400000D RID: 13
		private static Dictionary<string, string> tags;
	}
}
