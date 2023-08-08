using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public string pinMapName;
    public string cctvURL;
   public static EventManager Instance { get; private set; }

    //// 패널 생성 요청 이벤트를 정의
    public event Action<MapPinInfo> OnPanelCreationRequested;
    public event Action<MapPinInfo> OnPanelDestructionResponsed;
    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("EventManager.Instance 생성");
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // 이 객체가 씬 전환 시 파괴되지 않도록 설정
    }

    // 패널 생성 요청 함수
    public void RequestPanelCreation(MapPinInfo mapPinInfo)
    {
        // 패널 생성 요청 이벤트를 발생시킴
        Debug.Log("패널 생성 요청 이벤트를 발생시킴");
        OnPanelCreationRequested?.Invoke(mapPinInfo);

    }

    public void ResponsePanelDestruction(MapPinInfo mapPinInfo)
    {
        // 패널 파괴 반응 이벤트를 발생시킴
        Debug.Log("패널 파괴 반응 이벤트를 발생시킴");
        OnPanelDestructionResponsed?.Invoke(mapPinInfo);
    }
}
