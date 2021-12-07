using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected int m_MaxPV = 50;

    protected int m_CurrentPV = 0;

    virtual protected void Awake()
    {
        m_CurrentPV = m_MaxPV;
    }

    public int GetCurrentPV()
    {
        return m_CurrentPV;
    }

    public int GetMaxPV()
    {
        return m_MaxPV;
    }
}
