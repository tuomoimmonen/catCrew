using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Transform enemyParents;

    [Header("Settings")]
    [SerializeField] int enemyAmount;
    [SerializeField] float angle;
    [SerializeField] float radius;
    void Start()
    {
        GenerateEnemies();
    }

    void Update()
    {
        
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            Vector3 enemyLocalPosition = GetRunnerLocalPosition(i);

            Vector3 enemyWorldPosition = transform.TransformPoint(enemyLocalPosition);

            Instantiate(enemyPrefab, enemyWorldPosition, Quaternion.identity, enemyParents);
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }
}
