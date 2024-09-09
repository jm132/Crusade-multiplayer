using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JM
{
    public class PlayerUIHudManager : MonoBehaviour
    {
        [SerializeField] CanvasGroup[] canvasGroup;

        [Header("Stat Bars")]
        [SerializeField] UI_StatBar healthBar;
        [SerializeField] UI_StatBar staminaBar;

        [Header("Quick Slots")]
        [SerializeField] Image rightWeaponQuickSlotsIcon;
        [SerializeField] Image leftWeaponQuickSlotsIcon;

        [Header("Boss Health Bar")]
        public Transform bossHealthBarParent;
        public GameObject bossHealthBarObjects;

        public void ToggleHUD(bool status)
        {
            // to do fade in and out time

            if (status)
            {
                foreach (var canvas in canvasGroup)
                {
                    canvas.alpha = 1;
                }
            }
            else
            {
                foreach (var canvas in canvasGroup)
                {
                    canvas.alpha = 0;
                }
            }
        }

        public void RefreshHUD()
        {
            healthBar.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(true);
            staminaBar.gameObject.SetActive(false);
            staminaBar.gameObject.SetActive(true);
        }

        public void SetNewHealthValue(int oldValue, int newValue)
        {
            healthBar.SetStat(newValue);
        }

        public void SetMaxHealthValue(int maxhealth)
        {
            healthBar.SetMaxStats(maxhealth);
        }

        public void SetNewStaminaValue(float oldValue, float newValue)
        {
            staminaBar.SetStat(Mathf.RoundToInt(newValue));
        }

        public void SetMaxStaminaValue(int maxStamina)
        {
            staminaBar.SetMaxStats(maxStamina);
        }

        public void SetRightWeaponQuickSlotsIcon(int weaponID)
        {
            //1. method one, directly reference the right weapon in the hand of the player
            //pros: it's super straight forward
            //cons: if forget to call this function after loaded weapon first, it weill give an error
            //example: load a previously saved game, go to reference the weapons upon loading UI but arent instantiated yet
            //final notes: this method is perfectly fine if remember order of operations

            //2. method two, require an item Id of the weapon, fetch the weapon from database and use it to get the weapon items icon
            //pros: since save the current weapons id, dont need to wait to get it from the player could get it befor han if requird
            //cons:
            //final notes: this method is great if dont remember the order of operations
            
            // if the datebase does not contain a weapon matching the given ID, return

            WeaponItem weapon = WorldItemDatabase.Instance.GetWeaponByID(weaponID);
            if (WorldItemDatabase.Instance.GetWeaponByID(weaponID) == null)
            {
                Debug.Log("item is null");
                rightWeaponQuickSlotsIcon.enabled = false;
                rightWeaponQuickSlotsIcon.sprite = null;
                return;
            }

            if (weapon.itemIcon == null)
            {
                Debug.Log("NO ICON");
                rightWeaponQuickSlotsIcon.enabled = false;
                rightWeaponQuickSlotsIcon.sprite = null;
                return;
            }

            // check to see if meet the items requirements if wnat to creat the waring for not being able to wield it in the ui

            rightWeaponQuickSlotsIcon.sprite = weapon.itemIcon;
            rightWeaponQuickSlotsIcon.enabled = true;
        }

        public void SetLeftWeaponQuickSlotsIcon(int weaponID)
        {
            //1. method one, directly reference the right weapon in the hand of the player
            //pros: it's super straight forward
            //cons: if forget to call this function after loaded weapon first, it weill give an error
            //example: load a previously saved game, go to reference the weapons upon loading UI but arent instantiated yet
            //final notes: this method is perfectly fine if remember order of operations

            //2. method two, require an item Id of the weapon, fetch the weapon from database and use it to get the weapon items icon
            //pros: since save the current weapons id, dont need to wait to get it from the player could get it befor han if requird
            //cons:
            //final notes: this method is great if dont remember the order of operations

            // if the datebase does not contain a weapon matching the given ID, return

            WeaponItem weapon = WorldItemDatabase.Instance.GetWeaponByID(weaponID);
            if (WorldItemDatabase.Instance.GetWeaponByID(weaponID) == null)
            {
                Debug.Log("item is null");
                leftWeaponQuickSlotsIcon.enabled = false;
                leftWeaponQuickSlotsIcon.sprite = null;
                return;
            }

            if (weapon.itemIcon == null)
            {
                Debug.Log("NO ICON");
                leftWeaponQuickSlotsIcon.enabled = false;
                leftWeaponQuickSlotsIcon.sprite = null;
                return;
            }

            // check to see if meet the items requirements if wnat to creat the waring for not being able to wield it in the ui

            leftWeaponQuickSlotsIcon.sprite = weapon.itemIcon;
            leftWeaponQuickSlotsIcon.enabled = true;
        }
    }
}
