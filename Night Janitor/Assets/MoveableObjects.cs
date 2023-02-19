using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjects : MonoBehaviour
{
    private IEnumerator coroutine;
    private IEnumerator coroutinetwo;
    public PlayerScript theplayerscript;
    public GameObject Player;
    public static float howMuchDecreasePlayerSpeedWhenPushingObjects = 1;
    public Vector3 distanceBetweenObjectAndPlayer;
    public Vector3 previousPosition = new Vector3(0,0,0);
    public Rigidbody2D m_Rigidbody;
    public static bool hasDecreased = false;
    public bool currentlyShifting = false;
    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.tag == "Player" && Input.GetKey(KeyCode.LeftShift) && theplayerscript.numberOfObjectsDragging < 4)
        {
            if(!currentlyShifting)
            {
                distanceBetweenObjectAndPlayer = Player.transform.position - transform.position;
            }
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        
        if (collider.gameObject.tag == "Player" && !currentlyShifting && Input.GetKey(KeyCode.LeftShift) && theplayerscript.numberOfObjectsDragging < 2)
        {
            distanceBetweenObjectAndPlayer = Player.transform.position - transform.position;
            StartCoroutine(StickToPlayerOnShift());
        }
    }
    IEnumerator StickToPlayerOnShift()
    {
        theplayerscript.numberOfObjectsDragging += 1;
        while(Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentlyShifting = true;
            DecreaseSpeed();
            transform.position = Player.transform.position - distanceBetweenObjectAndPlayer;
            previousPosition = transform.position;
            yield return new WaitForSeconds(.001f);
        }
        theplayerscript.numberOfObjectsDragging -= 1;
        currentlyShifting = false;
        IncreaseSpeed();
    }
    IEnumerator WaitBeforeIncreaseSpeed(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        IncreaseSpeed();
    }
    void IncreaseSpeed()
    {
        if(hasDecreased == true)
        {
            theplayerscript.speed += howMuchDecreasePlayerSpeedWhenPushingObjects;
            hasDecreased = false;
        }
    }
    void DecreaseSpeed()
    {
        if (hasDecreased == false)
        {
            theplayerscript.speed -= howMuchDecreasePlayerSpeedWhenPushingObjects;
            hasDecreased = true;
        }
    }

}
