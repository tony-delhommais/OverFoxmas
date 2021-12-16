using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_BulletSpeed = 2f;

    public event Action<int> OnHit;

    private bool m_HaveHitWall = false;

    // Update is called once per frame
    void Update()
    {
        UpdateBulletPosition();
    }

    /// <summary>
    /// Update bullet position
    /// </summary>
    private void UpdateBulletPosition()
    {
        if (!m_HaveHitWall)
        {
            transform.position += transform.up * m_BulletSpeed * Time.deltaTime;

            Vector3 BulletPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);

            if (BulletPosOnScreen.y > Screen.height)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (collider.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }

        if (collider.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }

        if (collider.CompareTag("HitWall"))
        {
            StartCoroutine(OnHitWall());

            m_HaveHitWall = true;
        }
    }

    public void EnemyHitIsDead(int p_incScore)
    {
        OnHit(p_incScore);
    }

    private IEnumerator OnHitWall()
    {
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}
