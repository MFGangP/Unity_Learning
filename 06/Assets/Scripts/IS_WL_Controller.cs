using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_WL_Controller : MonoBehaviour
{
    private string Inside_Water_Level_1 = "Inside_Water_Level_1"; // ã�� ���� ������Ʈ �̸�
    private string Inside_Water_Level_2 = "Inside_Water_Level_2"; // ã�� ���� ������Ʈ �̸�
    private string Inside_Water_Level_3 = "Inside_Water_Level_3"; // ã�� ���� ������Ʈ �̸�

    private GameObject Water_Level_1; // ���� ������Ʈ ���� ����
    private GameObject Water_Level_2; // ���� ������Ʈ ���� ����
    private GameObject Water_Level_3; // ���� ������Ʈ ���� ����

    private float updateInterval = 30.1f; // ������Ʈ �ֱ� (1��)

    private float DB_SensorData_Water_Level; // �����κ��� ���� ���� ������

    // ���α׷��� ���� �� ���� ���� ���� �� ��ǥ �� ������ �ٸ��� ������ ���� �ؾߵȴ�.
    // ���� ������Ʈ ���� ��ġ �� Y = 279.62f : ���� �ν� �Ǿ��� �� ������Ʈ ���� ��ġ = 282.0f : �����뼱 Y = 285.0f

    void Start()
    {
        // "Inside_Water_Level_1" �̸��� ������Ʈ�� ã�Ƽ� ������ ����
        Water_Level_1 = GameObject.Find(Inside_Water_Level_1);
        // "Inside_Water_Level_2" �̸��� ������Ʈ�� ã�Ƽ� ������ ����
        Water_Level_2 = GameObject.Find(Inside_Water_Level_2);
        // "Inside_Water_Level_3" �̸��� ������Ʈ�� ã�Ƽ� ������ ����
        Water_Level_3 = GameObject.Find(Inside_Water_Level_3);

        // ��� ���� ������Ʈ�� ã�������� Ȯ��
        if (Water_Level_1 != null && Water_Level_2 != null && Water_Level_3 != null)
        {
            Debug.Log("������Ʈ ã�� �Ϸ�");

            // �ֱ������� ���� ���� �о���� �Լ��� ȣ��
            // 1�� ReadAndAdjustWaterLevels �Լ� ����
            InvokeRepeating("Read_IS_WL", 0.1f, updateInterval);
        }
    }

    private void Read_IS_WL()
    {
        //Debug.Log(Water_Level_1.transform.position);
        //Debug.Log(Water_Level_2.transform.position);
        //Debug.Log(Water_Level_3.transform.position);

        // API �����Ϳ��� �Ƴ��α� ���˽� ���� ���� ���� �����ͼ� �Ľ��Ͽ� ����
        DB_SensorData_Water_Level = float.Parse(API_Data.sensorData.AD4_RCV_WL_CNNT);

        if (DB_SensorData_Water_Level == 0)
        {
            // ���� ������Ʈ ��ġ �� 0�� ���� �Ⱥ����� ��
            Water_Level_1.transform.position = new Vector3(2108.73f, 279.62f, 1290.99f);
            Water_Level_2.transform.position = new Vector3(2000.43f, 279.62f, 1290.99f);
            Water_Level_3.transform.position = new Vector3(1925.42f, 279.62f, 1017.71f);
        }
        // ���� 40 �̻��̸� ���̻� ǥ�� �� �ʿ䰡 ���� �ִ밪�� 285.0f�� ����
        else if (DB_SensorData_Water_Level > 40) 
        {
            // ���ο� ��ġ ���� �����Ͽ� ���� ������Ʈ�� ��ġ ����
            Water_Level_1.transform.position = new Vector3(2108.73f, 285.0f, 1290.99f);
            Water_Level_2.transform.position = new Vector3(2000.43f, 285.0f, 1290.99f);
            Water_Level_3.transform.position = new Vector3(1925.42f, 285.0f, 1017.71f);
        }
        // 0 ~ 40 ������ ���� ���� �����ָ� �ȴ�.
        else
        {
            // ���� ���� ����ȭ�Ͽ� ���� ��ȭ�� ���
            float normalizedSensorValue = DB_SensorData_Water_Level / 40.0f;

            // ���� ������Ʈ�� Y ��ġ�� 282.0f���� 285.0f���� ��ȭ��Ŵ
            float newYPosition_1 = Mathf.Lerp(282.15f, 285.0f, normalizedSensorValue);
            float newYPosition_2 = Mathf.Lerp(282.15f, 285.0f, normalizedSensorValue);
            float newYPosition_3 = Mathf.Lerp(282.15f, 285.0f, normalizedSensorValue);

            // ���ο� ��ġ ���� �����Ͽ� ���� ������Ʈ�� Y ��ġ�� ����
            Vector3 newPosition_1 = new Vector3(2108.73f, newYPosition_1, 1290.99f);
            Vector3 newPosition_2 = new Vector3(2000.43f, newYPosition_2, 1290.99f);
            Vector3 newPosition_3 = new Vector3(1925.42f, newYPosition_3, 1017.71f);

            // ���ο� ��ġ ���� �����Ͽ� ���� ������Ʈ�� ��ġ ����
            Water_Level_1.transform.position = newPosition_1;
            Water_Level_2.transform.position = newPosition_2;
            Water_Level_3.transform.position = newPosition_3;
        }
    }
}
