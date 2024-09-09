using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;

        [Header("Network Join")]
        [SerializeField] bool startGameAsClinet;

        [HideInInspector] public PlayerUIHudManager playerUIHudManager;
        [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager;
        [HideInInspector] public PlayerUICharacterMenuManager playerUICharacterMenuManager;
        [HideInInspector] public PlayerUIEquipmentManger playerUIEquipmentManger;

        [Header("UI Flags")]
        public bool menuWindowIsOpen = false;  // inventory sceen, equipment menu ect
        public bool popUpWindowIsOpen = false; // item pick up, dialogue pop up ect


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
            playerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
            playerUICharacterMenuManager = GetComponentInChildren<PlayerUICharacterMenuManager>();
            playerUIEquipmentManger = GetComponentInChildren<PlayerUIEquipmentManger>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (startGameAsClinet)
            {
                startGameAsClinet = false;
                // we must first shut down, because we have as a host during the title screen
                NetworkManager.Singleton.Shutdown();
                // we then restart, as a client
                NetworkManager.Singleton.StartClient();
            }
        }

        public void CloseAllMenuWindows()
        {
            playerUICharacterMenuManager.CloseCharacterMenu();
            playerUIEquipmentManger.CloseEquipmentMangerMenu();
        }
    }
}
