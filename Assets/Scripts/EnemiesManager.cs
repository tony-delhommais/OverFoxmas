using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{

    [SerializeField]
    private GameObject m_EnemyInstance;
    [SerializeField]
    [Min(0.1f)]
    private float m_EnemySpawnSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Apparition());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Apparition()
    {
        while (Application.isPlaying)
        {
            float RandomPosX = Random.Range(0f, Screen.width);
            Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3 (RandomPosX, Screen.height, Camera.main.nearClipPlane));
            newPos.z = 0;
            Instantiate(m_EnemyInstance, newPos, Quaternion.identity);
            yield return new WaitForSeconds(1.0f / (float)m_EnemySpawnSpeed);
        }
    }
       
}