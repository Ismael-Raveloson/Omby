using System.Data.SqlClient;

namespace ombyvery;

    public class Connexion{
        public SqlConnection connectSqlServer(){
            string stringconn = "Server=DESKTOP-LGAA2JC\\SQLEXPRESS;Database=Omby;Trusted_Connection=True;";
            SqlConnection conn = new SqlConnection(stringconn);
            return conn;
        }
    }
    
