using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Order
{
    public string restarauntName;
    public bool isActive;
    public bool isPickedUp;
    public bool isDelivered;
    public float pay;
    public Transform pickUpPoint;
    public Transform deliveryPoint;
}
