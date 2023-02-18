using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjects : MonoBehaviour
{
    private IEnumerator coroutine;
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

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player" && Input.GetKey("return"))
        {
            distanceBetweenObjectAndPlayer = Player.transform.position - transform.position;
            StickToPlayerOnShift();
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
    void StickToPlayerOnShift()
    {
        while(Input.GetKey("return"))
        {
            transform.position = Player.transform.position + distanceBetweenObjectAndPlayer;
        }
    }
    IEnumerator WaitBeforeIncreaseSpeed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        theplayerscript.speed += howMuchDecreasePlayerSpeedWhenPushingObjects;
    }

}
