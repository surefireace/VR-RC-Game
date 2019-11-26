//By Donovan Colen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a simple script that toggles a object "on" and "off"
/// </summary>
public class TogglePlate : MonoBehaviour
{
    [SerializeField]
    private GameObject m_actiavteObj = null;
    [SerializeField]
    private GameObject m_unactiavteObj = null;
    [SerializeField]
    private string m_tag = "";

    // Start is called before the first frame update
    void Start()
    {
        if (m_actiavteObj == null || m_unactiavteObj == null)
        {
            Debug.Log("missing object");
        }
    }

    // hide and turn off colision of the object
    public void Action()
    {
        m_actiavteObj.GetComponent<Renderer>().enabled = !m_actiavteObj.GetComponent<Renderer>().enabled;
        m_actiavteObj.GetComponent<Collider>().enabled = !m_actiavteObj.GetComponent<Collider>().enabled;
        m_unactiavteObj.GetComponent<Renderer>().enabled = !m_unactiavteObj.GetComponent<Renderer>().enabled;
        m_unactiavteObj.GetComponent<Collider>().enabled = !m_unactiavteObj.GetComponent<Collider>().enabled;
        GetComponent<AudioSource>().Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == m_tag)
        {
            Action();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == m_tag)
        {
            Action();
        }

    }
}
