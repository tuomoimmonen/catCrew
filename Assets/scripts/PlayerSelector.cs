using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform runnersParent;
    [SerializeField] RunnerSelector runnerSelectorPrefab;

    void Start()
    {
        ShopManager.onSkinSelected += SelectSkin;
    }

    private void OnDisable()
    {
        ShopManager.onSkinSelected -= SelectSkin;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SelectSkin(Random.Range(0, 6));
        }
    }

    public void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < runnersParent.childCount; i++) 
        { 
            runnersParent.GetChild(i).GetComponent<RunnerSelector>().ChangeSkin(skinIndex);
        }

        runnerSelectorPrefab.ChangeSkin(skinIndex);
    }
}
