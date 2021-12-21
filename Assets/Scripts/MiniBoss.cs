using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Entity
{
    [SerializeField]
    protected GameObject m_BonusPrefab = null;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            OnBulletEnter(collider);
        }
    }

    protected virtual void OnBulletEnter(Collider collider)
    {
        m_CurrentPV -= (int)(collider.GetComponent<Bullet>().GetBulletDammage());

        if (m_CurrentPV <= 0)
        {
            collider.GetComponent<Bullet>().EnemyHitIsDead(20);

            if (m_BonusPrefab)
            {
                GameObject bonus = Instantiate(m_BonusPrefab, transform.position, Quaternion.identity);
                bonus.transform.Rotate(new Vector3(-90, 0, 0));
            }

            Dispawn();
        }
    }

    public override void Spawn()
    {
        //print("Spawn MiniBoss");
    }

    protected override void Dispawn()
    {
        EnemiesManager.Current.MiniBossDead();

        DispawnEntity();
    }
}
