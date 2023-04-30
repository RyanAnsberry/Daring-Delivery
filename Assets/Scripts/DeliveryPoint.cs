using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    public string customerName;

    private Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.currentOrder.isPickedUp)
        {
            player.currentOrder.isDelivered = true;
            Debug.Log("Delivered!");
        }
    }
}
