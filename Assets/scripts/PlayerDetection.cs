using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] CrowdSystem crowdSystem;
    [SerializeField] Transform runnersParent;
    [SerializeField] GameObject coinCollectEffect;

    [Header("Events")]
    public static Action onDoorsHit;
    public static Action onCoinCollected;

    [Header("Settings")]
    [SerializeField] int minCoinAmounToAdd;
    [SerializeField] int maxCoinAmounToAdd;

    bool runDetection = true;
    void Start()
    {
        
    }

    void Update()
    {
        
        if (GameManager.instance.IsGameState())
        {
            if(runDetection) { DetectColliders(); }
            else { StartCoroutine(DetectionCooldown()); }
            
        }
        
    }

    private void FixedUpdate()
    {
        /*
        if (GameManager.instance.IsGameState())
        {
            DetectColliders();
        }
        */
    }

    private void DetectColliders()
    {
        //Collider[] detectedColliders = Physics.OverlapSphere(transform.position, crowdSystem.GetCrowdRadius());
        
        var detectedColliders = new Collider[30];
        Physics.OverlapSphereNonAlloc(transform.position, crowdSystem.GetCrowdRadius(), detectedColliders);
        

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i] != null)
            {
                if (detectedColliders[i].TryGetComponent(out Doors doors))
                {
                    int bonusAmount = doors.GetBonusAmount(transform.position.x);
                    BonusType bonusType = doors.GetBonusType(transform.position.x);

                    doors.Disable(); //disable doors to prevent multiple hits
                                     //PlayerController.instance.ShootFireWorks();

                    crowdSystem.ApplyBonus(bonusType, bonusAmount);
                    //Debug.Log("door");

                    onDoorsHit?.Invoke();
                }
                else if (detectedColliders[i].tag == "Finish")
                {
                    int randomCoinAmount = UnityEngine.Random.Range(minCoinAmounToAdd, maxCoinAmounToAdd) * runnersParent.childCount;
                    DataManager.instance.AddCoins(randomCoinAmount);
                    //Debug.Log(randomCoinAmount.ToString());
                    //Debug.Log("finish");
                    PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                    GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);
                    //SceneManager.LoadScene(0);
                }

                else if (detectedColliders[i].tag == "Coin")
                {
                    onCoinCollected?.Invoke();
                    Instantiate(coinCollectEffect, transform.position, Quaternion.identity);
                    Destroy(detectedColliders[i].gameObject);
                    DataManager.instance.AddCoins(1);
                }
            }

        }

        runDetection = false;
    }

    private IEnumerator DetectionCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        runDetection = true;
    }
}
