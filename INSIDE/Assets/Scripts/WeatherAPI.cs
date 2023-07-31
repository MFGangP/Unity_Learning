using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor.PackageManager.Requests;
using System.IO;
using System.Text;
using static UnityEditor.LightingExplorerTableColumn;
using Unity.VisualScripting;

public class WeatherAPI : MonoBehaviour
{
    private string apiUrl = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getUltraSrtNcst";
    private string apiKey = "yR%2BIgD8xUGGLPLv5yhXB%2F1N8mrSZZShOsMcBdKjRIHdtT2R1%2FDttqVn95bmWMWkbyEbcz%2FJ8qqJaoq5sRLTbbw%3D%3D";
    private string pageNo = "1";
    private string numOfRows = "1000";
    private string dataType = "JSON";
    private string baseDate = DateTime.Now.ToString("yyyyMMdd");
    private string baseTime = "0900";
    private string nx = "98";
    private string ny = "76";

    // 업데이트 주기 (초 단위)
    private float updateInterval = 3.0f * 3600.0f; // 3시간 (3.0f * 3600.0f 초)

    // Start is called before the first frame update
    void Start()
    {
        // 기상 정보 업데이트 시작
        StartCoroutine(UpdateWeatherPeriodically());
    }

    // 기상 정보를 주기적으로 업데이트하는 코루틴 메서드
    private IEnumerator UpdateWeatherPeriodically()
    {
        // 최초 업데이트 수행
        SetBaseTime(DateTime.Now.Hour, DateTime.Now.Minute);
        UpdateWeatherData();

        while (true)
        {
            // 주기적으로 기상 정보 업데이트
            yield return new WaitForSeconds(updateInterval);
            SetBaseTime(DateTime.Now.Hour, DateTime.Now.Minute);
            UpdateWeatherData();
        }
    }

    // 기상 정보 업데이트
    private void UpdateWeatherData()
    {
        // API URL with updated baseTime
        string requestUrl = $"{apiUrl}?serviceKey={apiKey}" +
            $"&pageNo={pageNo}&numOfRows={numOfRows}&dataType={dataType}" +
            $"&base_date={baseDate}&base_time={baseTime}" +
            $"&nx={nx}&ny={ny}";

        GET(requestUrl);
    }

    // GET JSON Response
    public void GET(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        try
        {
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                string data = reader.ReadToEnd();
                Debug.Log(data);
            }
        }
        catch (WebException ex)
        {
            WebResponse errorResponse = ex.Response;
            using (Stream responseStream = errorResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                string errorText = reader.ReadToEnd();
                // log errorText
            }
            throw;
        }
    }

    // 기상 정보 업데이트를 위한 baseTime 설정
    private void SetBaseTime(int hour, int minute)
    {
        if (hour >= 2 && hour < 5)
            baseTime = "0200";
        else if (hour >= 5 && hour < 8)
            baseTime = "0500";
        else if (hour >= 8 && hour < 11)
            baseTime = "0800";
        else if (hour >= 11 && hour < 14)
            baseTime = "1100";
        else if (hour >= 14 && hour < 17)
            baseTime = "1400";
        else if (hour >= 17 && hour < 20)
            baseTime = "1700";
        else if (hour >= 20 && hour < 23)
            baseTime = "2000";
        else // 만약 현재 시간이 23:10 이후이거나 02:10 이전이면 23:00으로 설정
            baseTime = "2300";
    }
}
