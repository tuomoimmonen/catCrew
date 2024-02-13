using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Button thisButton;
    [SerializeField] Image skinImage;
    [SerializeField] GameObject lockIcon;
    [SerializeField] GameObject selectorOutline;

    private bool unlocked;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Configure(Sprite skinSprite, bool unlocked)
    {
        skinImage.sprite = skinSprite;
        this.unlocked = unlocked;

        if(unlocked)
        {
            Unlock();
        }
        else
        {
            Lock();
        }


    }

    public void Unlock()
    {
        thisButton.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockIcon.gameObject.SetActive(false);

        unlocked = true;
    }

    private void Lock()
    {
        thisButton.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockIcon.gameObject.SetActive(true);
    }

    public void Select()
    {
        selectorOutline.SetActive(true);
    }

    public void DeSelect()
    {
        selectorOutline.SetActive(false);
    }

    public bool IsUnlocked()
    {
        return unlocked;
    }

    public Button GetButton()
    {
        return thisButton;
    }
}
