using System;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System.Collections;

public class Inside_Weather : MonoBehaviour
{
    // 데이터베이스 연결에 필요한 정보를 설정합니다.
    private string db_Address = "210.119.12.112";  // 데이터베이스 서버 IP 주소
    private string db_Port = "10000";        // 데이터베이스 포트 번호
    private string db_Id = "pi";          // 데이터베이스 접속 ID
    private string db_Pw = "12345";         // 데이터베이스 접속 비밀번호
    private string db_Name = "team1_iot";    // 사용할 데이터베이스 이름

    // 데이터베이스 연결 문자열을 설정
    private string conn_string;

    // Start is called before the first frame update
    private void OnEnable()
    {
        conn_string = "Server=" + db_Address + ";Port=" + db_Port + ";Database=" + db_Name + ";User=" + db_Id + ";Password=" + db_Pw;
    }
    IEnumerable Update_River_Flow()
    {
        yield return new WaitForSeconds(3000);
        {
            try
            {
                // 데이터베이스 연결을 시도
                using (MySqlConnection conn = new MySqlConnection(conn_string))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    else
                    {
                        Debug.Log("DB 연결 오류!");
                    }
                    Debug.Log("Mysql state: " + conn.State);

                    string sql = string.Format("SELECT siteName, waterLevel, obsrTime, alertLevel1, alertLevel2, alertLevel3, alertLevel4, sttus FROM team1_iot.riverflow WHERE siteName =\"연안교\" ORDER BY idx DESC LIMIT 1;");

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    using MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        // 쿼리 결과를 읽어와서 Unity 콘솔에 로그로 출력
                        // READ하면 다음 행으로 넘어가버리기 때문에 값이 있는지 없는지 체크하는게 가장 좋은 방법
                        while (reader.Read() == true)
                        {
                            Debug.Log($"siteName: '{reader[0]}'");
                            Debug.Log($"waterLevel: '{reader[1]}'");
                            Debug.Log($"obsrTime: '{reader[2]}'");
                            Debug.Log($"alertLevel1: '{reader[3]}'");
                            Debug.Log($"alertLevel2: '{reader[4]}'");
                            Debug.Log($"alertLevel3: '{reader[5]}'");
                            Debug.Log($"alertLevel4: '{reader[6]}'");
                            Debug.Log($"sttus: '{reader[7]}'");

                            Debug.Log($"출력 종료");

                            // 아날로그 접촉식 수위 센서 0~1500
                            // 연안교 둔치 = 1.5 주의 = 4.09 경계 = 4.69 위험 = 5.2

                            // 문자열 내 숨겨진 문자가 포함되어서 나오기 때문에 Replace 해줘야한다.
                            // string Login_ID_Text = Id_Text.text.Replace("​", "");
                            //string Login_PW_Text = PW_Text.text.Replace("​", "");

                            //if (reader[0].ToString() == Login_ID_Text &&
                            //    reader[1].ToString() == Login_PW_Text &&
                            //    reader[2].ToString() == "0")
                            //{

                            break; // 로그인 성공했으므로 더 이상 확인할 필요가 없으므로 반복문을 종료
                        }
                    }
                    else
                    {
                        Debug.Log("DB 출력 오류!");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine("Update_River_Flow");
    }
}
