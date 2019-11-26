//By Donovan Colen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// simple script that destroys the selected object on activation
/// </summary>
public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private GameObject m_actiavteObj = null;
    [SerializeField]
    private string m_tag = "";


    // Start is called before the first frame update
    void Start()
    {
        if(m_actiavteObj == null)
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
        if(collision.transform.tag == m_tag && enabled)
        {
            Action();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == m_tag && enabled)
        {
            Action();
        }

    }

}
