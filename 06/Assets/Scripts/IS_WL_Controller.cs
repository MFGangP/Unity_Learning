using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_WL_Controller : MonoBehaviour
{
    private string Inside_Water_Level_1 = "Inside_Water_Level_1"; // ã�� ���� ������Ʈ �̸�
    private string Inside_Water_Level_2 = "Inside_Water_Level_2"; // ã�� ���� ������Ʈ �̸�
    private string Inside_Water_Level_3 = "Inside_Water_Level_3"; // ã�� ���� ������Ʈ �̸�

    private float DB_UltrasrtfcstData_Water_Level;

    // Start is called before the first frame update
    void Start()
    {
        // "Rain"�̶�� �̸��� ������Ʈ�� ã�Ƽ� ����
        GameObject Water_Level_1 = GameObject.Find(Inside_Water_Level_1);
        // "Rain1"�̶�� �̸��� ������Ʈ�� ã�Ƽ� ����
        GameObject Water_Level_2 = GameObject.Find(Inside_Water_Level_2);
        // "Rain1"�̶�� �̸��� ������Ʈ�� ã�Ƽ� ����
        GameObject Water_Level_3 = GameObject.Find(Inside_Water_Level_3);
        // ������Ʈ�� ã�������� �ش� ��ƼŬ �ý��� ������Ʈ�� ������
        if (Water_Level_1 != null && Water_Level_2 != null && Water_Level_3)
        {
            Debug.Log("��� �Ϸ�");
        }
    }
    private void Read_IS_WL()
    {
        try
        {
            // API �����Ϳ��� �Ƴ��α� ���˽� ���� ���� ���� �����ͼ� �Ľ��Ͽ� ���� 0 ~ 1500
            DB_UltrasrtfcstData_Water_Level = float.Parse(API_Data.sensorData.AD4_RCV_WL_CNNT); // string �� float ������ Parse
        }
        catch (System.Exception)
        {

        }
    }
}
