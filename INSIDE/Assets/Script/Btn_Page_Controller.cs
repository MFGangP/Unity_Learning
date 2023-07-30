using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    public GameObject Pnl_Hb_Menu; // Pnl_Hb_Menu 패널을 Inspector에서 연결해주세요.
    private bool isMenuVisible = false;

    // 버튼을 누를 때 호출되는 메서드입니다.
    public void OnBtn_HamburgerBarClick()
    {
        // isMenuVisible 값에 따라 패널을 활성화 또는 비활성화합니다.
        if (isMenuVisible)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
    }

    // 메뉴를 보이게 하는 메서드입니다.
    private void ShowMenu()
    {
        Pnl_Hb_Menu.SetActive(true);
        isMenuVisible = true;
    }

    // 메뉴를 숨기는 메서드입니다.
    private void HideMenu()
    {
        Pnl_Hb_Menu.SetActive(false);
        isMenuVisible = false;
    }
}