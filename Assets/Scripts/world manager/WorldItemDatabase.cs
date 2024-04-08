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

            // assign all of our items a unique item id
            for (int i = 0; i < weapons.Count; i++)
            {
                items[i].itemID = i;
            }
        }

        public WeaponItem GetWeaponByID(int ID)
        {
            return weapons.FirstOrDefault(weapon => weapon.itemID == ID);
        }
    }
}