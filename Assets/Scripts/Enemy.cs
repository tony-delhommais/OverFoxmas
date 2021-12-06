using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float m_EnemySpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
