using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType
{
    MultiShoot,
    ShootSpeed,
    Shield,
    Heal,
    Points,
    SIZE
}

public struct BonusValue
{
    public BonusType m_BonusType;

    public int m_BonusValue;
};

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private float m_LifeTime = 5.0f;

    private BonusValue m_BonusValue;

    void Start()
    {
        GenerateRandomBonusValue();

        StartCoroutine(DispawnCoroutine());
    }  

    IEnumerator DispawnCoroutine()
    {
        yield return new WaitForSeconds(m_LifeTime);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void GenerateRandomBonusValue()
    {
        BonusType randomBonusType = (BonusType)Random.Range(0, (int)BonusType.SIZE);

        m_BonusValue.m_BonusType = randomBonusType;

        switch(randomBonusType)
        {
            case BonusType.Heal:
                m_BonusValue.m_BonusValue = Random.Range(5, 50);
                break;
            case BonusType.MultiShoot:
                m_BonusValue.m_BonusValue = (Random.Range(0f, 1f) < 0.7 ? 3 : 5);
                break;
            case BonusType.Shield:
                m_BonusValue.m_BonusValue = Random.Range(3, 10);
                break;
            case BonusType.ShootSpeed:
                m_BonusValue.m_BonusValue = Random.Range(3, 15);
                break;
            case BonusType.Points:
                m_BonusValue.m_BonusValue = Random.Range(5, 50);
                break;
        }
    }

    public BonusValue GetBonus()
    {
        return m_BonusValue;
    }
}
