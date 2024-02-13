using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("Coin texts")]
    [SerializeField] TMP_Text[] cointexts;
    private int coins;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        coins = PlayerPrefs.GetInt("coins", 0);
    }
    void Start()
    {
        UpdateCoinsTexts();
    }

    void Update()
    {
        
    }

    private void UpdateCoinsTexts()
    {
        foreach (TMP_Text coinText in cointexts) 
        {
            coinText.text = coins.ToString();
        }
    }

    public void AddCoins(int coinAmount)
    {
        coins += coinAmount;

        UpdateCoinsTexts();
        PlayerPrefs.SetInt("coins", coins);
    }

    public int GetCoins() { return coins; }

    public void UseCoins(int coinAmount)
    {
        coins -= coinAmount;
        UpdateCoinsTexts();
        PlayerPrefs.SetInt("coins", coins);
    }


}
