using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtScroll : MonoBehaviour
{

    public Animator animator;
    private void Start() {
        animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if(GameManager.instance.running){
        animator.speed = GameManager.instance.depthIndex/10f;
        }else
        {
            animator.speed = 0f;
        }
    }
}
