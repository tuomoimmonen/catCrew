using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] CrowdSystem crowdSystem;

    [Header("Events")]

    public static Action onDoorsHit;
    public static Action onCoinCollected;
    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.instance.IsGameState())
        {
            DetectColliders();
        }
    }

    private void DetectColliders()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, crowdSystem.GetCrowdRadius());

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.Disable(); //disable doors to prevent multiple hits

                crowdSystem.ApplyBonus(bonusType, bonusAmount);
                //Debug.Log("door");

                onDoorsHit?.Invoke();
            }
            else if (detectedColliders[i].tag == "Finish")
            {
                //Debug.Log("finish");
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);
                //SceneManager.LoadScene(0);
            }

            else if (detectedColliders[i].tag == "Coin")
            {
                Destroy(detectedColliders[i].gameObject);
                onCoinCollected?.Invoke();
                DataManager.instance.AddCoins(1);
                
            }
        }
    }
}
