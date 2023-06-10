using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private PlayerWeapon _playerWeapon;

    [SerializeField] private Image _rifleSlot;
    [SerializeField] private Image _pistolSlot;
    [SerializeField] private Image _additionalSlot;
    [SerializeField] private Animator _rigAnimator;

    [Space(5f)]
    [SerializeField] private ItemInventory _activeItem;

    private Image _activeSlot;
    private ItemInventory _rifle;
    private ItemInventory _pistol;
    private ItemInventory _additional;


    private void Start()
    {
        _activeSlot = _rifleSlot;
        PlayerGun existGun = _playerWeapon.GetPivotWeapon();
        if (existGun)
        {
            AddItem(existGun.itemInventory);
            switch (existGun.itemInventory.gunType)
            {
                case GunType.Rifle:
                    ChangeActiveItem(SlotType.Rifle);
                    break;
                case GunType.Pistol:
                    ChangeActiveItem(SlotType.Pistol);
                    break;
            }
            _activeItem = existGun.itemInventory;
            _playerWeapon.GetWeapon(existGun);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeActiveItem(SlotType.Rifle);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeActiveItem(SlotType.Pistol);
        }
        else
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeActiveItem(SlotType.Additional);
        }
    }

    public void AddItem(ItemInventory newItem)
    {
        switch (newItem.gunType)
        {
            case GunType.Rifle:
                if (!_rifle)
                {
                    _rifle = newItem;
                    _rifleSlot.sprite = newItem.icon;
                }
                else
                {
                    _additional = newItem;
                    _additionalSlot.sprite = newItem.icon;
                }
                break;
            case GunType.Pistol:
                if (!_pistol)
                {
                    _pistol = newItem;
                    _pistolSlot.sprite = newItem.icon;
                }
                else
                {
                    _additional = newItem;
                    _additionalSlot.sprite = newItem.icon;
                }
                break;
        }
    }

    public void ChangeActiveItem(SlotType slotType)
    {
        if(_activeItem && _playerWeapon.playerGun)
            _activeItem.bulletCount = _playerWeapon.playerGun.bim;


        switch (slotType)
        {
            case SlotType.Rifle:
                {
                    if (_rifle && _activeItem != _rifle)
                    {
                        ItemContour activeContour = _activeSlot.GetComponent<ItemContour>();
                        if (activeContour)
                        {
                            activeContour.DisableContour();
                        }
                        ChangeSlot(_rifle);
                        ItemContour contour = _rifleSlot.GetComponent<ItemContour>();
                        if (contour)
                        {
                            contour.ActiveContour();
                            _activeSlot = _rifleSlot;
                        }
                    }
                    break;
                }
            case SlotType.Pistol:
                {
                    if (_pistol && _activeItem != _pistol)
                    {
                        ItemContour activeContour = _activeSlot.GetComponent<ItemContour>();
                        if (activeContour)
                        {
                            activeContour.DisableContour();
                        }
                        ChangeSlot(_pistol);
                        ItemContour contour = _pistolSlot.GetComponent<ItemContour>();
                        if (contour)
                        {
                            contour.ActiveContour();
                            _activeSlot = _pistolSlot;
                        }
                    }
                    break;
                }
            case SlotType.Additional:
                {
                    if (_additional && _activeItem != _additional)
                    {
                        ItemContour activeContour = _activeSlot.GetComponent<ItemContour>();
                        if (activeContour)
                        {
                            activeContour.DisableContour();
                        }
                        ChangeSlot(_additional);
                        ItemContour contour = _additionalSlot.GetComponent<ItemContour>();
                        if (contour)
                        {
                            contour.ActiveContour();
                            _activeSlot = _additionalSlot;
                        }
                    }
                    break;
                }
        }
    }

    private void ChangeSlot(ItemInventory gunSlot)
    {
        _activeItem = gunSlot;
        PlayerGun playerGun = Instantiate(_activeItem.playerGun);
        playerGun.bim = _activeItem.bulletCount;
        playerGun.itemInventory = _activeItem;
        _playerWeapon.GetWeapon(playerGun);

        _rigAnimator.SetTrigger("ChangeWeapon");
    }

    public enum SlotType
    {
        Rifle,
        Pistol,
        Additional
    }
}
