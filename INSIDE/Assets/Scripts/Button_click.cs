using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_click : MonoBehaviour
{
    // �˾�â ����
    public GameObject Popup;

    // Start is called before the first frame update
    void Start()
    {
        // �˾�â ��Ȱ��ȭ -> ���� ȭ�鿡�� �˾�â ���̸� �ȵǴϱ�
        Popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ���� ��ư Ŭ���� �˾�â Ȱ��ȭ 
    public void Start_Btn_weather()
    {
        // �˾�â (��)Ȱ��ȭ �̺�Ʈ 
        Popup.SetActive(true);
    }

    // ���� ��ư Ŭ���� �˾�â Ȱ��ȭ 
    public void Start_Btn_sensor()
    {
        // �˾�â (��)Ȱ��ȭ �̺�Ʈ
        Popup.SetActive(true);
    }
    
    // ���� ��ư Ŭ���� �˾�â Ȱ��ȭ
    public void Start_Btn_control()
    {
        // �˾�â (��)Ȱ��ȭ �̺�Ʈ
        Popup.SetActive(true);
    }

    // ���� ��ư Ŭ���� ���â �˾� Ȱ��ȭ 
    public void Start_Btn_control_warning()
    {
        Popup.SetActive(true);
    }

    // �������� ��ư Ŭ���� �˾�â Ȱ��ȭ
    public void Start_Btn_Booking_ParkingLot()
    {
        // �˾�â (��)Ȱ��ȭ �̺�Ʈ
        Popup.SetActive(true);
    }
}
