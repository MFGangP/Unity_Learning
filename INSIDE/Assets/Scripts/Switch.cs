using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public Image On_t;
    public Image Off_t;
    public int index ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void On()
    {
        index = 1;
        Off_t.gameObject.SetActive(true);
        On_t.gameObject.SetActive(false);
    }

    public void Off()
    {
        index = 0;
        Off_t.gameObject.SetActive(false);
        On_t.gameObject.SetActive(true);
    }

}
