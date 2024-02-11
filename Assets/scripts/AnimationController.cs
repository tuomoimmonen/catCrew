using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform runnersParent;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Run()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator animator = runner.GetComponent<Animator>();

            animator.SetTrigger("run");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator animator = runner.GetComponent<Animator>();

            animator.SetTrigger("idle");
        }
    }
}
