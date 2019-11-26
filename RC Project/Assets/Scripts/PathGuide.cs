//By Donovan Colen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the path line renderer to show the player and RC how to solve the maze
/// </summary>
public class PathGuide : MonoBehaviour
{
    [SerializeField] private float m_speed = 1;
    [SerializeField] private Transform m_objTrans;
    public TrailRenderer m_trail;
    private LineRenderer m_path;
    Vector3 m_pos;
    int m_index = 0;
    int m_startIndex = 0;
    Vector3[] m_vectors;

    // Start is called before the first frame update
    void Start()
    {
        CalculatePath();
    }

    private void OnEnable()
    {
        CalculatePath();
    }

    private void OnDisable()
    {
        m_trail.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(m_trail.gameObject.transform.position, m_vectors[0]) < 0.1f)
        {
            m_trail.emitting = true;
        }

        m_trail.gameObject.transform.position = Vector3.Lerp(m_trail.gameObject.transform.position, m_vectors[m_index], Time.deltaTime * m_speed);

        if(Vector3.Distance(m_trail.gameObject.transform.position, m_vectors[m_index]) < 0.1f)
        {
            if (m_index < m_vectors.Length - 1)
            {
                m_index++;
            }
            else
            {
                m_index = 0;
                m_trail.emitting = false;
                //m_trail.gameObject.transform.position = m_pos;
            }
        }
    }

    //calculates the start node for the path and the required rest of the nodes to the end of the maze.
    private void CalculatePath()
    {
        m_path = GetComponent<LineRenderer>();
        m_pos = transform.position;

        m_vectors = new Vector3[m_path.positionCount];

        int index = -1;
        float smallestDist = float.MaxValue;
        m_path.GetPositions(m_vectors);
        for (int i = 0; i < m_path.positionCount; ++i)
        {
            m_vectors[i] += m_pos;
            float t = Vector3.Distance(m_objTrans.position, m_vectors[i]);
            if (t < smallestDist)
            {
                smallestDist = t;
                index = i;
            }

        }

        if (index > -1)
        {
            m_startIndex = index;
            m_vectors = new Vector3[m_path.positionCount - m_startIndex + 1];
            m_vectors[0] = m_objTrans.position;
            int j = 1;
            for (int i = m_startIndex; i < m_path.positionCount; ++i)
            {
                m_vectors[j] = m_path.GetPosition(i);
                m_vectors[j] += m_pos;
                ++j;
            }
            m_startIndex = -1;
        }

        m_index = 0;
        m_trail.Clear();
        m_trail.transform.position = m_objTrans.position;
        m_trail.enabled = true;

    }

}
