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
    private float m_RotationRadius = 1.0f;

    [SerializeField]
    private bool m_RotationClockwise = true;
    
    private float m_RotationAngle = 0.0f;

    [SerializeField]
    protected GameObject m_BonusPrefab = null;

    protected bool m_SpawnBonus = false;

    protected bool m_SpawnAnim = false;

    protected Vector3 m_TargetSpawnPosition = new Vector3(0, 0, 0);

    override protected void Awake()
    {
        base.Awake();

        m_OriginalPos = transform.position;
    }

    float time = 0f;

    // Update is called once per frame
    virtual protected void Update()
    {
        if(!m_SpawnAnim) UpdateEnemyPosition();
        else
        {
            Vector3 TargetSpawnPosR = m_TargetSpawnPosition;
            if (m_MovmentType == EnemyMovmentType.Circular)
            {
                float xPos = Mathf.Sin(m_RotationAngle) * m_RotationRadius;
                float yPos = Mathf.Cos(m_RotationAngle) * m_RotationRadius;

                TargetSpawnPosR += new Vector3(xPos, yPos, 0);
            }

            if (Vector3.Distance(transform.position, TargetSpawnPosR) > 0.01)
            {
                transform.position = BellLerp(m_OriginalPos, TargetSpawnPosR, Mathf.Clamp(time / 2, 0f, 1f));
                time += Time.deltaTime;
            }
            else
            {
                m_SpawnAnim = false;

                m_OriginalPos = m_TargetSpawnPosition;
            }
        }
    }

    private static Vector3 BellLerp(Vector3 a, Vector3 b, float t)
    {
        if (t <= 0) return a;

        if (t >= 1) return b;

        float relativ_b_length = Vector3.Scale(b - a, new Vector3(1, 1, 0)).magnitude;

        float f_a = Mathf.Abs((b.z - a.z) / (0.4f * relativ_b_length * relativ_b_length));

        float f_b = -1 * (f_a * relativ_b_length * relativ_b_length + a.z - b.z) / relativ_b_length;

        float f_c = a.z;

        float r_l_pos = relativ_b_length * t;


        Vector3 CurrentPos = Vector3.Scale(Vector3.Lerp(a, b, t), new Vector3(1, 1, 0));

        CurrentPos.z = f_a * r_l_pos * r_l_pos + f_b * r_l_pos + f_c;

        return CurrentPos;
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
        transform.position += transform.forward * -1 * m_EnemySpeed * Time.deltaTime;

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
            OnPlayerEnter(collider);
        }

        if (collider.CompareTag("Bullet"))
        {
            OnBulletEnter(collider);
        }
    }

    protected virtual void OnPlayerEnter(Collider collider)
    {
        Dispawn();
    }

    protected virtual void OnBulletEnter(Collider collider)
    {
        m_CurrentPV -= (int)(collider.GetComponent<Bullet>().GetBulletDammage());

        if (m_CurrentPV <= 0)
        {
            collider.GetComponent<Bullet>().EnemyHitIsDead(5);

            if (m_SpawnBonus)
            {
                if (m_BonusPrefab) Instantiate(m_BonusPrefab, transform.position, Quaternion.identity);
            }

            Dispawn();
        }
    }

    public override void Spawn()
    {
        //print("Spawn Enemy");
    }

    protected override void Dispawn()
    {
        EnemiesManager.Current.DecreaseEnemyCount();

        DispawnEntity();
    }

    public void StartWithASpawnAnim()
    {
        m_SpawnAnim = true;
    }

    public void SetTargetSpawnPosition(Vector3 p_TargetSpawnPosition)
    {
        m_TargetSpawnPosition = p_TargetSpawnPosition;
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

    public void SetSpawnBonus(bool p_SpawnBonus)
    {
        m_SpawnBonus = p_SpawnBonus;
    }
}
