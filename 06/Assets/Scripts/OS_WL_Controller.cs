using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OS_WL_Controller : MonoBehaviour
{
    private string Outside_Water_Level = "Outside_Water_Level"; // ã�� ���� ������Ʈ �̸�

    private GameObject Water_Level; // ���� ������Ʈ ���� ����

    private float updateInterval = 30.1f; // ������Ʈ �ֱ� (30��)

    private float DB_RiverFlowData_Water_Level; // API DB�κ��� ���� ���� ������

    // ���α׷��� ���� �� ���� ���� ���� �� ��ǥ �� ������ �ٸ��� ������ ���� �ؾߵȴ�.
    // ���� ������Ʈ ���� ��ġ �� Y = 282.2562f : ���� �ν� �Ǿ��� �� ������Ʈ ���� ��ġ Y = f : �����뼱 Y = f
    // -45635.11 -1005.607 -27774.17
    void Start()
    {
        // "Inside_Water_Level_1" �̸��� ������Ʈ�� ã�Ƽ� ������ ����
        Water_Level = GameObject.Find(Outside_Water_Level);
        // ��� ���� ������Ʈ�� ã�������� Ȯ��
        if (Water_Level != null)
        {
            Debug.Log("������Ʈ ã�� �Ϸ�");

            // �ֱ������� ���� ���� �о���� �Լ��� ȣ��
            // 30�� Read_OS_WL �Լ� ����
            InvokeRepeating("Read_OS_WL", 0.1f, updateInterval);
        }
    }

    private void Read_OS_WL()
    {
        Debug.Log(Water_Level.transform.position);

        // API �����Ϳ��� �Ƴ��α� ���˽� ���� ���� ���� �����ͼ� �Ľ��Ͽ� ����
        DB_RiverFlowData_Water_Level = float.Parse(API_Data.riverFlowData.waterLevel);

        if (DB_RiverFlowData_Water_Level == 0)
        {
            // ���� ������Ʈ ��ġ �� 0�� ���� �Ⱥ����ߵȴ�.
            Water_Level.transform.position = new Vector3(2108.73f, 279.62f, 1290.99f);
        }
        // ���� 40 �̻��̸� ���̻� ǥ�� �� �ʿ䰡 ���� �ִ밪�� 285.0f�� ����
        else if (DB_RiverFlowData_Water_Level > 40)
        {
            // ���ο� ��ġ ���� �����Ͽ� ���� ������Ʈ�� ��ġ ����
            Water_Level.transform.position = new Vector3(2108.73f, 285.0f, 1290.99f);
        }
        // 0 ~ 40 ������ ���� ���� �����ָ� �ȴ�.
        else
        {
            // ���� ���� ����ȭ�Ͽ� ���� ��ȭ�� ���
            float normalizedSensorValue = DB_RiverFlowData_Water_Level / 40.0f;

            // ���� ������Ʈ�� Y ��ġ�� 282.0f���� 285.0f���� ��ȭ��Ŵ
            float newYPosition_1 = Mathf.Lerp(282.15f, 285.0f, normalizedSensorValue);

            // ���ο� ��ġ ���� �����Ͽ� ���� ������Ʈ�� Y ��ġ�� ����
            Vector3 newPosition_1 = new Vector3(2108.73f, newYPosition_1, 1290.99f);

            // ���ο� ��ġ ���� �����Ͽ� ���� ������Ʈ�� ��ġ ����
            Water_Level.transform.position = newPosition_1;
        }
    }
}