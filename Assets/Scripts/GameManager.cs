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
    //and removes it from the deck
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
}

public class GameManager : MonoBehaviour
{
    deck deckManager = new deck();
    List<card> playerHand = new List<card>();
    List<card> compHand = new List<card>();
    List<card> communityCards = new List<card>();
    void Start()
    {
        deckManager.createDeck();
        deckManager.shuffle();
    }
    //round start
    //deal two cards
    public void dealHands()
    {
        deckManager.burn();
        for (int i = 0; i < 2; i++)
        {
          playerHand[i] = deckManager.dealCard();
          compHand[i] = deckManager.dealCard();
        }
    }

   
        //deal 2 cards to computer and player


    public void flop() { 
        //flop happens, 3 cards come out
        deckManager.burn();
        for (int i = 0; i < 3; i++)
        {
            communityCards[i] = deckManager.dealCard();
        }

    }
    //display the 3 cards


    //decide whether to check or fold
    //insert functions for that here
    public void decision()
    {

    }



    public void turnRiverCard() {
        //flip over the turn card
        deckManager.burn();
        communityCards.Add(deckManager.dealCard());
    }

        //check or fold


        //flip over the river card
      

        //check or fold


        //check for winning hand
    public void checkWinCon()
    {

    }


    }


