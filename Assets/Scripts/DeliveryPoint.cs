using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    public string customerName;

    private OrderDispatch dispatch;

    private void Awake()
    {
        dispatch = GameObject.Find("Order Dispatch").GetComponent<OrderDispatch>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dispatch.currentOrder.isPickedUp)
        {
            dispatch.currentOrder.isDelivered = true;
            customerName = dispatch.currentOrder.deliveryPoint.name;
            Debug.Log("Delivered to " + customerName);
        }
    }
}
