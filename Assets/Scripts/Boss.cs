using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    [SerializeField]
    private GameObject m_BonusPrefab = null;

    [SerializeField]
    private Transform m_PartOne = null;
    [SerializeField]
    private Transform m_PartTwo = null;
    [SerializeField]
    private Transform m_PartTree = null;
    [SerializeField]
    private Transform m_PartFour = null;
    [SerializeField]
    private Transform m_PartFive = null;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            m_CurrentPV -= 5;

            OnHPDecrease();

            if (m_CurrentPV <= 0)
            {
                collider.GetComponent<Bullet>().EnemyHitIsDead(50);

                Dispawn();
            }
        }
    }

    private void OnHPDecrease()
    {
        if (m_PartOne && m_CurrentPV < m_MaxPV / 6 * 5)
            m_PartOne.gameObject.SetActive(false);

        if (m_PartTwo && m_CurrentPV < m_MaxPV / 6 * 4)
            m_PartTwo.gameObject.SetActive(false);

        if (m_PartTree && m_CurrentPV < m_MaxPV / 6 * 3)
            m_PartTree.gameObject.SetActive(false);

        if (m_PartFour && m_CurrentPV < m_MaxPV / 6 * 2)
            m_PartFour.gameObject.SetActive(false);

        if (m_PartFive && m_CurrentPV < m_MaxPV / 6)
            m_PartFive.gameObject.SetActive(false);
    }

    public override void Spawn()
    {
        //print("Spawn Boss");
    }

    public override void Dispawn()
    {
        EnemiesManager.Current.BossDead();

        if (m_BonusPrefab) Instantiate(m_BonusPrefab, transform.position, Quaternion.identity);

        base.Dispawn();
    }
}
