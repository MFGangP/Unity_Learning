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

    // �г� ���� ��û �̺�Ʈ�� ����
    public event Action OnPanelCreationRequested;
    public event Action OnPanelDestructionResponsed;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // �� ��ü�� �� ��ȯ �� �ı����� �ʵ��� ����
    }

    // �г� ���� ��û �Լ�
    public void RequestPanelCreation(string pinMapName, string cctvURL)
    {
        // �����ϴ� �г� cctv ���� ����
        this.pinMapName = pinMapName;
        this.cctvURL = cctvURL;

        // �г� ���� ��û �̺�Ʈ�� �߻���Ŵ
        Debug.Log("�г� ���� ��û �̺�Ʈ�� �߻���Ŵ");
        OnPanelCreationRequested?.Invoke();

    }

    public void ResponsePanelDestruction(string pinMapName, string cctvURL)
    {
        // �ı��ϴ� �г� cctv ���� ����
        this.pinMapName = pinMapName;
        this.cctvURL = cctvURL;

        // �г� �ı� ���� �̺�Ʈ�� �߻���Ŵ
        Debug.Log("�г� �ı� ���� �̺�Ʈ�� �߻���Ŵ");
        OnPanelDestructionResponsed?.Invoke();
    }
}
