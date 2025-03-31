using UnityEngine;
using System.Collections.Generic;




public class card
{
    public string number;
}

public class deck
{

    //this a list of card objects that is the deck of cards
    private List<card> cardDeck = new List<card>();

    //creates the deck and adds the cards
    //this deck is 52 cards but all of the same suit
    //hence why there is no suit variable in the card class
    public void createDeck()
    {
        string[] numbers = { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };

        for (int i = 0; i < 4; i++)
        {
            foreach (string number in numbers)
            {
                {
                    card tempCard = new card();
                    tempCard.number = number;
                    cardDeck.Add(tempCard);
                }
            }
        }
        //add in the jokers of which there are only two
        card tempWild = new card();
        tempWild.number = "Joker";
        cardDeck.Add(tempWild);
        cardDeck.Add(tempWild);
    }

    //shuffles the deck using the Fisher-Yates algorithm
    public void shuffle()
    {
        int n = cardDeck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            card temp = cardDeck[k];
            cardDeck[k] = cardDeck[n];
            cardDeck[n] = temp;
        }
    }

    //function that returns the top card on the deck,
    //then removes it
    public card dealCard()
    {
     /*   List<List<card>> hands = new List<List<card>>();*/
        card tempCard = cardDeck[0];
        Debug.Log("Dealt");
        Debug.Log(cardDeck[0]);
        cardDeck.RemoveAt(0);
        return tempCard;
    }


    //a "burn" happens at the before dealing each player
    //their 2 cards and before the "flop"
    public void burn()
    {
        Debug.Log("burned top card");
        cardDeck.RemoveAt(0);
    }


    public void flipCard()
    {
        Debug.Log(cardDeck[0].number);
        cardDeck.RemoveAt(0);
    }
    //this is the function for the flop
    //revealing the 3 cards players can use to build their hand
    public void flop()
    {
        burn();
        flipCard();
        flipCard();
        flipCard();
    }
}

public class GameManager : MonoBehaviour
{
    void Start()
    {
        deck deckManager = new deck();
        deckManager.createDeck();
        deckManager.shuffle();
        deckManager.flop();
    }
}

