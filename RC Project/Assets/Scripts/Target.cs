//By Donovan Colen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
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
        GetComponent<AudioSource>().Play();
        Destroy(m_actiavteObj);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "BB")
        {
            Action();
        }
    }
}
