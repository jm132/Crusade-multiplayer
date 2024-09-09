using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
    public class PickUpItemInteractable : Interactable
    {
        public ItemPickUpType pickUpType;

        [Header("Item")]
        [SerializeField] Item item;

        [Header("World Spawn Pick Up")]
        [SerializeField] int itemID;
        [SerializeField] bool hasBeenLooted = false;

        protected override void Start()
        {
            base.Start();

            if (pickUpType == ItemPickUpType.WorldSpawn)
                CheckIfWorldItemWasAlreadyLooted();
        }

        private void CheckIfWorldItemWasAlreadyLooted()
        {
            // 0. if the player isn't the host, hide the item
            if (!NetworkManager.Singleton.IsHost)
            {
                gameObject.SetActive(false);
                return;
            }

            // 1. compare the date of looted items i.d's with this item's id
            if (!WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey(itemID))
            {
                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(itemID, false);
            }

            hasBeenLooted = WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted[itemID];

            // 2. if it has been looted, hide the gameobject
            if (hasBeenLooted)
                gameObject.SetActive(false);
        }

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);

            // 1. play a sfx
            player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.Instance.pickUpItemSFX);

            // 2. add item to inventory
            player.playerInventoryManager.AddItemToInventory(item);

            // 3. display a ui pop up showing item's name and picture
            PlayerUIManager.instance.playerUIPopUpManager.SendItemPopUp(item, 1);

            // 4. save loot staus if it's a world spawn
            if (pickUpType == ItemPickUpType.WorldSpawn)
            {
                if (WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey((int)itemID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Remove(itemID);
                }

                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(itemID, true);
            }

            // destroy gameobject
            Destroy(gameObject);
        }
    }
}