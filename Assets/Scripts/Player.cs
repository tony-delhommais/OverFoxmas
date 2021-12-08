using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

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

    private bool m_HaveShield = false;

    private int m_MultiShootValue = 1;

    private bool m_CanWinPoints = true;

    public event Action OnHPChange;
    public event Action OnScoreChange;

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
        if (!GameManagerr.Current.GetPause())
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
                if (m_Stopwatch.Elapsed.TotalSeconds > 1.0 / (float)m_BulletSpawnSpeed)
                {
                    Vector3 SpawnPoint = transform.position;

                    GameObject newBullet = Instantiate(m_BulletInstance, SpawnPoint, Quaternion.identity) as GameObject;
                    //abonnement Being Sport
                    newBullet.GetComponent<Bullet>().OnHit += OnBulletHit;

                    if(m_MultiShootValue >= 3)
                    {
                        newBullet = Instantiate(m_BulletInstance, SpawnPoint, Quaternion.Euler(0, 0, 45)) as GameObject;
                        newBullet.GetComponent<Bullet>().OnHit += OnBulletHit;

                        newBullet = Instantiate(m_BulletInstance, SpawnPoint, Quaternion.Euler(0, 0, -45)) as GameObject;
                        newBullet.GetComponent<Bullet>().OnHit += OnBulletHit;
                    }

                    if (m_MultiShootValue >= 5)
                    {
                        newBullet = Instantiate(m_BulletInstance, SpawnPoint, Quaternion.Euler(0, 0, 22.5f)) as GameObject;
                        newBullet.GetComponent<Bullet>().OnHit += OnBulletHit;

                        newBullet = Instantiate(m_BulletInstance, SpawnPoint, Quaternion.Euler(0, 0, -22.5f)) as GameObject;
                        newBullet.GetComponent<Bullet>().OnHit += OnBulletHit;
                    }

                    m_Stopwatch.Restart();
                }
            }
        }

        //PAUSE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManagerr.Current.TogglePlayPause();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Enemy"))
        {
            DecreaseHP(5);
        }

        if (collider.CompareTag("EnemyBullet"))
        {
            DecreaseHP(10);
        }

        if (collider.CompareTag("Bonus"))
        {
            BonusValue bonus = collider.gameObject.GetComponent<Bonus>().GetBonus();

            switch (bonus.m_BonusType)
            {
                case BonusType.Heal:
                    Heal(bonus.m_BonusValue);
                    break;
                case BonusType.MultiShoot:
                    if (bonus.m_BonusValue > m_MultiShootValue)
                        m_MultiShootValue = bonus.m_BonusValue;
                    break;
                case BonusType.Shield:
                    m_HaveShield = true;
                    StartCoroutine(ShieldCoroutine(bonus.m_BonusValue));
                    break;
                case BonusType.ShootSpeed:
                    if (bonus.m_BonusValue > m_BulletSpawnSpeed)
                        m_BulletSpawnSpeed = bonus.m_BonusValue;
                    break;
                case BonusType.Points:
                    m_Score += bonus.m_BonusValue;
                    break;
            }
        }
    }

    IEnumerator ShieldCoroutine(int p_duration)
    {
        yield return new WaitForSeconds(p_duration);

        m_HaveShield = false;
    }

    private void OnBulletHit(int p_incScore)
    {
        if (m_CanWinPoints)
        {
            m_Score += p_incScore;

            OnScoreChange();
        }
    }

    private void DecreaseHP(int p_hp)
    {
        if (!m_HaveShield)
        {
            m_CurrentPV -= p_hp;
            OnHPChange();

            if (m_CurrentPV <= 0)
            {
                GameManagerr.Current.EndGame(false);
                Dispawn();
            }
        }
    }

    private void Heal(int p_hp)
    {
        m_CurrentPV = Mathf.Clamp(m_CurrentPV + p_hp, 0, m_MaxPV);

        OnHPChange();
    }

    public int GetScore()
    {
        return m_Score;
    }

    public void GameFinished()
    {
        m_HaveShield = true;
        m_CanWinPoints = false;
    }

    public override void Spawn()
    {
        print("Spawn Player");
    }

    public override void Dispawn()
    {
        base.Dispawn();
    }
}
