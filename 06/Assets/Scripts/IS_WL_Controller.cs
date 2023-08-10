using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_WL_Controller : MonoBehaviour
{
    private string Inside_Water_Level_1 = "Inside_Water_Level_1"; // 찾을 수위 오브젝트 이름
    private string Inside_Water_Level_2 = "Inside_Water_Level_2"; // 찾을 수위 오브젝트 이름
    private string Inside_Water_Level_3 = "Inside_Water_Level_3"; // 찾을 수위 오브젝트 이름

    private GameObject Water_Level_1; // 수위 오브젝트 참조 변수
    private GameObject Water_Level_2; // 수위 오브젝트 참조 변수
    private GameObject Water_Level_3; // 수위 오브젝트 참조 변수

    private float updateInterval = 30.1f; // 업데이트 주기 (1분)

    private float DB_SensorData_Water_Level; // 센서로부터 읽은 수위 데이터

    // 프로그램이 시작 할 때랑 시작 했을 때 좌표 값 기준이 다르기 때문에 주의 해야된다.
    // 수위 오브젝트 최초 위치 값 Y = 279.62f : 물이 인식 되었을 때 오브젝트 시작 위치 = 282.0f : 마지노선 Y = 285.0f

    void Start()
    {
        // "Inside_Water_Level_1" 이름의 오브젝트를 찾아서 변수에 대입
        Water_Level_1 = GameObject.Find(Inside_Water_Level_1);
        // "Inside_Water_Level_2" 이름의 오브젝트를 찾아서 변수에 대입
        Water_Level_2 = GameObject.Find(Inside_Water_Level_2);
        // "Inside_Water_Level_3" 이름의 오브젝트를 찾아서 변수에 대입
        Water_Level_3 = GameObject.Find(Inside_Water_Level_3);

        // 모든 수위 오브젝트가 찾아졌는지 확인
        if (Water_Level_1 != null && Water_Level_2 != null && Water_Level_3 != null)
        {
            Debug.Log("오브젝트 찾기 완료");

            // 주기적으로 센서 값을 읽어오는 함수를 호출
            // 1분 ReadAndAdjustWaterLevels 함수 실행
            InvokeRepeating("Read_IS_WL", 0.1f, updateInterval);
        }
    }

    private void Read_IS_WL()
    {
        //Debug.Log(Water_Level_1.transform.position);
        //Debug.Log(Water_Level_2.transform.position);
        //Debug.Log(Water_Level_3.transform.position);

        // API 데이터에서 아날로그 접촉식 수위 센서 값을 가져와서 파싱하여 저장
        DB_SensorData_Water_Level = float.Parse(API_Data.sensorData.AD4_RCV_WL_CNNT);

        if (DB_SensorData_Water_Level == 0)
        {
            // 최초 오브젝트 위치 값 0일 때는 안보여야 됌
            Water_Level_1.transform.position = new Vector3(2108.73f, 279.62f, 1290.99f);
            Water_Level_2.transform.position = new Vector3(2000.43f, 279.62f, 1290.99f);
            Water_Level_3.transform.position = new Vector3(1925.42f, 279.62f, 1017.71f);
        }
        // 수위 40 이상이면 더이상 표현 할 필요가 없음 최대값인 285.0f로 고정
        else if (DB_SensorData_Water_Level > 40) 
        {
            // 새로운 위치 값을 적용하여 수위 오브젝트의 위치 조정
            Water_Level_1.transform.position = new Vector3(2108.73f, 285.0f, 1290.99f);
            Water_Level_2.transform.position = new Vector3(2000.43f, 285.0f, 1290.99f);
            Water_Level_3.transform.position = new Vector3(1925.42f, 285.0f, 1017.71f);
        }
        // 0 ~ 40 사이의 수위 값만 보여주면 된다.
        else
        {
            // 센서 값을 정규화하여 높이 변화에 사용
            float normalizedSensorValue = DB_SensorData_Water_Level / 40.0f;

            // 수위 오브젝트의 Y 위치를 282.0f에서 285.0f까지 변화시킴
            float newYPosition_1 = Mathf.Lerp(282.15f, 285.0f, normalizedSensorValue);
            float newYPosition_2 = Mathf.Lerp(282.15f, 285.0f, normalizedSensorValue);
            float newYPosition_3 = Mathf.Lerp(282.15f, 285.0f, normalizedSensorValue);

            // 새로운 위치 값을 생성하여 수위 오브젝트의 Y 위치를 조정
            Vector3 newPosition_1 = new Vector3(2108.73f, newYPosition_1, 1290.99f);
            Vector3 newPosition_2 = new Vector3(2000.43f, newYPosition_2, 1290.99f);
            Vector3 newPosition_3 = new Vector3(1925.42f, newYPosition_3, 1017.71f);

            // 새로운 위치 값을 적용하여 수위 오브젝트의 위치 조정
            Water_Level_1.transform.position = newPosition_1;
            Water_Level_2.transform.position = newPosition_2;
            Water_Level_3.transform.position = newPosition_3;
        }
    }
}
