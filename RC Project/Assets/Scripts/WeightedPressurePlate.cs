//By Donovan Colen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// simple script to tell when a specific object it placed on top
/// </summary>
public class WeightedPressurePlate : MonoBehaviour
{
    [SerializeField]
    private GameObject m_actiavteObj = null;

    // Start is called before the first frame update
    void Start()
    {
        if (m_actiavteObj == null)
        {
            Debug.Log("missing object");
        }
    }

    public void Action()
    {
        Destroy(m_actiavteObj);
        GetComponent<AudioSource>().Play();
        enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "CC" && enabled)
        {
            Action();
        }
    }
}
