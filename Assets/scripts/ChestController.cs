using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [Header("Elements")]
    Animator chestAnim;
    [SerializeField] ParticleSystem openParticles;

    private void Awake()
    {
        chestAnim = GetComponent<Animator>();
    }
    void Start()
    {
        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }

    void Update()
    {
        
    }

    private void GameStateChangedCallBack(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.LevelComplete:
                chestAnim.SetTrigger("finish");
                //DataManager.instance.AddCoins(coinAmount);
                openParticles.Play();
                break;
        }
    }
}
