using UnityEngine;
using System.Collections.Generic;
using TMPro;



public class card
{
    public int number;
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

        int[] numbers = { 2,3,4,5,6,7,8,9,10,11,12,13,14 };

        for (int i = 0; i < 4; i++)
        {
            foreach (int number in numbers)
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
        tempWild.number = 100;
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
        card tempCard = cardDeck[0];
        Debug.Log("Dealt");
        Debug.Log(cardDeck[0].number);
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

public class DeckManager : MonoBehaviour
{
    //health for the player and npc
    public float pHealth = 5;
    public float npcHealth = 5;

    //all the sprites for the cards
    public Sprite back;
    public Sprite ones;
    public Sprite twos;
    public Sprite three;
    public Sprite four;
    public Sprite fives;
    public Sprite sixes;
    public Sprite sevens;
    public Sprite eight;
    public Sprite nine;
    public Sprite ten;
    public Sprite jack;
    public Sprite queen;
    public Sprite king;
    public Sprite ace;
    public Sprite joker;

    //health text references
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI playerHealthText;



    // all the sprite renderers for each object
    public List<SpriteRenderer> commRenderer;
    public List<SpriteRenderer> playerRenderer;
    public List<SpriteRenderer> npcRenderer;

    //lists of objects for each card
    public List<GameObject> communitySprites;
    public List<GameObject> npcSprites;
    public List<GameObject> playerSprites;

    //button references for actions in the game
    public GameObject checkButton;
    public GameObject foldButton;
    public GameObject roundButton;

    //Lists of cards for each hand in the game
    deck deckManager = new deck();
    public List<card> playerHand = new List<card>();
    public List<card> compHand = new List<card>();
    public List<card> communityCards = new List<card>();

    public int decisionCount;


    public void startGame()
    {
        enemyHealthText.text = "Health: " + npcHealth.ToString("F2");
        playerHealthText.text = "Health: " + pHealth.ToString("F2");
        decisionCount = 0;
        //set up all the renderers to the right objects
        playerRenderer.Add(playerSprites[0].GetComponent<SpriteRenderer>());
        playerRenderer.Add(playerSprites[1].GetComponent<SpriteRenderer>());

        npcRenderer.Add(npcSprites[0].GetComponent<SpriteRenderer>());
        npcRenderer.Add(npcSprites[1].GetComponent<SpriteRenderer>());

        commRenderer.Add(communitySprites[0].GetComponent<SpriteRenderer>());
        commRenderer.Add(communitySprites[1].GetComponent<SpriteRenderer>());
        commRenderer.Add(communitySprites[2].GetComponent<SpriteRenderer>());
        commRenderer.Add(communitySprites[3].GetComponent<SpriteRenderer>());
        commRenderer.Add(communitySprites[4].GetComponent<SpriteRenderer>());

        //set the buttons to be allowed to use
        checkButton.SetActive(true);
        foldButton.SetActive(true);
        roundButton.SetActive(false);

        deckManager.createDeck();
        deckManager.shuffle();
        dealHands();
        flop();


    }

    public void resetRound()
    {
        deckManager = new deck();


        //players
        for (int i = 0; i < playerHand.Count; i++)
        {
            playerHand.RemoveAt(i);
        }

        for (int i = 0; i < playerRenderer.Count; i++)
        {
            playerRenderer[i].sprite = getSprite(0);
        }

        //npc
        for (int i = 0; i < compHand.Count; i++)
        {
            compHand.RemoveAt(i);
        }

        for (int i = 0; i < npcRenderer.Count; i++)
        {
            npcRenderer[i].sprite = getSprite(0);
        }

        //community
        for (int i = 0; i < communityCards.Count; i++)
        {
            communityCards.RemoveAt(i);
        }

        for (int i = 0; i < commRenderer.Count; i++)
        {
            commRenderer[i].sprite = getSprite(0);
        }

        playerHand.Clear();
        compHand.Clear();
        communityCards.Clear();

        startGame();
    }

    public Sprite getSprite(int number)
    {
        Sprite tempSprite = null;

        switch (number)
        {
            case 0:
                tempSprite = back;
                break;
            case 2:
                tempSprite = twos;
                break;
            case 3:
                tempSprite = three;
                break;
            case 4:
                tempSprite = four;
                break;
            case 5:
                tempSprite = fives;
                break;
            case 6:
                tempSprite = sixes;
                break;
            case 7:
                tempSprite = sevens;
                break;
            case 8:
                tempSprite = eight;
                break;
            case 9:
                tempSprite = nine;
                break;
            case 10:
                tempSprite = ten;
                break;
            case 11:
                tempSprite = jack;
                break;
            case 12:
                tempSprite = queen;
                break;
            case 13:
                tempSprite = king;
                break;
            case 14:
                tempSprite = ace;
                break;
            case 100:
                tempSprite = joker;
                break;
            default:
                break;
        }
        return tempSprite;
    }

    //round start
    //deal two cards
    public void dealHands()
    {
        deckManager.burn();
        for (int i = 0; i < 2; i++)
        {
            playerHand.Add(deckManager.dealCard());
            playerRenderer[i].sprite = getSprite(playerHand[i].number);

            compHand.Add(deckManager.dealCard());
        }
    }

    public void flop()
    {
        //flop happens, 3 cards come out
        deckManager.burn();
        for (int i = 0; i < 3; i++)
        {
            communityCards.Add(deckManager.dealCard());
            commRenderer[i].sprite = getSprite(communityCards[i].number);
        }
    }

    public void fold()
    {
        pHealth -= 0.5f;
        resetRound();
    }

    //display the 3 cards
    public void turnRiverCard()
    {

        if (communityCards.Count < 5)
        {
            Debug.Log("dealing card for turn river card");
            //flip over the turn card
            deckManager.burn();
            communityCards.Add(deckManager.dealCard());
            commRenderer[communityCards.Count - 1].sprite = getSprite(communityCards[communityCards.Count - 1].number);
            decisionCount += 1;
        }
        else if (decisionCount == 2)
        {
            Debug.Log("revealing npc cards");
            npcRenderer[0].sprite = getSprite(compHand[0].number);
            npcRenderer[1].sprite = getSprite(compHand[1].number);

            checkButton.SetActive(false);
            foldButton.SetActive(false);

            if (checkWinCon() == true)
            {
                npcHealth -= 0.5f;
                enemyHealthText.text = "Health: " + npcHealth.ToString("F2");
            }
            else
            {
                pHealth -= 1;
                playerHealthText.text = "Health: " + pHealth.ToString("F2");
             }
            roundButton.SetActive(true);
        } 

    }
    //check for winning hand
    public bool checkWinCon()
    {
        for (int i = 0; i < communityCards.Count; i++)
        {
            playerHand.Add(communityCards[i]);
        }

        for (int i = 0; i < communityCards.Count; i++)
        {
            compHand.Add(communityCards[i]);
        }

        //checking for the player
        int playerScore = 0;
        int pHandValue = 0;

        playerHand.Sort((card p1, card p2) => p1.number.CompareTo(p2.number));  
        playerHand.Reverse(); 

        for (int i = 0; i < 5; i++)
        {
            playerScore += playerHand[i].number;
        }

        if(playerScore > 100)
        {
            pHandValue = 10;
        }

        if (playerScore == 60)
        {
            Debug.Log("Player Royal Flush");
            pHandValue = 8;
        }

        if (playerScore > 57 && playerScore < 68)
        { 
            Debug.Log("Player Royal Full House");
            pHandValue = 7;
        }

        if (playerScore > 23 && playerScore < 40)
        {
            Debug.Log("Player Not So Royal But Kind Of Straight Flush");
            pHandValue = 6;
        }

        if(playerScore == 69)
        {
            Debug.Log("Player Nice");
            pHandValue = 5;
        }

        if(playerScore >= 40 && playerScore <= 50)
        {
            Debug.Log("Player 4 Hand");
            pHandValue = 4;
        }

        if (playerScore >= 30 && playerScore <= 40)
        {
            Debug.Log("Player 3 Hand");
            pHandValue = 3;
        }

        if (playerScore >= 20 && playerScore <= 30)
        {
            Debug.Log("Player 2 Hand");
            pHandValue = 2;
        }

        if (playerScore >= 10 && playerScore <= 20)
        {
            Debug.Log("Player 1 Hand");
            pHandValue = 1;
        }
  

     

        //checking for the npc
        int npcScore = 0;
        int npcHandValue = 0;
        compHand.Sort((card p1, card p2) => p1.number.CompareTo(p2.number));
        compHand.Reverse();

        for (int i = 0; i < 5; i++)
        {
            npcScore += compHand[i].number;
        }

        if (npcScore > 100)
        {
            npcHandValue = 10;
        }

        if (npcScore == 60)
        {
            Debug.Log("Enemy Royal Flush");
            npcHandValue = 8;
        }

        if (npcScore > 57 && npcScore < 68)
        {
            Debug.Log("Enemy Royal Full House");
            npcHandValue = 7;
        }

        if (npcScore > 23 && npcScore < 40)
        {
            Debug.Log("Enemy Not So Royal But Kind Of Straight Flush");
            npcHandValue = 6;
        }

        if (npcScore == 69)
        {
            Debug.Log("Enemy Nice");
            npcHandValue = 5;
        }

        if (npcScore >= 40 && npcScore <= 50)
        {
            Debug.Log("Enemy 4 Hand");
            npcHandValue = 4;
        }

        if (npcScore >= 30 && npcScore <= 40)
        {
            Debug.Log("Enemy 3 Hand");
            npcHandValue = 3;
        }

        if (npcScore >= 20 && npcScore <= 30)
        {
            Debug.Log("Enemy 2 Hand");
            npcHandValue = 2;
        }

        if (npcScore >= 10 && npcScore <= 20)
        {
            Debug.Log("Enemy 1 Hand");
            npcHandValue = 1;
        }


        if (npcHandValue > pHandValue)
        {
            return false;
        }

        if(npcHandValue == pHandValue)
        {
            if(npcScore > playerScore)
            {
                return false;
            }
        }

        return true;

    }

    
}