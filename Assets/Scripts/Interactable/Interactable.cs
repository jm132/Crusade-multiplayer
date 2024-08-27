using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
    public class Interactable : NetworkBehaviour
    {
        public string interactableText; // text prompt when entering the interaction collider (pick up iten, pull level ect)
        [SerializeField] protected Collider interactableCollider; // collider that checks for player interaction
        [SerializeField] protected bool hostOnlyInteractable = true; // when enabled, object cannot be interacted with by coop player

        protected virtual void Awake()
        {
            // check if its null, in case have to manually asign a collider as a child object (depending on interactable)
            if (interactableCollider == null )
                interactableCollider = GetComponent<Collider>();
        }

        protected virtual void Start()
        {
            
        }

        public virtual void Interact(PlayerManager player)
        {
            Debug.Log("you have interacted!");
            if (!player.IsOwner)
                return;

            // remover the interaction from the player
            interactableCollider.enabled = false;
            player.playerInteractionManager.RemoveInteractionFromList(this);
            PlayerUIManager.instance.playerUIPopUpManager.CloseAllPopUpWindows();
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();

            if (player != null)
            {
                if (!player.playerNetworkManager.IsHost && hostOnlyInteractable)
                    return;

                if(!player.IsOwner)
                    return;

                // pass the interaction to the player
                player.playerInteractionManager.AddInteractionToList(this);
            }
        }

        public virtual void OnTriggerExit(Collider other)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();

            if (player != null)
            {
                if (!player.playerNetworkManager.IsHost && hostOnlyInteractable)
                    return;

                if (!player.IsOwner)
                    return;

                // remover the interaction from the player
                player.playerInteractionManager.RemoveInteractionFromList(this);
                PlayerUIManager.instance.playerUIPopUpManager.CloseAllPopUpWindows();
            }
        }
    }
}
