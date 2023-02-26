using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaracadeExits : MonoBehaviour
{
    float numberOfBaracades = 0;
    public GameObject redBar;
    float counter = 0;
    public GameObject display;
    public float numberNeededToBarracade = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "MoveableObject")
        {
            numberOfBaracades += .5f;
            counter ++;
            if (counter >= 2)
            {
                counter = 0;
                TextMeshProUGUI mText = display.GetComponent<TextMeshProUGUI>();
                mText.text = numberOfBaracades.ToString();
                if(numberOfBaracades >= numberNeededToBarracade)
                {
                    redBar.SetActive(false);
                }
            }
            
        }
        
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "MoveableObject")
        {
            numberOfBaracades -= .5f;
            counter ++;
            if (counter >= 2)
            {
                TextMeshProUGUI mText = display.GetComponent<TextMeshProUGUI>();
                mText.text = numberOfBaracades.ToString(); 
                counter = 0;
                if(numberOfBaracades < 2)
                {
                    redBar.SetActive(true);
                }
            }
        }
    }
}
