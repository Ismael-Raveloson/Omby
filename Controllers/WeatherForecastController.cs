using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ombyvery.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost("insertDeclaration",Name="insertDeclaration")]
    public void insertDeclaration(Declaration decla){
        Connexion connect = new Connexion();
        String query  = "INSERT INTO OMBY_VERY(IDPERTE,IDOMBY,LIEU,ETAT,DATE_PERTE,DATE_RETROUVER) VALUES ('PERT' +  CAST(NEXT VALUE FOR SeqPerte AS VARCHAR), @idOmby , @idLieu , 0, @date_perte , NULL)";
        using(SqlConnection con = connect.connectSqlServer()){
            con.Open();

            using( SqlCommand cmd = new SqlCommand(query,con)){
                cmd.Parameters.AddWithValue("@idOmby",decla.idOmby);
                cmd.Parameters.AddWithValue("@idLieu",decla.idLieu);
                cmd.Parameters.AddWithValue("@date_perte", decla.datePerte);
                cmd.ExecuteNonQuery();
            }
        }
    }

    [HttpGet("getAllOmbyVery",Name="getAllOmbyVery")]
    public List<Declaration> getOmbyVery(){
        List<Declaration> list = new List<Declaration>();
        Connexion connect = new Connexion();
        
        using(SqlConnection con = connect.connectSqlServer()){
            con.Open();
            String query = "SELECT * FROM OMBY_VERY WHERE ETAT = 0";
            SqlCommand com = new SqlCommand(query,con);

            using (SqlDataReader reader =com.ExecuteReader()){
                while (reader.Read())
                {
                    Declaration decla = new Declaration();
                    
                    decla.idPerte = reader.GetString(reader.GetOrdinal("IDPERTE"));
                    decla.idOmby = reader.GetString(reader.GetOrdinal("IDOMBY"));
                    decla.idLieu = reader.GetString(reader.GetOrdinal("LIEU"));
                    decla.etat = reader.GetInt32(reader.GetOrdinal("ETAT"));            
                    decla.datePerte =reader.GetDateTime(reader.GetOrdinal("DATE_PERTE"));
                    decla.dateRetrouver = null;
                    list.Add(decla);
                }
            }   
        }
            return list;
    }

    [HttpGet("getAllOmbyHita",Name="getAllOmbyHita")]
    public List<Declaration> getOmbyHita(){
        List<Declaration> list = new List<Declaration>();
        Connexion connect = new Connexion();
        
        using(SqlConnection con = connect.connectSqlServer()){
            con.Open();
            String query = "SELECT * FROM OMBY_VERY WHERE ETAT = 1";
            SqlCommand com = new SqlCommand(query,con);

            using (SqlDataReader reader =com.ExecuteReader()){
                while (reader.Read())
                {
                    Declaration decla = new Declaration();
                    
                    decla.idPerte = reader.GetString(reader.GetOrdinal("IDPERTE"));
                    decla.idOmby = reader.GetString(reader.GetOrdinal("IDOMBY"));
                    decla.idLieu = reader.GetString(reader.GetOrdinal("LIEU"));
                    decla.etat = reader.GetInt32(reader.GetOrdinal("ETAT"));            
                    decla.datePerte = reader.GetDateTime(reader.GetOrdinal("DATE_PERTE"));
                    decla.dateRetrouver = reader.GetDateTime(reader.GetOrdinal("DATE_RETROUVER"));
                    list.Add(decla);
                }
            }   
        }
            return list;
    }


}
