using System;
using System.Data;
using UnityEngine;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

// 수위 API 정보를 담는 클래스
public class RiverFlowData
{
    public string siteName;
    public string waterLevel;
    public string obsrTime;
    public string alertLevel1;
    public string alertLevel2;
    public string alertLevel3;
    public string alertLevel4;
    public string sttus;
}

// 예측, 현재 날씨 데이터를 담는 클래스
public class PredictData
{
    public string predict;
    public string basedate;
    public string basetime;
    public string temp;
    public string deg;
    public string rain;
    public string windspeed;
}
// 예보 데이터를 담는 클래스
public class UltrasrtfcstData
{
    public string FcstDate;
    public string FcstTime;
    public string T1H;
    public string RN1;
    public string SKY;
    public string REH;
    public string PTY;
    public string VEC;
    public string WSD;
}


public class API_Data : MonoBehaviour
{
    // 데이터를 저장할 객체들
    public static RiverFlowData riverFlowData = new RiverFlowData();
    public static PredictData predictData = new PredictData();
    public static UltrasrtfcstData ultrasrtfcstData = new UltrasrtfcstData();

    private string db_Address = "localhost"; // "210.119.12.112";
    private string db_Port = "3306"; // "10000";
    private string db_Id = "root"; // "pi";
    private string db_Pw = "12345";
    private string db_Name = "team1_iot";
    private string conn_string;

