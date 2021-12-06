using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using UnityEngine;

public class Player : MonoBehaviour
{

    private Camera m_MainCamera;

    [SerializeField]
    private float m_VerticalSpeed = 5f;
    [SerializeField]
    private float m_HorizontalSpeed = 5f;

    private Stopwatch m_Stopwatch;

    [SerializeField]
    private GameObject m_BulletInstance;
    [SerializeField]
    private float m_BulletSpawnSpeed;


    private void Awake()
    {
        m_MainCamera = Camera.main;

        m_Stopwatch = Stopwatch.StartNew();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    /// <summary>
    /// 
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
            if(m_Stopwatch.Elapsed.TotalSeconds > m_BulletSpawnSpeed)
            {
                Instantiate(m_BulletInstance, transform.position, Quaternion.identity);
                m_Stopwatch.Restart();
            }
        }

    }
}
