using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouplePigeon : MonoBehaviour
{
    public GameObject Lovers;
    public GameObject Lover2;
    GameObject Lover1;

    void Awake()
    {
        Lover1 = GameObject.FindGameObjectWithTag("Lover");
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Lover2")
        {
            Debug.Log("hit");
            Lover1.SetActive(false);
            Lover2.SetActive(false);
            Lovers.SetActive(true);
            FusionLovers();
        }
    }

    private void FusionLovers()
    {
        Lovers.transform.position = new Vector3(Lover1.transform.position.x, Lover1.transform.position.y, Lover1.transform.position.z);
    }
}
