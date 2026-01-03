using System;
using System.Collections;
using UnityEngine;

public class Market : MonoBehaviour
{
    public Transform[] deliverySlots;
    public Transform[] customerSlots;

    public Transform deliveryStart;
    public Transform deliveryEnd;
    public Transform customerStart;
    public Transform customerEnd;

    public Delivery[] currentDeliveries;
    public Customer[] currentCustomers;

    public int GetDeliveryEmptySlot()
    {
        return Array.FindIndex(currentDeliveries, x => x == null);
    }

}
