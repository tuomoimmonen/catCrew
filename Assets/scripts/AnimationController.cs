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
            Animator animator = runner.GetComponent<Runner>().GetAnimator();

            animator.SetTrigger("run");
        }
    }

    public void Idle()
    {
        Debug.Log(runnersParent.childCount);

        int animatorsToIdle = runnersParent.childCount;

        for (int i = 0; i < animatorsToIdle; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator animator = runner.GetComponent<Runner>().GetAnimator();
            Debug.Log(runner);

            animator.SetTrigger("idle");
        }

        StartCoroutine(StartIdling(animatorsToIdle)); //guard for non idlers
    }

    private IEnumerator StartIdling(int idleAnimators)
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < idleAnimators; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator animator = runner.GetComponent<Runner>().GetAnimator();

            animator.SetTrigger("idle");
        }

    }
}
