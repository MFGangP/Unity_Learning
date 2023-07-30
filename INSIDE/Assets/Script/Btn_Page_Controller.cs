using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    public GameObject Pnl_Hb_Menu; // Pnl_Hb_Menu �г��� Inspector���� �������ּ���.
    private bool isMenuVisible = false;

    // ��ư�� ���� �� ȣ��Ǵ� �޼����Դϴ�.
    public void OnBtn_HamburgerBarClick()
    {
        // isMenuVisible ���� ���� �г��� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�մϴ�.
        if (isMenuVisible)
        {
            HideMenu();
        }
        else
        {
            ShowMenu();
        }
    }

    // �޴��� ���̰� �ϴ� �޼����Դϴ�.
    private void ShowMenu()
    {
        Pnl_Hb_Menu.SetActive(true);
        isMenuVisible = true;
    }

    // �޴��� ����� �޼����Դϴ�.
    private void HideMenu()
    {
        Pnl_Hb_Menu.SetActive(false);
        isMenuVisible = false;
    }
}