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

    public event Action OnHit;

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
        transform.position += transform.up * m_BulletSpeed * Time.deltaTime;

        Vector3 BulletPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        if(BulletPosOnScreen.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            OnHit();
            Destroy(gameObject);
        }
    }
}
