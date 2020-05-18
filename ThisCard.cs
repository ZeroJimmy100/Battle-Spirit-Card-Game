using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour
{

    public List<Card> thisCard = new List<Card>();
    
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

    public bool cardBack; 

    public static bool staticCardBack;

    public GameObject Hand;

    public int numberOfCardsInDeck;

    public bool canBeSummon;
    public bool summoned;
    public GameObject battleZone;

    public static int drawX;
    public int drawXcards;

    public int addXmaxMana;

    public GameObject attackBorder;

    public GameObject Target;

    public GameObject Enemy;

    public bool summoningExtreme;
    public bool cantAttack;

    public bool canAttack;
    public static bool staticTargeting;
    public static bool staticTargetingEnemy;
    public bool targeting; 
    public bool targetingEnemy;
    public bool onlythiscardAttack;

    public GameObject summonBorder;

    public bool canBeDestroyed;
    public GameObject Graveyard;
    public bool beInGraveyard;

    //Over here
    public int hurted;
    public int actualpower;
    public int returnXcards;
    public bool useReturn;

    public static bool UcanReturn;

    public int healXpower;
    public bool canHeal;

    public GameObject EnemyZone;

    public AICardToHand aICardToHand;
    

    // Start is called before the first frame update
    void Start()
    {
        thisCard[0] = CardDataBase.cardList[thisId];
        numberOfCardsInDeck = PlayerDeck.deckSize;

        canBeSummon = false;
        summoned = false;

        drawX = 0;

        canAttack = false;
        summoningExtreme = true;

        Enemy = GameObject.Find("EnemyHealth");

        targeting = false;
        targetingEnemy = false;

        beInGraveyard = false;
        
        canHeal = true;

        EnemyZone = GameObject.Find("Enemy Zone");

    }

    // Update is called once per frame
    void Update()
    {

        Hand = GameObject.Find("Hand");
        if(this.transform.parent == Hand.transform.parent)
        {
            cardBack = false;
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
        returnXcards = thisCard[0].returnXcards;
       
       healXpower = thisCard[0].healXpower;


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

        staticCardBack = cardBack;

        if(this.tag == "First")
        {
            thisCard[0] = PlayerDeck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            PlayerDeck.deckSize -= 1;
            cardBack = false;
            this.tag = "Untagged";
        }

        if(TurnSystem.currentMana >= cost && summoned==false && beInGraveyard == false && TurnSystem.isYourTurn == true)
        {
            canBeSummon = true;

        }else canBeSummon = false;

        if(canBeSummon == true)
        {
            gameObject.GetComponent<Draggable>().enabled = true;
        }else gameObject.GetComponent<Draggable>().enabled = false;
        
        battleZone = GameObject.Find("Zone");
        
        if(summoned == false && this.transform.parent == battleZone.transform)
        {
            Summon();
        }


        //Check here 
        if(canAttack == true && beInGraveyard == false)
        {

            attackBorder.SetActive(true);

        }else{
            attackBorder.SetActive(false);
        }

        if(TurnSystem.isYourTurn == false && summoned == true)
        {
            summoningExtreme = false;
            cantAttack = false;
        }

        if(TurnSystem.isYourTurn == true && summoningExtreme == false && cantAttack == false)
        {
            canAttack = true;
        }else{
            canAttack = false;
        }

        targeting = staticTargeting;
        targetingEnemy = staticTargetingEnemy;

        if(targetingEnemy == true)
        {

            Target = Enemy;

        }else{
            Target = null;
        }

        if(targeting == true  /* && targetingEnemy == true */ && onlythiscardAttack == true)
        {
            Attack();
        }

        //Check here
        if(canBeSummon == true || UcanReturn == true && beInGraveyard == true) /* fix */
        {
            summonBorder.SetActive(true);
        }else{
            summonBorder.SetActive(false);
        }

        //Over here
        if(actualpower <= 0)
        {
            Destroy();
        }

        if(returnXcards > 0 && summoned == true && useReturn == false && TurnSystem.isYourTurn == true)
        {
            Return(returnXcards);
            useReturn = true;
        }

        if(TurnSystem.isYourTurn == false)
        {
            UcanReturn = false;
        }

        if(canHeal == true && summoned == true)
        {
            Heal();
            canHeal = false;
        }



    }

    public void Summon()
    {
        TurnSystem.currentMana -= cost;
        summoned = true;


        MaxMana(addXmaxMana);
        drawX = drawXcards;

    }

    public void MaxMana(int x)
    {
        TurnSystem.maxMana += x;
    }

    public void Attack()
    {
        if(canAttack == true && summoned == true)
        {
            if(Target != null)
            {

              


                if(Target == Enemy)
                {
                    Enemyhp.staticHP -= power;
                    targeting = false;
                    cantAttack = true;
                }

                // if(Target.name == "CardToHand(Clone)")
                // {
                //     canAttack = true;
                // }
            }
            else{
                foreach(Transform child in EnemyZone.transform)
                {
                    if(child.GetComponent<AICardToHand>().isTarget == true)
                    {
                        child.GetComponent<AICardToHand>().hurted = power;
                        hurted = child.GetComponent<AICardToHand>().power;
                        cantAttack = true;
                    }                
                }
            }

        }
    }

    public void UntargetEnemy()
    {
        staticTargetingEnemy = false;
    }

    public void TargetEnemy()
    {
        staticTargetingEnemy = true;
    }

    public void startAttack()
    {
        staticTargeting = true;
    }

    public void StopAttack()
    {
        staticTargeting = false;
    }

    public void OneCardAttack()
    {
        onlythiscardAttack = true;
    }

    public void OneCardAttackStop()
    {
        onlythiscardAttack = false;
    }

    public void Destroy()
    {
        // Graveyard = GameObject.Find("Player Graveyard");
        // canBeDestroyed = true;
        // if(canBeDestroyed == true)
        // {
        //     this.transform.SetParent(Graveyard.transform);
        //     canBeDestroyed = false;
        //     summoned = false;
        //     beInGraveyard = true;

        //     hurted = 0;
        // }
   
   }

   public void Return(int x)
   {
       for(int i = 0; i <= x; i++)
       {
           ReturnCard();


       }
   }

   public void ReturnCard()
   {
       UcanReturn = true;

   }

   public void ReturnThis()
   {
       if(beInGraveyard == true && UcanReturn == true)
       {
           this.transform.SetParent(Hand.transform);
           UcanReturn = false;
           beInGraveyard = false;
           summoningExtreme = true;
       }
   }

   public void Heal()
   {
       Playerhp.staticHP += healXpower;
    }

}
