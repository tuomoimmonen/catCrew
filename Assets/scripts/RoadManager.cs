using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager instance;
    [Header("Elements")]
    [SerializeField] LevelSO[] levels;
    [SerializeField] Road[] roadPrefabs; //for random levels
    [SerializeField] Road[] roadLevelPrefab;
    [SerializeField] GameObject finishLine;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    void Start()
    {
        GenerateLevel();
        //CreateOrderedLevel();
        //CreateRandomLevel();
        finishLine = GameObject.FindGameObjectWithTag("Finish");
    }

    private void GenerateLevel()
    {
        int currentLevel = GetLevel();
        currentLevel = currentLevel % levels.Length;
        LevelSO level = levels[currentLevel];

        CreateLevel(level.roads);
    }

    private void CreateOrderedLevel()
    {
        Vector3 roadPos = Vector3.zero;

        for (int i = 0; i < roadLevelPrefab.Length; i++)
        {
            Road roadToCreate = roadLevelPrefab[i];

            if (i > 0)
            {
                roadPos.z += roadToCreate.GetLenght() / 2;
            }

            Road roadInstance = Instantiate(roadToCreate, roadPos, Quaternion.identity, transform);

            roadPos.z += roadInstance.GetLenght() / 2;
        }
    }

    private void CreateLevel(Road[] roads)
    {
        Vector3 roadPos = Vector3.zero;

        for (int i = 0; i < roads.Length; i++)
        {
            Road roadToCreate = roads[i];

            if (i > 0)
            {
                roadPos.z += roadToCreate.GetLenght() / 2;
            }

            Road roadInstance = Instantiate(roadToCreate, roadPos, Quaternion.identity, transform);

            roadPos.z += roadInstance.GetLenght() / 2;
        }
    }

    private void CreateRandomLevel()
    {
        Vector3 roadPos = Vector3.zero;

        for (int i = 0; i < 5; i++)
        {
            Road roadToCreate = roadPrefabs[Random.Range(0, roadPrefabs.Length)];

            if (i > 0)
            {
                roadPos.z += roadToCreate.GetLenght() / 2;
            }

            Road roadInstance = Instantiate(roadToCreate, roadPos, Quaternion.identity, transform);

            roadPos.z += roadInstance.GetLenght() / 2;
        }
    }

    public float GetFinishPositionZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("level", 0);
    }
}
