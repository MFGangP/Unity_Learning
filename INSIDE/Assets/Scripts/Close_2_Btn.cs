using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Btn : MonoBehaviour
{
    // 팝업창 변수
    public GameObject Popup;
    public GameObject Popup1 = null;

    // Start is called before the first frame update
    void Start()
    {
        // 제어 팝업 비활성화 -> 경고 문구 창 다음에 떠야해서 시작할 때는 비활성화 해야함 
        Popup1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 날씨 팝업창 X 버튼 클릭시 닫기
    public void CloseBtn_Weather()
    {
        // 팝업창 비활성화
        Popup.SetActive(false);
    }

    // 센서 팝업창 X 버튼 클릭시 닫기 
    public void CloseBtn_Sensor()
    {
        // 팝업창 비활성화 
        Popup.SetActive(false);
    }

    // 제어 팝업창 X 버튼 클릭시 닫기 
    public void CloseBtn_Control()
    {
        Popup.SetActive(false) ;
    }

    // 제어 팝업창 확인 버튼 클릭시 닫기 
    public void CloseBtn_Warning_Control()
    {
        // 경고 팝업창 비활성화 
        Popup.SetActive(false);
        // 제어 팝업창 활성화 
        Popup1.SetActive(true);
    }

    // 주차예약 팝업창 X 버튼 클릭시 닫기 
    public void CloseBtn_Booking_ParkingLot()
    {
        Popup.SetActive(false);
    }
}
