using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking.Types;

public class EventManager : MonoBehaviour
{
    public string pinMapName;
    public string cctvURL;
   public static EventManager Instance { get; private set; }

    // 패널 생성 요청 이벤트를 정의
    public event Action OnPanelCreationRequested;
    public event Action OnPanelDestructionResponsed;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // 이 객체가 씬 전환 시 파괴되지 않도록 설정
    }

    // 패널 생성 요청 함수
    public void RequestPanelCreation(string pinMapName, string cctvURL)
    {
        // 생성하는 패널 cctv 정보 저장
        this.pinMapName = pinMapName;
        this.cctvURL = cctvURL;

        // 패널 생성 요청 이벤트를 발생시킴
        Debug.Log("패널 생성 요청 이벤트를 발생시킴");
        OnPanelCreationRequested?.Invoke();

    }

    public void ResponsePanelDestruction(string pinMapName, string cctvURL)
    {
        // 파괴하는 패널 cctv 정보 저장
        this.pinMapName = pinMapName;
        this.cctvURL = cctvURL;

        // 패널 파괴 반응 이벤트를 발생시킴
        Debug.Log("패널 파괴 반응 이벤트를 발생시킴");
        OnPanelDestructionResponsed?.Invoke();
    }
}
