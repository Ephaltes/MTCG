using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Transactions;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
using MTCG.Model.BaseClass;
using Npgsql;
using NpgsqlTypes;
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
                    ret.Username = result.SafeGetString(1);
                    ret.Password = result.SafeGetString(2);
                    ret.Salt = result.SafeGetString(3);
                    ret.Token = result.SafeGetString(4);
                    ret.Description = result.SafeGetString(5);
                    ret.DisplayName = result.SafeGetString(6);
                    ret.Image = result.SafeGetString(7);
                    ret.Elo = result.GetInt32(8);
                    ret.Win = result.GetInt32(9);
                    ret.Lose = result.GetInt32(10);
                    ret.Draw = result.GetInt32(11);
                    ret.Coins = result.GetInt32(12);
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
                    ret.Id = result.GetInt32(0);
                    ret.Username = result.SafeGetString(1);
                    ret.Password = result.SafeGetString(2);
                    ret.Salt = result.SafeGetString(3);
                    ret.Token = result.SafeGetString(4);
                    ret.Description = result.SafeGetString(5);
                    ret.DisplayName = result.SafeGetString(6);
                    ret.Image = result.SafeGetString(7);
                    ret.Elo = result.GetInt32(8);
                    ret.Win = result.GetInt32(9);
                    ret.Lose = result.GetInt32(10);
                    ret.Draw = result.GetInt32(11);
                    ret.Coins = result.GetInt32(12);
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

        public bool AddCardsToDatabase(List<CardEntity> cardsToAdd)
        {
            bool opened = false;
            NpgsqlTransaction transaction = null;
            
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                transaction = _connection.BeginTransaction();
                opened = true;
            }

            try
            {
                foreach (var card in cardsToAdd)
                {
                    var sql =
                        "INSERT INTO mtcg.Card(id,name,damage,description,elementtype,cardtype,race,cardplace) VALUES(@id,@name,@damage,@description,@elementtype,@cardtype,@race,@cardplace)";
                    var cmd = new NpgsqlCommand(sql, _connection);

                    cmd.Parameters.AddWithValue("id", card.Id);
                    cmd.Parameters.AddWithValue("name", card.Name);
                    cmd.Parameters.AddWithValue("damage", card.Damage);
                    cmd.Parameters.AddWithValue("description", card.Description?? string.Empty);
                    cmd.Parameters.AddWithValue("elementtype", (int)card.ElementType);
                    cmd.Parameters.AddWithValue("cardtype",(int) card.CardType);
                    cmd.Parameters.AddWithValue("race", (int)card.Race);
                    cmd.Parameters.AddWithValue("cardplace", (int) card.CardPlace);
                    
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    transaction?.Commit();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                transaction?.Rollback();
                return false;
            }
            finally
            {
                if(opened)
                    _connection.Close();
            }
        }

        public bool AddCardToDatabase(CardEntity card)
        {
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                opened = true;
            }

            try
            {
                var sql =
                    "INSERT INTO mtcg.Card(id,name,damage,weakdamage,description,elementtype,cardtype,race) VALUES(@id,@name,@damage,@weakdamage,@description,@elementtype,@cardtype,@race)";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("id", card.Id);
                cmd.Parameters.AddWithValue("name", card.Name);
                cmd.Parameters.AddWithValue("damage", card.Damage);
                cmd.Parameters.AddWithValue("description", card.Description);
                cmd.Parameters.AddWithValue("elementtype", card.ElementType);
                cmd.Parameters.AddWithValue("cardtype", card.CardType);
                cmd.Parameters.AddWithValue("race", card.Race);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
            finally
            {
                if(opened)
                    _connection.Close();
            }
        }
        
        public bool AddCardToStack(CardEntity card,UserEntity user)
        {
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                opened = true;
            }
            
            try
            {
                var sql =
                    "INSERT INTO mtcg.r_user_card(userid, cardid, cardplace) VALUES(@userid,@cardid,@cardplace)";
                var cmd = new NpgsqlCommand(sql, _connection);
                

                cmd.Parameters.AddWithValue("userid", user.Id);
                cmd.Parameters.AddWithValue("cardid", card.Id);
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
                if(opened)
                    _connection.Close();
            }
        }
        
        public bool AddCardToUser(List<CardEntity> cards,UserEntity user)
        {
            bool opened = false;
            NpgsqlTransaction transaction = null;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                transaction = _connection.BeginTransaction();
                opened = true;
            }

            try
            {
                foreach (var card in cards)
                {
                    var sql =
                        "INSERT INTO mtcg.r_user_card(userid, cardid) VALUES(@userid,@cardid)";
                    var cmd = new NpgsqlCommand(sql, _connection);
                

                    cmd.Parameters.AddWithValue("userid", user.Id);
                    cmd.Parameters.AddWithValue("cardid", card.Id);
                
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                transaction?.Commit();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                transaction?.Rollback();
                return false;
            }
            finally
            {
                if(opened)
                    _connection.Close();
            }
        }
        
        public bool UpdateCardStatus(CardEntity card,UserEntity user,CardPlace cardPlace)
        {
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                opened = true;
            }

            try
            {
                var sql =
                    "UPDATE mtcg.r_user_card set cardplace=@cardplace where cardid=@cardid AND userid=@userid";
                var cmd = new NpgsqlCommand(sql, _connection);
                

                cmd.Parameters.AddWithValue("userid", user.Id);
                cmd.Parameters.AddWithValue("cardid", card.Id);
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
                if(opened)
                    _connection.Close();
            }
        }


        public bool AddCardToPackage(PackageEntity packageEntity)
        {
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                opened = true;
            }

            try
            {
                foreach (var card in packageEntity.CardsInPackage)
                {
                    var sql =
                        "INSERT INTO mtcg.R_Package_Card (packageid,cardid) VALUES(@packageid,@cardid)";
                    var cmd = new NpgsqlCommand(sql, _connection);

                    cmd.Parameters.AddWithValue("packageid", packageEntity.Id);
                    cmd.Parameters.AddWithValue("cardid", card.Id);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
            finally
            {
                if(opened)
                    _connection.Close();
            }
        }
        
        public bool AddPackage(PackageEntity packageEntity)
        {
            _connection.Open();
            var transaction = _connection.BeginTransaction();

            try
            {
                var sql = "INSERT INTO mtcg.Package (id,amount) VALUES(@id,@amount)";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("id", packageEntity.Id);
                cmd.Parameters.AddWithValue("amount", packageEntity.Amount);

                cmd.Prepare();
                var response = cmd.ExecuteNonQuery();

                if (response > 0)
                    if(AddCardsToDatabase(packageEntity.CardsInPackage))
                        if (AddCardToPackage(packageEntity))
                        {
                            transaction.Commit();
                            return true;
                        }

                transaction.Rollback();
                return false;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }


        public bool OpenPackage(PackageEntity packageEntity,UserEntity userEntity)
        {
            _connection.Open();
            var transaction = _connection.BeginTransaction();

            try
            {
                var sql = "SELECT amount FROM mtcg.package where id=@packageid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("packageid", packageEntity.Id);
                
                cmd.Prepare();
                var response = Convert.ToInt32(cmd.ExecuteScalar());

                if (response < 1)
                {
                    transaction.Commit();
                    return false;
                }
              
                
                if (GetCoinsFromUser(userEntity) < Constant.PRICEPERPACKAGE)
                {
                    transaction.Commit();
                    return false;
                }

                if (!SetCoinsFromUser(userEntity))
                {
                    transaction.Commit();
                    return false;
                }

                sql = "UPDATE mtcg.package set amount=@amount where id=@packageid";
                cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("amount", packageEntity.Amount);
                cmd.Parameters.AddWithValue("packageid", packageEntity.Id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                if(AddCardsToDatabase(packageEntity.CardsInPackage))
                    if (AddCardToUser(packageEntity.CardsInPackage, userEntity))
                    {
                        transaction.Commit();
                        return true;
                    }

                transaction.Rollback();
                return false;

            }
            catch (Exception e)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public int GetCoinsFromUser(UserEntity user)
        {
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var sql = "SELECT coins FROM mtcg.User where id=@userid";
                var cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("userid", user.Id);
                cmd.Prepare();
                var response = Convert.ToInt32(cmd.ExecuteScalar());

                return response;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return 0;
            }
            finally
            {
                if(opened)
                    _connection.Close();
            }
        }
        
        public bool SetCoinsFromUser(UserEntity user)
        {
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var sql = "UPDATE mtcg.User set coins=@newcoins where id=@userid";
                var cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("userid", user.Id);
                cmd.Parameters.AddWithValue("newcoins", user.Coins);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
            finally
            {
                if(opened)
                    _connection.Close();
            }
        }
        
        public List<IPackage> GetPackages()
        {
            _connection.Open();
            
            try
            {
                List<IPackage> retList = new List<IPackage>();
                var sql ="select * from mtcg.package";
                var cmd = new NpgsqlCommand(sql, _connection);
                
                var result = cmd.ExecuteReader();
                
                List<string> packageIds = new List<string>();
                
                while (result.Read())
                {
                   packageIds.Add(result.SafeGetString(0));
                }
                
                result.Close();

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
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                PackageEntity entity = new PackageEntity();

                var sql = "select * from mtcg.package where id = @packageid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("packageid", packageid);
                cmd.Prepare();
                var result = cmd.ExecuteReader();
                
                while (result.Read())
                {
                    entity.Id = result.SafeGetString(0);
                    entity.Amount = result.GetInt32(1);
                }
                result.Close();

                entity.CardsInPackage = GetCardsInPackage(packageid);
                PackageModell model = new PackageModell(entity,this);
                return model;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NpgsqlException();
            }
            finally
            {
                if(opened)
                    _connection.Close();
            }
        }

        public List<CardEntity> GetCardsInPackage(string packageid)
        {
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();

            }
          
            try
            {
                List<CardEntity> ret = new List<CardEntity>();
                var sql =
                    " select cardid from mtcg.package INNER JOIN mtcg.r_package_card ON package.id=r_package_card.packageid WHERE package.id = @packageid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("packageid", packageid);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                List<string> cardIds = new List<string>();

                while (result.Read())
                {
                    cardIds.Add(result.SafeGetString(0));
                }
                result.Close();
                
                
                foreach (var id in cardIds)
                {
                    sql = "select * from mtcg.card where id = @cardid";
                    cmd = new NpgsqlCommand(sql,_connection);

                    cmd.Parameters.AddWithValue("cardid", id);
                    cmd.Prepare();
                    cmd.ExecuteReader();

                    CardEntity entity = null;
                    
                    while (result.Read())
                    {
                        entity = new CardEntity
                        {
                            Id = result.SafeGetString(0),
                            Name = result.SafeGetString(1),
                            Damage = result.GetDouble(2),
                            Description = result.SafeGetString(3),
                            ElementType = (ElementType) result.GetInt32(4),
                            CardType = (CardType) result.GetInt32(5),
                            Race = (Race) result.GetInt32(6)
                        };
                    }
                    
                    
                    if (entity == null)
                    {
                        result.Close();
                        continue;
                    }
                    
                    if(entity != null && string.IsNullOrEmpty(entity.Id))
                        throw new InvalidDataException();
                     
                    ret.Add(entity);
                    result.Close();
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
                if(opened)
                    _connection.Close();
            }
        }
        
         public List<CardEntity> GetStackFromUser(UserEntity userEntity)
        {
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }
          
            try
            {
                List<CardEntity> ret = new List<CardEntity>();
                var sql =
                    " select cardid from mtcg.r_user_card WHERE userid=@userid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("userid", userEntity.Id);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                List<string> cardIds = new List<string>();

                while (result.Read())
                {
                    cardIds.Add(result.SafeGetString(0));
                }
                
                result.Close();
                
                
                foreach (var id in cardIds)
                {
                    sql = "select * from mtcg.card where id = @cardid";
                    cmd = new NpgsqlCommand(sql,_connection);

                    cmd.Parameters.AddWithValue("cardid", id);
                    cmd.Prepare();
                    cmd.ExecuteReader();

                    CardEntity entity = new CardEntity();
                    
                    while (result.Read())
                    {
                        entity.Id = result.SafeGetString(0);
                        entity.Name = result.SafeGetString(1);
                        entity.Damage = result.GetDouble(2);
                        entity.Description = result.SafeGetString(3);
                        entity.ElementType = (ElementType)result.GetInt32(4);
                        entity.CardType = (CardType)result.GetInt32(5);
                        entity.Race = (Race)result.GetInt32(6);
                        entity.CardPlace = (CardPlace) result.GetInt32(7);
                    }
                    if(string.IsNullOrEmpty(entity.Id))
                         throw new InvalidDataException();
                     
                    ret.Add(entity);
                    result.Close();
                }

                return ret;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new NpgsqlException();
            }
            finally
            {
                if(opened)
                    _connection.Close();
            }
        }
         
           public List<CardEntity> GetDeckFromUser(UserEntity userEntity)
        {
            bool opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }
          
            try
            {
                List<CardEntity> ret = new List<CardEntity>();
                var sql =
                    " select cardid from mtcg.r_user_card WHERE userid=@userid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("userid", userEntity.Id);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                List<string> cardIds = new List<string>();

                while (result.Read())
                {
                    cardIds.Add(result.SafeGetString(0));
                }
                
                result.Close();
                
                
                foreach (var id in cardIds)
                {
                    sql = "select * from mtcg.card where id = @cardid AND cardplace=@cardplace";
                    cmd = new NpgsqlCommand(sql,_connection);

                    cmd.Parameters.AddWithValue("cardid", id);
                    cmd.Parameters.AddWithValue("cardplace", (int)CardPlace.Deck);
                    cmd.Prepare();
                    cmd.ExecuteReader();

                    CardEntity entity = null;
                    
                    while (result.Read())
                    {
                        entity = new CardEntity
                        {
                            Id = result.SafeGetString(0),
                            Name = result.SafeGetString(1),
                            Damage = result.GetDouble(2),
                            Description = result.SafeGetString(3),
                            ElementType = (ElementType) result.GetInt32(4),
                            CardType = (CardType) result.GetInt32(5),
                            Race = (Race) result.GetInt32(6),
                            CardPlace = (CardPlace) result.GetInt32(7)
                        };
                    }

                    if (entity == null)
                    {
                        result.Close();
                        continue;
                    }
                    
                    if(entity != null && string.IsNullOrEmpty(entity.Id))
                         throw new InvalidDataException();
                    
                    ret.Add(entity);
                    result.Close();
                }

                return ret;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new NpgsqlException();
            }
            finally
            {
                if(opened)
                    _connection.Close();
            }
        }
           public bool SetDeckByCardIds(List<string> cardIDs,UserEntity userEntity)
           {
               bool opened = false;
               NpgsqlTransaction transaction = null;
            
               if (_connection.State != ConnectionState.Open)
               {
                   _connection.Open();
                   transaction = _connection.BeginTransaction();
                   opened = true;
               }

               try
               {

                   var sql = "SELECT card.id FROM mtcg.card INNER JOIN mtcg.r_user_card ON card.id=r_user_card.cardid where r_user_card.userid=@userid AND card.cardplace=@cardplace";
                   var cmd = new NpgsqlCommand(sql, _connection);

                   cmd.Parameters.AddWithValue("userid", userEntity.Id);
                   cmd.Parameters.AddWithValue("cardplace", (int)CardPlace.Deck);
                   NpgsqlDataReader reader = cmd.ExecuteReader();

                   List<string> oldDeckIds = new List<string>();

                   while (reader.Read())
                   {
                      oldDeckIds.Add(reader.SafeGetString(0)); // Cause Reader is blocking execution from code
                   }
                   reader.Close();

                   foreach (var id in oldDeckIds)
                   {
                       sql = "UPDATE mtcg.card SET cardplace=@cardplace WHERE id = @id";
                       cmd = new NpgsqlCommand(sql, _connection);

                       cmd.Parameters.AddWithValue("id", id);
                       cmd.Parameters.AddWithValue("cardplace", (int)CardPlace.Stack);

                       cmd.ExecuteNonQuery();
                   }

                   sql = "UPDATE mtcg.card SET cardplace=@cardplace WHERE id = ANY(@list)";
                   cmd = new NpgsqlCommand(sql, _connection);

                   cmd.Parameters.AddWithValue("cardplace", (int)CardPlace.Deck);
                   cmd.Parameters.AddWithValue("list",cardIDs);
                   var result =cmd.ExecuteNonQuery();

                   if (result == cardIDs.Count)
                   {                       
                        transaction?.Commit();
                        return true;
                   }

                   transaction?.Rollback();
                   return false;
               }
               catch (Exception e)
               {
                   Console.WriteLine(e);
                   transaction?.Rollback();
                   return false;
               }
               finally
               {
                   if(opened)
                       _connection.Close();
               }
           }
    }
}