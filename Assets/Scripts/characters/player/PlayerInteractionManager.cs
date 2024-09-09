using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        PlayerManager player;

        private List<Interactable> currentInteractableAction;

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            currentInteractableAction = new List<Interactable>();
        }

        private void FixedUpdate()
        {
            if (!player.IsOwner)
                return;

            // if ui menu is not open, and dont have a pop up (current interaction message) check for interactable
            if (!PlayerUIManager.instance.menuWindowIsOpen && !PlayerUIManager.instance.popUpWindowIsOpen)
                CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            if (currentInteractableAction.Count == 0)
                return;

            if (currentInteractableAction[0] == null)
            {
                currentInteractableAction.RemoveAt(0); // if the currnt interactable item at postion 0 becomes null (remove from game), remove position 0 from the list
                return;
            }

            // if have an interactable action and have not notified player, do so here
            if (currentInteractableAction[0] != null)
                PlayerUIManager.instance.playerUIPopUpManager.SendPlayerMessagePopUp(currentInteractableAction[0].interactableText);
        }

        private void RefreshInteractionList()
        {
            for (int i = currentInteractableAction.Count - 1; i > -1; i--)
            {
                if (currentInteractableAction[i] == null)
                    currentInteractableAction.RemoveAt(i);
            }
        }

        public void AddInteractionToList(Interactable interactableObject)
        {
            RefreshInteractionList();

            if(!currentInteractableAction.Contains(interactableObject))
                currentInteractableAction.Add(interactableObject);
        }

        public void RemoveInteractionFromList(Interactable interactableObject)
        {
            if (currentInteractableAction.Contains(interactableObject))
                currentInteractableAction.Remove(interactableObject);

            RefreshInteractionList();
        }

        public void Interact()
        {

            // if press the interact button with or without an interactable, it will clear the pop up windows (item pick ups, message, ect)
            PlayerUIManager.instance.playerUIPopUpManager.CloseAllPopUpWindows();

            if(currentInteractableAction.Count == 0)
                return;

            if (currentInteractableAction[0] != null)
            {
                currentInteractableAction[0].Interact(player);
                RefreshInteractionList();
            }
        }
    }
}
