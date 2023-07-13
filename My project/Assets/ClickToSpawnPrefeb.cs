using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSpawnPrefeb : MonoBehaviour
{
    public GameObject prefab; // ������ ����
    public Vector3 scaleMultiplier = new Vector3(10f, 10f, 10f); // �������� ũ�⸦ �����ϱ� ���� ������ ���
    public float yOffset = 10f; // �������� �� ���� �ø��� ���� ������ ��

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject != null && clickedObject.name == "MapPinVCL1")
                {
                    if (prefab != null)
                    {
                        // Ŭ���� ������Ʈ ���� ������ ����
                        GameObject spawnedPrefab = Instantiate(prefab, clickedObject.transform.position, Quaternion.identity);
                        spawnedPrefab.transform.SetParent(clickedObject.transform);

                        // �������� ũ�� ����
                        spawnedPrefab.transform.localScale = Vector3.Scale(spawnedPrefab.transform.localScale, scaleMultiplier);

                        // �������� x �� ȸ��
                        spawnedPrefab.transform.Rotate(Vector3.right, -90f);

                        // �������� y �� ȸ��
                        spawnedPrefab.transform.Rotate(Vector3.up, 180f);

                        // �������� y �� ������
                        spawnedPrefab.transform.position += new Vector3(0f, yOffset, 0f);
                    }
                    else
                    {
                        Debug.LogError("Prefab is not assigned!");
                    }
                }
            }
        }
    }
}