using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Btn_Page_Changer : MonoBehaviour
{
    public TMP_Text Btn_PG_Changer;
    // Btn_Inside ��ư�� Ŭ�� �̺�Ʈ�� ������ �޼���
    public void OnBtnPageChangerClick()
    {

        // ���� �ε�. LoadSceneMode.Single�� ���� ���� ��ε��ϰ� ���ο� ���� �ε�
        if (Btn_PG_Changer.text == "�ǿ�")
        {
            // INSIDE_SC ���� ��ε��մϴ�.
            SceneManager.UnloadSceneAsync("INSIDE_SC");
            // OUTSIDE_SC �� �ε�
            SceneManager.LoadScene("OUTSIDE_SC", LoadSceneMode.Additive);
            Btn_PG_Changer.text = "�ǳ�";
        }
        else if (Btn_PG_Changer.text == "�ǳ�")
        {
            // OUTSIDE_SC ���� ��ε��մϴ�.
            SceneManager.UnloadSceneAsync("OUTSIDE_SC");
            // OUTSIDE_SC �� �ε�
            SceneManager.LoadScene("INSIDE_SC", LoadSceneMode.Additive);
            Btn_PG_Changer.text = "�ǿ�";
        }
    }
}
