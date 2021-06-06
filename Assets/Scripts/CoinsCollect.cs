using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class CoinsCollect : Entity
{
    // Start is called before the first frame update
    public static int coinsCount;
    public TMP_Text text;


    void Start()
    {
        coinsCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "X " + coinsCount;
    }
}