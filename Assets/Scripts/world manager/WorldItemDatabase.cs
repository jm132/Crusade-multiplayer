using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace JM
{
    public class WorldItemDatabase : MonoBehaviour
    {
        public static WorldItemDatabase Instance;

        public WeaponItem unarmedWeapon;

        [Header("Weapons")]
        [SerializeField] List<WeaponItem> weapons = new List<WeaponItem>();

        [Header("Head Equipment")]
        [SerializeField] List<HeadEquipmentItem> headEquipment = new List<HeadEquipmentItem>();

        [Header("Body Equipment")]
        [SerializeField] List<BodyEquipmentItem> bodyEquipment = new List<BodyEquipmentItem>();

        [Header("Leg Equipment")]
        [SerializeField] List<LegEquipmentItem> legEquipment = new List<LegEquipmentItem>();

        [Header("Hand Equipment")]
        [SerializeField] List<HandEquipmentItem> handEquipment = new List<HandEquipmentItem>();

        // a list of every item in the game
        [Header("Items")]
        private List<Item> items = new List<Item>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // add all weapons to the list of items
            foreach (var weapon in weapons)
            {
                items.Add(weapon);
            }

            foreach (var item in handEquipment)
            {
                items.Add(item);
            }

            foreach (var item in bodyEquipment)
            {
                items.Add(item);
            }

            foreach (var item in legEquipment)
            {
                items.Add(item);
            }

            foreach (var item in handEquipment)
            {
                items.Add(item);
            }

            // assign all of our items a unique item id
            for (int i = 0; i < items.Count; i++)
            {
                items[i].itemID = i;
            }
        }

        public WeaponItem GetWeaponByID(int ID)
        {
            return weapons.FirstOrDefault(weapon => weapon.itemID == ID);
        }

        public HeadEquipmentItem GetHeadEquipmentByID(int ID)
        {
            return headEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }

        public BodyEquipmentItem GetBodyEquipmentByID(int ID)
        {
            return bodyEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }

        public LegEquipmentItem GetLegEquipmentByID(int ID)
        {
            return legEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }

        public HandEquipmentItem GetHandEquipmentByID(int ID)
        {
            return handEquipment.FirstOrDefault(equipment => equipment.itemID == ID);
        }
    }
}