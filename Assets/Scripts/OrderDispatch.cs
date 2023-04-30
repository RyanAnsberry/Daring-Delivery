using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderDispatch : MonoBehaviour
{
    public Order[] orders;
    public Transform[] customers;
    public Order currentOrder;

    public Player player;

    public GameObject pickUpSensor;
    public GameObject deliverySensor;
    public GameObject phone;
    public TextMeshProUGUI offerText;
    public TextMeshProUGUI payText;

    private float nextOrderDelay = 0f;

    private void Start()
    {
            GenerateRandomOrder();
    }

    private void Update()
    {
        if (!player.hasCurrentOrder && Time.time > nextOrderDelay)
        {
            SendOffer();
        }

        if (player.currentOrder.isPickedUp && player.currentOrder.isDelivered == false)
        {
            pickUpSensor.SetActive(false);
            deliverySensor.SetActive(true);
        }

        if (player.currentOrder.isDelivered)
        {
            deliverySensor.SetActive(false);
            GenerateRandomOrder();
        }
    }

    private void GenerateRandomOrder()
    {
        currentOrder = orders[Random.Range(0, orders.Length)];
        currentOrder.deliveryPoint = customers[Random.Range(0, customers.Length)];
        float distance = Vector2.Distance(currentOrder.pickUpPoint.position, currentOrder.deliveryPoint.position);
        currentOrder.pay = distance / 4f;
    } 

    public void SendOffer()
    {
        phone.SetActive(true);
        offerText.text = "New Order From: " + currentOrder.restarauntName;
        payText.text = "$ " + currentOrder.pay.ToString("N2");
        nextOrderDelay = Time.time + Random.Range(3, 6);
    }

    public void AcceptOffer()
    {
        phone.SetActive(false);
        player.hasCurrentOrder = true;
        currentOrder.isActive = true;
        player.currentOrder = currentOrder;
        pickUpSensor.transform.position = currentOrder.pickUpPoint.position;
        deliverySensor.transform.position = currentOrder.deliveryPoint.position;
        pickUpSensor.SetActive(true);
    }

    public void DeclineOffer()
    {
        GenerateRandomOrder();
        phone.SetActive(false);
    }
}
