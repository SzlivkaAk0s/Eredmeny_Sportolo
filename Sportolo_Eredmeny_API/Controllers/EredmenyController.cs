using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportolo_Eredmeny_API.Models;
using MySqlConnector;
using Sportolo_Eredmeny_API.Models.DTOs;

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

        [HttpGet("byId")]
        public object GetEredmenyById(int id)
        {
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            string sql = @"SELECT * FROM `eredmeny` WHERE `id` = @id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            var datareader = cmd.ExecuteReader();

            object? data = null;

            if (datareader.Read() == true)
            {
                var eredmeny = new Eredmeny
                {
                    Id = datareader.GetInt32("id"),
                    Competition = datareader.GetString("competition"),
                    Description = datareader.GetString("description"),
                    ResultTime = datareader.GetDateTime("resultTime"),
                    UpdateTime = datareader.GetDateTime("updateTime"),
                    SportoloId = datareader.GetInt32("sportoloId")
                }
                ;
                data = new { message = "Sikeres lekérdezés", result = eredmeny };
            }
            else
            {
                data = new { message = "Nincs ilyen eredmeny", result = "" };
            }

            connection.Close();
            return data;
        }

        [HttpPost]
        public object AddNewEredmeny([FromBody] AddNewEredmenyDTO addNewEredmenyDto)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            string sql = @"INSERT INTO `eredmeny`(`Competition`, `Description`, `ResultTime`, `UpdateTime`, `SportoloId`) VALUES (@Competition, @Description, @ResultTime, @UpdateTime, @SportoloId)";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@Competition", addNewEredmenyDto.Competition);
            cmd.Parameters.AddWithValue("@Description", addNewEredmenyDto.Description);
            cmd.Parameters.AddWithValue("@ResultTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@SportoloId", addNewEredmenyDto.SportoloId);


            cmd.ExecuteNonQuery();

            connection.Close();
            return new { message = "Sikeres felvétel", result = addNewEredmenyDto };
        }

        [HttpPut]
        public object UpdateEredmeny([FromQuery] int id, UpdateEredmenyDTO updateEredmenyDTO)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            var sql = @"UPDATE `eredmeny` SET `Competition`=@Competetion,`Description`=@Description,`UpdateTime`=@UpdateTime WHERE `Id` = @id;";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@Competetion", updateEredmenyDTO.Competition);
            cmd.Parameters.AddWithValue("@Description", updateEredmenyDTO.Description);
            cmd.Parameters.AddWithValue("@UpdateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            connection.Close();

            return new { message = "Sikeres frissítés", result = updateEredmenyDTO };
        }

    }
}
