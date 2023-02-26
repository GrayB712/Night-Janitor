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
    void Awake()
    {
        m_Camera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetMouseButtonDown(0))
       {
           mousePosition = Input.mousePosition;
           Ray ray = m_Camera.ScreenPointToRay(mousePosition);
           if (Physics.Raycast(ray, out RaycastHit hit))
           {
               Debug.Log(hit.transform.name);
           }
       }
    }
}
