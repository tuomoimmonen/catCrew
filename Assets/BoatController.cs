using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] float boatSpeed = 1f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position -= Vector3.forward * boatSpeed * Time.deltaTime;
    }

    private void StartMoving()
    {

    }
}
