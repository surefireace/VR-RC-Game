//By Donovan Colen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a simple scipt that scales the materials on the object so different 
/// sized objects can have the same tileing
/// </summary>
public class MaterialEditScript : MonoBehaviour
{
    public Vector2 m_scale = new Vector2(1 ,1);

    private MaterialPropertyBlock m_matPropBlock;
    private Renderer m_renderer;

    // Start is called before the first frame update
    void Awake()
    {
        m_matPropBlock = new MaterialPropertyBlock();
        m_renderer = GetComponent<Renderer>();
    }

    // adjusts the scaleing of the material after every value change
    private void OnValidate()
    {
        if(m_renderer == null)
        {
            m_renderer = GetComponent<Renderer>();

        }

        if(m_matPropBlock == null)
        {
            m_matPropBlock = new MaterialPropertyBlock();

        }

        m_renderer.GetPropertyBlock(m_matPropBlock);

        m_matPropBlock.SetVector("_MainTex_ST", m_scale);

        m_renderer.SetPropertyBlock(m_matPropBlock);
    }

}
