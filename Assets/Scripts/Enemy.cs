using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Enemy : Entity
{
    [SerializeField]
    private float m_EnemySpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyPosition();
    }

    /// <summary>
    /// Update enemy position
    /// </summary>
    private void UpdateEnemyPosition()
    {
        transform.position += transform.up * -1 * m_EnemySpeed * Time.deltaTime;

        Vector3 EnemyPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);

       if (EnemyPosOnScreen.y < 0)
       {
            Destroy(gameObject);
       }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            m_CurrentPV -= 10;

            if (m_CurrentPV <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
