using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderDispatch : MonoBehaviour
{
    public Order[] orders;
    public Transform[] customers;
    public Order currentOrder;

    public DeliveryArrow arrow;

    public GameObject pickUpSensor;
    public GameObject deliverySensor;
    public GameObject phone;
    public TextMeshProUGUI offerText;
    public TextMeshProUGUI payText;
    public TextMeshProUGUI distanceText;
    public GameManager gameManager;

    private float nextOrderDelay = 0f;


    private void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Time.time > nextOrderDelay && currentOrder.isActive == false)
            {
                SendOffer();
            }
            else if (currentOrder.isPickedUp && currentOrder.isDelivered == false)
            {
                CompletePickUp();
            }

            else if (currentOrder.isDelivered)
            {
                CompleteDelivery();
            }
        }
    }

    private void GenerateRandomOrder()
    {
        currentOrder = orders[Random.Range(0, orders.Length)];
        currentOrder.deliveryPoint = customers[Random.Range(0, customers.Length)];
        float distance = Vector2.Distance(currentOrder.pickUpPoint.position, currentOrder.deliveryPoint.position);
        distanceText.text = distance.ToString("N1") + " m";
        currentOrder.pay = distance / 4f;
    }

    public void SendOffer()
    {
        GenerateRandomOrder();

        phone.SetActive(true);
        currentOrder.isActive = true;
        offerText.text = "New Order From: " + currentOrder.restarauntName;
        payText.text = "$ " + currentOrder.pay.ToString("N2");
        nextOrderDelay = Time.time + Random.Range(3, 8);
    }

    private void CompletePickUp()
    {
        pickUpSensor.SetActive(false);
        deliverySensor.SetActive(true);
        arrow.target = deliverySensor.transform;
    }

    private void CompleteDelivery()
    {
        gameManager.AddPay(currentOrder.pay);

        // disable delivery sensor and arrow
        deliverySensor.SetActive(false);
        arrow.gameObject.SetActive(false);

        // reset current order
        currentOrder.isActive = false;
        currentOrder.isPickedUp = false;
        currentOrder.isDelivered = false;
    }

    public void AcceptOffer()
    {
        phone.SetActive(false);
        currentOrder.isActive = true;

        // move the sensors to the order's pick up and delivery points
        pickUpSensor.transform.position = currentOrder.pickUpPoint.position;
        deliverySensor.transform.position = currentOrder.deliveryPoint.position;

        // activate the pick up sensor
        pickUpSensor.SetActive(true);

        // activate the navigation arrow and set it's target to the pick up location
        arrow.gameObject.SetActive(true);
        arrow.target = pickUpSensor.transform;
    }

    public void DeclineOffer()
    {
        phone.SetActive(false);
        currentOrder.isActive = false;
        nextOrderDelay = Time.time + Random.Range(3, 8);
    }
}
