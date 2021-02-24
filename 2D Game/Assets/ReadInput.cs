using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

public class ReadInput : MonoBehaviour
{
    public Button buttonClick;

    public InputField playerInput;
    public InputField itemInput;
    public InputField quantityInput;

    private string dbName = "URI=file:GameDB.db";

    // Start is called before the first frame update
    void Start()
    {
        buttonClick.onClick.AddListener(GetInputOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetInputOnClick()
    {
        Debug.Log(playerInput.text + "" + itemInput.text + "" + quantityInput.text);

        AddPlayer();
    }

    public void CreateDB()
    {
        // Create the db connection
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Set up an object called "command" to allow database control
            using (var command = connection.CreateCommand())
            {
                // Create a table called "Players" to with 2 fields: 
                command.CommandText = "CREATE TABLE IF NOT EXISTS Players " +
                                      "(playerID  INTEGER NOT NULL, " +
                                      "playerName    VARCHAR(20) NOT NULL, " +
                                      "PRIMARY KEY(playerID AUTOINCREMENT)); ";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void AddPlayer()
    {
        /*
        string conn = "URI=file:GameDB.db"; // Path
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "INSERT INTO Players (playerName) VALUES ('" + playerInput.text + "');";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        */
        
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Set up an object called "command" to allow database control
            using (var command = connection.CreateCommand())
            {
                // Write the SQL command to insert a record -- values are pulled from UI InputFields
                command.CommandText = "INSERT INTO Players (playerName) VALUES ('" + playerInput.text + "');";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
        
    }

    public void DisplayPlayer()
    {
        // Clear out list before displaying new contents

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Set up an object called "command" to allow database control
            using (var command = connection.CreateCommand())
            {
                // Select what you want to retrieve from database
                command.CommandText = "SELECT * FROM Players ORDER BY Players.playerID";

                // Iterate
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log("Player: " + playerInput.text + " " + itemInput.text + " " + quantityInput.text);
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }
    }

}
