using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected int m_MaxPV = 50;

    protected int m_CurrentPV = 0;

    virtual protected void Awake()
    {
        m_CurrentPV = m_MaxPV;
    }

    private void Start()
    {
        Spawn();
    }

    public int GetCurrentPV()
    {
        return m_CurrentPV;
    }

    public int GetMaxPV()
    {
        return m_MaxPV;
    }

    abstract public void Spawn();

    virtual public void Dispawn()
    {
        Destroy(gameObject);
    }
}
