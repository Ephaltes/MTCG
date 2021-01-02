using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MTCG.Entity;
using MTCG.Helpers;
using MTCG.Interface;
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
                Log.Error(e.Message);
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
                Log.Error(e.Message);
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

                var ret = new UserEntity();

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
                Log.Error(e.Message);
                throw new NpgsqlException();
            }
            finally
            {
                _connection.Close();
            }
        }

        public UserEntity GetUserById(int id)
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var sql = "SELECT * from mtcg.user where id=@id";
                using var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("id", id);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                var ret = new UserEntity();

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

                result.Close();
                return ret;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new NpgsqlException();
            }
            finally
            {
                if (opened)
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

                var ret = new UserEntity();

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
                Log.Error(e.Message);
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
                var sql =
                    "UPDATE mtcg.user SET password=@password, description=@description, displayname=@displayname, image=@image, salt=@salt where token=@token";
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
            var opened = false;
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
                    cmd.Parameters.AddWithValue("description", card.Description ?? string.Empty);
                    cmd.Parameters.AddWithValue("elementtype", (int) card.ElementType);
                    cmd.Parameters.AddWithValue("cardtype", (int) card.CardType);
                    cmd.Parameters.AddWithValue("race", (int) card.Race);
                    cmd.Parameters.AddWithValue("cardplace", (int) card.CardPlace);

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    transaction?.Commit();
                }

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
                if (opened)
                    _connection.Close();
            }
        }

        public bool AddCardToDatabase(CardEntity card)
        {
            var opened = false;
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
                if (opened)
                    _connection.Close();
            }
        }

        public bool AddCardToStack(CardEntity card, UserEntity user)
        {
            var opened = false;
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
                Log.Error(e.Message);
                return false;
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public bool UpdateCardStatus(CardEntity card, UserEntity user, CardPlace cardPlace)
        {
            var opened = false;
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
                Log.Error(e.Message);
                return false;
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }


        public bool AddCardToPackage(PackageEntity packageEntity)
        {
            var opened = false;
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
                if (opened)
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
                    if (AddCardsToDatabase(packageEntity.CardsInPackage))
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


        public bool OpenPackage(PackageEntity packageEntity, UserEntity userEntity)
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

                if (AddCardsToDatabase(packageEntity.CardsInPackage))
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

        public List<IPackage> GetPackages()
        {
            _connection.Open();

            try
            {
                var retList = new List<IPackage>();
                var sql = "select * from mtcg.package";
                var cmd = new NpgsqlCommand(sql, _connection);

                var result = cmd.ExecuteReader();

                var packageIds = new List<string>();

                while (result.Read()) packageIds.Add(result.SafeGetString(0));

                result.Close();

                foreach (var id in packageIds) retList.Add(GetPackage(id));

                return retList;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }

        public IPackage GetPackage(string packageid)
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var entity = new PackageEntity();

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
                var model = new PackageModell(this) {Entity = entity};
                return model;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new NpgsqlException();
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public List<CardEntity> GetCardsInPackage(string packageid)
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var ret = new List<CardEntity>();
                var sql =
                    " select cardid from mtcg.package INNER JOIN mtcg.r_package_card ON package.id=r_package_card.packageid WHERE package.id = @packageid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("packageid", packageid);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                var cardIds = new List<string>();

                while (result.Read()) cardIds.Add(result.SafeGetString(0));
                result.Close();


                foreach (var id in cardIds)
                {
                    sql = "select * from mtcg.card where id = @cardid";
                    cmd = new NpgsqlCommand(sql, _connection);

                    cmd.Parameters.AddWithValue("cardid", id);
                    cmd.Prepare();
                    cmd.ExecuteReader();

                    CardEntity entity = null;

                    while (result.Read())
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


                    if (entity == null)
                    {
                        result.Close();
                        continue;
                    }

                    if (entity != null && string.IsNullOrEmpty(entity.Id))
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
                if (opened)
                    _connection.Close();
            }
        }


        public List<CardEntity> GetStackFromUser(UserEntity userEntity)
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var ret = new List<CardEntity>();
                var sql =
                    " select cardid from mtcg.r_user_card WHERE userid=@userid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("userid", userEntity.Id);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                var cardIds = new List<string>();

                while (result.Read()) cardIds.Add(result.SafeGetString(0));

                result.Close();


                foreach (var id in cardIds)
                {
                    sql = "select * from mtcg.card where id = @cardid";
                    cmd = new NpgsqlCommand(sql, _connection);

                    cmd.Parameters.AddWithValue("cardid", id);
                    cmd.Prepare();
                    cmd.ExecuteReader();

                    var entity = new CardEntity();

                    while (result.Read())
                    {
                        entity.Id = result.SafeGetString(0);
                        entity.Name = result.SafeGetString(1);
                        entity.Damage = result.GetDouble(2);
                        entity.Description = result.SafeGetString(3);
                        entity.ElementType = (ElementType) result.GetInt32(4);
                        entity.CardType = (CardType) result.GetInt32(5);
                        entity.Race = (Race) result.GetInt32(6);
                        entity.CardPlace = (CardPlace) result.GetInt32(7);
                    }

                    if (string.IsNullOrEmpty(entity.Id))
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
                if (opened)
                    _connection.Close();
            }
        }

        public List<CardEntity> GetDeckFromUser(UserEntity userEntity)
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var ret = new List<CardEntity>();
                var sql =
                    " select cardid from mtcg.r_user_card WHERE userid=@userid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("userid", userEntity.Id);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

                var cardIds = new List<string>();

                while (result.Read()) cardIds.Add(result.SafeGetString(0));

                result.Close();


                foreach (var id in cardIds)
                {
                    sql = "select * from mtcg.card where id = @cardid AND cardplace=@cardplace";
                    cmd = new NpgsqlCommand(sql, _connection);

                    cmd.Parameters.AddWithValue("cardid", id);
                    cmd.Parameters.AddWithValue("cardplace", (int) CardPlace.Deck);
                    cmd.Prepare();
                    cmd.ExecuteReader();

                    CardEntity entity = null;

                    while (result.Read())
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

                    if (entity == null)
                    {
                        result.Close();
                        continue;
                    }

                    if (entity != null && string.IsNullOrEmpty(entity.Id))
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
                if (opened)
                    _connection.Close();
            }
        }

        public bool SetDeckByCardIds(List<string> cardIDs, UserEntity userEntity)
        {
            var opened = false;
            NpgsqlTransaction transaction = null;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                transaction = _connection.BeginTransaction();
                opened = true;
            }

            try
            {
                var sql = 
                    "SELECT card.id FROM mtcg.card INNER JOIN mtcg.r_user_card ON card.id=r_user_card.cardid where r_user_card.userid=@userid AND card.id= ANY(@list)";
                var cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("userid", userEntity.Id);
                cmd.Parameters.AddWithValue("list", cardIDs);
                var reader = cmd.ExecuteReader();
                int cardnumber = 0;

                while (reader.Read())
                    cardnumber++;
                if (cardnumber != cardIDs.Count)
                    return false;
                
                reader.Close();

                sql =
                    "SELECT card.id FROM mtcg.card INNER JOIN mtcg.r_user_card ON card.id=r_user_card.cardid where r_user_card.userid=@userid AND card.cardplace=@cardplace";
                cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("userid", userEntity.Id);
                cmd.Parameters.AddWithValue("cardplace", (int) CardPlace.Deck); 
                reader = cmd.ExecuteReader();

                var oldDeckIds = new List<string>();

                while (reader.Read())
                    oldDeckIds.Add(reader.SafeGetString(0)); // Cause Reader is blocking execution from code
                reader.Close();

                foreach (var id in oldDeckIds)
                {
                    sql = "UPDATE mtcg.card SET cardplace=@cardplace WHERE id = @id";
                    cmd = new NpgsqlCommand(sql, _connection);

                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("cardplace", (int) CardPlace.Stack);

                    cmd.ExecuteNonQuery();
                }

                sql = "UPDATE mtcg.card SET cardplace=@cardplace WHERE id = ANY(@list) AND cardplace != @transaction";
                cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("cardplace", (int) CardPlace.Deck);
                cmd.Parameters.AddWithValue("list", cardIDs);
                cmd.Parameters.AddWithValue("transaction", (int)CardPlace.Transaction);
                var result = cmd.ExecuteNonQuery();

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
                Log.Error(e.Message);
                transaction?.Rollback();
                return false;
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public bool AddCardToDeckByCardId(string cardId, UserEntity userEntity)
        {
            var opened = false;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                opened = true;
            }

            try
            {
                var sql = 
                    "SELECT card.id FROM mtcg.card INNER JOIN mtcg.r_user_card ON card.id=r_user_card.cardid where r_user_card.userid=@userid AND card.id= @id";
                var cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("userid", userEntity.Id);
                cmd.Parameters.AddWithValue("id", cardId);
                var cardid = (string)cmd.ExecuteScalar();

                if (string.IsNullOrEmpty(cardid))
                    return false;
                
                sql =
                    "SELECT card.id FROM mtcg.card INNER JOIN mtcg.r_user_card ON card.id=r_user_card.cardid where r_user_card.userid=@userid AND card.cardplace=@cardplace";
                cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("userid", userEntity.Id);
                cmd.Parameters.AddWithValue("cardplace", (int) CardPlace.Deck);
                var reader = cmd.ExecuteReader();

                var oldDeckIds = new List<string>();

                while (reader.Read())
                    oldDeckIds.Add(reader.SafeGetString(0)); // Cause Reader is blocking execution from code
                reader.Close();

                if (oldDeckIds.Count >= Constant.MAXCARDSINDECK)
                    return false;

                sql = "UPDATE mtcg.card SET cardplace=@cardplace WHERE id = @id AND cardplace != @transaction";
                cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("cardplace", (int) CardPlace.Deck);
                cmd.Parameters.AddWithValue("id", cardId);
                cmd.Parameters.AddWithValue("transaction", (int)CardPlace.Transaction);
                var result = cmd.ExecuteNonQuery();

                if (result == 1) return true;

                return false;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public bool RemoveCardFromDeckByCardId(string cardId, UserEntity userEntity)
        {
            var opened = false;

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                opened = true;
            }

            try
            {
                var sql =
                    "SELECT card.id FROM mtcg.card INNER JOIN mtcg.r_user_card ON card.id=r_user_card.cardid where r_user_card.userid=@userid AND card.cardplace=@cardplace";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("userid", userEntity.Id);
                cmd.Parameters.AddWithValue("cardplace", (int) CardPlace.Deck);
                var reader = cmd.ExecuteReader();

                var oldDeckIds = new List<string>();

                while (reader.Read())
                    oldDeckIds.Add(reader.SafeGetString(0)); // Cause Reader is blocking execution from code
                reader.Close();

                foreach (var id in oldDeckIds)
                    if (id == cardId)
                    {
                        sql = "UPDATE mtcg.card SET cardplace=@cardplace WHERE id = @id";
                        cmd = new NpgsqlCommand(sql, _connection);

                        cmd.Parameters.AddWithValue("id", id);
                        cmd.Parameters.AddWithValue("cardplace", (int) CardPlace.Stack);

                        cmd.ExecuteNonQuery();
                    }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public List<UserEntity> LoadScoreBoard()
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                opened = true;
            }

            try
            {
                var sql = "SELECT * FROM mtcg.user order by elo desc limit @limit";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("limit", Constant.TOP10);

                var reader = cmd.ExecuteReader();
                var retList = new List<UserEntity>();

                while (reader.Read())
                {
                    var entity = new UserEntity
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.SafeGetString(1),
                        Password = reader.SafeGetString(2),
                        Salt = reader.SafeGetString(3),
                        Token = reader.SafeGetString(4),
                        Description = reader.SafeGetString(5),
                        DisplayName = reader.SafeGetString(6),
                        Image = reader.SafeGetString(7),
                        Elo = reader.GetInt32(8),
                        Win = reader.GetInt32(9),
                        Lose = reader.GetInt32(10),
                        Draw = reader.GetInt32(11),
                        Coins = reader.GetInt32(12)
                    };

                    retList.Add(entity);
                }

                reader.Close();

                if (retList.Count < 1)
                    return null;

                return retList;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        //
        public bool UpdateElo(UserEntity me, UserEntity enemy, bool won = true)
        {
            //Formel : https://medium.com/purple-theory/what-is-elo-rating-c4eb7a9061e0
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                opened = true;
            }

            try
            {
                var sql = "SELECT elo from mtcg.user where id=@enemyid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("enemyid", enemy.Id);

                var enemyElo = (int) cmd.ExecuteScalar();

                sql = "SELECT elo from mtcg.user where id=@id";
                cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("id", me.Id);

                var myElo = (int) cmd.ExecuteScalar();

                double estimatedFactor = 1 / (1 + (10 ^ ((enemyElo - myElo) / 400)));
                var newScore = Convert.ToInt32(myElo + (Constant.kFactor * (won ? 1 : 0 - estimatedFactor)));

                sql = "UPDATE mtcg.user SET elo=@newscore where id=@id";
                cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("newscore", newScore);
                cmd.Parameters.AddWithValue("id", me.Id);

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
                if (opened)
                    _connection.Close();
            }
        }

        public bool AddCardToUser(List<CardEntity> cards, UserEntity user)
        {
            var opened = false;
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
                if (opened)
                    _connection.Close();
            }
        }

        public int GetCoinsFromUser(UserEntity user)
        {
            var opened = false;
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
                if (opened)
                    _connection.Close();
            }
        }

        public bool SetCoinsFromUser(UserEntity user)
        {
            var opened = false;
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
                if (opened)
                    _connection.Close();
            }
        }

        public List<TradingEntity> GetAllTradingDeals()
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var sql = "SELECT * FROM mtcg.trading";
                var cmd = new NpgsqlCommand(sql, _connection);

                var reader = cmd.ExecuteReader();

                List<TradingEntity> tradingList = new List<TradingEntity>();

                while (reader.Read())
                {
                    var entity = new TradingEntity()
                    {
                        Id = reader.SafeGetString(0),
                        CardToTrade = new CardEntity()
                        {
                            Id = reader.SafeGetString(1),
                        },
                        WantCardType = (CardType) reader.GetInt32(2),
                        WantMinDamage = reader.GetDouble(3),
                        WantRace = (Race) reader.GetInt32(4),
                        WantElementType = (ElementType) reader.GetInt32(5),
                        UserEntity = new UserEntity()
                        {
                            Id = reader.GetInt32(6)
                        }
                    };
                    tradingList.Add(entity);
                }

                reader.Close();

                foreach (var tradingEntity in tradingList)
                {
                    tradingEntity.CardToTrade = GetCardById(tradingEntity.CardToTrade.Id);
                    tradingEntity.UserEntity = GetUserById(tradingEntity.UserEntity.Id);

                    if (tradingEntity.CardToTrade == null || tradingEntity.UserEntity == null)
                        return null;
                }

                return tradingList;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public CardEntity GetCardById(string cardid)
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var sql = "SELECT * FROM mtcg.card where id=@id";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("id", cardid);
                cmd.Prepare();
                var result = cmd.ExecuteReader();

                if (!result.HasRows)
                    return null;

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

                result.Close();
                return entity;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new NpgsqlException();
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public bool RemoveTradeByUserRequest(string tradeId, int userid)
        {
            var opened = false;
            NpgsqlTransaction transaction = null;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
                transaction = _connection.BeginTransaction();
            }

            try
            {
                var tradeEntity = GetTradeById(tradeId);
                
                if (tradeEntity == null)
                    return false;
                
                var sql = "DELETE FROM mtcg.trading where userid=@userid AND id=@id";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("id", tradeEntity.Id);
                cmd.Parameters.AddWithValue("userid", userid);
                cmd.Prepare();
                var result = cmd.ExecuteNonQuery();

                if (result == 0)
                    return false;
                
                sql = "UPDATE mtcg.card SET cardplace=@cardplace WHERE id=@id";
                cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("cardplace", (int)CardPlace.Stack);
                cmd.Parameters.AddWithValue("id", tradeEntity.CardToTrade.Id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                transaction?.Commit();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                transaction?.Rollback();
                throw new NpgsqlException();
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public bool AddTrade(TradingEntity entity)
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var sql = "SELECT id FROM mtcg.r_user_card WHERE userid=@userid AND cardid=@cardid";
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("userid", entity.UserEntity.Id);
                cmd.Parameters.AddWithValue("cardid", entity.CardToTrade.Id);
                cmd.Prepare();
                var result = cmd.ExecuteScalar(); //id der karte

                if (result == null || (int) result < 1)
                    return false;

                sql = "UPDATE mtcg.card SET cardplace=@cardplace WHERE id=@id";
                cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("cardplace", (int)CardPlace.Transaction);
                cmd.Parameters.AddWithValue("id", entity.CardToTrade.Id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                sql =
                    "INSERT INTO mtcg.trading (id,cardtotrade,cardtype,mindamage,race,elementtype,userid) VALUES(@id,@cardtotrade,@cardtype,@mindamage,@race,@elementtype,@userid)";
                cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("id", entity.Id);
                cmd.Parameters.AddWithValue("cardtotrade", entity.CardToTrade.Id);
                cmd.Parameters.AddWithValue("cardtype", (int) entity.WantCardType);
                cmd.Parameters.AddWithValue("mindamage", entity.WantMinDamage);
                cmd.Parameters.AddWithValue("race", (int) entity.WantRace);
                cmd.Parameters.AddWithValue("elementtype", (int) entity.WantElementType);
                cmd.Parameters.AddWithValue("userid", entity.UserEntity.Id);
                cmd.Prepare();
                result = cmd.ExecuteNonQuery();

                if ((int) result == 0)
                    return false;

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw new NpgsqlException();
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public TradingEntity GetTradeById(string tradeid)
        {
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
            }

            try
            {
                var sql = "SELECT * FROM mtcg.trading WHERE id=@id";
                var cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("id", tradeid);
                cmd.Prepare();
                
                var reader = cmd.ExecuteReader();

                TradingEntity entity = null;

                while (reader.Read())
                {
                    entity = new TradingEntity()
                    {
                        Id = reader.SafeGetString(0),
                        CardToTrade = new CardEntity()
                        {
                            Id = reader.SafeGetString(1),
                        },
                        WantCardType = (CardType) reader.GetInt32(2),
                        WantMinDamage = reader.GetDouble(3),
                        WantRace = (Race) reader.GetInt32(4),
                        WantElementType = (ElementType) reader.GetInt32(5),
                        UserEntity = new UserEntity()
                        {
                            Id = reader.GetInt32(6)
                        }
                    };
                }
                reader.Close();

                if (entity == null)
                    return null;

                entity.CardToTrade = GetCardById(entity.CardToTrade.Id);
                entity.UserEntity = GetUserById(entity.UserEntity.Id);

                if (entity.CardToTrade == null || entity.UserEntity == null)
                    return null;

                return entity;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }

        public bool TradeCards(string tradeid, string cardToTrade,UserEntity userEntity)
        {
            var opened = false;
            NpgsqlTransaction transaction = null;
            if (_connection.State != ConnectionState.Open)
            {
                opened = true;
                _connection.Open();
                transaction = _connection.BeginTransaction();
            }

            try
            {
                var tradeEntity = GetTradeById(tradeid);

                if (tradeEntity.UserEntity.Id == userEntity.Id)
                    return false;
                
                var cardEntity = GetCardById(cardToTrade);

                if (cardEntity.CardPlace == CardPlace.Transaction)
                    return false;

                if (!(cardEntity.Damage >= tradeEntity.WantMinDamage)) return false;

                if (tradeEntity.WantCardType != CardType.Unknown && tradeEntity.WantCardType != cardEntity.CardType) return false;
                
                if (tradeEntity.WantRace != Race.Unknow && tradeEntity.WantRace != cardEntity.Race) return false;
                
                if (tradeEntity.WantElementType != ElementType.Unknown && tradeEntity.WantElementType != cardEntity.ElementType) return false;

                var sql = "UPDATE mtcg.r_user_card SET userid=@newuser WHERE cardid=@cardid";
                var cmd = new NpgsqlCommand(sql, _connection);
                
                cmd.Parameters.AddWithValue("newuser", userEntity.Id);
                cmd.Parameters.AddWithValue("cardid", tradeEntity.CardToTrade.Id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("newuser", tradeEntity.UserEntity.Id);
                cmd.Parameters.AddWithValue("cardid", cardEntity.Id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                sql = "UPDATE mtcg.card SET cardplace=@cardplace WHERE id=@card1 OR id=@card2";
                cmd = new NpgsqlCommand(sql, _connection);
                cmd.Parameters.AddWithValue("cardplace", (int) CardPlace.Stack);
                cmd.Parameters.AddWithValue("card1", tradeEntity.CardToTrade.Id);
                cmd.Parameters.AddWithValue("card2", cardEntity.Id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                if (RemoveTradeByUserRequest(tradeEntity.Id, tradeEntity.UserEntity.Id))
                {
                    transaction?.Commit();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                transaction?.Rollback();
                throw new NpgsqlException();
            }
            finally
            {
                if (opened)
                    _connection.Close();
            }
        }
        
        public bool UpdateScore(UserEntity me, ScoreUpdate update)
        {
            //Formel : https://medium.com/purple-theory/what-is-elo-rating-c4eb7a9061e0
            var opened = false;
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
                opened = true;
            }

            try
            {

                var sql = update == ScoreUpdate.win
                    ? $"UPDATE mtcg.user SET {update.ToString()}={update.ToString()}+1 , coins=coins+1 where id=@id"
                    : $"UPDATE mtcg.user SET {update.ToString()}={update.ToString()}+1 where id=@id";
                
                var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("id", me.Id);

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
                if (opened)
                    _connection.Close();
            }
        }
    }
}