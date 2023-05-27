using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PATP : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    public float distanceThreshold = 5f; 

    private bool isFalling = false; 

    private void Update()
    {
        
        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);

        
        if (distance < distanceThreshold)
        {
            
            if (!isFalling)
            {
                isFalling = true; 
                object3.SetActive(true); 
                FallObject3();
            }
        }
        else
        {
            isFalling = false; 
        }
    }

    private void FallObject3()
    {
        object3.transform.position = new Vector3(object2.transform.position.x, object2.transform.position.y + 10, object2.transform.position.z);
    }
}
