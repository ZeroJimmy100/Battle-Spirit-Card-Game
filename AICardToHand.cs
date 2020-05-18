using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AICardToHand : MonoBehaviour
{

    public List<Card> thisCard = new List<Card>();

    // public static List<Card> cardsInHandStatic = new List<Card>();

    // public List<Card> cardsInHand = new List<Card>();

    // public static int cardsInHandNumber;


    public int thisId;

    public int id;

    public string cardName;
    public int cost;
    public int power;
    public string cardDescription; 

    public Text nameText;
    public Text costText; 
    public Text powerText; 
    public Text descriptionText;

    public Sprite thisSprite; 
    public Image thatImage;

    public Image Frame;

    public static int DrawX;
    public int drawXcards;
    public int addXmaxMana;

    public int hurted;
    public int actualpower;
    public int returnXCards;

    public GameObject Hand; 

    public int z = 0;
    public GameObject It;

    public int numberOfCardsInDeck;

    public bool isTarget;
    public GameObject Graveyard;

    public bool thisCardcanBeDestroyed; 

    public GameObject cardBack;
    public GameObject AiZone;


    // Start is called before the first frame update
    void Start()
    {

        // cardsInHandStatic = cardsInHand;

        thisCard[0] = CardDataBase.cardList[thisId];
        Hand = GameObject.Find("Enemy Hand");

        z = 0;
        numberOfCardsInDeck += AI.deckSize;

        Graveyard = GameObject.Find("Enemy Graveyard");
        StartCoroutine(AfterVoidStart());

        AiZone = GameObject.Find("Enemy Zone");
    }

    // Update is called once per frame
    void Update()
    {
        if(z==0)
        {
            It.transform.SetParent(Hand.transform);
            It.transform.localScale = Vector3.one;
            It.transform.position = new Vector3(transform.position.x, transform.position.y, -48);
            It.transform.eulerAngles = new Vector3(25, 0, 0);
            z = 1;
        }

        id = thisCard[0].id;
        cardName = thisCard[0].cardName;
        cost = thisCard[0].cost;
        power = thisCard[0].power;
        cardDescription = thisCard[0].cardDescription;

        thisSprite = thisCard[0].thisImage;

        drawXcards = thisCard[0].drawXCards;
        addXmaxMana = thisCard[0].addXmaxMana;

        //Over here 
        returnXCards = thisCard[0].returnXcards;
       


        nameText.text = ""+cardName;
        costText.text = ""+cost;

        //Over here
        actualpower = power-hurted;

        powerText.text = ""+actualpower;
        descriptionText.text = ""+cardDescription;

        thatImage.sprite = thisSprite;

        if(thisCard[0].color=="Red")
        {
            Frame.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
        if(thisCard[0].color=="Blue")
        {
            Frame.GetComponent<Image>().color = new Color32(0, 0, 225, 255);
        }
        if(thisCard[0].color=="Yellow")
        {
            Frame.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
        }
        if(thisCard[0].color=="Purple")
        {
            Frame.GetComponent<Image>().color = new Color32(255, 0, 255, 255);
        }
        if(thisCard[0].color=="Green")
        {
            Frame.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        }

        if(this.tag == "Clone")
        {
            // cardsInHand[cardsInHandNumber] = AI.staticEnemyDeck[numberOfCardsInDeck-1];

            // cardsInHandNumber ++;

            thisCard[0] = AI.staticEnemyDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            AI.deckSize -= 1;
            this.tag = "Untagged";
        }

        if(hurted == power && thisCardcanBeDestroyed == true)
        {
            this.transform.SetParent(Graveyard.transform);
            hurted = 0;

        }


        if(this.transform.parent == Hand.transform)
        {
            //cardBack.SetActive(true);
        }

        if(this.transform.parent == AiZone.transform)
        {
            //cardBack.SetActive(false);
        }

        // for(int i=0; i < 40; i++)
        // {
        //     if(cardsInHand[i].id != 0)
        //     {
        //         cardsInHandStatic[i] = cardsInHand[i];
        //     }
        // }

    }

     public void BeingTarget()
        {
            isTarget = true; 

        }

        public void DontBeingTarget()
        {
            isTarget = false;
        }

        IEnumerator AfterVoidStart()
        {
            yield return new WaitForSeconds(1);

            thisCardcanBeDestroyed = true;
        }
}
