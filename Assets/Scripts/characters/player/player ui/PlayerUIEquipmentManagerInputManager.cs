using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerUIEquipmentManagerInputManager : MonoBehaviour
    {
        PlayerControles playerControls;

        PlayerUIEquipmentManger playerUIEquipmentManger;

        [Header("Input")]
        [SerializeField] bool unequipItemInput;

        private void Awake()
        {
            playerUIEquipmentManger = GetComponentInParent<PlayerUIEquipmentManger>();
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControles();

                playerControls.PlayerAction.UseItem.performed += i => unequipItemInput = true;
            }

            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            HandlePlayerUIEquipmentManagerInput();
        }

        private void HandlePlayerUIEquipmentManagerInput()
        {
            if (unequipItemInput)
            {
                unequipItemInput = false;
                playerUIEquipmentManger.UnEquipSelectedItem();
            }
        }
    }
}
