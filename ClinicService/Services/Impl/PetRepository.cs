using ClinicService.Models;
using System.Data.SQLite;

namespace ClinicService.Services.Impl
{

    public class PetRepository : IPetRepository
    {
        private const string connectionString1 = "Data Source = clinic.db; Version = 3; Pooling = true; Max Pool Size = 100;";

        public int Create(Pet item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString1);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "INSERT INTO Pets(PetID, ClientId, Name, Birthday) VALUES(@PetId, @ClientId, @Name, @Birthday)";
            command.Parameters.AddWithValue("@PetId", item.PetId);
            command.Parameters.AddWithValue("@ClientId", item.ClientId);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public int Update(Pet item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString1);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "UPDATE Pets SET PetId = @PetId, Name = @Name, SurName = @SurName, Patronymic = @Patronymic, Birthday = @Birthday WHERE ClientId=@ClientId";
            command.Parameters.AddWithValue("@ClientId", item.ClientId);
            command.Parameters.AddWithValue("@PetId", item.PetId);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public int Delete(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString1);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "DELETE FROM Pets WHERE ClientId=@ClientId";
            command.Parameters.AddWithValue("@ClientId", id);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public IList<Pet> GetAll()
        {
            List<Pet> list = new List<Pet>();
            SQLiteConnection connection = new SQLiteConnection(connectionString1);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM Pets";
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Pet pet = new Pet();
                pet.PetId = reader.GetInt32(0);
                pet.ClientId = reader.GetInt32(1);
                pet.Name = reader.GetString(2);
                pet.Birthday = new DateTime(reader.GetInt64(3));

                list.Add(pet);
            }

            connection.Close();
            return list;
        }

        public Pet GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString1);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM Pets WHERE PetId=@PetId";
            command.Parameters.AddWithValue("@PetId", id);
            command.Prepare();
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Pet pet = new Pet();
                pet.PetId = reader.GetInt32(0);
                pet.ClientId = reader.GetInt32(1);
                pet.Name = reader.GetString(2);
                pet.Birthday = new DateTime(reader.GetInt64(3));


                connection.Close();
                return pet;
            }
            else
            {
                connection.Close();
                return null;
            }
        }


    }
}
