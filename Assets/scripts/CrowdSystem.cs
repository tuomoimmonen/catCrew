using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform runnersParent;
    [SerializeField] GameObject runnerPrefab;
    [SerializeField] AnimationController playerAnim;
    [SerializeField] GameObject goodFireworks;
    [SerializeField] GameObject badFireworks;

    [Header("Settings")]
    [SerializeField] float angle;
    [SerializeField] float radius;

    void Start()
    {
        
    }

    void Update()
    {
        if (!GameManager.instance.IsGameState())
        {
            return;
        }
        PlaceRunners();

        if(runnersParent.childCount <=  0)
        {
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
        }
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                Instantiate(goodFireworks, transform.position, Quaternion.identity);
                break;
            case BonusType.Multiplication:
                int runnersToAdd = (runnersParent.childCount * bonusAmount) - runnersParent.childCount;
                AddRunners(runnersToAdd);
                Instantiate(goodFireworks, transform.position, Quaternion.identity);
                break;
            case BonusType.Division:
                int runnerToRemove = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                RemoveRunners(runnerToRemove);
                Instantiate(badFireworks, transform.position, Quaternion.identity);
                break;
            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                Instantiate(badFireworks, transform.position, Quaternion.identity);
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnersParent);
            playerAnim.Run();
        }
    }

    private void RemoveRunners(int amount)
    {
        if (amount > runnersParent.childCount)
        {
            amount = runnersParent.childCount;
        }

        int runnersAmount = runnersParent.childCount;

        for (int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            //Debug.Log(i);
            Transform runnerToDestroy = runnersParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }
}
