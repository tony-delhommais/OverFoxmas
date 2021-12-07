using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;

delegate void OnHit();

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Player : Entity
{

    private Camera m_MainCamera;

    [SerializeField]
    private float m_VerticalSpeed = 5f;
    [SerializeField]
    private float m_HorizontalSpeed = 5f;

    private Stopwatch m_Stopwatch;

    [SerializeField]
    private GameObject m_BulletInstance = null;
    [SerializeField]
    [Min(1)]
    private int m_BulletSpawnSpeed = 1;

    private int m_Score = 0;

    override protected void Awake()
    {
        base.Awake();
        m_MainCamera = Camera.main;

        m_Stopwatch = Stopwatch.StartNew();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    /// <summary>
    /// Control player input
    /// </summary>
    private void PlayerControl()
    {

       Vector3 PlayerPosOnScreen = m_MainCamera.WorldToScreenPoint(transform.position);


        //RIGHT
        if (PlayerPosOnScreen.x < Screen.width)
        {  
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += transform.right * Time.deltaTime * m_HorizontalSpeed;
            }
        }

        //LEFT
        if (PlayerPosOnScreen.x > 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += transform.right * -1 * Time.deltaTime * m_HorizontalSpeed;
            }
        }

        //UP
        if (PlayerPosOnScreen.y < Screen.height)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += transform.up * Time.deltaTime * m_VerticalSpeed;
            }
        }

        //DOWN
        if (PlayerPosOnScreen.y > 0)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += transform.up * -1 * Time.deltaTime * m_VerticalSpeed;
            }
        }

        // TIR
        if (Input.GetKey(KeyCode.Space))
        {
            if(m_Stopwatch.Elapsed.TotalSeconds > 1.0 / (float)m_BulletSpawnSpeed)
            {
                Vector3 SpawnPoint = transform.position;
                SpawnPoint.y += 0.7f;
                GameObject newBullet = Instantiate(m_BulletInstance, SpawnPoint, Quaternion.identity) as GameObject;

                newBullet.GetComponent<Bullet>().OnHit += OnBulletHit;

                m_Stopwatch.Restart();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            m_CurrentPV -= 5;

            if (m_CurrentPV <= 0)
            {
                print($"Game Over! Score: {m_Score}");
                Destroy(gameObject);
            }
        }
    }

    private void OnBulletHit()
    {
        m_Score += 5;
        print($"Score: {m_Score}");
    }
}
