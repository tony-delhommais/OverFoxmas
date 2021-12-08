using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
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

        base.Dispawn();
    }
}
