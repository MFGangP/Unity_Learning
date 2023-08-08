using System;
using System.Data;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class API_Data : MonoBehaviour
{
    private string db_Address = "210.119.12.112";
    private string db_Port = "10000";
    private string db_Id = "pi";
    private string db_Pw = "12345";
    private string db_Name = "team1_iot";
    private string conn_string;
    
    private void Start()
    {
        conn_string = "Server=" + db_Address + ";Port=" + db_Port + ";Database=" + db_Name + ";User=" + db_Id + ";Password=" + db_Pw;

        // 최초에 0초 후에 30분마다 UpdateRiverFlow 함수를 호출하도록 설정
        InvokeRepeating("UpdateRiverFlow", 0f, 1800f);
    }

    private void UpdateRiverFlow()
    {
        try
        {
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

                string River_sql = "SELECT siteName, waterLevel, obsrTime, alertLevel1, alertLevel2, alertLevel3, alertLevel4, sttus FROM team1_iot.riverflow WHERE siteName =\"연안교\" ORDER BY idx DESC LIMIT 1;";

                MySqlCommand River_cmd = new MySqlCommand(River_sql, conn);
                using (MySqlDataReader River_Reader = River_cmd.ExecuteReader())
                {
                    if (River_Reader.HasRows)
                    {
                        while (River_Reader.Read())
                        {
                            Debug.Log($"siteName: '{River_Reader[0]}'");
                            Debug.Log($"waterLevel: '{River_Reader[1]}'");
                            Debug.Log($"obsrTime: '{River_Reader[2]}'");
                            Debug.Log($"alertLevel1: '{River_Reader[3]}'");
                            Debug.Log($"alertLevel2: '{River_Reader[4]}'");
                            Debug.Log($"alertLevel3: '{River_Reader[5]}'");
                            Debug.Log($"alertLevel4: '{River_Reader[6]}'");
                            Debug.Log($"sttus: '{River_Reader[7]}'");
                        }
                    }
                    else
                    {
                        Debug.Log("DB 출력 오류!");
                    }
                }
                string Predict_sql = "SELECT predict, basedate, basetime, temp, deg, rain, windspeed FROM team1_iot.predict ORDER BY idx DESC LIMIT 1;";
                List<int> Predict_Reader_List = new List<int>();
                MySqlCommand Predict_cmd = new MySqlCommand(Predict_sql, conn);
                using (MySqlDataReader Predict_Reader = Predict_cmd.ExecuteReader())
                {
                    if (Predict_Reader.HasRows)
                    {
                        while (Predict_Reader.Read())
                        {
                            // 예측치
                            Debug.Log($"predict: '{Predict_Reader[0]}'");
                            // 기준 날짜
                            Debug.Log($"basedate: '{Predict_Reader[1]}'");
                            // 기준 시간
                            Debug.Log($"basetime: '{Predict_Reader[2]}'");
                            // 온도
                            Debug.Log($"temp: '{Predict_Reader[3]}'");
                            // 풍향
                            Debug.Log($"deg: '{Predict_Reader[4]}'");
                            // 강수량
                            Debug.Log($"rain: '{Predict_Reader[5]}'");
                            // 풍속
                            Debug.Log($"windspeed: '{Predict_Reader[6]}'");
                        }
                    }
                    else
                    {
                        Debug.Log("DB 출력 오류!");
                    }
                }
                string ultrasrtfcst_sql = "SELECT FcstDate, FcstTime, T1H, RN1, SKY, REH, PTY, VEC, WSD FROM team1_iot.ultrasrtfcst ORDER BY idx ASC LIMIT 6;";

                MySqlCommand ultrasrtfcst_cmd = new MySqlCommand(ultrasrtfcst_sql, conn);
                using (MySqlDataReader ultrasrtfcst_Reader = ultrasrtfcst_cmd.ExecuteReader())
                {
                    if (ultrasrtfcst_Reader.HasRows)
                    {
                        while (ultrasrtfcst_Reader.Read())
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                int columnIndex = i * 9;  // 각 반복에서 열 인덱스 계산
                                // 기준 날짜
                                Debug.Log($"FcstDate {i + 1}번째 열: '{ultrasrtfcst_Reader[columnIndex]}'");
                                // 기준 시간
                                Debug.Log($"FcstTime {i + 1}번째 열: '{ultrasrtfcst_Reader[columnIndex + 1]}'");
                                // 기온
                                Debug.Log($"T1H {i + 1}번째 열: '{ultrasrtfcst_Reader[columnIndex + 2]}'");
                                // 1시간 강수량
                                Debug.Log($"RN1 {i + 1}번째 열: '{ultrasrtfcst_Reader[columnIndex + 3]}'");
                                // 하늘상태
                                Debug.Log($"SKY {i + 1}번째 열: '{ultrasrtfcst_Reader[columnIndex + 4]}'");
                                // 습도
                                Debug.Log($"REH {i + 1}번째 열: '{ultrasrtfcst_Reader[columnIndex + 5]}'");
                                // 강수형태
                                Debug.Log($"PTY {i + 1}번째 열: '{ultrasrtfcst_Reader[columnIndex + 6]}'");
                                // 풍향
                                Debug.Log($"VEC {i + 1}번째 열: '{ultrasrtfcst_Reader[columnIndex + 7]}'");
                                // 풍속
                                Debug.Log($"WSD {i + 1}번째 열: '{ultrasrtfcst_Reader[columnIndex + 8]}'");
                            }
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log("DB 출력 오류!");
                    }
                }
                conn.Close();
            Debug.Log("DB 연결 해제!");
            }
            
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}