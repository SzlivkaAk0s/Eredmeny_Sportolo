using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportolo_Eredmeny_API.Models;
using MySqlConnector;

namespace Sportolo_Eredmeny_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EredmenyController : ControllerBase
    {
        public string ConnectionString = "server=localhost;uid=root;password=;database=sportolo13b;";

        [HttpGet]
        public List<Eredmeny> GetBloggers()
        {
            List<Eredmeny> eredmenyek = new List<Eredmeny>();

            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = "SELECT * FROM `eredmeny`;";

            var cmd = new MySqlCommand(sql, connection);

            var data = cmd.ExecuteReader();

            while (data.Read())
            {
                var eredmeny = new Eredmeny()
                {
                    Id = data.GetInt32("id"),
                    Competition = data.GetString("competition"),
                    Description = data.GetString("description"),
                    ResultTime = data.GetDateTime("resultTime"),
                    UpdateTime = data.GetDateTime("updateTime"),
                    SportoloId = data.GetInt32("sportoloId")
                };
                eredmenyek.Add(eredmeny);
            }

            connection.Close();

            return eredmenyek;
        }
    }
}
