using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace JM
{
    public class UI_EquipmentInventorySlot : MonoBehaviour
    {
        public Image itemIcon;
        public Image highlightedIcon;
        [SerializeField] public Item currentItem;

        public void AddItem(Item item)
        {
            if (item == null)
            {
                itemIcon.enabled = false;
                return;
            }

            itemIcon.enabled = true;

            currentItem = item;
            itemIcon.sprite = item.itemIcon;
        }

        public void SelectSlot()
        {
            highlightedIcon.enabled = true;
        }

        public void DeselectSlot()
        {
            highlightedIcon.enabled = false;
        }

        public void EquipItem()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            Item equippedItem;

            switch (PlayerUIManager.instance.playerUIEquipmentManger.currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:

                    // if current weapon in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.weaponsInRightHandSlots[0];

                    if (equippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then replace the weapon in that slot with new weapon
                    player.playerInventoryManager.weaponsInRightHandSlots[0] = currentItem as WeaponItem;

                    // then remove the new weapon from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new weapon if holding the current weapon in this slot
                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                case EquipmentType.RightWeapon02:

                    // if current weapon in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.weaponsInRightHandSlots[1];

                    if (equippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then replace the weapon in that slot with new weapon
                    player.playerInventoryManager.weaponsInRightHandSlots[1] = currentItem as WeaponItem;

                    // then remove the new weapon from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new weapon if holding the current weapon in this slot
                    if (player.playerInventoryManager.rightHandWeaponIndex == 1)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                case EquipmentType.RightWeapon03:

                    // if current weapon in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.weaponsInRightHandSlots[2];

                    if (equippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then replace the weapon in that slot with new weapon
                    player.playerInventoryManager.weaponsInRightHandSlots[2] = currentItem as WeaponItem;

                    // then remove the new weapon from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new weapon if holding the current weapon in this slot
                    if (player.playerInventoryManager.rightHandWeaponIndex == 2)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                case EquipmentType.LeftWeapon01:

                    // if current weapon in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.weaponsInLeftHandSlots[0];

                    if (equippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then replace the weapon in that slot with new weapon
                    player.playerInventoryManager.weaponsInLeftHandSlots[0] = currentItem as WeaponItem;

                    // then remove the new weapon from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new weapon if holding the current weapon in this slot
                    if (player.playerInventoryManager.leftHandWeaponIndex == 0)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                case EquipmentType.LeftWeapon02:

                    // if current weapon in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.weaponsInLeftHandSlots[1];

                    if (equippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then replace the weapon in that slot with new weapon
                    player.playerInventoryManager.weaponsInLeftHandSlots[1] = currentItem as WeaponItem;

                    // then remove the new weapon from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new weapon if holding the current weapon in this slot
                    if (player.playerInventoryManager.leftHandWeaponIndex == 1)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                case EquipmentType.LeftWeapon03:

                    // if current weapon in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.weaponsInLeftHandSlots[2];

                    if (equippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then replace the weapon in that slot with new weapon
                    player.playerInventoryManager.weaponsInLeftHandSlots[2] = currentItem as WeaponItem;

                    // then remove the new weapon from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new weapon if holding the current weapon in this slot
                    if (player.playerInventoryManager.leftHandWeaponIndex == 2)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                case EquipmentType.Head:

                    // if current equipment in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.headEquipment;

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then assign the slot new item
                    player.playerInventoryManager.headEquipment = currentItem as HeadEquipmentItem;

                    // then remove the new item from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new item
                    player.playerEquipmentManager.LoadHeadEquipment(player.playerInventoryManager.headEquipment);

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                case EquipmentType.Body:

                    // if current equipment in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.bodyEquipment;

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then assign the slot new item
                    player.playerInventoryManager.bodyEquipment = currentItem as BodyEquipmentItem;

                    // then remove the new item from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new item
                    player.playerEquipmentManager.LoadBodyEquipment(player.playerInventoryManager.bodyEquipment);

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                case EquipmentType.Legs:

                    // if current equipment in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.legEquipment;

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then assign the slot new item
                    player.playerInventoryManager.legEquipment = currentItem as LegEquipmentItem;

                    // then remove the new item from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new item
                    player.playerEquipmentManager.LoadLegEquipment(player.playerInventoryManager.legEquipment);

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                case EquipmentType.Hands:

                    // if current equipment in this slot, is not and unarmed item, add it to inventory
                    equippedItem = player.playerInventoryManager.handEquipment;

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    // then assign the slot new item
                    player.playerInventoryManager.handEquipment = currentItem as HandEquipmentItem;

                    // then remove the new item from inentory
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    // re-equip new item
                    player.playerEquipmentManager.LoadHandEquipment(player.playerInventoryManager.handEquipment);

                    // refreshs equipment window
                    PlayerUIManager.instance.playerUIEquipmentManger.RefreshMenu();

                    break;
                default:
                    break;
            }

            PlayerUIManager.instance.playerUIEquipmentManger.SelectLastSelectedEquipmentSlot();
        }
    }
}
