﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemyhp : MonoBehaviour
{
    public static float maxHP;
    public static float staticHP;

    public float hp;

    public Image Health;

    public Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        maxHP = 25000;
        staticHP = 20000;
    }

    // Update is called once per frame
    void Update()
    {
        hp = staticHP;
        Health.fillAmount = hp / maxHP;

        if(hp >= maxHP)
        {
            hp = maxHP;
        }

        hpText.text = hp + "HP";
    }
}
