using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillerPower : EnemyKiller
{
    override protected IEnumerator ShootBullet()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(m_ShootRate);

            Instantiate(m_EnemyBulletInstance, transform.position, Quaternion.identity);
            Instantiate(m_EnemyBulletInstance, transform.position, Quaternion.Euler(0, 0, 45));
            Instantiate(m_EnemyBulletInstance, transform.position, Quaternion.Euler(0, 0, -45));
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
                collider.GetComponent<Bullet>().EnemyHitIsDead(12);

                Dispawn();
            }
        }
        /*if (collider.CompareTag("Enemy"))
        {
            DestroySelf();
        }*/
    }
}
