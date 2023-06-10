using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName="InventoryItem")]
public class ItemInventory : ScriptableObject
{
    public Sprite icon;
    public PlayerGun playerGun;
    public GunType gunType;
    public int bulletCount;

    public ItemInventory(ItemInventory itemInventory)
    {
        this.icon = itemInventory.icon;
        this.playerGun = itemInventory.playerGun;
        this.gunType = itemInventory.gunType;
        this.bulletCount = itemInventory.bulletCount;
    }
}

public enum GunType
{
    Rifle,
    Pistol
}
