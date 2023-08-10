using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_WL_Controller : MonoBehaviour
{
    private string Inside_Water_Level_1 = "Inside_Water_Level_1"; // 찾을 수위 오브젝트 이름
    private string Inside_Water_Level_2 = "Inside_Water_Level_2"; // 찾을 수위 오브젝트 이름
    private string Inside_Water_Level_3 = "Inside_Water_Level_3"; // 찾을 수위 오브젝트 이름

    private float DB_UltrasrtfcstData_Water_Level;

    // Start is called before the first frame update
    void Start()
    {
        // "Rain"이라는 이름의 오브젝트를 찾아서 대입
        GameObject Water_Level_1 = GameObject.Find(Inside_Water_Level_1);
        // "Rain1"이라는 이름의 오브젝트를 찾아서 대입
        GameObject Water_Level_2 = GameObject.Find(Inside_Water_Level_2);
        // "Rain1"이라는 이름의 오브젝트를 찾아서 대입
        GameObject Water_Level_3 = GameObject.Find(Inside_Water_Level_3);
        // 오브젝트가 찾아졌으면 해당 파티클 시스템 컴포넌트를 가져옴
        if (Water_Level_1 != null && Water_Level_2 != null && Water_Level_3)
        {
            Debug.Log("출력 완료");
        }
    }
    private void Read_IS_WL()
    {
        try
        {
            // API 데이터에서 아날로그 접촉식 수위 센서 값을 가져와서 파싱하여 저장 0 ~ 1500
            DB_UltrasrtfcstData_Water_Level = float.Parse(API_Data.sensorData.AD4_RCV_WL_CNNT); // string 값 float 형으로 Parse
        }
        catch (System.Exception)
        {

        }
    }
}
