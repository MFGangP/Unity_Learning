using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSpawnPrefeb : MonoBehaviour
{
    public GameObject prefab; // 프리팹 에셋
    public Vector3 scaleMultiplier = new Vector3(10f, 10f, 10f); // 프리팹의 크기를 조절하기 위한 스케일 계수
    public float yOffset = 10f; // 프리팹을 더 높이 올리기 위한 오프셋 값

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
                        // 클릭한 오브젝트 위에 프리팹 생성
                        GameObject spawnedPrefab = Instantiate(prefab, clickedObject.transform.position, Quaternion.identity);
                        spawnedPrefab.transform.SetParent(clickedObject.transform);

                        // 프리팹의 크기 조절
                        spawnedPrefab.transform.localScale = Vector3.Scale(spawnedPrefab.transform.localScale, scaleMultiplier);

                        // 프리팹의 x 축 회전
                        spawnedPrefab.transform.Rotate(Vector3.right, -90f);

                        // 프리팹의 y 축 회전
                        spawnedPrefab.transform.Rotate(Vector3.up, 180f);

                        // 프리팹의 y 축 오프셋
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