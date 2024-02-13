using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] GameObject shopPanel;
    [SerializeField] SkinButton[] skinButtons;
    [SerializeField] Button purchaseButton;

    [Header("Skins")]
    [SerializeField] Sprite[] skins;

    [Header("Price")]
    [SerializeField] int skinPrice;
    [SerializeField] TMP_Text textPrice;

    [Header("Events")]
    public static Action<int> onSkinSelected;

    private void Awake()
    {
        UnlockSkin(0); //make sure the first skin is unlocked
        textPrice.text = skinPrice.ToString();
    }
    IEnumerator Start()
    {
        ConfigureButtons();
        UpdatePurchaseButton();

        yield return null; //wait for next frame

        SelectSkin(GetLastSelectedSkin());
    }

    void Update()
    {

    }

    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;
            skinButtons[i].Configure(skins[i], unlocked);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        //Debug.Log(PlayerPrefs.GetInt("skinButton" + skinIndex).ToString());
        skinButtons[skinIndex].Unlock();
    }

    private void SelectSkin(int skinIndex)
    {
        for(int i = 0;i < skinButtons.Length;i++)
        {
            if(skinIndex == i)
            {
                skinButtons[i].Select();
            }
            else
            {
                skinButtons[i].DeSelect();
            }
        }

        onSkinSelected?.Invoke(skinIndex);
        SaveLastSelectedSkin(skinIndex);
    }

    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }

    public void PurchaseSkin()
    {
        List<SkinButton> skinButtonList = new List<SkinButton>();

        for(int i = 0; i < skinButtons.Length; i++)
            if (!skinButtons[i].IsUnlocked())
                skinButtonList.Add(skinButtons[i]);


        if (skinButtonList.Count <= 0)
            return;

        SkinButton randomSkinButton = skinButtonList[UnityEngine.Random.Range(0, skinButtonList.Count)];

        UnlockSkin(randomSkinButton);
        SelectSkin(randomSkinButton.transform.GetSiblingIndex());

        DataManager.instance.UseCoins(skinPrice);

        UpdatePurchaseButton();
    }

    public void UpdatePurchaseButton()
    {
        if(DataManager.instance.GetCoins() < skinPrice)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }
    }

    private int GetLastSelectedSkin()
    {
        return PlayerPrefs.GetInt("lastSelectedSkin", 0);
    }

    private void SaveLastSelectedSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("lastSelectedSkin", skinIndex);
    }
}
