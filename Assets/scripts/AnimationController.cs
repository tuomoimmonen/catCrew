using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform runnersParent;
    ParticleSystem runParticles;
    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            Idle();
        }
    }

    public void Run()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator animator = runner.GetComponent<Runner>().GetAnimator();
            animator.speed = UnityEngine.Random.Range(0.95f, 1.05f);
            runParticles = runner.GetComponent<Runner>().GetParticleSystem();
            runParticles.Play();
            //animator.SetTrigger("run"); //triggerit aktivoituu randomilla, ei toimi run->idle
            animator.Play("cat9Run");
        }
    }

    public void Idle()
    {
        //Debug.Log(runnersParent.childCount);

        int animatorsToIdle = runnersParent.childCount;
        
        for (int i = 0; i < animatorsToIdle; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator animator = runner.GetComponent<Runner>().GetAnimator();
            runParticles = runner.GetComponent<Runner>().GetParticleSystem();
            runParticles.Stop();
            //Debug.Log(runner);

            //animator.SetTrigger("idle"); //triggerit aktivoituu randomilla, ei toimi
            animator.Play("cat9Idle");
        }
    }

    public void WinAnimation()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator animator = runner.GetComponent<Runner>().GetAnimator();
            runParticles = runner.GetComponent<Runner>().GetParticleSystem();
            runParticles.Stop();
            int randomAnim = Random.Range(0, 3);
            animator.speed = Random.Range(1, 1.5f);
            switch (randomAnim)
            {
                case 0:
                    animator.Play("cat9WinDance");
                    break;
                case 1:
                    animator.Play("cat9Win");
                    break;
                case 2:
                    animator.Play("cat9WinClap");
                    break;
            }
        }
    }
}
