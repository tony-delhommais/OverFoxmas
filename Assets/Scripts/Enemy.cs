using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;


public enum EnemyMovmentType
{
    TopDown,
    Circular
};

public class Enemy : Entity
{
    private Vector3 m_OriginalPos;

    [SerializeField]
    private float m_EnemySpeed = 1f;

    [SerializeField]
    private EnemyMovmentType m_MovmentType = EnemyMovmentType.TopDown;

    [SerializeField]
    [ShowIf("m_MovmentType", EnemyMovmentType.Circular)]
    private float m_RotationRadius = 1.0f;

    [SerializeField]
    [ShowIf("m_MovmentType", EnemyMovmentType.Circular)]
    private bool m_RotationClockwise = true;
    
    private float m_RotationAngle = 0.0f;

    override protected void Awake()
    {
        base.Awake();

        m_OriginalPos = transform.position;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        UpdateEnemyPosition();
    }

    /// <summary>
    /// Update enemy position
    /// </summary>
    virtual protected void UpdateEnemyPosition()
    {
        if (m_MovmentType == EnemyMovmentType.TopDown)
            TopDownMovment();
        else if (m_MovmentType == EnemyMovmentType.Circular)
            CircularMovment();
    }

    protected void TopDownMovment()
    {
        transform.position += transform.up * -1 * m_EnemySpeed * Time.deltaTime;

        CheckBottomPos();
    }

    protected void CircularMovment()
    {
        if(m_RotationClockwise)
            m_RotationAngle += m_EnemySpeed * Time.deltaTime;
        else
            m_RotationAngle -= m_EnemySpeed * Time.deltaTime;

        m_RotationAngle = m_RotationAngle % (2 * Mathf.PI);

        float xPos = Mathf.Sin(m_RotationAngle) * m_RotationRadius;
        float yPos = Mathf.Cos(m_RotationAngle) * m_RotationRadius;

        transform.position = m_OriginalPos + new Vector3(xPos, yPos, 0.0f);

        CheckBottomPos();
    }

    protected void CheckBottomPos()
    {
        Vector3 EnemyPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        if (EnemyPosOnScreen.y < 0)
        {
            Dispawn();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Dispawn();
        }
        if (collider.CompareTag("Bullet"))
        {
            m_CurrentPV -= 10;

            if (m_CurrentPV <= 0)
            {
                collider.GetComponent<Bullet>().EnemyHitIsDead(5);

                Dispawn();
            }
        }
        /*if (collider.CompareTag("Enemy"))
        {
            DestroySelf();
        }*/
    }

    public override void Spawn()
    {
        print("Spawn Enemy");
    }

    public override void Dispawn()
    {
        EnemiesManager.Current.DecreaseEnemyCount();

        base.Dispawn();
    }

    public void SetEnemySpeed(float p_EnemySpeed)
    {
        m_EnemySpeed = p_EnemySpeed;
    }

    public void SetMovmentType(EnemyMovmentType p_MovmentType)
    {
        m_MovmentType = p_MovmentType;
    }

    public void SetRotationRadius(float p_RotationRadius)
    {
        m_RotationRadius = p_RotationRadius;
    }

    public void SetRotationClockwise(bool p_RotationClockwise)
    {
        m_RotationClockwise = p_RotationClockwise;
    }

    public void SetRotationAngle(float p_RotationAngle)
    {
        m_RotationAngle = p_RotationAngle;
    }
}
