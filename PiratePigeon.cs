using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiratePigeon : MonoBehaviour
{
    public GameObject Pirate;
    public GameObject PirateShip;
    GameObject PirateCaptain;

    void Awake()
    {
        PirateCaptain = GameObject.FindGameObjectWithTag("Pirate");
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "PirateShip")
        {
            Debug.Log("hit");
            PirateCaptain.SetActive(false);
            PirateShip.SetActive(false);
            Pirate.SetActive(true);
            FusionPirate();
        }
    }

    private void FusionPirate()
    {
        Pirate.transform.position = new Vector3(PirateCaptain.transform.position.x, PirateCaptain.transform.position.y, PirateCaptain.transform.position.z);
    }
}
