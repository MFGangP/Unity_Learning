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
                    // �Ϲ� ����� �α��� ����
                    loginSuccess = true;
                    Debug.Log("�Ϲ� ����� �α��� ����!");
                    SceneManager.LoadScene("YourMainScene"); // �α��� ���� �� ���� ������ �̵�
                }
                else if (isAdmin == 1)
                {
                    // ������ �α��� ����
                    loginSuccess = true;
                    Debug.Log("������ �α��� ����!");
                    SceneManager.LoadScene("AdminScene"); // ������ �α��� �� ������ ������ �̵�
                }
            }

            rdr.Close();

            if (!loginSuccess)
            {
                Debug.Log("���̵�� ��й�ȣ�� Ȯ�����ּ���.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("���� �߻�: " + e.Message);
            Debug.Log("MySQL Error: " + e.Message);
        }
    }
}
