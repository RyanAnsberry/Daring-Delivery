using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Order currentOrder;

    public bool hasCurrentOrder;
    public float money;

    private void Update()
    {
        if (currentOrder.isDelivered)
        {
            money += currentOrder.pay;
            hasCurrentOrder = false;
            currentOrder.isActive = false;
            currentOrder.isPickedUp = false;
            currentOrder.isDelivered = false;
        }
    }
}
