using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjects : MonoBehaviour
{
    public AudioSource sfx;
    private IEnumerator coroutine;
    private IEnumerator coroutinetwo;
    public PlayerScript theplayerscript;
    public GameObject Player;
    public static float howMuchDecreasePlayerSpeedWhenPushingObjects = 40;
    public Vector3 distanceBetweenObjectAndPlayer;
    public Vector3 previousPosition = new Vector3(0,0,0);
    public Rigidbody2D m_Rigidbody;
    public Rigidbody2D player_Rigidbody;
    public static bool hasDecreased = false;
    public bool currentlyShifting = false;
    public static float maxDistanceToPlayer = 5f;
    public float friction = 6;
    public bool countsAsTwoBaracades = false;
    public AudioClip[] audioClipArray;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        player_Rigidbody = Player.GetComponent<Rigidbody2D>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.tag == "Player" && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && theplayerscript.numberOfObjectsDragging < 4)
        {
            if(!currentlyShifting)
            {
                distanceBetweenObjectAndPlayer = Player.transform.position - transform.position;
            }
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        
        if (collider.gameObject.tag == "Player" && !currentlyShifting && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && theplayerscript.numberOfObjectsDragging < 2)
        {
            distanceBetweenObjectAndPlayer = Player.transform.position - transform.position;
            StartCoroutine(StickToPlayerOnShift());
            //sfx.Play(0);
        }
    }
    IEnumerator StickToPlayerOnShift()
    {
        Debug.Log((Vector3.Distance (Player.transform.position, transform.position)));
        theplayerscript.numberOfObjectsDragging += 1;
        m_Rigidbody.drag = 0f;
        m_Rigidbody.angularDrag = 0f;
        while((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (maxDistanceToPlayer > (Vector3.Distance (Player.transform.position, transform.position))))
        {
            currentlyShifting = true;
            DecreaseSpeed();
            m_Rigidbody.velocity = player_Rigidbody.velocity;
            //transform.position = Player.transform.position - distanceBetweenObjectAndPlayer;
            //previousPosition = transform.position;
            yield return new WaitForSeconds(.001f);
        }
        m_Rigidbody.drag = friction;
        m_Rigidbody.angularDrag = friction;
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
