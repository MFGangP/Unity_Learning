using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    public GameObject panelPrefab; // �г� ������

    private void Awake()
    {
        // EventManager.Instance�� null���� üũ�Ͽ� �����ϰ� �̺�Ʈ�� ����
        if (EventManager.Instance == null)
        {
            Debug.Log("Waiting for EventManager.Instance to be initialized...");
            return; // EventManager.Instance�� null�� ��쿡�� ���� �ڵ带 �������� �ʰ� ����
        }

        // ���� EventManager.Instance�� null�� �ƴ��� �����ϰ� �̺�Ʈ�� ����
        EventManager.Instance.OnPanelCreationRequested += CreatePanel;
        Debug.Log("���� EventManager.Instance�� null�� �ƴ��� �����ϰ� �̺�Ʈ�� ����");
    }

    // �г��� �������� �����ϴ� �Լ�
    private void CreatePanel()
    {
        Debug.Log("�г��� �������� �����ϴ� �Լ�");

        // �г� �������� �������� �����Ͽ� UI ���� �߰�
        GameObject panelInstance = Instantiate(panelPrefab);

        // UI ���� ĵ���� �Ʒ��� ������ �г��� �ڽ����� ��ġ
        panelInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);

        // �г��� Ȱ��ȭ�Ͽ� ȭ�鿡 ��Ÿ��
        panelInstance.SetActive(true);


    }
}
