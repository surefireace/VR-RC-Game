//By Donovan Colen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// simple script that deactivates a object for a period of time 
/// </summary>
public class TimedPressurePlate : MonoBehaviour
{
    private float m_timePassed = 0;
    bool m_triggered = false;

    [SerializeField]
    private GameObject m_actiavteObj = null;
    [SerializeField]
    private float m_duration = 1;
    [SerializeField]
    private string m_tag = "";


    // Start is called before the first frame update
    void Start()
    {
        if (m_actiavteObj == null)
        {
            Debug.Log("missing object");
        }

    }

    public void StartTimer()
    {
        m_triggered = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "RC Tank")
        {
            StartTimer();
            Action();
        }
    }

    public void Action()
    {
        m_actiavteObj.GetComponent<Renderer>().enabled = false;
        m_actiavteObj.GetComponent<Collider>().enabled = false;
    }

    public void ResetAction()
    {
        m_actiavteObj.GetComponent<Renderer>().enabled = true;
        m_actiavteObj.GetComponent<Collider>().enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(m_triggered)
        {
            m_timePassed += Time.deltaTime;

            if (m_timePassed >= m_duration)
            {
                ResetAction();
            }
        }
    }
}
