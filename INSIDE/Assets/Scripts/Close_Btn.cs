using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Btn : MonoBehaviour
{
    // �˾�â ����
    public GameObject Popup;
    public GameObject Popup1 = null;

    // Start is called before the first frame update
    void Start()
    {
        // ���� �˾� ��Ȱ��ȭ -> ��� ���� â ������ �����ؼ� ������ ���� ��Ȱ��ȭ �ؾ��� 
        Popup1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���� �˾�â X ��ư Ŭ���� �ݱ�
    public void CloseBtn_Weather()
    {
        // �˾�â ��Ȱ��ȭ
        Popup.SetActive(false);
    }

    // ���� �˾�â X ��ư Ŭ���� �ݱ� 
    public void CloseBtn_Sensor()
    {
        // �˾�â ��Ȱ��ȭ 
        Popup.SetActive(false);
    }

    // ���� �˾�â X ��ư Ŭ���� �ݱ� 
    public void CloseBtn_Control()
    {
        Popup.SetActive(false) ;
    }

    // ���� �˾�â Ȯ�� ��ư Ŭ���� �ݱ� 
    public void CloseBtn_Warning_Control()
    {
        // ��� �˾�â ��Ȱ��ȭ 
        Popup.SetActive(false);
        // ���� �˾�â Ȱ��ȭ 
        Popup1.SetActive(true);
    }

    // �������� �˾�â X ��ư Ŭ���� �ݱ� 
    public void CloseBtn_Booking_ParkingLot()
    {
        Popup.SetActive(false);
    }
}
