using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] AudioSource buttonSound;
    [SerializeField] AudioSource doorHitSound;
    [SerializeField] AudioSource runnerDieSound;
    [SerializeField] AudioSource levelCompleteSound;
    [SerializeField] AudioSource gameOverSound;

    private void Start()
    {
        PlayerDetection.onDoorsHit += PlayDoorHitSound;
        GameManager.onGameStateChanged += GameStateChangedCallback;
        Enemy.onRunnerDie += PlayRunnerDieSound;
        PlayerDetection.onCoinCollected += PlayDoorHitSound;
    }

    private void OnDisable()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
        Enemy.onRunnerDie -= PlayRunnerDieSound;
        PlayerDetection.onCoinCollected -= PlayDoorHitSound;
    }

    private void PlayDoorHitSound()
    {
        float randomPitch = Random.Range(0.5f, 1.5f);
        doorHitSound.pitch = randomPitch;
        doorHitSound.Play();
    }

    private void PlayRunnerDieSound()
    {
        float randomPitch = Random.Range(0.5f, 1.5f);
        runnerDieSound.pitch = randomPitch;
        runnerDieSound.Play();
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.LevelComplete)
        {
            float randomPitch = Random.Range(0.5f, 1.5f);
            levelCompleteSound.pitch = randomPitch;
            levelCompleteSound.Play();
        }
        else if(state == GameManager.GameState.GameOver)
        {
            float randomPitch = Random.Range(0.5f, 1.5f);
            gameOverSound.pitch = randomPitch;
            gameOverSound.Play();
        }
    }

    public void EnableSounds()
    {
        doorHitSound.volume = 1f;
        runnerDieSound.volume = 1f;
        levelCompleteSound.volume = 1f;
        gameOverSound.volume = 1f;
        buttonSound.volume = 1f;
    }

    public void DisableSounds()
    {
        doorHitSound.volume = 0f;
        runnerDieSound.volume = 0f;
        levelCompleteSound.volume = 0f;
        gameOverSound.volume = 0f;
        buttonSound.volume = 0f;
    }
}
