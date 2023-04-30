using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    private float money;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$ " + money.ToString("N2");
    }

    public void AddPay(float pay)
    {
        money += pay;
    }
}
