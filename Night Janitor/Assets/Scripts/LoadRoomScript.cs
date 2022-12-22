using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRoomScript : MonoBehaviour
{
    public GameObject roomContents;
    private GameObject roomContentsInstance;

    public Vector3 roomLocation = new Vector3(0.000f, 0.000f, 0.000f);

    void OnTriggerStay2D()
    {
        if(roomContentsInstance == null)
        {
            roomContentsInstance = Instantiate(roomContents, this.transform.position, Quaternion.identity, this.transform);
        }
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(roomContentsInstance != null)
        {
            Destroy(roomContentsInstance);
        }
        
    }
    // void OnDrawGizmosSelected()
    // {
    //     // Draw a yellow sphere at the transform's position
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawSphere(roomLocation, .25f);
    // }
}
