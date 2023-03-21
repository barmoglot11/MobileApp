using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck;

    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();
    }
    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();

        for(int i = 0; i < 10; i++)
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);

        return list;
    }
}

public class GameManagerScript : MonoBehaviour
{
    public Game CurrentGame;
    public Transform enemyHand, playerHand;
    public GameObject CardPref;
    public TextMeshProUGUI TimerUIText;
    public Button ButtonEndTurnUI;
    int Turn, TurnTime = 30;

    public List<CardInfoScript> PlayerHandCards = new List<CardInfoScript>(),   EnemyHandCards = new List<CardInfoScript>(), 
                                PlayerFieldCards = new List<CardInfoScript>(),  EnemyFieldCards = new List<CardInfoScript>();

    public bool isPlayerTurn
    {
        get
        {
            return Turn % 2 == 0;
        }
    }

    void Start()
    {
        Turn = 0;
        CurrentGame = new Game();

        GiveHandCard(CurrentGame.EnemyDeck, enemyHand);
        GiveHandCard(CurrentGame.PlayerDeck, playerHand);

        StartCoroutine(TurnFunc());
    }

    void GiveHandCard(List<Card> Deck, Transform Hand)
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(Deck, Hand);
    }

    void GiveCardToHand(List<Card> Deck, Transform Hand)
    {
        if (Deck.Count == 0)
            return;

        Card card = Deck[0];
        GameObject cardGO = Instantiate(CardPref, Hand, false);

        if (Hand == enemyHand)
        {
            cardGO.GetComponent<CardInfoScript>().HideInfoCard(card);
            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScript>());
        }
        else
        {
            cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card);
            PlayerHandCards.Add(cardGO.GetComponent<CardInfoScript>());
        }

        Deck.RemoveAt(0);
    }
    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TimerUIText.text = TurnTime.ToString();

        if (isPlayerTurn)
        {
            while(TurnTime-- > 0)
            {
                TimerUIText.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            while (TurnTime-- > 27)
            {
                TimerUIText.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        ChangeTurn();
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;

        ButtonEndTurnUI.interactable = isPlayerTurn;

        if (isPlayerTurn)
            GiveNewCard();

        StartCoroutine(TurnFunc());
    }

    void GiveNewCard()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, enemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, playerHand);
    }
}
