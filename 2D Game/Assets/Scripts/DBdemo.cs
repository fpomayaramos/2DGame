using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DBdemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DisplayInventories();
    }

    public void DisplayInventories()
    {
        string dbName = "URI=file:GameDB.db";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using(var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * " + 
                                      "FROM Players " +
                                      "INNER JOIN Inventories " +
                                      "ON Players.playerID = Inventories.playerID " +
                                      "INNER JOIN Items " +
                                      "ON Inventories.itemID = Items.itemID " +
                                      "ORDER BY Players.playerID";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log("Player: " + reader["playerName"] + " ~~ Item: " + reader["itemName"] + " ~~ Quantity: " + reader["quantity"] );
                    }

                    reader.Close();
                }
            }
            connection.Close();
        }
    }
}
