using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Vector3 size;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, size);
    }

    public float GetLenght() //for roadmanager
    {
        return size.z;
    }
}
