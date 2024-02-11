using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState { Menu, Game, LevelComplete, GameOver}

    private GameState gameState;

    public static Action<GameState> onGameStateChanged;

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
        
    }

    void Update()
    {
        
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState); //prevent errors if noone subscribed

        Debug.Log("gamestate changed: " + gameState);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
