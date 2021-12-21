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
    private Transform m_SpawnArea = null;

    [Space]

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
    private GameObject m_EnemyFastInstance = null;

    [SerializeField]
    private GameObject m_KillerEnemyInstance = null;

    [SerializeField]
    private GameObject m_KillerPowerEnemyInstance = null;

    [SerializeField]
    private GameObject m_MiniBossInstance = null;

    [SerializeField]
    private GameObject m_BossInstance = null;

    [Space]

    [SerializeField]
    private Transform[] m_CookieSpawnPoints = null;

    [SerializeField]
    private Transform[] m_GingerBreadSpawnPoints = null;

    [SerializeField]
    private Transform[] m_CupcakeSpawnPoints = null;

    [SerializeField]
    private Transform[] m_DonutSpawnPoints = null;

    private int m_EnemyCount = 0;
    private float m_Ratio = 0.1f;

    private bool m_SpawnWaveCoroutineIsRunning = false;

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
        if (!m_SpawnWaveCoroutineIsRunning)
        {
            m_SpawnWaveCoroutineIsRunning = true;

            while (Application.isPlaying)
            {
                yield return new WaitForSeconds(m_TimeBetweenWaves);

                if (m_CurrentWaveAttackerTypePose >= m_WaveAttackStruct.Length)
                    break;

                int currentWave = ++m_CurrentWaveAttackerTypePose;

                bool BreakForBoss = false;

                if (m_WaveAttackStruct[currentWave] == WaveAttackerType.MultipleEnemies)
                {
                    for (int i = 0; i < currentWave + 1; i++)
                    {
                        SpawnRandomWaveStructure();
                    }
                }
                else if (m_WaveAttackStruct[currentWave] == WaveAttackerType.MiniBoss)
                {
                    SpawnMiniBoss();
                    BreakForBoss = true;
                }
                else if (m_WaveAttackStruct[currentWave] == WaveAttackerType.Boss)
                {
                    SpawnBoss();
                    BreakForBoss = true;
                }

                if (BreakForBoss)
                    break;
            }

            m_SpawnWaveCoroutineIsRunning = false;
        }
    }
    
    private void SpawnRandomSingleEnemy()
    {
        GameObject enemy = null;

        if (m_EnemyInstance && m_EnemyFastInstance)
        {
            bool spawnSimpleEnemy = Random.Range(0f, 1f) < 0.5f;

            GameObject instanceToSpawn = (spawnSimpleEnemy ? m_EnemyInstance : m_EnemyFastInstance);

            Vector3 instanceOrigin = (spawnSimpleEnemy ? 
                m_GingerBreadSpawnPoints[Random.Range(0, m_GingerBreadSpawnPoints.Length)].transform.position :
                m_CookieSpawnPoints[Random.Range(0, m_CookieSpawnPoints.Length)].transform.position
            );

            enemy = Instantiate(instanceToSpawn, instanceOrigin, Quaternion.identity);
        }
        else if(m_EnemyInstance)
        {
            GameObject instanceToSpawn = m_EnemyInstance;

            Vector3 instanceOrigin = m_GingerBreadSpawnPoints[Random.Range(0, m_GingerBreadSpawnPoints.Length)].transform.position;

            enemy = Instantiate(instanceToSpawn, instanceOrigin, Quaternion.identity);
        }
        else if (m_EnemyFastInstance)
        {
            GameObject instanceToSpawn = m_EnemyFastInstance;

            Vector3 instanceOrigin = m_CookieSpawnPoints[Random.Range(0, m_CookieSpawnPoints.Length)].transform.position;

            enemy = Instantiate(instanceToSpawn, instanceOrigin, Quaternion.identity);
        }

        if(enemy)
        {
            Vector3 targetSpawnPos = RandomSpawnPos();

            enemy.transform.Rotate(new Vector3(-90, 0, 0));
            enemy.GetComponent<Enemy>().StartWithASpawnAnim();
            enemy.GetComponent<Enemy>().SetTargetSpawnPosition(targetSpawnPos);

            m_EnemyCount++;
        }
    }

    private void SpawnRandomWaveStructure()
    {
        int randomStruct = Random.Range(0, m_WavesShapes.Length);
        EnemyWaveShape WaveShape = m_WavesShapes[randomStruct];

        Vector3 shapePos = RandomSpawnPos(WaveShape.m_MinSpawnOffset, WaveShape.m_MaxSpawnOffset);

        bool bonus = Random.Range(0f, 1f) < 0.5f;
        int bonusPos = -1;

        if(bonus)
        {
            bonusPos = Random.Range(0, WaveShape.m_EnemyWaveItems.Length);
        }

        int pos = 0;
        foreach (EnemyWaveShapeItem WaveShapeItem in WaveShape.m_EnemyWaveItems)
        {
            if (WaveShapeItem.m_EnemyInstance)
            {
                GameObject enemy = null;

                if (WaveShapeItem.m_EnemyInstance == m_EnemyInstance)
                {
                    enemy = Instantiate(WaveShapeItem.m_EnemyInstance, m_GingerBreadSpawnPoints[Random.Range(0, m_GingerBreadSpawnPoints.Length)].transform.position, Quaternion.identity);
                }
                else if (WaveShapeItem.m_EnemyInstance == m_EnemyFastInstance)
                {
                    enemy = Instantiate(WaveShapeItem.m_EnemyInstance, m_CookieSpawnPoints[Random.Range(0, m_CookieSpawnPoints.Length)].transform.position, Quaternion.identity);
                }

                if (enemy)
                {
                    Vector3 itemPose = shapePos + new Vector3(WaveShapeItem.m_RelativePosition.x, WaveShapeItem.m_RelativePosition.y, 0.0f);

                    enemy.transform.Rotate(new Vector3(-90, 0, 0));
                    enemy.GetComponent<Enemy>().StartWithASpawnAnim();
                    enemy.GetComponent<Enemy>().SetTargetSpawnPosition(itemPose);

                    if (bonusPos == pos)
                    {
                        enemy.GetComponent<Enemy>().SetSpawnBonus(true);
                    }

                    m_EnemyCount++;
                }
            }

            pos++;
        }
    }

    private Vector3 RandomSpawnPos(float p_MinOffset = 0f, float p_MaxOffset = 0f)
    {
        if (m_SpawnArea)
        {
            Vector3 spawnPos;
            spawnPos.x = Random.Range(m_SpawnArea.position.x - (m_SpawnArea.localScale.x / 2) + p_MinOffset, m_SpawnArea.position.x + (m_SpawnArea.localScale.x / 2) - p_MaxOffset);
            spawnPos.y = m_SpawnArea.position.y;
            spawnPos.z = m_SpawnArea.position.z;

            return spawnPos;
        }

        return new Vector3(0, 0, 0);
    }

    public void DecreaseEnemyCount()
    {
        m_EnemyCount--;
    }

    private void SpawnMiniBoss()
    {
        Vector3 SpawnPos = m_SpawnArea.position;

        GameObject miniboss = null;

        if (m_MiniBossInstance)
        {
            miniboss = Instantiate(m_MiniBossInstance, SpawnPos, Quaternion.identity);
            miniboss.transform.Rotate(new Vector3(-90, 0, 0));
        }

        if (miniboss && m_EnemyInstance && m_KillerEnemyInstance)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject instanceToSpawn = (i % 2 == 0 ? m_KillerEnemyInstance : m_EnemyInstance);
                
                GameObject enemy = null;

                if (instanceToSpawn == m_EnemyInstance)
                {
                    enemy = Instantiate(instanceToSpawn, m_GingerBreadSpawnPoints[Random.Range(0, m_GingerBreadSpawnPoints.Length)].transform.position, Quaternion.identity);
                }
                else if (instanceToSpawn == m_KillerEnemyInstance)
                {
                    enemy = Instantiate(instanceToSpawn, m_CupcakeSpawnPoints[Random.Range(0, m_CupcakeSpawnPoints.Length)].transform.position, Quaternion.identity);
                }

                if (enemy)
                {
                    enemy.transform.Rotate(new Vector3(-90, 0, 0));

                    Enemy NewEnemyScript = enemy.GetComponent<Enemy>();

                    NewEnemyScript.SetMovmentType(EnemyMovmentType.Circular);
                    NewEnemyScript.SetEnemySpeed(1.0f);
                    NewEnemyScript.SetRotationAngle(Mathf.PI / 2 * i);
                    NewEnemyScript.SetRotationRadius(2.2f);

                    NewEnemyScript.StartWithASpawnAnim();
                    NewEnemyScript.SetTargetSpawnPosition(SpawnPos);

                    m_EnemyCount++;
                }
            }
        }
    }

    public void MiniBossDead()
    {
        StartCoroutine(WaveSpawn());
    }

    private void SpawnBoss()
    {
        Vector3 SpawnPos = m_SpawnArea.position;

        GameObject boss = null;

        if (m_BossInstance)
        {
            boss = Instantiate(m_BossInstance, SpawnPos, Quaternion.identity);
            boss.transform.Rotate(new Vector3(-90, 0, 0));
        }

        if (boss && m_EnemyInstance && m_KillerPowerEnemyInstance)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject instanceToSpawn = (i % 2 == 0 ? m_KillerPowerEnemyInstance : m_EnemyInstance);

                GameObject enemy = null;

                if (instanceToSpawn == m_EnemyInstance)
                {
                    enemy = Instantiate(instanceToSpawn, m_GingerBreadSpawnPoints[Random.Range(0, m_GingerBreadSpawnPoints.Length)].transform.position, Quaternion.identity);
                }
                else if (instanceToSpawn == m_KillerPowerEnemyInstance)
                {
                    enemy = Instantiate(instanceToSpawn, m_DonutSpawnPoints[Random.Range(0, m_DonutSpawnPoints.Length)].transform.position, Quaternion.identity);
                }

                if (enemy)
                {
                    enemy.transform.Rotate(new Vector3(-90, 0, 0));

                    Enemy NewEnemyScript = enemy.GetComponent<Enemy>();

                    NewEnemyScript.SetMovmentType(EnemyMovmentType.Circular);
                    NewEnemyScript.SetEnemySpeed(1.0f);
                    NewEnemyScript.SetRotationAngle(Mathf.PI / 2 * i);
                    NewEnemyScript.SetRotationRadius(2.2f);

                    NewEnemyScript.StartWithASpawnAnim();
                    NewEnemyScript.SetTargetSpawnPosition(SpawnPos);

                    m_EnemyCount++;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                GameObject instanceToSpawn = (i % 4 == 0 ? m_KillerPowerEnemyInstance : m_EnemyInstance);

                GameObject enemy = null;

                if (instanceToSpawn == m_EnemyInstance)
                {
                    enemy = Instantiate(instanceToSpawn, m_GingerBreadSpawnPoints[Random.Range(0, m_GingerBreadSpawnPoints.Length)].transform.position, Quaternion.identity);
                }
                else if (instanceToSpawn == m_KillerPowerEnemyInstance)
                {
                    enemy = Instantiate(instanceToSpawn, m_DonutSpawnPoints[Random.Range(0, m_DonutSpawnPoints.Length)].transform.position, Quaternion.identity);
                }

                if (enemy)
                {
                    enemy.transform.Rotate(new Vector3(-90, 0, 0));

                    Enemy NewEnemyScript = enemy.GetComponent<Enemy>();

                    NewEnemyScript.SetMovmentType(EnemyMovmentType.Circular);
                    NewEnemyScript.SetEnemySpeed(1.0f);
                    NewEnemyScript.SetRotationAngle(Mathf.PI / 4 * i);
                    NewEnemyScript.SetRotationRadius(2.7f);

                    NewEnemyScript.StartWithASpawnAnim();
                    NewEnemyScript.SetTargetSpawnPosition(SpawnPos);
                    NewEnemyScript.SetRotationClockwise(false);

                    m_EnemyCount++;
                }
            }
        }
    }

    public void BossDead()
    {
        GameManagerr.Current.EndGame(true);
    }
}