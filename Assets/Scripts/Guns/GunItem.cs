using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : MonoBehaviour
{
    [SerializeField] private ItemInventory weaponItem;

    private void OnTriggerStay(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory && Input.GetKey(KeyCode.E))
        {
            ItemInventory newGunItem = Instantiate(weaponItem);
            playerInventory.AddItem(newGunItem);
            Destroy(gameObject);
        }
    }
}
