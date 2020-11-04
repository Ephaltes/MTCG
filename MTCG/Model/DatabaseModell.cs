using System;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model.MonsterTypes.Dragon;
using Npgsql;

namespace MTCG.Model
{
    public class DatabaseModell : IDatabase
    {
        private readonly NpgsqlConnection _connection;

        public DatabaseModell()
        {
            _connection = new NpgsqlConnection(AppSettings.Settings.ConnectionString);
            _connection.Open();
        }
        public bool CreateUser(UserEntity userEntity)
        {
            var sql = "INSERT INTO mtcg.user(username,password,salt,token) VALUES(@username,@password,@salt,@token)";
            using var cmd = new NpgsqlCommand(sql,_connection);

            cmd.Parameters.AddWithValue("username", userEntity.Username);
            cmd.Parameters.AddWithValue("password", userEntity.Password);
            cmd.Parameters.AddWithValue("salt", userEntity.Salt);
            cmd.Parameters.AddWithValue("token", userEntity.Token);

            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UserExists(string username)
        {
            var sql = "SELECT * from mtcg.user where username=@username";
            using var cmd = new NpgsqlCommand(sql,_connection);

            cmd.Parameters.AddWithValue("username", username);
            try
            {
                cmd.Prepare();
                var result = cmd.ExecuteScalar();
                if (result == null)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NpgsqlException();
            }
        }

        public UserEntity GetUserByToken(string token)
        {
            var sql = "SELECT * from mtcg.user where token=@token";
            using var cmd = new NpgsqlCommand(sql,_connection);

            cmd.Parameters.AddWithValue("token", token);
            try
            {
                cmd.Prepare();
                var result = cmd.ExecuteReader();
                
                if (!result.HasRows)
                    return null;
                
                UserEntity ret = new UserEntity();

                while (result.Read())
                {
                    ret.Username = result.GetString(0);
                    ret.Password = result.GetString(1);
                    ret.Salt = result.GetString(2);
                    ret.Token = result.GetString(3);
                    ret.Elo = result.GetInt32(4);
                    ret.Win = result.GetInt32(5);
                    ret.Lose = result.GetInt32(6);
                    ret.Draw = result.GetInt32(7);
                }
                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NpgsqlException();
            }
        }
    }
}