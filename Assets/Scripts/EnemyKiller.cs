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
            yield return new WaitForSeconds(1f / m_ShootRate);

            Instantiate(m_EnemyBulletInstance, transform.position + new Vector3(0, 0, Random.Range(-0.2f, 0.2f)), Quaternion.identity);
        }
    }

    protected override void OnBulletEnter(Collider collider)
    {
        m_CurrentPV -= (int)(collider.GetComponent<Bullet>().GetBulletDammage());

        if (m_CurrentPV <= 0)
        {
            collider.GetComponent<Bullet>().EnemyHitIsDead(7);

            if (m_SpawnBonus)
            {
                if (m_BonusPrefab) Instantiate(m_BonusPrefab, transform.position, Quaternion.identity);
            }

            Dispawn();
        }
    }
}
