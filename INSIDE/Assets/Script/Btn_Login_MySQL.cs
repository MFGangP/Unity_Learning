using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Security.Cryptography;

using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

using MySql.Data;
using MySql.Data.MySqlClient;
using Unity.VisualScripting;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Btn_Login_MySQL : MonoBehaviour
{
    // �����ͺ��̽� ���ῡ �ʿ��� ������ �����մϴ�.
    public string ipAddress = "127.0.0.1";  // �����ͺ��̽� ���� IP �ּ�
    public string db_port = "3306";        // �����ͺ��̽� ��Ʈ ��ȣ
    public string db_id = "root";          // �����ͺ��̽� ���� ID
    public string db_pw = "12345";         // �����ͺ��̽� ���� ��й�ȣ
    public string db_name = "iot1team";    // ����� �����ͺ��̽� �̸�
    public bool pooling = true;           // �����ͺ��̽� ���� Ǯ�� ����

    private string conn_string;
    private MySqlConnection con = null;
    private MySqlCommand cmd = null;
    private MySqlDataReader rdr = null;
    public TMP_Text Btn_Login_ID_Text;
    public TMP_Text Btn_Login_PW_Text;
    private void Start()
    {
        // �����ͺ��̽� ���� ���ڿ��� ����
        conn_string = "Server=" + ipAddress + ";Port=" + db_port + ";Database=" + db_name + ";User=" + db_id + ";Password=" + db_pw;
    }

    private void OnApplicationQuit()
    {
        // ���ø����̼��� ����� �� �����ͺ��̽� ������ �ݽ��ϴ�.
        if (con != null)
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
                Debug.Log("Mysql connection closed");
            }
            con.Dispose();
        }
    }

    // �� �޼���� LOGINButton�� Ŭ���� �� ȣ��
    public void OnLoginButtonClick()
    {
        try
        {
            // �����ͺ��̽� ������ �õ�
            con = new MySqlConnection(conn_string);
            con.Open();
            Debug.Log("Mysql state: " + con.State);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        try
        {
            // �����ͺ��̽����� �����͸� ������ SQL ������ �ۼ�
            string sql = string.Format("SELECT id, password, admin FROM account_parking WHERE id = '{0}' AND password = '{1}' AND admin = '0';", Btn_Login_ID_Text.text, Btn_Login_PW_Text.text);
            cmd = new MySqlCommand(sql, con);
            rdr = cmd.ExecuteReader();

            // ���� ����� �о�ͼ� Unity �ֿܼ� �α׷� ���
            while (rdr.Read() == true)
            {
                Debug.Log($"rdr[0].ToString(): '{rdr[0].ToString()}'");
                Debug.Log($"Btn_Login_ID_Text.text: '{Btn_Login_ID_Text.text}'");

                if (rdr[0].ToString() == Btn_Login_ID_Text.text && 
                    rdr[1].ToString() == Btn_Login_PW_Text.text &&
                    rdr[2].ToString() == "0")
                {
                    // �α��� ���� �� ó��
                    SceneManager.LoadScene("UI_SC");
                    SceneManager.LoadScene("INSIDE_SC", LoadSceneMode.Additive);
                    SceneManager.UnloadSceneAsync("LOGIN_SC");
                    break; // �α��� ���������Ƿ� �� �̻� Ȯ���� �ʿ䰡 �����Ƿ� �ݺ����� �����մϴ�.
                }
            }
            if (rdr.Read() == false)
            {
                // �α��� ���� �� ó��
                Debug.Log("���̵�" + rdr[0] + "��й�ȣ" + rdr[1]);
                Debug.Log("���̵�� ��й�ȣ�� Ȯ�����ּ���.");
            }
            rdr.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}