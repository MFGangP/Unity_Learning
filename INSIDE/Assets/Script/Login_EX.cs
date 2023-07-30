using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using TMPro;

public class Login_EX : MonoBehaviour
{
    public TMP_Text usernameInput;
    public TMP_Text passwordInput;

    private string connString;
    private MySqlConnection connection;
    private string dbHost = "127.0.0.1";
    private string dbName = "iot1team";
    private string dbUser = "root";
    private string dbPassword = "12345";

    private void Start()
    {
        connString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};";
        connection = new MySqlConnection(connString);
        try
        {
            connection.Open();
            Debug.Log("MySQL Connection Successful");
        }
        catch (Exception e)
        {
            Debug.Log("MySQL Connection Error: " + e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        if (connection != null && connection.State != System.Data.ConnectionState.Closed)
        {
            connection.Close();
            Debug.Log("MySQL Connection Closed");
        }
    }

    public void OnLoginButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        bool loginSuccess = false;

        try
        {
            string query = $"SELECT id, password, admin FROM account_parking";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                int isAdmin = rdr.GetInt32(0);
                if (isAdmin == 0)
                {
                    // 일반 사용자 로그인 성공
                    loginSuccess = true;
                    Debug.Log("일반 사용자 로그인 성공!");
                    SceneManager.LoadScene("YourMainScene"); // 로그인 성공 시 메인 씬으로 이동
                }
                else if (isAdmin == 1)
                {
                    // 관리자 로그인 성공
                    loginSuccess = true;
                    Debug.Log("관리자 로그인 성공!");
                    SceneManager.LoadScene("AdminScene"); // 관리자 로그인 시 관리자 씬으로 이동
                }
            }

            rdr.Close();

            if (!loginSuccess)
            {
                Debug.Log("아이디와 비밀번호를 확인해주세요.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("오류 발생: " + e.Message);
            Debug.Log("MySQL Error: " + e.Message);
        }
    }
}
