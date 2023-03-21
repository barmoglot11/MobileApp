using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScript : MonoBehaviour
{
    public Card SelfCard;
    public Image CardImage;
    public TextMeshProUGUI CardName, CardAttack, CardHealth;
    public GameObject HideObj;

    public void HideInfoCard(Card card)
    {
        SelfCard = card;
        HideObj.SetActive(true);
        
    }
    public void ShowCardInfo(Card card)
    {
        SelfCard = card;

        CardImage.sprite = card.CardImage;
        CardImage.preserveAspect = true;
        CardName.text = card.CardName;
        CardAttack.text = card.CardAttack.ToString();
        CardHealth.text = card.CardHealth.ToString();
        HideObj.SetActive(false);
    } 
}
