using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] TMP_Text runnerCountText;
    [SerializeField] Transform runnersParent;
    void Start()
    {
        
    }

    void Update()
    {
        runnerCountText.text = runnersParent.childCount.ToString();

        if(runnersParent.childCount <= 0 ) { Destroy(gameObject); }
    }
}
