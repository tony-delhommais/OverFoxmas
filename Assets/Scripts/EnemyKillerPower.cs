using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillerPower : EnemyKiller
{
    override protected IEnumerator ShootBullet()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(1f / m_ShootRate);

            Instantiate(m_EnemyBulletInstance, transform.position + new Vector3(0, 0, Random.Range(-0.2f, 0.2f)), Quaternion.identity);
            Instantiate(m_EnemyBulletInstance, transform.position + new Vector3(0, 0, Random.Range(-0.2f, 0.2f)), Quaternion.Euler(0, 0, Random.Range(22.5f, 67.5f)));
            Instantiate(m_EnemyBulletInstance, transform.position + new Vector3(0, 0, Random.Range(-0.2f, 0.2f)), Quaternion.Euler(0, 0, Random.Range(-22.5f, -67.5f)));
        }
    }

    protected override void OnBulletEnter(Collider collider)
    {
        m_CurrentPV -= (int)(collider.GetComponent<Bullet>().GetBulletDammage());

        if (m_CurrentPV <= 0)
        {
            collider.GetComponent<Bullet>().EnemyHitIsDead(12);

            if (m_SpawnBonus)
            {
                if (m_BonusPrefab) Instantiate(m_BonusPrefab, transform.position, Quaternion.identity);
            }

            Dispawn();
        }
    }
}