    private void Start()
    {
        conn_string = "Server=" + db_Address + ";Port=" + db_Port + ";Database=" + db_Name + ";User=" + db_Id + ";Password=" + db_Pw;
        // 최초 실행 후 5분마다 UpdateRiverFlow 함수를 호출
        InvokeRepeating("UpdateRiverFlow", 0f, 300f);
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

                // 물의 흐름 정보 가져오기
                string River_sql = "SELECT siteName, waterLevel, obsrTime, alertLevel1, alertLevel2, alertLevel3, alertLevel4, sttus FROM team1_iot.riverflow WHERE siteName =\"연안교\" ORDER BY idx DESC LIMIT 1;";
                MySqlCommand River_cmd = new MySqlCommand(River_sql, conn);
                using (MySqlDataReader River_Reader = River_cmd.ExecuteReader())
                {
                    if (River_Reader.HasRows)
                    {
                        while (River_Reader.Read())
                        {
                            riverFlowData.siteName = River_Reader.GetString(0); // 측정 지역
                            riverFlowData.waterLevel = River_Reader.GetString(1); // 현재 수위
                            riverFlowData.obsrTime = River_Reader.GetString(2); // 측정 시간
                            riverFlowData.alertLevel1 = River_Reader.GetString(3); // 둔치 수위
                            riverFlowData.alertLevel2 = River_Reader.GetString(4); // 주의 수위
                            riverFlowData.alertLevel3 = River_Reader.GetString(5); // 경계 수위
                            riverFlowData.alertLevel4 = River_Reader.GetString(6); // 위험 수위
                            riverFlowData.sttus = River_Reader.GetString(7); // 데이터 상태

                            // Debug.Log($"siteName: '{River_Reader.GetString(0)}' / waterLevel: '{River_Reader.GetString(1)}' / obsrTime: '{River_Reader.GetString(2)}' / alertLevel1: '{River_Reader.GetString(3)}' / alertLevel2: '{River_Reader.GetString(4)} / alertLevel2: '{River_Reader.GetString(4)}' / alertLevel3: '{River_Reader.GetString(5)}' / alertLevel4: '{River_Reader.GetString(6)}' / sttus: '{River_Reader.GetString(7)}'");
                        }
                    }
                    else
                    {
                        Debug.Log("riverFlow DB 출력 오류!");
                    }
                }

                // 기상 데이터 가져오기
                string Predict_sql = "SELECT predict, basedate, basetime, temp, deg, rain, windspeed FROM team1_iot.predict ORDER BY idx DESC LIMIT 1;";
                MySqlCommand Predict_cmd = new MySqlCommand(Predict_sql, conn);
                using (MySqlDataReader Predict_Reader = Predict_cmd.ExecuteReader())
                {
                    if (Predict_Reader.HasRows)
                    {
                        while (Predict_Reader.Read())
                        {
                            predictData.predict = Predict_Reader.GetString(0); // 예측치
                            predictData.basedate = Predict_Reader.GetString(1); // 기준 날짜
                            predictData.basetime = Predict_Reader.GetString(2); // 기준 시간
                            predictData.temp = Predict_Reader.GetString(3); // 온도
                            predictData.deg = Predict_Reader.GetString(4); // 풍향
                            predictData.rain = Predict_Reader.GetString(5); // 강수량
                            predictData.windspeed = Predict_Reader.GetString(6); // 풍속

                            // Debug.Log($"predict: '{Predict_Reader.GetString(0)}' / basedate: '{Predict_Reader.GetString(1)}' / basetime: '{Predict_Reader.GetString(2)}' / temp: '{Predict_Reader.GetString(3)}' / deg: '{Predict_Reader.GetString(4)}' / rain: '{Predict_Reader.GetString(5)}' / windspeed: '{Predict_Reader.GetString(6)}'");
                        }
                    }
                    else
                    {
                        Debug.Log("predict DB 출력 오류!");
                    }
                }
                // 예보 데이터 가져오기
                string ultrasrtfcst_sql = $"SELECT FcstDate, FcstTime, T1H, RN1, SKY, REH, PTY, VEC, WSD FROM team1_iot.ultrasrtfcst ORDER BY idx ASC LIMIT 6;";
                MySqlCommand ultrasrtfcst_cmd = new MySqlCommand(ultrasrtfcst_sql, conn);
                using (MySqlDataReader ultrasrtfcst_Reader = ultrasrtfcst_cmd.ExecuteReader())
                {
                    if (ultrasrtfcst_Reader.HasRows)
                    {
                        while (ultrasrtfcst_Reader.Read())
                        {
                            ultrasrtfcstData.FcstDate = ultrasrtfcst_Reader.GetString(0); // 기준 날짜
                            ultrasrtfcstData.FcstTime = ultrasrtfcst_Reader.GetString(1); // 기준 시간
                            ultrasrtfcstData.T1H = ultrasrtfcst_Reader.GetString(2); // 기온
                            ultrasrtfcstData.RN1 = ultrasrtfcst_Reader.GetString(3); // 1시간 강수량
                            ultrasrtfcstData.SKY = ultrasrtfcst_Reader.GetString(4); // 하늘상태
                            ultrasrtfcstData.REH = ultrasrtfcst_Reader.GetString(5); // 습도
                            ultrasrtfcstData.PTY = ultrasrtfcst_Reader.GetString(6); // 강수형태
                            ultrasrtfcstData.VEC = ultrasrtfcst_Reader.GetString(7); // 풍향
                            ultrasrtfcstData.WSD = ultrasrtfcst_Reader.GetString(8); // 풍속

                            // Debug.Log($"FcstDate : '{ultrasrtfcstData.FcstDate}' / FcstTime 열: '{ultrasrtfcstData.FcstTime}' / T1H : '{ultrasrtfcstData.T1H}' / RN1 : '{ultrasrtfcstData.RN1}' / SKY : '{ultrasrtfcstData.SKY}' / REH : '{ultrasrtfcstData.REH}' / PTY : '{ultrasrtfcstData.PTY}' / VEC : '{ultrasrtfcstData.VEC}' / WSD : '{ultrasrtfcstData.WSD}'");
                        }
                    }
                    else
                    {
                        Debug.Log("ultrasrtfcst DB 출력 오류!");
                    }
                }

                // 다른 데이터들을 가져오는 부분도 유사하게 작성

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
