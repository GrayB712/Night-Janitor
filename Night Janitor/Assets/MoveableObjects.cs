using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjects : MonoBehaviour
{
    private IEnumerator coroutine;
    private IEnumerator coroutinetwo;
    public PlayerScript theplayerscript;
    public GameObject Player;
    public float howMuchDecreasePlayerSpeedWhenPushingObjects = 5;
    public Vector3 distanceBetweenObjectAndPlayer;
    public Rigidbody2D m_Rigidbody;
    public static bool hasDecreased = false;
    public bool currentlyShifting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnColliderEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            DecreaseSpeed();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Trigger Enter");
        if (collider.gameObject.tag == "Player" && Input.GetKey(KeyCode.LeftShift))
        {
            if(!currentlyShifting)
            {
                distanceBetweenObjectAndPlayer = Player.transform.position - transform.position;
                StartCoroutine(StickToPlayerOnShift());
            }
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("Trigger Stay");
        if (collider.gameObject.tag == "Player" && Input.GetKey(KeyCode.LeftShift))
        {
            distanceBetweenObjectAndPlayer = Player.transform.position - transform.position;
            StartCoroutine(StickToPlayerOnShift());
        }
    }
    void OnColliderExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if(!currentlyShifting)
            {
                distanceBetweenObjectAndPlayer = Player.transform.position - transform.position;
                StartCoroutine(StickToPlayerOnShift());
            }
        }
    } 
    IEnumerator StickToPlayerOnShift()
    {
        while(Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentlyShifting = true;
            DecreaseSpeed();
            transform.position = Player.transform.position - distanceBetweenObjectAndPlayer;
            yield return new WaitForSeconds(0.001f);
        }
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
