using System;
using System.Collections.Generic;
using System.IO;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model.BaseClass;
using Npgsql;
using Serilog;

namespace MTCG.Model
{
    public class DatabaseModell : IDatabase
    {
        private readonly NpgsqlConnection _connection;

        public DatabaseModell()
        {
            _connection = new NpgsqlConnection(AppSettings.Settings.ConnectionString);
        }

        public bool CreateUser(UserEntity userEntity)
        {

            try
            {
                _connection.Open();
                var sql =
                    "INSERT INTO mtcg.user(username,password,salt,token,displayname) VALUES(@username,@password,@salt,@token,@displayname)";
                using var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("username", userEntity.Username);
                cmd.Parameters.AddWithValue("password", userEntity.Password);
                cmd.Parameters.AddWithValue("salt", userEntity.Salt);
                cmd.Parameters.AddWithValue("token", userEntity.Token);
                cmd.Parameters.AddWithValue("displayname", userEntity.Username);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool UserExists(string username)
        {
           
            try
            {
                _connection.Open();
                var sql = "SELECT * from mtcg.user where username=@username";
                using var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("username", username);
                
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
            finally
            {
                _connection.Close();
            }
        }

        public UserEntity GetUserByToken(string token)
        {
            try
            {
                _connection.Open();
                var sql = "SELECT * from mtcg.user where token=@token";
                using var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("token", token);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                UserEntity ret = new UserEntity();

                while (result.Read())
                {
                    ret.Id = result.GetInt32(0);
                    ret.Username = result.GetString(1);
                    ret.Password = result.GetString(2);
                    ret.Salt = result.GetString(3);
                    ret.Token = result.GetString(4);
                    ret.Description = result.GetString(5);
                    ret.Image = result.GetString(6);
                    ret.Elo = result.GetInt32(7);
                    ret.Win = result.GetInt32(8);
                    ret.Lose = result.GetInt32(9);
                    ret.Draw = result.GetInt32(10);
                    ret.Coins = result.GetInt32(11);
                }

                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NpgsqlException();
            }
            finally
            {
                _connection.Close();
            }
        }
        
        public UserEntity GetUserByUsername(string username)
        {
            try
            {
                _connection.Open();
                var sql = "SELECT * from mtcg.user where username=@username";
                using var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("username", username);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                UserEntity ret = new UserEntity();

                while (result.Read())
                {
                    ret.Username = result.GetString(1);
                    ret.Description = result.GetString(5);
                    ret.Image = result.GetString(6);
                    ret.Win = result.GetInt32(8);
                    ret.Lose = result.GetInt32(9);
                    ret.Draw = result.GetInt32(10);
                }

                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NpgsqlException();
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool UpdateUser(UserEntity user)
        {
            try
            {
                _connection.Open();
                var sql = "UPDATE mtcg.user SET password=@password, description=@description, displayname=@displayname, image=@image, salt=@salt where token=@token";
                using var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("token", user.Token);
                cmd.Parameters.AddWithValue("password", user.Password);
                cmd.Parameters.AddWithValue("salt", user.Salt);
                cmd.Parameters.AddWithValue("description", user.Description);
                cmd.Parameters.AddWithValue("displayname", user.DisplayName);
                cmd.Parameters.AddWithValue("image", user.Image);
                cmd.Prepare();
                var result = cmd.ExecuteNonQuery();

                if (result > 0)
                    return true;

                return false;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new NpgsqlException();
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool AddCardsToDatabase(List<ICard> cardsToAdd)
        {
            _connection.Open();
            var transaction = _connection.BeginTransaction();

            try
            {
                foreach (var card in cardsToAdd)
                {
                    var sql =
                        "INSERT INTO mtcg.Card(id,name,damage,weakdamage,description,elementtype,cardtype,race) VALUES(@id,@name,@damage,@weakdamage,@description,@elementtype,@cardtype,@race)";
                    var cmd = new NpgsqlCommand(sql, _connection);

                    cmd.Parameters.AddWithValue("id", card.Entity.Id);
                    cmd.Parameters.AddWithValue("name", card.Entity.Name);
                    cmd.Parameters.AddWithValue("damage", card.Entity.Damage);
                    cmd.Parameters.AddWithValue("description", card.Entity.Description);
                    cmd.Parameters.AddWithValue("elementtype", card.Entity.ElementType);
                    cmd.Parameters.AddWithValue("cardtype", card.Entity.CardType);
                    cmd.Parameters.AddWithValue("race", card.Entity.Race);
                    
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool AddCardToDatabase(ICard card)
        {
            _connection.Open();
            var transaction = _connection.BeginTransaction();

            try
            {
                var sql =
                    "INSERT INTO mtcg.Card(id,name,damage,weakdamage,description,elementtype,cardtype,race) VALUES(@id,@name,@damage,@weakdamage,@description,@elementtype,@cardtype,@race)";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("id", card.Entity.Id);
                cmd.Parameters.AddWithValue("name", card.Entity.Name);
                cmd.Parameters.AddWithValue("damage", card.Entity.Damage);
                cmd.Parameters.AddWithValue("description", card.Entity.Description);
                cmd.Parameters.AddWithValue("elementtype", card.Entity.ElementType);
                cmd.Parameters.AddWithValue("cardtype", card.Entity.CardType);
                cmd.Parameters.AddWithValue("race", card.Entity.Race);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction.Rollback();
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }
        
        public bool AddCardToStack(ICard card,UserEntity user)
        {
            _connection.Open();
            try
            {
                var sql =
                    "INSERT INTO mtcg.r_user_card(userid, cardid, cardplace) VALUES(@userid,@cardid,@cardplace)";
                var cmd = new NpgsqlCommand(sql, _connection);
                

                cmd.Parameters.AddWithValue("userid", user.Id);
                cmd.Parameters.AddWithValue("cardid", card.Entity.Id);
                cmd.Parameters.AddWithValue("cardplace", CardPlace.Stack);
                
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }
        
        public bool UpdateCardStatus(ICard card,UserEntity user,CardPlace cardPlace)
        {
            _connection.Open();
            try
            {
                var sql =
                    "UPDATE mtcg.r_user_card set cardplace=@cardplace where cardid=@cardid AND userid=@userid";
                var cmd = new NpgsqlCommand(sql, _connection);
                

                cmd.Parameters.AddWithValue("userid", user.Id);
                cmd.Parameters.AddWithValue("cardid", card.Entity.Id);
                cmd.Parameters.AddWithValue("cardplace", cardPlace);
                
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<IPackage> GetPackages()
        {
            _connection.Open();
            
            try
            {
                List<IPackage> retList = new List<IPackage>();
                var sql ="select id from package";
                var cmd = new NpgsqlCommand(sql, _connection);
                
                var result = cmd.ExecuteReader();
                
                List<string> packageIds = new List<string>();
                
                while (result.Read())
                {
                   packageIds.Add(result.GetString(0));
                }

                foreach (var id in packageIds)
                {
                    retList.Add(GetPackage(id));
                }

                return retList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public IPackage GetPackage(string packageid)
        {
            _connection.Open();
            try
            {
                PackageEntity entity = new PackageEntity();

                var sql =
                    " select * from package where id = @packageid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("packageid", packageid);
            
                cmd.Prepare();
                var result = cmd.ExecuteReader();
                
                while (result.Read())
                {
                    entity.Id = result.GetString(0);
                    entity.Amount = result.GetInt32(1);
                }

                entity.CardsInPackage = GetCardsInPackage(packageid);
                DatabaseModell databse = new DatabaseModell();
                PackageModell model = new PackageModell(entity, databse);
                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NpgsqlException();
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<ICard> GetCardsInPackage(string packageid)
        {
           _connection.Open();
          
            try
            {
                List<ICard> ret = new List<ICard>();
                var sql =
                    " select cardid from package INNER JOIN r_package_card ON package.id=r_package_card.packageid WHERE package.id = @packageid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("packageid", packageid);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                List<string> cardIds = new List<string>();

                while (result.Read())
                {
                    cardIds.Add(result.GetString(0));
                }

                foreach (var id in cardIds)
                {
                    sql = "select * from card where id = @cardid";
                    cmd = new NpgsqlCommand(sql,_connection);

                    cmd.Parameters.AddWithValue("cardid", id);
                    cmd.Prepare();
                    cmd.ExecuteReader();

                    CardEntity entity = new CardEntity();
                    
                    while (result.Read())
                    {
                        entity.Id = result.GetString(0);
                        entity.Name = result.GetString(1);
                        entity.Damage = result.GetDouble(2);
                        entity.Description = result.GetString(3);
                        entity.ElementType = (ElementType)result.GetInt32(4);
                        entity.CardType = (CardType)result.GetInt32(5);
                        entity.Race = (Race)result.GetInt32(6);
                    }
                    var model = new CardModell(entity);
                     
                     if(string.IsNullOrEmpty(entity.Id))
                         throw new InvalidDataException();
                     
                     ret.Add(model);
                }

                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NpgsqlException();
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}