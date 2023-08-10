using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OS_WL_Controller : MonoBehaviour
{
    private string Outside_Water_Level = "Outside_Water_Level"; // 찾을 수위 오브젝트 이름

    private GameObject Water_Level; // 수위 오브젝트 참조 변수

    private float updateInterval = 30.1f; // 업데이트 주기 (30초)

    private float DB_RiverFlowData_Water_Level; // API DB로부터 읽은 수위 데이터

    // 프로그램이 시작 할 때랑 시작 했을 때 좌표 값 기준이 다르기 때문에 주의 해야된다.
    // 수위 오브젝트 최초 위치 값 Y = 282.2562f : 물이 인식 되었을 때 오브젝트 시작 위치 Y = f : 마지노선 Y = f
    // -45635.11 -1005.607 -27774.17
    void Start()
    {
        // "Inside_Water_Level_1" 이름의 오브젝트를 찾아서 변수에 대입
        Water_Level = GameObject.Find(Outside_Water_Level);
        // 모든 수위 오브젝트가 찾아졌는지 확인
        if (Water_Level != null)
        {
            Debug.Log("오브젝트 찾기 완료");

            // 주기적으로 센서 값을 읽어오는 함수를 호출
            // 30초 Read_OS_WL 함수 실행
            InvokeRepeating("Read_OS_WL", 0.1f, updateInterval);
        }
    }

    private void Read_OS_WL()
    {
        Debug.Log(Water_Level.transform.position);

        // API 데이터에서 아날로그 접촉식 수위 센서 값을 가져와서 파싱하여 저장
        DB_RiverFlowData_Water_Level = float.Parse(API_Data.riverFlowData.waterLevel);

        if (DB_RiverFlowData_Water_Level == 0)
        {
            // 최초 오브젝트 위치 값 0일 때는 안보여야된다.
            Water_Level.transform.position = new Vector3(2108.73f, 279.62f, 1290.99f);
        }
        // 수위 40 이상이면 더이상 표현 할 필요가 없음 최대값인 285.0f로 고정
        else if (DB_RiverFlowData_Water_Level > 40)
        {
            // 새로운 위치 값을 적용하여 수위 오브젝트의 위치 조정
            Water_Level.transform.position = new Vector3(2108.73f, 285.0f, 1290.99f);
        }
        // 0 ~ 40 사이의 수위 값만 보여주면 된다.
        else
        {
            // 센서 값을 정규화하여 높이 변화에 사용
            float normalizedSensorValue = DB_RiverFlowData_Water_Level / 40.0f;

            // 수위 오브젝트의 Y 위치를 282.0f에서 285.0f까지 변화시킴
            float newYPosition_1 = Mathf.Lerp(282.15f, 285.0f, normalizedSensorValue);

            // 새로운 위치 값을 생성하여 수위 오브젝트의 Y 위치를 조정
            Vector3 newPosition_1 = new Vector3(2108.73f, newYPosition_1, 1290.99f);

            // 새로운 위치 값을 적용하여 수위 오브젝트의 위치 조정
            Water_Level.transform.position = newPosition_1;
        }
    }
}