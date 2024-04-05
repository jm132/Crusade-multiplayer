using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class TitleScreenLoadMenuInputManager : MonoBehaviour
    {
        PlayerControles playerControles;

        [Header("Title Screen Inputs")]
        [SerializeField] bool deletCharacterSlot = false;

        private void Update()
        {
            if (deletCharacterSlot)
            {
                deletCharacterSlot = false;
                TitleScreenManager.Instance.AttemptToDeleteCharacterSlot();
            }
        }

        private void OnEnable()
        {
            if(playerControles == null)
            {
                playerControles = new PlayerControles();
                playerControles.UI.X.performed += i => deletCharacterSlot = true;
            }

            playerControles.Enable();
        }

        private void OnDisable()
        {
            playerControles.Disable();
        }
    }
}
