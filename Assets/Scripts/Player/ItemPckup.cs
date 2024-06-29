using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] public float pickupRange = 3f; 
    [SerializeField] public LayerMask itemLayer; 
    [SerializeField] public GameObject pickupPrompt;
    [SerializeField] public GameObject itemSlot;
    [SerializeField] private InputActionReference interact;
    [SerializeField] private InputActionReference useInput;
    [SerializeField] public bool holdingItem;

    private GameObject currentInteractable;

    void Update()
    {
        CheckForItems();
        
    }

    void Start()
    {
        interact.action.performed += HandleInput;
        useInput.action.performed += UseItem;
    }

    void CheckForItems()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange, itemLayer);

        if (colliders.Length > 0)
        {
            GameObject foundInteractable = colliders[0].gameObject;

            if (foundInteractable == currentInteractable && !holdingItem)
            {
                ShowPickupPrompt();
            }
            else
            {
                currentInteractable = foundInteractable;
                if (!holdingItem)
                {
                    ShowPickupPrompt();
                }
                else
                {
                    HidePickupPrompt();
                }
            }
        }
        else
        {
            currentInteractable = null;
            if (!holdingItem)
            {
                HidePickupPrompt();
            }
        }
    }


    void HandleInput(InputAction.CallbackContext obj)
    {
			Debug.Log("Here");
        if (currentInteractable != null)
        {
                PickUpItem(currentInteractable);
        }
    }

    void PickUpItem(GameObject item)
    {
        Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();

        if (holdingItem)
        {
            item.transform.parent = null;
            itemRigidbody.isKinematic = false; 
            itemRigidbody.useGravity = true;
            ShowPickupPrompt();
            holdingItem = false;
        }
        else
        {
            item.transform.parent = itemSlot.transform;
            item.transform.localPosition = Vector3.zero; 
            itemRigidbody.isKinematic = true; 
            itemRigidbody.useGravity = false;
            HidePickupPrompt();
            holdingItem = true;
        }

        Debug.Log("Picked up: " + item.name);
        HidePickupPrompt();
    }

    void UseItem(InputAction.CallbackContext obj)
    {
        if (holdingItem)
        {
            // Access the currently held item
            GameObject heldItem = itemSlot.transform.GetChild(0).gameObject;

            // Check if the held item has a script representing its functionality
            Item baseItem = heldItem.GetComponent<Item>();

            // If the held item has a script representing its functionality, use the item
            if (baseItem != null)
            {
                baseItem.UseItem();
                // Additional logic specific to using an item
            }
        }
    }


    void ShowPickupPrompt()
    {
        pickupPrompt.SetActive(true);
    }

    void HidePickupPrompt()
    {
        pickupPrompt.SetActive(false);
    }
}

