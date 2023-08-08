using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public string pinMapName;
    public string cctvURL;
   public static EventManager Instance { get; private set; }

    //// �г� ���� ��û �̺�Ʈ�� ����
    public event Action<MapPinInfo> OnPanelCreationRequested;
    public event Action<MapPinInfo> OnPanelDestructionResponsed;
    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("EventManager.Instance ����");
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // �� ��ü�� �� ��ȯ �� �ı����� �ʵ��� ����
    }

    // �г� ���� ��û �Լ�
    public void RequestPanelCreation(MapPinInfo mapPinInfo)
    {
        // �г� ���� ��û �̺�Ʈ�� �߻���Ŵ
        Debug.Log("�г� ���� ��û �̺�Ʈ�� �߻���Ŵ");
        OnPanelCreationRequested?.Invoke(mapPinInfo);

    }

    public void ResponsePanelDestruction(MapPinInfo mapPinInfo)
    {
        // �г� �ı� ���� �̺�Ʈ�� �߻���Ŵ
        Debug.Log("�г� �ı� ���� �̺�Ʈ�� �߻���Ŵ");
        OnPanelDestructionResponsed?.Invoke(mapPinInfo);
    }
}
