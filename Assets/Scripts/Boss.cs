using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    [SerializeField]
    private GameObject m_BonusPrefab = null;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            m_CurrentPV -= 5;

            if (m_CurrentPV <= 0)
            {
                collider.GetComponent<Bullet>().EnemyHitIsDead(50);

                Dispawn();
            }
        }
    }

    public override void Spawn()
    {
        print("Spawn Boss");
    }

    public override void Dispawn()
    {
        EnemiesManager.Current.BossDead();

        if (m_BonusPrefab) Instantiate(m_BonusPrefab, transform.position, Quaternion.identity);

        base.Dispawn();
    }
}
