using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BonusType {Addition, Difference, Multiplication, Division}
public class Doors : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] SpriteRenderer leftDoorRenderer;
    [SerializeField] SpriteRenderer rightDoorRenderer;
    [SerializeField] TMP_Text leftDoorText;
    [SerializeField] TMP_Text rightDoorText;
    [SerializeField] Collider doorCollider;

    [Header("Settings")]
    [SerializeField] BonusType rightDoorBonusType;
    [SerializeField] int rightDoorBonusAmount;

    [SerializeField] BonusType leftDoorBonusType;
    [SerializeField] int leftDoorBonusAmount;

    [SerializeField] Color bonusColor;
    [SerializeField] Color penaltyColor;

    void Start()
    {
        ConfigureDoors();
    }

    void Update()
    {
        
    }

    private void ConfigureDoors()
    {
        //right door

        switch (rightDoorBonusType)
        {
            case BonusType.Addition:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "+" + rightDoorBonusAmount;
                break;
            case BonusType.Difference:
                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "-" + rightDoorBonusAmount;
                break;
            case BonusType.Multiplication:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "*" + rightDoorBonusAmount;
                break;
            case BonusType.Division:
                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "/" + rightDoorBonusAmount;
                break;
        }

        //left door
        switch (leftDoorBonusType)
        {
            case BonusType.Addition:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "+" + leftDoorBonusAmount;
                break;
            case BonusType.Difference:
                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "-" + leftDoorBonusAmount;
                break;
            case BonusType.Multiplication:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "*" + leftDoorBonusAmount;
                break;
            case BonusType.Division:
                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "/" + leftDoorBonusAmount;
                break;
        }
    }

    public int GetBonusAmount(float xPosition)
    {
        if(xPosition > 0) { return rightDoorBonusAmount; }
        else { return leftDoorBonusAmount; }
    }

    public BonusType GetBonusType(float xPosition)
    {
        if(xPosition > 0) { return rightDoorBonusType; }
        else { return leftDoorBonusType; }
    }

    public void Disable()
    {
        doorCollider.enabled = false;
    }
}
