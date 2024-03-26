using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State { Idle, Running}

    [Header("Elements")]
    Animator enemyAnim;
    [SerializeField] GameObject fightParticles;

    [Header("Settings")]
    [SerializeField] float searchRadius;
    [SerializeField] float enemySpeed;
    [SerializeField] float currentSpeed;
    [SerializeField] float accelerationSpeed = 0.1f;

    [Header("Events")]
    public static Action onRunnerDie;

    private State state;
    private Transform targetRunner;

    private void Awake()
    {
        enemyAnim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        enemyAnim.speed = UnityEngine.Random.Range(0.9f, 1.1f);
    }

    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                SearchForTarget();
                break;
            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                {
                    continue;
                }

                runner.SetTarget();
                targetRunner = runner.transform;

                StartRunningTowardsTarget();

                return; //prevent same enemy to set multiple targets
            }
        }
    }

    private void StartRunningTowardsTarget()
    {
        state = State.Running;
        //Debug.Log(state.ToString());
        enemyAnim.Play("bossRun");
    }

    private void RunTowardsTarget()
    {
        if(targetRunner == null)
        {
            return;
        }

        currentSpeed += enemySpeed * accelerationSpeed;
        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, currentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            onRunnerDie?.Invoke();
            targetRunner.SetParent(null);
            Instantiate(fightParticles, transform.position, Quaternion.identity);
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }
}
