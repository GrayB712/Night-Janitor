using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ComputerScreenScript : MonoBehaviour
{
    
    public GameObject generatorOn;
    public GameObject batteryOn;
    public GameObject reservePowerOn;
    public GameObject generatorOff;
    public GameObject batteryOff;
    public GameObject reservePowerOff;
    public Slider powerSupply;
    float amountOfPower = 0f;
    public float generatorPower = 4f;
    public float reservePower = 4f;
    public float batteryPower = 2f;
    Vector3 mousePosition;
    Camera m_Camera;
    public GameObject[] greenExits;
    private SpriteRenderer redExitRend;
    void Awake()
    {
        m_Camera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y-1.2f, 0);
        amountOfPower = 0f;
        if(ComputerOpenerScript.generatorConnected)
        {
            amountOfPower += generatorPower;
            generatorOn.SetActive(true);
            generatorOff.SetActive(false);
        }
        if(ComputerOpenerScript.reservePowerConnected)
        {
            amountOfPower += reservePower;
            reservePowerOn.SetActive(true);
            reservePowerOff.SetActive(false);
        }
        if(ComputerOpenerScript.fridgeBatteryConnected)
        {
            amountOfPower += batteryPower;
            batteryOn.SetActive(true);
            batteryOff.SetActive(false);
        }
        powerSupply.value = amountOfPower;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetMouseButtonDown(0)) {
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                Debug.Log(hit.collider.gameObject.tag);
                if(hit.collider.gameObject.tag == "MapExit")
                {
                    
                    for(int i=0; i<greenExits.Length; i++)
                    {
                        if(hit.collider.gameObject.name == greenExits[i].gameObject.name)
                        {
                            if(greenExits[i].activeInHierarchy)
                            {
                                amountOfPower += 1f;
                                greenExits[i].SetActive(false);
                                redExitRend = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                                redExitRend.sortingOrder = 119;
                            }else
                            {
                                if(amountOfPower > 0f)
                                {
                                    amountOfPower -= 1f;
                                    greenExits[i].SetActive(true);
                                    redExitRend = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                                    redExitRend.sortingOrder = 114;
                                }
                            }
                            powerSupply.value = amountOfPower;
                        }
                    }
                }
            }
        }
    }
}
