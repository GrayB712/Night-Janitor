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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            theplayerscript.speed -= howMuchDecreasePlayerSpeedWhenPushingObjects;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.LeftShift))
        {
            distanceBetweenObjectAndPlayer = Player.transform.position - transform.position;
            StartCoroutine(StickToPlayerOnShift());
        }
    }
    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            coroutine = WaitBeforeIncreaseSpeed(0.2f);
            StartCoroutine(coroutine);
        }
    } 
    IEnumerator StickToPlayerOnShift()
    {
        while(Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("yee");
            transform.position = Player.transform.position + distanceBetweenObjectAndPlayer;
            yield return new WaitForSeconds(.02f);
        }
        
    }
    IEnumerator WaitBeforeIncreaseSpeed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        theplayerscript.speed += howMuchDecreasePlayerSpeedWhenPushingObjects;
    }

}
