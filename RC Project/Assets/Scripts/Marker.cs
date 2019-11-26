//By Donovan Colen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// simple script for the markers so they can be picked up again
/// </summary>
public class Marker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.tag == "RC Tank")
        {
            other.gameObject.GetComponent<RC_Tank>().CollectMarker();
            Destroy(gameObject);
        }
    }

}
