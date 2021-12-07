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

    private int m_CurrentWaveAttackerTypePose = 0;

    [SerializeField]
    private EnemyWaveShape[] m_WavesShapes;

    [SerializeField]
    private GameObject[] m_EnemyInstance;

    [SerializeField]
    [Min(0.1f)]
    private float m_EnemySpawnSpeed = 1.0f;

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

            if(m_WaveAttackStruct[m_CurrentWaveAttackerTypePose] == WaveAttackerType.MultipleEnemies)
            {
                for(int i = 0; i < m_CurrentWaveAttackerTypePose + 1; i++)
                {
                    SpawnRandomWaveStructure();
                }
            }
            else if (m_WaveAttackStruct[m_CurrentWaveAttackerTypePose] == WaveAttackerType.MiniBoss)
            {
                // TODO
            }
            else if (m_WaveAttackStruct[m_CurrentWaveAttackerTypePose] == WaveAttackerType.Boss)
            {
                // TODO
            }

            m_CurrentWaveAttackerTypePose++;

            if (m_CurrentWaveAttackerTypePose == m_WaveAttackStruct.Length)
                break;
        }
    }
    
    private void SpawnRandomSingleEnemy()
    {
        Vector3 newPos = RandomScreenPos();

        Instantiate(m_EnemyInstance[0], newPos, Quaternion.identity);

        m_EnemyCount++;
    }

    private void SpawnRandomWaveStructure()
    {
        int randomStruct = Random.Range(0, m_WavesShapes.Length);
        EnemyWaveShape WaveShape = m_WavesShapes[randomStruct];

        Vector3 shapePos = RandomScreenPos(WaveShape.m_MinSpawnOffset, WaveShape.m_MaxSpawnOffset);

        foreach (EnemyWaveShapeItem WaveShapeItem in WaveShape.m_EnemyWaveItems)
        {
            Vector3 itemPose = shapePos + new Vector3(WaveShapeItem.m_RelativePosition.x, WaveShapeItem.m_RelativePosition.y, 0.0f);
            Instantiate(WaveShapeItem.m_EnemyInstance, itemPose, Quaternion.identity);

            m_EnemyCount++;
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
}