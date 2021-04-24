using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public float preassure;
    public float temputure;
    public float energy;

    public float prIndex;
    public float teIndex;
    public float enIndex;
    public float prMods;
    public float teMods;
    public float enMods;

    public float depth;
    public float depthIndex;
    public bool running;
    private float constantIndex;

    public Interactable vinder;
    public Interactable[] allInter;
    Animator animator;

    public GameObject menuObject;
    public GameObject gameUIObject;
    public FirstPersonController cm;
    bool playing;

    void Awake() {
  
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
    }


    void Start()
    {
        preassure = .5f;
        temputure = .5f;
        energy = .5f;

        allInter = FindObjectsOfType(typeof(Interactable)) as Interactable[];
    }

    void ResetInters(){
        foreach (Interactable c in allInter)
        {
            if(c.hold){
                c.TurnOff();
            }
        }
    }

    void StartGame(){
        foreach (Interactable c in allInter)
        {
            c.TurnOff();
        }
        depth = 0;
        preassure = .5f;
        temputure = .5f;
        energy = .5f;
        vinder.value = 0f;
        playing = true;
        prMods = 0f;
        teMods = 0f;
        enMods = 0f;
    }

    public void ActivateMenu(){
        menuObject.SetActive(true);
        gameUIObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        cm.enabled = false;
    }

    public void DeactivateMenu(){
        gameUIObject.SetActive(true);
        menuObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cm.enabled = true;
    }

    public void PlayButton(){
        DeactivateMenu();
        cm.gameObject.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0,0,0);
        StartGame();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        if(Physics.Raycast(ray, out hit) && playing){
            Interactable inter = hit.transform.GetComponent<Interactable>();
            if(inter != null){
                GetComponent<UIManager>().ShowHandy(true);
                if(Input.GetKey(KeyCode.E) && inter.hold){
                    hit.transform.GetComponent<Interactable>().TurnOn();
                }else if(!Input.GetKey(KeyCode.E) && inter.hold){
                    hit.transform.GetComponent<Interactable>().TurnOff();
                }
                if(Input.GetKeyDown(KeyCode.E) && !inter.hold){
                    hit.transform.GetComponent<Interactable>().Interact();
                }
            }else{
                GetComponent<UIManager>().ShowHandy(false);
                ResetInters();
            }
        }

        if(vinder.value <= 0)
        {
            running = false;
        }else{
            running = true;
        }
        if(!running){
            depthIndex = 0;
        }

        if(running){
            depth += depthIndex;
            preassure += prIndex;
            temputure += teIndex;
            energy += enIndex;
        }

        GetComponent<UIManager>().UpdateValues(preassure,temputure,energy);
        GetComponent<UIManager>().SetDepthText(depth);

        if(depth > 4000){
            constantIndex = 0.0015f;
        }else if(depthIndex > 1000)
        {
            constantIndex = 0.009f;
        }else if(depthIndex > 0)
        {
            constantIndex = 0.005f;
        }

        if(preassure > 1f){
                preassure = 1f;
                Die();
        }
        if(preassure < 0f){
            preassure = 0f;
            Die();
        }
        if(temputure > 1f){
            temputure = 1f;
            Die();
        }
        if(temputure < 0f){
            temputure = 0f;
            Die();
        }
        if(energy > 1f){
            energy = 1f;
            Die();
        }
        if(energy < 0f){
            energy = 0f;
            Die();
        }

        if(depth >= 5000){
            Win();
        }

        depthIndex = ((((Mathf.Abs(Mathf.Abs((temputure * 2f) - 1f)-1f))) + (Mathf.Abs(Mathf.Abs((preassure * 2f) - 1f)-1f)) + (Mathf.Abs(Mathf.Abs((energy * 2f) - 1f)-1f)))/3f) * 25f * Time.deltaTime;
        prIndex = Random.Range(-constantIndex,constantIndex) + prMods;
        teIndex = Random.Range(-constantIndex,constantIndex) + teMods;
        enIndex = Random.Range(-constantIndex,constantIndex) + enMods;
    }

    public void Die(){
        playing = false;
        ActivateMenu();
    }

    public void Win(){
        playing = false;
        ActivateMenu();
    }
}
