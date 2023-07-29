using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MapButtonClickHandler : MonoBehaviour
{
    [SerializeField]
    string cctvURL;
    string pinMapName;

    private void Awake()
    {
        // EventManager.Instance�� null���� üũ�Ͽ� �����ϰ� �̺�Ʈ�� ����
        if (EventManager.Instance == null)
        {
            Debug.Log("Waiting for EventManager.Instance to be initialized...");
            return; // EventManager.Instance�� null�� ��쿡�� ���� �ڵ带 �������� �ʰ� ����
        }

        // ���� EventManager.Instance�� null�� �ƴ��� �����ϰ� �̺�Ʈ�� ����
        EventManager.Instance.OnPanelDestructionResponsed += DestructPanel;
        Debug.Log("���� EventManager.Instance�� null�� �ƴ��� �����ϰ� �̺�Ʈ�� ����");
    }
    // ��ư Ŭ�� �̺�Ʈ ó�� �Լ�
    private void OnMouseDown()
    {
        pinMapName = gameObject.name;
        
        // �г��� ���� ���� Collider�� ��Ȱ��ȭ�Ͽ� Ŭ���� ����
        GetComponent<Collider>().enabled = false;

        Debug.Log("��ư Ŭ�� �̺�Ʈ ó�� �Լ�");
        // �г� ���� ��û�� EventManager�� ���� ����
        EventManager.Instance.RequestPanelCreation(pinMapName, cctvURL);
    }

    // �г��� ������ �� ȣ��Ǵ� �Լ�
    public void DestructPanel()
    {
        if (gameObject.name == EventManager.Instance.pinMapName)
        {
            // �г��� ���� �Ŀ� Collider�� �ٽ� Ȱ��ȭ�Ͽ� Ŭ�� �����ϰ� ��
            GetComponent<Collider>().enabled = true;
        }
    }
}
