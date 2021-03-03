using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

/**
 * This Class creates a Connection to the Database
 * This Class will read the inputs from the "Register Scene"
 * inputed by the user, it will then proceed to create a 
 * new Player record if certain requirements are met
 */
public class RegisterPlayer : MonoBehaviour
{
    // Components
    public Button buttonClick;

    public InputField playerNameInput;
    public InputField playerPasswordInput;
    public InputField confirmPasswordInput;

    public Text validUsername;
    public Text validPassword;

    // Database path
    private string dbName = "URI=file:GameDB.db";

    // Start is called before the first frame update
    void Start()
    {
        buttonClick.onClick.AddListener(GetInputOnClick);
    }

    /**
     * This function Adds a new Player record to the database
     * Only if specific requirements are met
     * Otherwise errors are displayed on the screen
     */
    public void GetInputOnClick()
    {
        validUsername.text = "";
        validPassword.text = "";

        AddPlayer();
    }

    /**
     * This function Creates a connection with the database
     * This function Executes a Query that creates the Players table
     * If the Players table already exist, nothing happens
     */

    /*
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
    */
    public void CheckPasswords()
    {
        if (playerPasswordInput.text != confirmPasswordInput.text)
        {
            validPassword.text = "Passwords don't match";
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

        try
        {
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                // Set up an object called "command" to allow database control
                using (var command = connection.CreateCommand())
                {
                    CheckPasswords();

                    // Write the SQL command to insert a record -- values are pulled from UI InputFields
                    command.CommandText = "INSERT INTO Players (playerName, playerPassword, playerCurrentHealth, playerTotalHealth," +
                                                               "playerMinAttack, playerMaxAttack, playerDefense, playerScore) " +
                                                               "VALUES ('" + playerNameInput.text + "', '" + playerPasswordInput.text + "', " +
                                                                       "'1', '1', '1', '1', '1', '1');";
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

        } 
        catch (SqliteException)
        {
            validUsername.text = "Username already in use";
        }
        
    }

    /*
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
                        Debug.Log("Player: " + playerNameInput.text + " " + playerPasswordInput.text + " " + confirmPasswordInput.text);
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }
    }
    */
}
