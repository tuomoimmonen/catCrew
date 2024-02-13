using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSelector : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Runner runner;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeSkin(int skinIndex)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(i == skinIndex)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
