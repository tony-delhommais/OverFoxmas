using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class EnemyBullet : MonoBehaviour
{
    private float m_BulletSpeed = 2f;

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
        transform.position += -1 * transform.up * m_BulletSpeed * Time.deltaTime;

        Vector3 BulletPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        if (BulletPosOnScreen.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }

        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
