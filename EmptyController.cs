using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

/// <summary>
///  CSV出力用サンプル
/// </summary>
namespace CSV.Controllers
{
    public class EmptyController : Controller
    {
        // GET: Empty
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Export()
        {
            return View();
        }

        // コンストラクタ、、にしたかった所（笑）
        DateTime today = DateTime.Today;
        List<string> pList = new List<string>();
        string Id;
        string rNum; // れ
        int listCount = 1000; // 1000件
        string minute; // 分
        string second; // 秒
        string storeId; // み
        int storeCount = 110; //

        /// <summary>
        /// CSVで１行ずつ出力する。
        /// 110ファイルくぉ出力する。
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("ExportDownloaded")]
        [ValidateAntiForgeryToken]
        public ActionResult ExportDownloaded()
        {
            for (int i = 0; i < storeCount; i++)
            {
                string storeCount = "001" + String.Format("{0:000}", i); // み

                // 出力用ファイルをオープンする
                // ファイルを空にする
                StreamWriter empty = new StreamWriter(
                @"C:\Users\who\Desktop\DDD\OUTPUT\output_" + storeCount + ".csv",
                false, Encoding.GetEncoding("Shift_JIS"));
                empty.Close();
            }

            for (int i = 0; i < storeCount; i++)
            {
                string ii = i.ToString();
                string iii = ii.ToString();
                int storeNum = i;

                //for (int j = 0; j < listCount; j++)
                for (int j = 60; j < (listCount + 60); j++) //1000
                {
                    // 時間
                    // 1秒ずつ時間を経過させる（1000件（秒）ぶん）
                    int m = j / 60;
                    minute = String.Format("{0:00}", m); // 分
                    int sec = j % 60;
                    second = String.Format("{0:00}", sec); // 秒

                    storeId = "001" + String.Format("{0:000}", storeNum); // み
                    rNum = String.Format("{0:0000}", j);      // レ

                    if (listCount % 2 == 0) // Dあり
                    {
                        // リストを作成します
                        Id = "D1" + storeId
                            + today.Year.ToString() + today.Month.ToString() + today.Day.ToString()
                            + "06" + minute + second;
                        pList.Add(storeId);                        // 店
                        pList.Add(today.Year.ToString() + today.Month.ToString() + today.Day.ToString() + "06" + minute + second); // 日時
                        pList.Add(Id);                            // P
                        pList.Add(rNum);                           // レジ
                        if (j % 5 == 0) { pList.Add("1"); } else { pList.Add("0"); }

                        ExportPList(pList, storeId); // CSV出力する
                        pList = new List<string>();  // 空にする
                    }
                    else　                  // Dなし
                    {
                        // リストを作成します
                        Id = storeId + string.Format("\"{0:0000}\"", i)
                            + today.Year.ToString() + today.Month.ToString() + today.Day.ToString()
                            + "06" + minute + second;
                        pList.Add(storeId);                        // み
                        pList.Add(today.Year.ToString() + today.Month.ToString() + today.Day.ToString() + "06" + minute + second); // 日時
                        pList.Add(Id);                            // D
                        pList.Add(rNum);                           // レ
                        if (j % 5 == 0) { pList.Add("1"); } else { pList.Add("0"); }

                        ExportPList(pList, storeId); // CSV出力する
                        pList = new List<string>();  // 空にする
                    }
                }
            }

            return View();
        }

        /// <summary>
        /// CSVを出力します
        /// </summary>
        /// <param name="pList"></param>
        public void ExportPList(List<string> pList, string store)
        {
            string csvString = string.Empty;
            string[] stCsv = pList.ToArray();
            csvString = string.Join(",", stCsv);

            // ファイルに書き込む
            StreamWriter writer = new StreamWriter(
                @"C:\Users\who\Desktop\DDD\OUTPUT\output_" + store + ".csv",
                true, Encoding.GetEncoding("Shift_JIS"));
            // 1 行ずつ読み込み、出力ファイルに書き込む
            // このときの改行コードは Windows標準の CRLF となる
            writer.WriteLine(csvString);
            writer.Close();
        }
    }
}




//                    //line = Regex.Replace(line, @"\r\n?|\n", ",");


