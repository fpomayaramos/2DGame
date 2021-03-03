using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.SceneManagement;

public class SignInPlayer : MonoBehaviour
{
    // Components
    public Button buttonClick;

    public InputField usernameInput;
    public InputField passwordInput;

    public Text validUsername;
    public Text validPassword;

    public string sceneName;

    // Database path
    private string dbName = "URI=file:GameDB.db";

    // Start is called before the first frame update
    void Start()
    {
        buttonClick.onClick.AddListener(AttemptSignInOnClick);
    }

    /**
     * When the Sign In button is clicked, this button is executed
     * The player attempts to sign into an existing account
     * If the username does not exist, or 
     * if the password does not match with the entered username
     * the player will not be able to sign in
     */
    public void AttemptSignInOnClick()
    {
        validUsername.text = "";
        validPassword.text = "";

        CheckPlayer();
    }

    public void CheckPlayer()
    {
        // This will control if a username exist or not
        int count = 0;
        string pass = "";

        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Set up an object called "command" to allow database control
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * " +
                                      "FROM Players " +
                                      "WHERE playerName = \"" + usernameInput.text + "\"";

                using (IDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        // A record was found
                        count+=1;

                        // Get pass
                        pass = (string)reader["playerPassword"];
                    }
                }
            }

            connection.Close();
        }

        // Check Username (if count is less than 1, that user does not exist)
        if (count < 1)
        {
            validUsername.text = "User does not exist";
        }
        else // if it exist, then check password
        {
            // Check Password
            if (passwordInput.text == pass)
            {
                ChangeScene(sceneName); // if both password and username are valid, you can play the game!
            }
            else
            {
                validPassword.text = "Invalid Password";
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
