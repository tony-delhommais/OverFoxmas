using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MiniBoss
{
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

    protected override void OnBulletEnter(Collider collider)
    {
        m_CurrentPV -= (int)(collider.GetComponent<Bullet>().GetBulletDammage());

        OnHPDecrease();

        if (m_CurrentPV <= 0)
        {
            collider.GetComponent<Bullet>().EnemyHitIsDead(50);

            if (m_BonusPrefab)
            {
                GameObject bonus = Instantiate(m_BonusPrefab, transform.position, Quaternion.identity);
                bonus.transform.Rotate(new Vector3(-90, 0, 0));
            }

            Dispawn();
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

    protected override void Dispawn()
    {
        EnemiesManager.Current.BossDead();

        DispawnEntity();
    }
}
