using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerInventoryManager : CharaterInventoryManager
    {
        public WeaponItem currentRightHandWeapon;
        public WeaponItem currentLeftHandWeapon;
        public WeaponItem currentTwoHandWeapon;

        [Header("Quick Slots")]
        public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[3];
        public int rightHandWeaponIndex = 0;
        public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[3];
        public int leftHandWeaponIndex = 0;

        [Header("Armor")]
        public HeadEquipmentItem headEquipment;
        public BodyEquipmentItem bodyEquipment;
        public LegEquipmentItem legEquipment;
        public HandEquipmentItem handEquipment;

        [Header("Inventory")]
        public List<Item> itemsInInventory;

        public void AddItemToInventory(Item item)
        {
            itemsInInventory.Add(item);
        }

        public void RemoveItemFromInventory(Item item)
        {
            // to do: create an rpc here that spawns item on network when dropped

            itemsInInventory.Remove(item);

            // checks for null list slot and remove them
            for (int i = itemsInInventory.Count - 1; i > -1; i--)
            {
                if (itemsInInventory[i] == item)
                {
                    itemsInInventory.RemoveAt(i);
                }
            }
        }
    }
}
