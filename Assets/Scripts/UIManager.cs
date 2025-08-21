using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private bool isWon = false;
    public bool isFailed = false;

    void Awake()
    {
        instance = this;
    }

    public void HideStartTip()
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }

    public void Win()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
        isWon = true;
    }
    
    public void Fail()
    {
        if (!isWon)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
