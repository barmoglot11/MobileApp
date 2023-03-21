using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string CardName;
    public Sprite CardImage;
    public int CardAttack, CardHealth;

    public Card(string name, string imagePath, int attack, int health)
    {
        CardName = name;
        CardImage = Resources.Load<Sprite>(imagePath);
        CardAttack = attack;
        CardHealth = health;
    }
}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManagerScript : MonoBehaviour
{
    public void Awake()
    {
        CardManager.AllCards.Add(new Card("Rei", "Sprites/Cards/rei", 2, 2));
        CardManager.AllCards.Add(new Card("Artist", "Sprites/Cards/artist", 6, 6));
        CardManager.AllCards.Add(new Card("Gas Girl", "Sprites/Cards/gasMask", 3, 6));
        CardManager.AllCards.Add(new Card("GigaKnight", "Sprites/Cards/GigaKnight", 7, 4));
        CardManager.AllCards.Add(new Card("Glitch", "Sprites/Cards/Glitch", 4, 4));
    }
}
