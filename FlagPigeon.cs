using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPigeon : MonoBehaviour
{
    GameObject FPigeon;
    public float ClimbTime;
    
    void Awake()
    {
        FPigeon = GameObject.FindGameObjectWithTag("Flags");
    } 

   void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Flag")
        {
            Debug.Log("hit");
            FPigeon.GetComponent<npcMove_rigidbody>().enabled = false;
            for(float i = 0; i <= ClimbTime; i++)
            {

            }
        }
    }
}
