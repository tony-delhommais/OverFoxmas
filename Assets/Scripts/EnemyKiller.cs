using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : Enemy
{
    [SerializeField]
    protected GameObject m_EnemyBulletInstance = null;

    [SerializeField]
    protected float m_ShootRate = 1.0f;

    private void Start()
    {
        StartCoroutine(ShootBullet());
    }

    override protected void Update()
    {
        base.Update();
    }

    virtual protected IEnumerator ShootBullet()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(m_ShootRate);

            Instantiate(m_EnemyBulletInstance, transform.position, Quaternion.identity);
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
                collider.GetComponent<Bullet>().EnemyHitIsDead(7);

                Dispawn();
            }
        }
        /*if (collider.CompareTag("Enemy"))
        {
            DestroySelf();
        }*/
    }
}
