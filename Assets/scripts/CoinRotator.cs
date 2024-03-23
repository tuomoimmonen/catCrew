using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float minRotationSpeed;
    [SerializeField] float maxRotationSpeed;
    float randomRotationSpeed;

    void Start()
    {
        randomRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }



    void Update()
    {
        transform.Rotate(0, randomRotationSpeed * Time.deltaTime, 0);
    }
}
