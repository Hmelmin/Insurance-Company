using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace coursev1
{

    static class DatabaseManager
    {

        static string Connect = "Database=testtb;Data Source=localhost;User Id=root;Password=12345678";
        public static void InsertClient(string login, string pass)
        {
            int id = GetMaxIdFromUsers() + 1;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "INSERT INTO users VALUES(@id, @log, @pass, 1)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@log", login);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int GetMaxIdFromUsers()
        {
            MySqlDataReader rdr = null;
            int result = -1;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT MAX(ID) AS x FROM USERS";
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    result = rdr.GetInt32(0);

                }



                myConnection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public static int GetMaxIdFromInsurances()
        {
            MySqlDataReader rdr = null;
            int result = -1;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT MAX(ID) AS x FROM contracts";
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    result = rdr.GetInt32(0);

                }



                myConnection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        public static int GetTypeOfUser(string log, string pass)
        {
            MySqlDataReader rdr = null;
            int result = -1;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT type  FROM USERS WHERE login = @log AND password = @pass ";
                cmd.Parameters.AddWithValue("@log", log);
                cmd.Parameters.AddWithValue("@pass", pass);

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    result = rdr.GetInt32(0);

                }



                myConnection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public static int GetIdOfUser(string log, string pass)
        {
            MySqlDataReader rdr = null;
            int result = -1;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT id  FROM USERS WHERE login = @log AND password = @pass ";
                cmd.Parameters.AddWithValue("@log", log);
                cmd.Parameters.AddWithValue("@pass", pass);

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    result = rdr.GetInt32(0);

                }



                myConnection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public static void UpdateStatus(insurance item, int status, int instance)
        {
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "UPDATE contracts  SET status = @status, instance = @instance  where id = @id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@instance", instance);
                cmd.Parameters.AddWithValue("@id", item.GetId());

                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void InsertInsurance(insurance item)
        {
            int id = DatabaseManager.GetMaxIdFromInsurances() + 1;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "INSERT INTO contracts VALUES(@id, @client, @payout, @mounthly, @ballance, @description, @status, @message_in, @message_out, @type, @instance , @message_between )";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@client", item.GetClient());
                cmd.Parameters.AddWithValue("@payout", item.GetPayout());
                cmd.Parameters.AddWithValue("@mounthly", item.GetMounthlyPay());
                cmd.Parameters.AddWithValue("@ballance", item.GetBallance());
                cmd.Parameters.AddWithValue("@description", item.GetDescription());
                cmd.Parameters.AddWithValue("@status", item.GetStatus());
                cmd.Parameters.AddWithValue("@message_in", item.GetMessageIn());
                cmd.Parameters.AddWithValue("@message_out", "?");
                cmd.Parameters.AddWithValue("@type", item.GetThisType());
                cmd.Parameters.AddWithValue("@instance", 0);
                cmd.Parameters.AddWithValue("@message_between", "?");


                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;

            }
        }
        public static int GetMaxIdFromContracts()
        {
            MySqlDataReader rdr = null;
            int result = -1;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT MAX(ID) AS x FROM contracts";
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    result = rdr.GetInt32(0);

                }



                myConnection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        public static List<insurance> GetRequiredInsurances(int value)
        {
            MySqlDataReader rdr = null;
            List<insurance> result = new List<insurance>();
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT * FROM contracts WHERE status = @status AND instance = 0";
                cmd.Parameters.AddWithValue("@status", value);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {


                    insurance x = new insurance(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetString(5), rdr.GetInt32(6), rdr.GetInt32(9));
                    x.SetMessageIn(rdr.GetString(7));
                    x.SetMessageOut(rdr.GetString(8));
                    x.SetMessageBetween(rdr.GetString(11));
                    result.Add(x);
                }
            }
            catch (Exception e)
            {
                throw e;
            }



            return result;

        }
        public static void UpdateBallance(insurance item, int amount)
        {
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "UPDATE contracts  SET ballance = ballance + @x  where id = @id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@x", amount);
                cmd.Parameters.AddWithValue("@id", item.GetId());

                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public static List<insurance> GetInsurancesById(int value)
        {
            MySqlDataReader rdr = null;
            List<insurance> result = new List<insurance>();
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT * FROM contracts WHERE client_id = @id";
                cmd.Parameters.AddWithValue("@id", value);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {


                    insurance x = new insurance(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetString(5), rdr.GetInt32(6), rdr.GetInt32(9));
                    x.SetMessageIn(rdr.GetString(7));
                    x.SetMessageOut(rdr.GetString(8));
                    x.SetMessageBetween(rdr.GetString(11));
                    result.Add(x);
                }
            }
            catch (Exception e)
            {
                throw e;
            }



            return result;

        }

        public static void UpdateAcct(int user_id, int amount)
        {
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "UPDATE clients  SET acct = acct + @x  where id = @id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@x", amount);
                cmd.Parameters.AddWithValue("@id", user_id);

                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetType(int id)
        {
            string result = "";
            MySqlDataReader rdr = null;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT type  FROM types WHERE id = @id ";
                cmd.Parameters.AddWithValue("@id", id);
                

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    result = rdr.GetString(0);
                }



                myConnection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }

            return result;

        }

        public static string GetClientInfo(int id)
        {
            string result = "";
            MySqlDataReader rdr = null;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT lastname, name, birth FROM clients WHERE id = @id ";
                cmd.Parameters.AddWithValue("@id", id);


                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    result = rdr.GetString(1) + " " + rdr.GetString(0) + " " + rdr.GetString(2) + " ";
                }



                myConnection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }


        public static string GetClientPhone(int id)
        {
            string result = "";
            MySqlDataReader rdr = null;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT phone FROM clients WHERE id = @id ";
                cmd.Parameters.AddWithValue("@id", id);


                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    result = rdr.GetString(0) ;
                }



                myConnection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public static void SetMessage_Out(int id, string message)
        {
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "UPDATE contracts  SET message_out = @message  where id = @id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@id",id);

                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SetMessage_In(int id, string message)
        {
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "UPDATE contracts  SET message_in = @message  where id = @id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void SetPayment(int id, int amount)
        {
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "INSERT INTO payments VALUES(@id, @date, @amount)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@date", Convert.ToString(DateTime.Today).Remove(10));
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Payment> GetHistoryById(int id)
        {
            MySqlDataReader rdr = null;
            List<Payment> result = new List<Payment>();
            try
            {
                
                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT * FROM payments WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Payment x = new Payment(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2));
                    result.Add(x);
                }
            }

            catch(Exception e)
            {
                throw e;
            }

            return result;
        }

        public static List<insurance> GetInsurancesToCheck( int instance)
        {
            List<insurance> result = new List<insurance>();
            MySqlDataReader rdr = null;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT * FROM contracts WHERE status = 4 AND instance = @instance";
                
                cmd.Parameters.AddWithValue("@instance", instance);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {


                    insurance x = new insurance(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetString(5), rdr.GetInt32(6), rdr.GetInt32(9));
                    x.SetMessageIn(rdr.GetString(7));
                    x.SetMessageOut(rdr.GetString(8));
                    x.SetMessageBetween(rdr.GetString(11));
                    result.Add(x);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        public static void SetMessage_Between(int id, string message)
        {
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "UPDATE contracts  SET message_between = @message  where id = @id";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool CheckUser(string login, string password)
        {
            bool result = false;
            MySqlDataReader rdr = null; 
                try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT type  FROM USERS WHERE login = @log OR password = @pass ";
                cmd.Parameters.AddWithValue("@log", login);
                cmd.Parameters.AddWithValue("@pass", password);

                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    result = true;

                }



                myConnection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public static int GetMaxIdFromClients()
        {
            MySqlDataReader rdr = null;
            int result = -1;
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "SELECT MAX(ID) AS x FROM clients";
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    result = rdr.GetInt32(0);

                }
                myConnection.Close();


            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public static void InsertClientInfo(string lastname, string name, string phone, string birth)
        {
            int id = GetMaxIdFromUsers();
            try
            {

                MySqlConnection myConnection = new MySqlConnection(Connect);
                myConnection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = myConnection;
                cmd.CommandText = "INSERT INTO clients VALUES(@id, @lastname ,@name , @phone, @birth, 0)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@birth", birth);
                cmd.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

