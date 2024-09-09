using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace JM
{
    public class PlayerUIEquipmentManger : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] GameObject menu;

        [Header("Weapon Slots")]
        [SerializeField] Image rightHandSlot01;
        [SerializeField] Image rightHandSlot02;
        [SerializeField] Image rightHandSlot03;
        [SerializeField] Image leftHandSlot01;
        [SerializeField] Image leftHandSlot02;
        [SerializeField] Image leftHandSlot03;
        [SerializeField] Image headEquipmentSlot;
        [SerializeField] Image bodyEquipmentSlot;
        [SerializeField] Image legEquipmentSlot;
        [SerializeField] Image handEquipmentSlot;

        // this inventory populates with related items when changing equipment
        [Header("Equipment Inventory")]
        public EquipmentType currentSelectedEquipmentSlot;
        [SerializeField] GameObject equipmentInventoryWindow;
        [SerializeField] GameObject equipmentInventorySlotPrefab;
        [SerializeField] Transform equpmentInventoryContentWindow;
        [SerializeField] Item currentSelectedItem;
        
        public void OpenEquipmentMangerMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = true;
            menu.SetActive(true);
            equipmentInventoryWindow.SetActive(false);

            ClearEquipmentInventory();
            RefreshEquipmentSlotIcons();
        }

        public void RefreshMenu()
        {
            ClearEquipmentInventory();
            RefreshEquipmentSlotIcons();
        }

        public void SelectLastSelectedEquipmentSlot()
        {
            Button lastSelectedButton = null;

            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:
                    lastSelectedButton = rightHandSlot01.GetComponentInParent<Button>();
                    break;
                case EquipmentType.RightWeapon02:
                    lastSelectedButton = rightHandSlot02.GetComponentInParent<Button>();
                    break;
                case EquipmentType.RightWeapon03:
                    lastSelectedButton = rightHandSlot03.GetComponentInParent<Button>();
                    break;
                case EquipmentType.LeftWeapon01:
                    lastSelectedButton = leftHandSlot01.GetComponentInParent<Button>();
                    break;
                case EquipmentType.LeftWeapon02:
                    lastSelectedButton = leftHandSlot02.GetComponentInParent<Button>();
                    break;
                case EquipmentType.LeftWeapon03:
                    lastSelectedButton = leftHandSlot03.GetComponentInParent<Button>();
                    break;
                case EquipmentType.Head:
                    lastSelectedButton = headEquipmentSlot.GetComponentInParent<Button>();
                    break;
                case EquipmentType.Body:
                    lastSelectedButton = bodyEquipmentSlot.GetComponentInParent<Button>();
                    break;
                case EquipmentType.Legs:
                    lastSelectedButton = legEquipmentSlot.GetComponentInParent<Button>();
                    break;
                case EquipmentType.Hands:
                    lastSelectedButton = handEquipmentSlot.GetComponentInParent<Button>();
                    break;
                default:
                    break;
            }
            if (lastSelectedButton != null)
            {
                lastSelectedButton.Select();
                lastSelectedButton.OnSelect(null);
            }
        }

        public void CloseEquipmentMangerMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = false;
            menu.SetActive(false);
        }

        private void RefreshEquipmentSlotIcons()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            // right weapon 01 
            WeaponItem rightHandWeapon01 = player.playerInventoryManager.weaponsInRightHandSlots[0];

            if (rightHandWeapon01.itemIcon != null)
            {
                rightHandSlot01.enabled = true;
                rightHandSlot01.sprite = rightHandWeapon01.itemIcon;
            }
            else
            {
                rightHandSlot01.enabled = false;
            }

            // right weapon 2
            WeaponItem rightHandWeapon02 = player.playerInventoryManager.weaponsInRightHandSlots[1];

            if (rightHandWeapon02.itemIcon != null)
            {
                rightHandSlot02.enabled = true;
                rightHandSlot02.sprite = rightHandWeapon02.itemIcon;
            }
            else
            {
                rightHandSlot02.enabled = false;
            }

            // right wepaon 3
            WeaponItem rightHandWeapon03 = player.playerInventoryManager.weaponsInRightHandSlots[2];

            if (rightHandWeapon03.itemIcon != null)
            {
                rightHandSlot03.enabled = true;
                rightHandSlot03.sprite = rightHandWeapon03.itemIcon;
            }
            else
            {
                rightHandSlot03.enabled = false;
            }

            // left weapon 01 
            WeaponItem leftHandWeapon01 = player.playerInventoryManager.weaponsInLeftHandSlots[0];

            if (leftHandWeapon01.itemIcon != null)
            {
                leftHandSlot01.enabled = true;
                leftHandSlot01.sprite = leftHandWeapon01.itemIcon;
            }
            else
            {
                leftHandSlot01.enabled = false;
            }

            // right weapon 2
            WeaponItem leftHandWeapon02 = player.playerInventoryManager.weaponsInLeftHandSlots[1];

            if (leftHandWeapon02.itemIcon != null)
            {
                leftHandSlot02.enabled = true;
                leftHandSlot02.sprite = leftHandWeapon02.itemIcon;
            }
            else
            {
                leftHandSlot02.enabled = false;
            }

            // right wepaon 3
            WeaponItem leftHandWeapon03 = player.playerInventoryManager.weaponsInLeftHandSlots[2];

            if (leftHandWeapon03.itemIcon != null)
            {
                leftHandSlot03.enabled = true;
                leftHandSlot03.sprite = leftHandWeapon03.itemIcon;
            }
            else
            {
                leftHandSlot03.enabled = false;
            }

            // head equipment
            HeadEquipmentItem headEquipment = player.playerInventoryManager.headEquipment;

            if (headEquipment != null)
            {
                headEquipmentSlot.enabled = true;
                headEquipmentSlot.sprite = headEquipment.itemIcon;
            }
            else
            {
                headEquipmentSlot.enabled = false;
            }

            // body equipment
            BodyEquipmentItem bodyEquipment = player.playerInventoryManager.bodyEquipment;

            if (bodyEquipment != null)
            {
                bodyEquipmentSlot.enabled = true;
                bodyEquipmentSlot.sprite = bodyEquipment.itemIcon;
            }
            else
            {
                bodyEquipmentSlot.enabled = false;
            }

            // leg equipment
            LegEquipmentItem legEquipment = player.playerInventoryManager.legEquipment;

            if (legEquipment != null)
            {
                legEquipmentSlot.enabled = true;
                legEquipmentSlot.sprite = legEquipment.itemIcon;
            }
            else
            {
                legEquipmentSlot.enabled = false;
            }

            // hand equipment
            HandEquipmentItem handEquipment = player.playerInventoryManager.handEquipment;

            if (handEquipment != null)
            {
                handEquipmentSlot.enabled = true;
                handEquipmentSlot.sprite = handEquipment.itemIcon;
            }
            else
            {
                handEquipmentSlot.enabled = false;
            }
        }

        private void ClearEquipmentInventory()
        {
            foreach (Transform item in equpmentInventoryContentWindow)
            {
                Destroy(item.gameObject);
            }
        }

        public void LoadEquipmentInventory()
        {
            equipmentInventoryWindow.SetActive(true);

            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.RightWeapon02:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.RightWeapon03:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.LeftWeapon01:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.LeftWeapon02:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.LeftWeapon03:
                    LoadWeaponInventory();
                    break;
                case EquipmentType.Head:
                    LoadHeadEquipmentInventory();
                    break;
                case EquipmentType.Body:
                    LoadBodyEquipmentInventory();
                    break;
                case EquipmentType.Legs:
                    LoadLegsEquipmentInventory();
                    break;
                case EquipmentType.Hands:
                    LoadHandsEquipmentInventory();
                    break;
                default:
                    break;
            }
        }

        private void LoadWeaponInventory()
        {
            List<WeaponItem> weaponsInInventory = new List<WeaponItem>();

            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            // search entier inventory, and out of all of the items in inventory if the item is a weapon add it to the weapon list
            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                WeaponItem weapon = player.playerInventoryManager.itemsInInventory[i] as WeaponItem;

                if (weapon != null)
                    weaponsInInventory.Add(weapon);
            }

            if (weaponsInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < weaponsInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equpmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(weaponsInInventory[i]);

                // this will select the first button in the list
                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot= true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        private void LoadHeadEquipmentInventory()
        {
            List<HeadEquipmentItem> headEquipmentInInventory = new List<HeadEquipmentItem>();

            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            // search entier inventory, and out of all of the items in inventory if the item is a weapon add it to the weapon list
            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                HeadEquipmentItem equipment = player.playerInventoryManager.itemsInInventory[i] as HeadEquipmentItem;

                if (equipment != null)
                    headEquipmentInInventory.Add(equipment);
            }

            if (headEquipmentInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < headEquipmentInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equpmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(headEquipmentInInventory[i]);

                // this will select the first button in the list
                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        private void LoadBodyEquipmentInventory()
        {
            List<BodyEquipmentItem> bodyEquipmentInInventory = new List<BodyEquipmentItem>();

            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            // search entier inventory, and out of all of the items in inventory if the item is a weapon add it to the weapon list
            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                BodyEquipmentItem equipment = player.playerInventoryManager.itemsInInventory[i] as BodyEquipmentItem;

                if (equipment != null)
                    bodyEquipmentInInventory.Add(equipment);
            }

            if (bodyEquipmentInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < bodyEquipmentInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equpmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(bodyEquipmentInInventory[i]);

                // this will select the first button in the list
                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        private void LoadLegsEquipmentInventory()
        {
            List<LegEquipmentItem> legsEquipmentInInventory = new List<LegEquipmentItem>();

            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            // search entier inventory, and out of all of the items in inventory if the item is a weapon add it to the weapon list
            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                LegEquipmentItem equipment = player.playerInventoryManager.itemsInInventory[i] as LegEquipmentItem;

                if (equipment != null)
                    legsEquipmentInInventory.Add(equipment);
            }

            if (legsEquipmentInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < legsEquipmentInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equpmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(legsEquipmentInInventory[i]);

                // this will select the first button in the list
                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        private void LoadHandsEquipmentInventory()
        {
            List<HandEquipmentItem> handEquipmentInInventory = new List<HandEquipmentItem>();

            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            // search entier inventory, and out of all of the items in inventory if the item is a weapon add it to the weapon list
            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                HandEquipmentItem equipment = player.playerInventoryManager.itemsInInventory[i] as HandEquipmentItem;

                if (equipment != null)
                    handEquipmentInInventory.Add(equipment);
            }

            if (handEquipmentInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < handEquipmentInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equpmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(handEquipmentInInventory[i]);

                // this will select the first button in the list
                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        public void SelectEquipmentSlot(int equipmentSlot)
        {
            currentSelectedEquipmentSlot = (EquipmentType)equipmentSlot;
        }

        public void UnEquipSelectedItem()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            Item unequippedItem;

            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentType.RightWeapon01:

                    unequippedItem = player.playerInventoryManager.weaponsInRightHandSlots[0];

                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsInRightHandSlots[0] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;
                case EquipmentType.RightWeapon02:

                    unequippedItem = player.playerInventoryManager.weaponsInRightHandSlots[1];

                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsInRightHandSlots[1] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.rightHandWeaponIndex == 1)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;
                case EquipmentType.RightWeapon03:

                    unequippedItem = player.playerInventoryManager.weaponsInRightHandSlots[2];

                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsInRightHandSlots[2] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.rightHandWeaponIndex == 2)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;
                case EquipmentType.LeftWeapon01:

                    unequippedItem = player.playerInventoryManager.weaponsInLeftHandSlots[0];

                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsInLeftHandSlots[0] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.leftHandWeaponIndex == 0)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;
                case EquipmentType.LeftWeapon02:

                    unequippedItem = player.playerInventoryManager.weaponsInLeftHandSlots[1];

                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsInLeftHandSlots[1] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.leftHandWeaponIndex == 1)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;
                case EquipmentType.LeftWeapon03:

                    unequippedItem = player.playerInventoryManager.weaponsInLeftHandSlots[2];

                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponsInLeftHandSlots[2] = Instantiate(WorldItemDatabase.Instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }

                    if (player.playerInventoryManager.leftHandWeaponIndex == 2)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = WorldItemDatabase.Instance.unarmedWeapon.itemID;

                    break;

                case EquipmentType.Head:

                    unequippedItem = player.playerInventoryManager.headEquipment;

                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);

                    player.playerInventoryManager.headEquipment = null;
                    player.playerEquipmentManager.LoadHeadEquipment(player.playerInventoryManager.headEquipment);

                    break;
                case EquipmentType.Body:

                    unequippedItem = player.playerInventoryManager.bodyEquipment;

                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);

                    player.playerInventoryManager.bodyEquipment = null;
                    player.playerEquipmentManager.LoadBodyEquipment(player.playerInventoryManager.bodyEquipment);

                    break;
                case EquipmentType.Legs:

                    unequippedItem = player.playerInventoryManager.legEquipment;

                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);

                    player.playerInventoryManager.legEquipment = null;
                    player.playerEquipmentManager.LoadLegEquipment(player.playerInventoryManager.legEquipment);

                    break;
                case EquipmentType.Hands:

                    unequippedItem = player.playerInventoryManager.handEquipment;

                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);

                    player.playerInventoryManager.handEquipment = null;
                    player.playerEquipmentManager.LoadHandEquipment(player.playerInventoryManager.handEquipment);

                    break;
                default:
                    break;
            }

            // refreshes menu (icones ect)
            RefreshMenu();
        }
    }
}