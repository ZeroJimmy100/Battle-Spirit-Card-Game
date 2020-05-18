using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
  public static List<Card> cardList = new List<Card> ();

  void Awake()
  {
      cardList.Add (new Card (0, "None", 0, 0, "None", Resources.Load <Sprite>("test"), "Green", 0, 0, 0, 0 ));
      cardList.Add (new Card (1, "Elf", 2, 1000, "Draw 2 card", Resources.Load <Sprite>("test"), "Purple", 2, 0, 0, 0 ));
      cardList.Add (new Card (2, "Dwarf", 3, 3000, "Add 1 max Spirit", Resources.Load <Sprite>("test"), "Blue", 0, 1, 0, 0 ));
      cardList.Add (new Card (3, "Human", 5, 6000, "Add 3 max Spirit", Resources.Load <Sprite>("test"), "Yellow", 0, 3, 0, 0 ));
      cardList.Add (new Card (4, "Demon", 1, 1000, "Draw 1 card from graveyard", Resources.Load <Sprite>("test"), "Red", 0, 0, 1, 0 ));
      cardList.Add (new Card (5, "Healer", 1, 1000, "Heal 2000", Resources.Load<Sprite>("test"), "Purple", 0, 0, 0, 2000 ));
  }
}
