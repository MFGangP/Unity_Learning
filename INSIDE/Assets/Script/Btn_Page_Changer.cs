using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Btn_Page_Changer : MonoBehaviour
{
    public TMP_Text Btn_PG_Changer;
    // Btn_Inside 버튼의 클릭 이벤트에 연결할 메서드
    public void OnBtnPageChangerClick()
    {

        // 씬을 로드. LoadSceneMode.Single은 이전 씬을 언로드하고 새로운 씬만 로드
        if (Btn_PG_Changer.text == "실외")
        {
            // INSIDE_SC 씬을 언로드합니다.
            SceneManager.UnloadSceneAsync("INSIDE_SC");
            // OUTSIDE_SC 씬 로드
            SceneManager.LoadScene("OUTSIDE_SC", LoadSceneMode.Additive);
            Btn_PG_Changer.text = "실내";
        }
        else if (Btn_PG_Changer.text == "실내")
        {
            // OUTSIDE_SC 씬을 언로드합니다.
            SceneManager.UnloadSceneAsync("OUTSIDE_SC");
            // OUTSIDE_SC 씬 로드
            SceneManager.LoadScene("INSIDE_SC", LoadSceneMode.Additive);
            Btn_PG_Changer.text = "실외";
        }
    }
}
