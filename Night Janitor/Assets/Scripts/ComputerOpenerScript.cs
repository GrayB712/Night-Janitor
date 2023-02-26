using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class ComputerOpenerScript : MonoBehaviour
{
    public static bool generatorConnected = true;
    public static bool reservePowerConnected = true;
    public static bool fridgeBatteryConnected = true;
    
    public bool haveLoaded = false;
    public PlayerScript theplayersscript;
    bool pressedE = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            pressedE = true;
            StartCoroutine(WaitTillUnpressE());
        }
    }
    IEnumerator WaitTillUnpressE()
    {
        yield return new WaitForSeconds(.05f);
        pressedE = false;
    } 
    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Player" && pressedE == true)
        {
            if(haveLoaded == false)
            {
                theplayersscript.paused = true;
                haveLoaded = true;
                SceneManager.LoadScene("ComputerScreen", LoadSceneMode.Additive);
            }else if (haveLoaded == true)
            {
                theplayersscript.paused = false;
                haveLoaded = false;
                SceneManager.UnloadSceneAsync("ComputerScreen");
            }
            pressedE = false;
        }
        
        
    }
}
