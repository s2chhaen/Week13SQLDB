using System.Data.Common;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

SQLiteConnection connection = CreateConnection();

if (connection != null)
{
    //AddCustomer (connection);
    //RemoveCustomer(connection); 
    ReadData(connection);

    connection.Close();
}

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version=3; New=True; Compress=True;");

    try
    {
        connection.Open();
        Console.WriteLine("Connection established");
    }
    catch
    {
        Console.WriteLine("DB connection failed");
    }

    return connection;
}

static void ReadData (SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand ();
    command.CommandText = "SELECT * FROM customer";

    reader = command.ExecuteReader ();

    while (reader.Read ())
    {
        string fiName = reader.GetString (0);
        string laName = reader.GetString (1);
        string dob = reader.GetString (2);

        Console.WriteLine($"Full Name: {fiName} {laName}; Date of birth: {dob}");
    }
}

static void AddCustomer (SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string fiName = "Harry";
    string laName = "Potter";
    string dob = "1980-07-31";

    command = myConnection.CreateCommand ();
    command.CommandText = $"INSERT INTO customer (firstName, lastName, dateOfBirth) VALUES ('{fiName}', '{laName}', '{dob}')";

    int rowInserted = command.ExecuteNonQuery ();

    Console.WriteLine($"Rows inserted: {rowInserted}");
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string IDToDelete = "9";

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {IDToDelete}";

    int rowDeleted = command.ExecuteNonQuery();
    Console.WriteLine($"Rows deleted: {rowDeleted}");
}