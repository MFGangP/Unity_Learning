using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_click : MonoBehaviour
{
    // 팝업창 변수
    public GameObject Popup;

    // Start is called before the first frame update
    void Start()
    {
        // 팝업창 비활성화 -> 메인 화면에서 팝업창 보이면 안되니까
        Popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 날씨 버튼 클릭시 팝업창 활성화 
    public void Start_Btn_weather()
    {
        // 팝업창 (비)활성화 이벤트 
        Popup.SetActive(true);
    }

    // 센서 버튼 클릭시 팝업창 활성화 
    public void Start_Btn_sensor()
    {
        // 팝업창 (비)활성화 이벤트
        Popup.SetActive(true);
    }
    
    // 제어 버튼 클릭시 팝업창 활성화
    public void Start_Btn_control()
    {
        // 팝업창 (비)활성화 이벤트
        Popup.SetActive(true);
    }

    // 제어 버튼 클릭시 경고창 팝업 활성화 
    public void Start_Btn_control_warning()
    {
        Popup.SetActive(true);
    }

    // 주차예약 버튼 클릭시 팝업창 활성화
    public void Start_Btn_Booking_ParkingLot()
    {
        // 팝업창 (비)활성화 이벤트
        Popup.SetActive(true);
    }
}
