using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public struct EnemyWaveShapeItem
{
    public Vector2 m_RelativePosition;
    public GameObject m_EnemyInstance;
}

[System.Serializable]
public struct EnemyWaveShape
{
    public float m_MinSpawnOffset;
    public float m_MaxSpawnOffset;
    public EnemyWaveShapeItem[] m_EnemyWaveItems;
}

[System.Serializable]
public enum WaveAttackerType
{
    MultipleEnemies,
    MiniBoss,
    Boss
}

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Current;

    [SerializeField]
    private WaveAttackerType[] m_WaveAttackStruct;

    [SerializeField]
    private int m_TimeBetweenWaves = 30;

    [Space]

    private int m_CurrentWaveAttackerTypePose = 0;

    [SerializeField]
    private EnemyWaveShape[] m_WavesShapes;

    [Space]

    [SerializeField]
    private GameObject m_EnemyInstance = null;

    [SerializeField]
    private GameObject m_KillerEnemyInstance = null;

    [SerializeField]
    private GameObject m_KillerPowerEnemyInstance = null;

    [SerializeField]
    private GameObject m_MiniBossInstance = null;

    [SerializeField]
    private GameObject m_BossInstance = null;

    private int m_EnemyCount = 0;
    private float m_Ratio = 0.1f;

    private void Awake()
    {
        Current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaveSpawn());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float SpawnChance = m_Ratio / (m_EnemyCount * 5);
        if (Random.Range(0.0f, 1.0f) < SpawnChance)
        {
            SpawnRandomSingleEnemy();
        }
    }

    IEnumerator WaveSpawn()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(m_TimeBetweenWaves);

            bool BreakForBoss = false;

            if(m_WaveAttackStruct[m_CurrentWaveAttackerTypePose] == WaveAttackerType.MultipleEnemies)
            {
                for(int i = 0; i < m_CurrentWaveAttackerTypePose + 1; i++)
                {
                    SpawnRandomWaveStructure();
                }
            }
            else if (m_WaveAttackStruct[m_CurrentWaveAttackerTypePose] == WaveAttackerType.MiniBoss)
            {
                SpawnMiniBoss();
                BreakForBoss = true;
            }
            else if (m_WaveAttackStruct[m_CurrentWaveAttackerTypePose] == WaveAttackerType.Boss)
            {
                SpawnBoss();
                BreakForBoss = true;
            }

            m_CurrentWaveAttackerTypePose++;

            if (m_CurrentWaveAttackerTypePose == m_WaveAttackStruct.Length || BreakForBoss)
                break;
        }
    }
    
    private void SpawnRandomSingleEnemy()
    {
        if (m_EnemyInstance)
        {
            Vector3 newPos = RandomScreenPos();

            Instantiate(m_EnemyInstance, newPos, Quaternion.identity);

            m_EnemyCount++;
        }
    }

    private void SpawnRandomWaveStructure()
    {
        int randomStruct = Random.Range(0, m_WavesShapes.Length);
        EnemyWaveShape WaveShape = m_WavesShapes[randomStruct];

        Vector3 shapePos = RandomScreenPos(WaveShape.m_MinSpawnOffset, WaveShape.m_MaxSpawnOffset);

        foreach (EnemyWaveShapeItem WaveShapeItem in WaveShape.m_EnemyWaveItems)
        {
            if (WaveShapeItem.m_EnemyInstance)
            {
                Vector3 itemPose = shapePos + new Vector3(WaveShapeItem.m_RelativePosition.x, WaveShapeItem.m_RelativePosition.y, 0.0f);
                Instantiate(WaveShapeItem.m_EnemyInstance, itemPose, Quaternion.identity);

                m_EnemyCount++;
            }
        }
    }

    private Vector3 RandomScreenPos(float p_MinOffset = 0f, float p_MaxOffset = 0f)
    {
        float RandomPosX = Random.Range(0f, Screen.width);
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(new Vector3(RandomPosX, Screen.height, Camera.main.nearClipPlane));

        float minSpawn = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane)).x + p_MinOffset;
        float maxSpawn = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, Screen.height, Camera.main.nearClipPlane)).x - p_MaxOffset;

        spawnPos.x = Mathf.Clamp(spawnPos.x, minSpawn, maxSpawn);
        spawnPos.y += 2;
        spawnPos.z = 0;

        return spawnPos;
    }

    public void DecreaseEnemyCount()
    {
        m_EnemyCount--;
    }

    private void SpawnMiniBoss()
    {
        Vector3 SpawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * 0.95f, Camera.main.nearClipPlane));
        SpawnPos.z = 0;

        GameObject miniboss = null;

        if (m_MiniBossInstance)
        {
            miniboss = Instantiate(m_MiniBossInstance, SpawnPos, Quaternion.identity);
        }

        if (miniboss && m_EnemyInstance && m_KillerEnemyInstance)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 newPos = RandomScreenPos();

                GameObject NewEnemy = Instantiate((i % 2 == 0 ? m_KillerEnemyInstance : m_EnemyInstance), SpawnPos, Quaternion.identity);
                //NewEnemy.transform.SetParent(miniboss.transform);

                Enemy NewEnemyScript = NewEnemy.GetComponent<Enemy>();

                NewEnemyScript.SetMovmentType(EnemyMovmentType.Circular);
                NewEnemyScript.SetEnemySpeed(1.0f);
                NewEnemyScript.SetRotationAngle(Mathf.PI / 2 * i);
                NewEnemyScript.SetRotationRadius(1.8f);

                m_EnemyCount++;
            }
        }
    }

    public void MiniBossDead()
    {
        StartCoroutine(WaveSpawn());
    }

    private void SpawnBoss()
    {
        Vector3 SpawnPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * 0.95f, Camera.main.nearClipPlane));
        SpawnPos.z = 0;

        GameObject boss = null;

        if (m_BossInstance)
        {
            boss = Instantiate(m_BossInstance, SpawnPos, Quaternion.identity);
        }

        if (boss && m_EnemyInstance && m_KillerPowerEnemyInstance)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector3 newPos = RandomScreenPos();

                GameObject NewEnemy = Instantiate((i % 2 == 0 ? m_KillerPowerEnemyInstance : m_EnemyInstance), SpawnPos, Quaternion.identity);
                //NewEnemy.transform.SetParent(boss.transform);

                Enemy NewEnemyScript = NewEnemy.GetComponent<Enemy>();

                NewEnemyScript.SetMovmentType(EnemyMovmentType.Circular);
                NewEnemyScript.SetEnemySpeed(1.0f);
                NewEnemyScript.SetRotationAngle(Mathf.PI / 2 * i);
                NewEnemyScript.SetRotationRadius(1.8f);

                m_EnemyCount++;
            }

            for (int i = 0; i < 8; i++)
            {
                Vector3 newPos = RandomScreenPos();

                GameObject NewEnemy = Instantiate((i % 4 == 0 ? m_KillerPowerEnemyInstance : m_EnemyInstance), SpawnPos, Quaternion.identity);
                //NewEnemy.transform.SetParent(boss.transform);

                Enemy NewEnemyScript = NewEnemy.GetComponent<Enemy>();

                NewEnemyScript.SetMovmentType(EnemyMovmentType.Circular);
                NewEnemyScript.SetEnemySpeed(1.0f);
                NewEnemyScript.SetRotationAngle(Mathf.PI / 4 * i);
                NewEnemyScript.SetRotationRadius(2.5f);
                NewEnemyScript.SetRotationClockwise(false);

                m_EnemyCount++;
            }
        }
    }

    public void BossDead()
    {
        
    }
}