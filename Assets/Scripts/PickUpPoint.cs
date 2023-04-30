using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoint : MonoBehaviour
{
    public string restarauntName;

    private OrderDispatch dispatch;

    private void Awake()
    {
        dispatch = GameObject.Find("Order Dispatch").GetComponent<OrderDispatch>();
        restarauntName = dispatch.currentOrder.restarauntName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Order Picked Up From " + restarauntName);
        dispatch.currentOrder.isPickedUp = true;
    }
}
