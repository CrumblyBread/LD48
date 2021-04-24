using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject handy;
    public TMP_Text depthText;
    public Image[] bars;
    public void UpdateValues(float pre, float temp, float en)
    {
        bars[1].transform.localScale = new Vector3(pre,bars[1].transform.localScale.y,bars[1].transform.localScale.z);
        bars[2].transform.localScale = new Vector3(temp,bars[2].transform.localScale.y,bars[2].transform.localScale.z);
        bars[3].transform.localScale = new Vector3(en,bars[3].transform.localScale.y,bars[3].transform.localScale.z);
    }

    public void ShowHandy(bool show){
        handy.SetActive(show);
    }

    public void SetDepthText(float value){
        depthText.text = value.ToString("0");
    }
}
