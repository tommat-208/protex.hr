using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using protexhr.repository;

namespace potexhr.webApp.Database
{
    public static class ProtexhrAuthDB
    {

        private static WebApplicationBuilder? builder = WebApplication.CreateBuilder();
        private static string connStr = builder.Configuration.GetConnectionString("ProtexhrDBConnectionString");

        /*
         // Modello generale
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            var txt = $@"";
            var cmd = new SqlCommand(txt, conn);
            var rdr = cmd.ExecuteReader();

            while (rdr.Read()) 
            {
                    
            }
    
        }
         */


        public static void InsertUserPerson(UserPerson u)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"INSERT INTO userPerson(username, email, passwordHash, type) VALUES
                            ('{u.Username}', '{u.Email}', '{u.PasswordHash}', {u.Type.Id})";
                var cmd = new SqlCommand(txt, conn);
                var ris = cmd.ExecuteNonQuery();
            }
        }


        public static bool UpdateUserPerson(UserPerson u)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"UPDATE userPerson SET username='{u.Username}', email='{u.Email}', passwordHash='{u.PasswordHash}', type={u.Type.Id}
                            WHERE id={u.Id};";
                var cmd = new SqlCommand(txt, conn);
                var ris = cmd.ExecuteNonQuery();

                return ris > 0;
            }
        }

        public static bool DeleteUserPerson(string username)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"DELETE FROM userPerson WHERE username='{username}';";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteNonQuery();

                return rdr > 0;
            }
        }


        public static UserPerson GetUserPerson(string username)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"SELECT * FROM userPerson WHERE username='{username}';";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var u = new UserPerson
                    {
                        Id = rdr.GetInt32(0),
                        Username = rdr.GetString(1),
                        Email = rdr.GetString(2),
                        PasswordHash = rdr.GetString(3),
                        Type = GetUserType(rdr.GetInt32(4))
                    };

                    return u;
                }
                return null;
            }
        }

        /* UserType */
        public static UserType GetUserType(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"SELECT * FROM userType WHERE id={id};";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var ut = new UserType
                    {
                        Id = rdr.GetInt32(0),
                        Name = rdr.GetString(1),
                        Authorized = rdr.GetBoolean(2)
                    };

                    return ut;
                }

                return null;
            }

        }

        public static List<UserType> GetAllUserType()
        {
            var l = new List<UserType>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"SELECT id FROM userType";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    l.Add(GetUserType(rdr.GetInt32(0)));
                }
            }

            return l;
        }


    }
}
