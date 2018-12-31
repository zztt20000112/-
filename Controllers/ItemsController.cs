using WebApplication1.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
namespace MFirstWAPI.Controllers
{
    public class ItemsController : ApiController
    {
        [HttpGet]
        // GET api/values
        public IEnumerable<Items> Get()
        {
            Console.WriteLine('t');
            List<Items> listItem = new List<Items>();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from ITEMS", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        Items item = new Items();
                        item.ItemsID = reader.GetInt32("ID");
                        item.ItemsTitle = reader.GetString("title");
                        item.ItemsDescribe = reader.GetString("describe");
                        item.ItemsDetail = reader.GetString("detail");
                        item.ItemsImage = reader.GetString("image");
                        listItem.Add(item);
                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                mysql.Close();
            }
            return listItem;
        }
        // POST api/values
        [HttpPost]
        public string POST([FromBody]Items ite)
        {
            var ItemsList = ite;
            MySqlConnection mysql = getMySqlConnection();
            string sqlstr = string.Format("INSERT INTO `xcy_csharp_project`.`ITEMS` (`id`, `title`, `describe`, `detail`, `image`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",ItemsList.ItemsID,ItemsList.ItemsTitle,ItemsList.ItemsDescribe,ItemsList.ItemsDetail,ItemsList.ItemsImage);
            //string sqlstr = "INSERT INTO `xcy_csharp_project`.`ITEMS` (`id`, `title`, `describe`, `detail`, `image`) VALUES ('100', '3', '33', '333', '3333')";
            //try
            //{
            //    mysql.Open();
            //    MySqlCommand mySqlCommand = getSqlCommand(sqlstr, mysql);
            //}
            //catch
            //{
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //}
            //finally
            //{
            //    mysql.Close();
            //}

            mysql.Open();
            MySqlCommand mycmd = new MySqlCommand(sqlstr, mysql);
            //MySqlCommand objCmd = new MySqlCommand("select * from `ITEMS` ", mysql);
            //MySqlDataReader r = objCmd.ExecuteReader();
            if (mycmd.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("数据插入成功!");
            }
            mysql.Close();

            return ItemsList.ItemsImage;
        }


        [HttpPost]
        // PUT api/values/5
        public void Put(int id, [FromBody] string _title, [FromBody] string _describe, [FromBody] string _detail, [FromBody] string _image)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sqlstr = "INSERT INTO `xcy_csharp_project`.`items` (`id`, `title`, `describe`, `detail`, `image`) VALUES ('" + id + "', '" + _title + "', '" + _describe + "', '" + _detail + "', '" + _image + "')";
            try
            {
                mysql.Open();
                MySqlCommand mySqlCommand = getSqlCommand(sqlstr, mysql);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            finally
            {
                mysql.Close();
            }
        }


        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        private static MySqlConnection getMySqlConnection()
        {
            MySqlConnection mysql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);
            return mysql;
        }
        public static MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);

            return mySqlCommand;
        }
    }
}