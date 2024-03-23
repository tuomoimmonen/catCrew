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
    [SerializeField] AudioSource coinCollectionSound;
    [SerializeField] AudioSource coinChestSound;

    private void Start()
    {
        PlayerDetection.onDoorsHit += PlayDoorHitSound;
        GameManager.onGameStateChanged += GameStateChangedCallback;
        Enemy.onRunnerDie += PlayRunnerDieSound;
        PlayerDetection.onCoinCollected += PlayCoinCollectedSound;
    }

    private void OnDisable()
    {
        PlayerDetection.onDoorsHit -= PlayDoorHitSound;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
        Enemy.onRunnerDie -= PlayRunnerDieSound;
        PlayerDetection.onCoinCollected -= PlayCoinCollectedSound;
    }

    private void PlayCoinCollectedSound()
    {
        float randomPitch = Random.Range(1f, 1.05f);
        coinCollectionSound.pitch = randomPitch;
        coinCollectionSound.Play();
    }
    private void PlayDoorHitSound()
    {
        float randomPitch = Random.Range(1f, 1.05f);
        doorHitSound.pitch = randomPitch;
        doorHitSound.Play();
    }

    private void PlayRunnerDieSound()
    {
        float randomPitch = Random.Range(0.9f, 1.2f);
        runnerDieSound.pitch = randomPitch;
        runnerDieSound.Play();
    }

    private void PlayCoinChestSound()
    {
        float randomPitch = Random.Range(0.95f, 1.05f);
        coinChestSound.pitch = randomPitch;
        coinChestSound.Play();
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.LevelComplete)
        {
            float randomPitch = Random.Range(0.95f, 1.05f);
            levelCompleteSound.pitch = randomPitch;
            levelCompleteSound.Play();
            Invoke("PlayCoinChestSound", 1f);
        }
        else if(state == GameManager.GameState.GameOver)
        {
            float randomPitch = Random.Range(0.95f, 1.05f);
            gameOverSound.pitch = randomPitch;
            gameOverSound.Play();
        }
    }

    public void EnableSounds()
    {
        doorHitSound.volume = 1f;
        runnerDieSound.volume = 0.25f;
        levelCompleteSound.volume = 1f;
        gameOverSound.volume = 1f;
        buttonSound.volume = 1f;
        coinCollectionSound.volume = 1f;
        coinChestSound.volume = 1f;
        MusicManager.instance.musicSource.volume = 0.3f;
    }

    public void DisableSounds()
    {
        doorHitSound.volume = 0f;
        runnerDieSound.volume = 0f;
        levelCompleteSound.volume = 0f;
        gameOverSound.volume = 0f;
        buttonSound.volume = 0f;
        coinCollectionSound.volume = 0f;
        coinChestSound.volume = 0f;
        MusicManager.instance.musicSource.volume = 0f;
    }
}
