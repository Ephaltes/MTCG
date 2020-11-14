using System;
using System.Collections.Generic;
using System.IO;
using MTCG.Entity;
using MTCG.Interface;
using MTCG.Model.BaseClass;
using Npgsql;

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

        public bool AddCardsToDatabase(List<CardModell> cardsToAdd)
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

                    switch (card.CardType)
                    {
                        case CardType.MonsterCard:
                            var monstercard = card as MonsterCardModell;

                            cmd.Parameters.AddWithValue("id", monstercard.Id);
                            cmd.Parameters.AddWithValue("name", monstercard.Name);
                            cmd.Parameters.AddWithValue("damage", monstercard.Damage);
                            cmd.Parameters.AddWithValue("weakdamage", 0);
                            cmd.Parameters.AddWithValue("description", monstercard.Description);
                            cmd.Parameters.AddWithValue("elementtype", monstercard.ElementType);
                            cmd.Parameters.AddWithValue("cardtype", monstercard.CardType);
                            cmd.Parameters.AddWithValue("race", monstercard.Race);
                            break;

                        case CardType.SpellCard:
                            var spellcard = card as SpellCardModell;

                            cmd.Parameters.AddWithValue("id", spellcard.Id);
                            cmd.Parameters.AddWithValue("name", spellcard.Name);
                            cmd.Parameters.AddWithValue("damage", spellcard.Damage);
                            cmd.Parameters.AddWithValue("weakdamage", spellcard.WeakDamage);
                            cmd.Parameters.AddWithValue("description", spellcard.Description);
                            cmd.Parameters.AddWithValue("elementtype", spellcard.ElementType);
                            cmd.Parameters.AddWithValue("cardtype", spellcard.CardType);
                            cmd.Parameters.AddWithValue("race", 0);
                            break;
                        default:
                            return false;
                    }

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

        public bool AddCardToDatabase(CardModell card)
        {
            _connection.Open();
            var transaction = _connection.BeginTransaction();

            try
            {
                var sql =
                    "INSERT INTO mtcg.Card(id,name,damage,weakdamage,description,elementtype,cardtype,race) VALUES(@id,@name,@damage,@weakdamage,@description,@elementtype,@cardtype,@race)";
                var cmd = new NpgsqlCommand(sql, _connection);

                switch (card.CardType)
                {
                    case CardType.MonsterCard:
                        var monstercard = card as MonsterCardModell;

                        cmd.Parameters.AddWithValue("id", monstercard.Id);
                        cmd.Parameters.AddWithValue("name", monstercard.Name);
                        cmd.Parameters.AddWithValue("damage", monstercard.Damage);
                        cmd.Parameters.AddWithValue("weakdamage", 0);
                        cmd.Parameters.AddWithValue("description", monstercard.Description);
                        cmd.Parameters.AddWithValue("elementtype", monstercard.ElementType);
                        cmd.Parameters.AddWithValue("cardtype", monstercard.CardType);
                        cmd.Parameters.AddWithValue("race", monstercard.Race);
                        break;

                    case CardType.SpellCard:
                        var spellcard = card as SpellCardModell;

                        cmd.Parameters.AddWithValue("id", spellcard.Id);
                        cmd.Parameters.AddWithValue("name", spellcard.Name);
                        cmd.Parameters.AddWithValue("damage", spellcard.Damage);
                        cmd.Parameters.AddWithValue("weakdamage", spellcard.WeakDamage);
                        cmd.Parameters.AddWithValue("description", spellcard.Description);
                        cmd.Parameters.AddWithValue("elementtype", spellcard.ElementType);
                        cmd.Parameters.AddWithValue("cardtype", spellcard.CardType);
                        cmd.Parameters.AddWithValue("race", 0);
                        break;
                    default:
                        return false;
                }

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
        
        public bool AddCardToStack(CardModell card,UserEntity user)
        {
            _connection.Open();
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
                _connection.Close();
            }
        }
        
        public bool UpdateCardStatus(CardModell card,UserEntity user,CardPlace cardPlace)
        {
            _connection.Open();
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
                _connection.Close();
            }
        }

        public List<PackageModell> GetPackages()
        {
            _connection.Open();
            
            try
            {
                List<PackageModell> retList = new List<PackageModell>();
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

        public PackageModell GetPackage(string packageid)
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

        public List<CardModell> GetCardsInPackage(string packageid)
        {
           _connection.Open();
          
            try
            {
                List<CardModell> ret = new List<CardModell>();
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
                        entity.WeakDamage = result.GetDouble(3);
                        entity.Description = result.GetString(4);
                        entity.ElementType = (ElementType)result.GetInt32(5);
                        entity.CardType = (CardType)result.GetInt32(6);
                        entity.Race = (Race)result.GetInt32(7);
                    }

                    var model = ConvertToCardModell(entity);
                     
                     if(model == null)
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

        protected CardModell ConvertToCardModell(CardEntity entity)
        {
            if (entity.CardType == CardType.MonsterCard)
            {
                return new MonsterCardModell(entity);
            }

            if (entity.CardType == CardType.SpellCard)
            {
                return new SpellCardModell(entity);
            }

            return null;
        }
    }
}