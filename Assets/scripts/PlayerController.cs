using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Elements")]
    [SerializeField] CrowdSystem crowdSystem;
    [SerializeField] AnimationController animController;

    [Header("Settings")]
    [SerializeField] float forwardSpeed = 1f;
    private bool canMove;

    [Header("Control")]
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] float roadWidth = 10f;
    Vector3 clickedScreenPosition;
    Vector3 clickedPlayerPosition;

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
        GameManager.onGameStateChanged += GameStateChangedCallback;
        Application.targetFrameRate = 60;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    void Update()
    {
        //DEBUG REMOVE WHEN BUILD
        if (Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
        if(canMove)
        {
            ForwardMovement();
            HorizontalMovement();
        }

    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Game)
        {
            StartMoving();
        }
        else if(state == GameManager.GameState.GameOver || state == GameManager.GameState.LevelComplete)
        {
            StopMoving();
        }
    }

    private void StartMoving()
    {
        canMove = true;
        animController.Run();
    }

    private void StopMoving()
    {
        canMove = false;
        animController.Idle();
    }

    private void ForwardMovement()
    {
        //forward movement
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
    }

    private void HorizontalMovement()
    {
        if(Input.GetMouseButtonDown(0)) //first contact
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0)) //pressing the screen
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;
            xScreenDifference /= Screen.width; //normalize
            xScreenDifference *= turnSpeed;
            //Debug.Log(xScreenDifference);
            
            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(), roadWidth / 2 - crowdSystem.GetCrowdRadius());
            transform.position = position;
        }
    }
}
