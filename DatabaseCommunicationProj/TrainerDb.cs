using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseCommunicationProj
{
    public class TrainerDb
    {
        string connectionString = null;

        public TrainerDb()
        {
            connectionString =
                ConfigurationManager.ConnectionStrings["B22DB"].ConnectionString;
        }

        // 1. Select All Trainers
        public void AllTrainers(out List<Trainer> trainers, out List<Student> students)
        {
            #region selecting record from one table

            //List<Trainer> trainers = new List<Trainer>();

            //string connectionString = 
            //    "server=.\\sqlexpress;database=B22DB;integrated security=true;";

            ////SqlConnection con = new SqlConnection();
            ////con.ConnectionString = connectionString;

            //SqlConnection con = new SqlConnection(connectionString);
            //// con.Open();

            //string cmdText = "select Id, Name, City, Experience from Trainer";
            //SqlCommand cmd = new SqlCommand(cmdText, con);

            //con.Open(); // open connection as late as possible
            //SqlDataReader reader = cmd.ExecuteReader();

            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        Trainer t = new Trainer();
            //        t.Id = (int) reader["Id"];
            //        t.Name = reader["Name"].ToString();
            //        t.City = reader["City"].ToString();
            //        t.Experience = (int) reader["Experience"];

            //        trainers.Add(t);
            //    }
            //}

            //con.Close();

            //return trainers;

            #endregion selecting record from one table

            #region selecting record from two table

            //trainers = new List<Trainer>();
            //students = new List<Student>();

            //string connectionString =
            //    "server=.\\sqlexpress;database=B22DB;integrated security=true;";

            //SqlConnection con = new SqlConnection(connectionString);

            //string cmdText = "select Id, Name, City, Experience from Trainer;select RollNumber, Name, Gender, TrainerId from Student";
            //SqlCommand cmd = new SqlCommand(cmdText, con);

            //con.Open();
            //SqlDataReader reader = cmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    Trainer t = new Trainer()
            //    {
            //        Id = (int)reader["Id"],
            //        Name = reader["Name"].ToString(),
            //        City = reader["City"].ToString(),
            //        Experience = (int)reader["Experience"]
            //    };
            //    trainers.Add(t);
            //}

            //reader.NextResult(); // it will point to next resultset

            //while (reader.Read())
            //{
            //    Student s = new Student()
            //    {
            //        RollNumber = (int)reader["RollNumber"],
            //        Name = reader["Name"].ToString(),
            //        Gender = reader["Gender"].ToString(),
            //        TrainerId = (int)reader["TrainerId"]
            //    };

            //    students.Add(s);
            //}

            //con.Close();


            #endregion selecting record from two table

            #region selecting record from two table using SqlDataAdapter

            trainers = new List<Trainer>();
            students = new List<Student>();

            //string connectionString =
            //    "server=.\\sqlexpress;database=B22DB;integrated security = true;";

            SqlConnection con = new SqlConnection(connectionString);

            string cmdText = "select Id, Name, City, Experience from Trainer;select RollNumber, Name, Gender, TrainerId from Student";
            SqlDataAdapter adapter =
                new SqlDataAdapter(cmdText, con);

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var row = ds.Tables[0].Rows[i];
                        Trainer t = new Trainer()
                        {
                            Id = (int)row["Id"],
                            Name = row["Name"].ToString(),
                            City = row["City"].ToString(),
                            Experience = (int)row["Experience"]
                        };

                        trainers.Add(t);
                    }
                }

                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        var row = ds.Tables[1].Rows[i];

                        Student s = new Student()
                        {
                            RollNumber = (int)row["RollNumber"],
                            Name = row["Name"].ToString(),
                            Gender = row["Gender"].ToString(),
                            TrainerId = (int)row["TrainerId"]
                        };

                        students.Add(s);
                    }
                }
            }

            #endregion selecting record from two table using SqlDataAdapter

        }

        // 2. Select A Trainer By Id
        public Trainer GetTrainerById(int id)
        {
            Trainer trainer = null;

            //string connectionString =
            //    "server=.\\sqlexpress;database=B22DB;integrated security=true;";

            SqlConnection con = new SqlConnection(connectionString);

            string cmdText =
                $"select Id, Name, City, Experience from Trainer where Id = {id}";

            SqlCommand cmd = new SqlCommand(cmdText, con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                trainer = new Trainer();
                trainer.Id = (int)reader["Id"];
                trainer.Name = reader["Name"].ToString();
                trainer.City = reader["City"].ToString();
                trainer.Experience = (int)reader["Experience"];

                break;
            }

            con.Close();

            return trainer;
        }


        // 3. Create a new Trainer
        public bool CreateTrainer(Trainer trainer, out int newRollNumber)
        {
            //string connextionString =
            //    "server=.\\sqlexpress;database=B22DB;integrated security=true;";

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("uspCreateTrainer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", trainer.Name);
            cmd.Parameters.AddWithValue("@City", trainer.City);
            cmd.Parameters.AddWithValue("@Experience", trainer.Experience);

            SqlParameter rollNumber = new SqlParameter()
            {
                ParameterName = "@RollNumber",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(rollNumber);

            SqlParameter status = new SqlParameter()
            {
                ParameterName = "@Status",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output,
            };

            cmd.Parameters.Add(status);

            con.Open();

            int rowsAffected = cmd.ExecuteNonQuery();

            newRollNumber = (int)rollNumber.Value;

            con.Close();

            return (bool)status.Value;
        }


        // 4. Update Exesting Trainer By Id
        public bool UpdateTrainer(Trainer trainer)
        {
            //string connextionString =
            //    "server=.\\sqlexpress;database=B22DB;integrated security=true;";

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand(DBConstants.spUpdateTrainer, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", trainer.Id);
            cmd.Parameters.AddWithValue("@Name", trainer.Name);
            cmd.Parameters.AddWithValue("@City", trainer.City);
            cmd.Parameters.AddWithValue("@Experience", trainer.Experience);

            SqlParameter status = new SqlParameter()
            {
                ParameterName = "@Status",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output,
            };

            cmd.Parameters.Add(status);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return (bool)status.Value;
        }


        // 5. Delete a Existing Trainer By Id
        public bool DeleteTrainer(int id)
        {
            //string connextionString =
            //    "server=.\\sqlexpress;database=B22DB;integrated security=true;";

            //string connectionString =
            //    ConfigurationManager.ConnectionStrings["B22ADONETDB"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand(DBConstants.spDeleteTrainer, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            SqlParameter status = new SqlParameter()
            {
                ParameterName = "@Status",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output,
            };

            cmd.Parameters.Add(status);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            return (bool)status.Value;
        }


        public bool BulkDataCopy()
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                //SqlCommand cmd = new SqlCommand("select * from trainer", con);
                //con.Open();
                //SqlDataReader reader = cmd.ExecuteReader();
                SqlDataAdapter adatper = new SqlDataAdapter("select * from trainer", con);
                DataSet ds = new DataSet();
                adatper.Fill(ds);

                string destinationConString =
                    ConfigurationManager.ConnectionStrings["ArchiveDB"].ConnectionString;

                SqlConnection destinationCon = new SqlConnection(destinationConString);

                SqlBulkCopy copy = new SqlBulkCopy(destinationCon);
                copy.DestinationTableName = "dbo.Trainer";

                destinationCon.Open();
                // copy.WriteToServer(reader);
                // copy all records from reader object to destinaiton table
                copy.WriteToServer(ds.Tables[0]);

                // con.Close();
                destinationCon.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
