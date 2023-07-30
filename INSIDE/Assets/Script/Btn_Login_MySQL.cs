using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Security.Cryptography;

using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;

public class Btn_Login_MySQL : MonoBehaviour
{
    public static MySqlConnection SqlConn;
    public string ipAddress = "127.0.0.1";
    public string db_port = "3306";
    public string db_id = "root";
    public string db_pw = "12345";
    public string db_name = "iot1team";
    public bool pooling = true;

    private string conn_string;
    private MySqlConnection con = null;
    private MySqlCommand cmd = null;
    private MySqlDataReader rdr = null;
    // public GameObject Btn_Login; // Pnl_Hb_Menu 패널을 Inspector에서 연결해주세요.
    // private bool isMenuVisible = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        conn_string = "Server=" + ipAddress + ";Database=" + db_name + ";User=" + db_id + ";Password=" + db_pw + ";Pooling=";

        if (pooling)
        {
            conn_string += "True";
        }
        else
        {
            conn_string += "False";
        }
        try
        {
            con = new MySqlConnection(conn_string);
            con.Open();
            Debug.Log("Mysql state: " + con.State);

            string sql = "SELECT * FROM user_base";
            cmd = new MySqlCommand(sql, con);
            cmd = new MySqlCommand(sql, con);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Debug.Log("???");
                Debug.Log(rdr[0] + " -- " + rdr[1]);
            }
            rdr.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    void onApplicationQuit()
    {
        if (con != null)
        {
            if (con.State.ToString() != "Closed")
            {
                con.Close();
                Debug.Log("Mysql connection closed");
            }
            con.Dispose();
        }
    }
}