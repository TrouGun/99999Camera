using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherManPigeon : MonoBehaviour
{
    GameObject FisherMan;
    
    void Awake()
    {
        FisherMan = GameObject.FindGameObjectWithTag("Fishing");
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Fond") 
        {
            Debug.Log("hit");
            FisherMan.GetComponent<npcMove_rigidbody>().enabled = false;
        }
            
    }
}
