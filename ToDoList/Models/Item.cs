using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    public string Description { get; set; }
    public int Id { get; set; }

    public Item(string description, int id)
    {
      Description = description;
      Id = id;
    }

    public Item(string description)
    {
      Description = description;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.Id == newItem.Id);
        bool descriptionEquality = (this.Description == newItem.Description);
        return (idEquality && descriptionEquality);
      }
    }
    public static List<Item> GetAll()
    {
      {
        List<Item> allItems = new List<Item> { }; //instiate new List
        MySqlConnection conn = DB.Connection(); // intsantiates new connections
        conn.Open(); // opens connection
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand; // contructs sql query --> must be stored in special object - MySqlCommand
        cmd.CommandText = @"SELECT * FROM items;";//commandText = where sql query is stored
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader; // reads returned data
        while (rdr.Read()) // /Read is a built in method for thg reader object - reads result one at  a time then moves on to next record - returns boolean
        {
            int itemId = rdr.GetInt32(0);
            string itemDescription = rdr.GetString(1);
            Item newItem = new Item(itemDescription, itemId); // this is an overloaded constructor - the id is needed to retrieve the data but only the description will display
            allItems.Add(newItem);
        }
        conn.Close(); // closes the database b/c databases are resource- intensive
        if (conn != null) // conditional that activates if the Close method doesn't work
        {
            conn.Dispose();
        }
        return allItems; // return the list after it has been populated
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection(); // create out conn object -- refers to DB class in Database models
      conn.Open(); // open the connection
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";
      cmd.ExecuteNonQuery();  // statements that modify data instead of querying and returning data are executed with the ExecuteNonQuery() method
      conn.Close();
        if (conn != null)
          {
            conn.Dispose();
          }
  }

    public static Item Find(int searchId)
    {
      Item placeholderItem = new Item("placeholder item");
      return placeholderItem;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;

      // Begin new code

      cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);"; // @ItemDescription is a parameter placeholder 
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value = this.Description;
      cmd.Parameters.Add(description);    
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      // End new code

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
