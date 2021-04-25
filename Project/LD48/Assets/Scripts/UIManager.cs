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
    public GameObject winObject;

    public Animator menuAnim;
    public Image storyPanel;
    public Sprite[] stories;

    public void UpdateValues(float pre, float temp, float en)
    {
        bars[1].transform.localScale = new Vector3(pre,bars[1].transform.localScale.y,bars[1].transform.localScale.z);
        bars[2].transform.localScale = new Vector3(temp,bars[2].transform.localScale.y,bars[2].transform.localScale.z);
        bars[3].transform.localScale = new Vector3(en,bars[3].transform.localScale.y,bars[3].transform.localScale.z);
    }

    public void ShowHandy(bool show){
        handy.SetActive(show);
    }

    public void StartStory(){
        storyPanel.gameObject.SetActive(true);
        storyPanel.sprite = stories[0];
        Invoke("StoryOne",2);
    }

    void StoryOne(){
        storyPanel.sprite = stories[1];
        Invoke("StoryTwo",2);
    }

    void StoryTwo(){
        storyPanel.sprite = stories[2];
        Invoke("StoryThree",2);
    }

    void StoryThree(){
        storyPanel.sprite = stories[3];
        Invoke("StoryFive",2);
    }
    void StoryFour(){
        storyPanel.sprite = stories[4];
        Invoke("StoryFive",2);
    }

    void StoryFive(){
        storyPanel.gameObject.SetActive(false);
        GameManager.instance.StartGame();
    }


    public void Credits(){
        menuAnim.SetTrigger("Credits");
    }

    public void Back(){
        menuAnim.SetTrigger("Menu");
    }

    public void Win(){
        winObject.SetActive(true);
        Invoke("WinTwo",5);
    }

    public void WinTwo(){
        winObject.SetActive(false);
        GameManager.instance.menuPanel.SetActive(true);
    }

    public void SetDepthText(float value){
        depthText.text = value.ToString("0");
    }

    public void QuitButton(){
        Application.Quit();
    }
}
