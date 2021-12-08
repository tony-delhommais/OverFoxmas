using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Entity
{
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            m_CurrentPV -= 5;

            if (m_CurrentPV <= 0)
            {
                collider.GetComponent<Bullet>().EnemyHitIsDead(20);

                Dispawn();
            }
        }
    }

    public override void Spawn()
    {
        print("Spawn MiniBoss");
    }

    public override void Dispawn()
    {
        EnemiesManager.Current.MiniBossDead();

        base.Dispawn();
    }
}
