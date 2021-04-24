using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool active;

    public bool hold;
    Animator animator;

    bool lastframe;

    public GameObject tuggingObject;
    public GameObject HelperObject;

    public float value;
    public bool pump;
    public int modType;
    public float modStrength;

    private void Start() {
        animator = this.GetComponent<Animator>();
        value = 0f;
    }
    public void Interact(){
        if(active){
            TurnOff();
        }else{
            TurnOn();
        }
    }

    private void Update() {
        if(active && hold && tuggingObject!= null){
            //4.702637
            //7.127
            value += Time.deltaTime/7f;
            Mathf.Clamp(value,0f, 1f );
            if(value > 1f){
                value = 1f;
            }
            tuggingObject.transform.localPosition = new Vector3(tuggingObject.transform.localPosition.x,4.702637f + (value * 2.424363f),tuggingObject.transform.localPosition.z);
        }
        if(!active && hold && tuggingObject!= null){
            value -= Time.deltaTime/20f;
            if(value < 0f){
                value = 0f;
            }
            tuggingObject.transform.localPosition = new Vector3(tuggingObject.transform.localPosition.x,4.702637f + (value * 2.424363f) ,tuggingObject.transform.localPosition.z);
        }
    }

    public void TurnOn(){
        active = true;
        if(HelperObject != null){
            HelperObject.SetActive(true);
        }
        if(pump){
            //pump stuff
            animator.SetTrigger("Active");
            Invoke("TurnOff",1);
            if(modType == 1){
                GameManager.instance.preassure += 0.1f;
            }
        }else{
            animator.SetBool("Active",active);
            switch (modType)
            {
                case 1:
                    GameManager.instance.prMods += modStrength;
                    break;
                case 2:
                    GameManager.instance.teMods += modStrength;
                    break;
                case 3:
                    GameManager.instance.enMods += modStrength;
                    break;
                default:
                    break;
            }
        }
    }

    public void TurnOff(){
        active = false;
        if(HelperObject != null){
            HelperObject.SetActive(false);
        }
        animator.SetBool("Active",active);
        if(pump){
            return;
        }
        switch (modType)
            {
                case 1:
                    GameManager.instance.prMods -= modStrength;
                    break;
                case 2:
                    GameManager.instance.teMods -= modStrength;
                    break;
                case 3:
                    GameManager.instance.enMods -= modStrength;
                    break;
                default:
                    break;
            }
    }
}
