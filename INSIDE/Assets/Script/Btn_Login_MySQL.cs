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
    // 데이터베이스 연결에 필요한 정보를 설정합니다.
    public string db_Address = "210.119.12.100";  // 데이터베이스 서버 IP 주소
    public string db_Port = "10000";        // 데이터베이스 포트 번호
    public string db_Id = "pi";          // 데이터베이스 접속 ID
    public string db_Pw = "12345";         // 데이터베이스 접속 비밀번호
    public string db_Name = "team1_iot";    // 사용할 데이터베이스 이름
    public bool pooling = true;           // 데이터베이스 연결 풀링 여부

    private string conn_string;
    private MySqlConnection con = null;
    private MySqlCommand cmd = null;
    private MySqlDataReader rdr = null;
    public TMP_Text Btn_Login_ID_Text;
    public TMP_Text Btn_Login_PW_Text;
    private void Start()
    {
        // 데이터베이스 연결 문자열을 설정
        conn_string = "Server=" + db_Address + ";Port=" + db_Port + ";Database=" + db_Name + ";User=" + db_Id + ";Password=" + db_Pw;
    }

    private void OnApplicationQuit()
    {
        // 애플리케이션이 종료될 때 데이터베이스 연결을 닫습니다.
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

    // 이 메서드는 LOGINButton이 클릭될 때 호출
    public void OnLoginButtonClick()
    {
        try
        {
            // 데이터베이스 연결을 시도
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
            // 데이터베이스에서 데이터를 가져올 SQL 쿼리를 작성
            string sql = string.Format("SELECT id, password, admin FROM account_parking WHERE id = '{0}' AND password = '{1}' AND admin = '0';", Btn_Login_ID_Text.text, Btn_Login_PW_Text.text);
            cmd = new MySqlCommand(sql, con);
            rdr = cmd.ExecuteReader();

            if (rdr.HasRows == false)
            {
                // 로그인 실패 시 처리
                Debug.Log("아이디" + rdr[0] + "비밀번호" + rdr[1]);
                Debug.Log("아이디와 비밀번호를 확인해주세요.");
            }

            if (rdr.HasRows)
            { 
                // 쿼리 결과를 읽어와서 Unity 콘솔에 로그로 출력
                // READ하면 다음 행으로 넘어가버리기 때문에 값이 있는지 없는지 체크하는게 가장 좋은 방법
                while (rdr.Read() == true)
                {
                    //Debug.Log($"rdr[0].ToString(): '{rdr[0]}'");
                    //Debug.Log($"Btn_Login_ID_Text.text: '{Btn_Login_ID_Text.text}'");

                    // 문자열 내 숨겨진 문자가 포함되어서 나오기 때문에 Replace 해줘야한다.
                    string Login_ID_Text = Btn_Login_ID_Text.text.Replace("​", "");
                    string Login_PW_Text = Btn_Login_PW_Text.text.Replace("​", "");

                    //Debug.Log($"Login_ID_Text: '{Login_ID_Text}'");
                    //Debug.Log($"rdr[0]: '{rdr[0].ToString()}'");
                    //Debug.Log($"Login_ID_Text.Length: {Login_ID_Text.Length}");
                    //Debug.Log($"rdr[0].Length: {rdr[0].ToString().Length}");
                    //foreach (char c in Login_ID_Text)
                    //{
                    //    Debug.Log($"Login_ID_Text char: '{Login_ID_Text}'");
                    //}
                    //foreach (char c in rdr[0].ToString())
                    //{
                    //    Debug.Log($"rdr[0] char: '{rdr[0]}'");
                    //}

                    if (rdr[0].ToString() == Login_ID_Text &&
                        rdr[1].ToString() == Login_PW_Text &&
                        rdr[2].ToString() == "0")
                    {
                        // 로그인 성공 시 처리
                        SceneManager.LoadScene("UI_SC");
                        SceneManager.LoadScene("INSIDE_SC", LoadSceneMode.Additive);
                        SceneManager.UnloadSceneAsync("LOGIN_SC");
                        break; // 로그인 성공했으므로 더 이상 확인할 필요가 없으므로 반복문을 종료
                    }
                    continue;
                }
            }
            rdr.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}