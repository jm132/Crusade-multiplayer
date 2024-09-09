using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

namespace JM
{
    public class PlayerManager : CharaterManager
    {

        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public PlayerCombatManager playerCombatManager;
        [HideInInspector] public PlayerInteractionManager playerInteractionManager;
        [HideInInspector] public PlayerEffectManager playerEffectManager;
        [HideInInspector] public PlayerBodyManager playerBodyManager;

        protected override void Awake()
        {
            base.Awake();

            // DO MORE STUFF, ONLY FOR THE PLAYER

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerNetworkManager = GetComponent<PlayerNetworkManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerInteractionManager = GetComponent<PlayerInteractionManager>();
            playerEffectManager = GetComponent<PlayerEffectManager>();
            playerBodyManager = GetComponent<PlayerBodyManager>();
        }

        protected override void Update()
        {
            base.Update();

            //IF WE DO NOT OWN THIS GAMEOBJECT, WE DONT NOT CONTROL OR EDIT IT
            if (!IsOwner)
                return;

            // Handle Movement
            playerLocomotionManager.HandleAllMovement();

            // Regen Stamina
            playerStatsManager.RegenerateStamina();
        }

        protected override void LateUpdate()
        {
            if (!IsOwner) 
                return;

            base.LateUpdate();

            PlayerCamera.instance.HandleAllCameraActions();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;

            // if this is the player object owned by this vlient
            if (IsOwner)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;
                WorldSaveGameManager.instance.player = this;

                // update the total amount of health or stamina when the stat is linked to eather changes
                playerNetworkManager.vitality.OnValueChanged += playerNetworkManager.SetNewMaxHealthValue;
                playerNetworkManager.endurance.OnValueChanged += playerNetworkManager.SetNewMaxStaminaValue;

                // update ui stat bars when the stat changes (health or stamina)
                playerNetworkManager.currentHealth.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue;
                playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue;
                playerNetworkManager.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenTimer;
            }

            // only update floating hp bar if this character is not the local player character (dont want to see a hp bar floating bove the player own head
            if (!IsOwner)
                characterNetworkManager.currentHealth.OnValueChanged += characterUIManager.OnHPChanged;

            // body type
            playerNetworkManager.isMale.OnValueChanged += playerNetworkManager.OnIsMaleChanged;

            // stats
            playerNetworkManager.currentHealth.OnValueChanged += playerNetworkManager.CheckHP;

            // lock on
            playerNetworkManager.isLockedOn.OnValueChanged += playerNetworkManager.OnIsLockedOnChanged;
            playerNetworkManager.currentTargetNetworkObjectID.OnValueChanged += playerNetworkManager.OnLockOnTargetIDChange;

            // equipment
            playerNetworkManager.currentRightHandWeaponID.OnValueChanged += playerNetworkManager.OnCurrentRightHandWeaponIDChange;
            playerNetworkManager.currentLeftHandWeaponID.OnValueChanged += playerNetworkManager.OnCurrentLeftHandWeaponIDChange;
            playerNetworkManager.currentWeaponBeingUsed.OnValueChanged += playerNetworkManager.OnCurrentWeaponBeingUsedIDChange;
            playerNetworkManager.isBlocking.OnValueChanged += playerNetworkManager.OnIsBlockingChanged;
            playerNetworkManager.headEquipmentID.OnValueChanged += playerNetworkManager.OnHeadEquipmentChanged;
            playerNetworkManager.bodyEquipmentID.OnValueChanged += playerNetworkManager.OnBodyEquipmentChanged;
            playerNetworkManager.legEquipmentID.OnValueChanged += playerNetworkManager.OnLegEquipmentChanged;
            playerNetworkManager.handEquipmentID.OnValueChanged += playerNetworkManager.OnHandEquipmentChanged;

            // two hand
            playerNetworkManager.isTwoHandingWeapon.OnValueChanged += playerNetworkManager.OnIsTwoHandingWeaponChanged;
            playerNetworkManager.isTwoHandingRightWeapon.OnValueChanged += playerNetworkManager.OnIsTwoHandingRightWeaponChanged;
            playerNetworkManager.isTwoHandingLeftWeapon.OnValueChanged += playerNetworkManager.OnIsTwoHandingLeftWeaponChanged;

            //flags
            playerNetworkManager.isChargingAttack.OnValueChanged += playerNetworkManager.OnIsChargingAttackChaged;

            // upon connecting, if the owner of this character, but not the sever, reload the character data to this newly instantiated character
            // dont runt this if server, because since they are host, they are already leaded in and dont need to reload their data
            if (IsOwner && !IsServer)
            {
                LoadGameDataFromCurrentCharacterData(ref WorldSaveGameManager.instance.currentCharacterData);
            }
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;

            // if this is the player object owned by this vlient
            if (IsOwner)
            {
                // update the total amount of health or stamina when the stat is linked to eather changes
                playerNetworkManager.vitality.OnValueChanged -= playerNetworkManager.SetNewMaxHealthValue;
                playerNetworkManager.endurance.OnValueChanged -= playerNetworkManager.SetNewMaxStaminaValue;

                // update ui stat bars when the stat changes (health or stamina)
                playerNetworkManager.currentHealth.OnValueChanged -= PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue;
                playerNetworkManager.currentStamina.OnValueChanged -= PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue;
                playerNetworkManager.currentStamina.OnValueChanged -= playerStatsManager.ResetStaminaRegenTimer;
            }

            if (!IsOwner)
                characterNetworkManager.currentHealth.OnValueChanged -= characterUIManager.OnHPChanged;

            // body type
            playerNetworkManager.isMale.OnValueChanged -= playerNetworkManager.OnIsMaleChanged;

            // stats
            playerNetworkManager.currentHealth.OnValueChanged -= playerNetworkManager.CheckHP;

            // lock on
            playerNetworkManager.isLockedOn.OnValueChanged -= playerNetworkManager.OnIsLockedOnChanged;
            playerNetworkManager.currentTargetNetworkObjectID.OnValueChanged -= playerNetworkManager.OnLockOnTargetIDChange;

            // equipment
            playerNetworkManager.currentRightHandWeaponID.OnValueChanged -= playerNetworkManager.OnCurrentRightHandWeaponIDChange;
            playerNetworkManager.currentLeftHandWeaponID.OnValueChanged -= playerNetworkManager.OnCurrentLeftHandWeaponIDChange;
            playerNetworkManager.currentWeaponBeingUsed.OnValueChanged -= playerNetworkManager.OnCurrentWeaponBeingUsedIDChange;
            playerNetworkManager.isBlocking.OnValueChanged -= playerNetworkManager.OnIsBlockingChanged;
            playerNetworkManager.headEquipmentID.OnValueChanged -= playerNetworkManager.OnHeadEquipmentChanged;
            playerNetworkManager.bodyEquipmentID.OnValueChanged -= playerNetworkManager.OnBodyEquipmentChanged;
            playerNetworkManager.legEquipmentID.OnValueChanged -= playerNetworkManager.OnLegEquipmentChanged;
            playerNetworkManager.handEquipmentID.OnValueChanged -= playerNetworkManager.OnHandEquipmentChanged;

            // two hand
            playerNetworkManager.isTwoHandingWeapon.OnValueChanged -= playerNetworkManager.OnIsTwoHandingWeaponChanged;
            playerNetworkManager.isTwoHandingRightWeapon.OnValueChanged -= playerNetworkManager.OnIsTwoHandingRightWeaponChanged;
            playerNetworkManager.isTwoHandingLeftWeapon.OnValueChanged -= playerNetworkManager.OnIsTwoHandingLeftWeaponChanged;

            //flags
            playerNetworkManager.isChargingAttack.OnValueChanged -= playerNetworkManager.OnIsChargingAttackChaged;
        }

        private void OnClientConnectedCallback(ulong clientID)
        {
            WorldGameSessionManager.instance.AddPlayerToActivePlayersList(this);
            
            // if the sever, and the host, dont need to load players to sync them
            // only nned to load other players gear to syne it if they join a game thats already been active with out them being present
            if (!IsServer && IsOwner)
            {
                foreach (var player in WorldGameSessionManager.instance.players)
                {
                    if (player != this)
                    {
                        player.LoadOtherPlayerCharacterWhenJoiningServer();
                    }
                }
            }
        }

        public override IEnumerator ProessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            if (IsOwner)
            {
                PlayerUIManager.instance.playerUIPopUpManager.SendYouDiedPopUp();
            }

            return base.ProessDeathEvent(manuallySelectDeathAnimation);
        }

        public override void Revivecharacter()
        {
            base.Revivecharacter();
            if (IsOwner)
            {
                isDead.Value = false;
                playerNetworkManager.currentHealth.Value = playerNetworkManager.maxHealth.Value;
                playerNetworkManager.currentStamina.Value = playerNetworkManager.maxStamina.Value;
                // retore focus points

                // play rebirth effects
                playerAnimatorManager.PlayTargetActionAnimation("Empty", false);
            }
        }

        public void SaveGameDataToCurrentCharacterData(ref CharaterSaveData currentCharacterData)
        {
            currentCharacterData.sceneIndex = SceneManager.GetActiveScene().buildIndex;

            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            currentCharacterData.isMale = playerNetworkManager.isMale.Value;
            currentCharacterData.yPosition = transform.position.x;
            currentCharacterData.xPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;

            currentCharacterData.currentHealth = playerNetworkManager.currentHealth.Value;
            currentCharacterData.currentStamina = playerNetworkManager.currentStamina.Value;

            currentCharacterData.vitality = playerNetworkManager.vitality.Value;
            currentCharacterData.endurance = playerNetworkManager.endurance.Value;

            // equipment
            currentCharacterData.headEquipment = playerNetworkManager.headEquipmentID.Value;
            currentCharacterData.bodyEquipment = playerNetworkManager.bodyEquipmentID.Value;
            currentCharacterData.legEquipment = playerNetworkManager.legEquipmentID.Value;
            currentCharacterData.handEquipment = playerNetworkManager.handEquipmentID.Value;

            currentCharacterData.rightWeaponIndex = playerInventoryManager.rightHandWeaponIndex;
            currentCharacterData.rightWeapon01 = playerInventoryManager.weaponsInRightHandSlots[0].itemID; // this should never be null
            currentCharacterData.rightWeapon02 = playerInventoryManager.weaponsInRightHandSlots[1].itemID; // this should never be null
            currentCharacterData.rightWeapon03 = playerInventoryManager.weaponsInRightHandSlots[2].itemID; // this should never be null

            currentCharacterData.leftWeaponIndex = playerInventoryManager.leftHandWeaponIndex;
            currentCharacterData.leftWeapon01 = playerInventoryManager.weaponsInLeftHandSlots[0].itemID; // this should never be null
            currentCharacterData.leftWeapon02 = playerInventoryManager.weaponsInLeftHandSlots[1].itemID; // this should never be null
            currentCharacterData.leftWeapon03 = playerInventoryManager.weaponsInLeftHandSlots[2].itemID; // this should never be null
        }

        public void LoadGameDataFromCurrentCharacterData(ref CharaterSaveData currentCharacterData)
        {
            // sync body type
            playerNetworkManager.OnIsMaleChanged(false, playerNetworkManager.isMale.Value);

            // sync weapon 
            playerNetworkManager.characterName.Value = currentCharacterData.characterName;
            playerNetworkManager.isMale.Value = currentCharacterData.isMale;
            playerBodyManager.ToggleBodyType(currentCharacterData.isMale); // toggle incase the value is the same as default
            Vector3 myPosition = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
            transform.position = myPosition;

            playerNetworkManager.vitality.Value = currentCharacterData.vitality;
            playerNetworkManager.endurance.Value = currentCharacterData.endurance;

            // this will be moved when saving and loading is added
            playerNetworkManager.maxHealth.Value = playerStatsManager.CalculateHealthBasedOnVitalityLevel(playerNetworkManager.vitality.Value);
            playerNetworkManager.maxStamina.Value = playerStatsManager.CalculateStaminaBasedOnEnduranceLevel(playerNetworkManager.endurance.Value);
            playerNetworkManager.currentHealth.Value = currentCharacterData.currentHealth;
            playerNetworkManager.currentStamina.Value = currentCharacterData.currentStamina;
            PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);

            // equipment
            if (WorldItemDatabase.Instance.GetHeadEquipmentByID(currentCharacterData.headEquipment))
            {
                HeadEquipmentItem headEquipment = Instantiate(WorldItemDatabase.Instance.GetHeadEquipmentByID(currentCharacterData.headEquipment));
                playerInventoryManager.headEquipment = headEquipment;
            }
            else
            {
                playerInventoryManager.headEquipment = null;
            }

            if (WorldItemDatabase.Instance.GetBodyEquipmentByID(currentCharacterData.bodyEquipment))
            {
                BodyEquipmentItem bodyEquipment = Instantiate(WorldItemDatabase.Instance.GetBodyEquipmentByID(currentCharacterData.bodyEquipment));
                playerInventoryManager.bodyEquipment = bodyEquipment;
            }
            else
            {
                playerInventoryManager.bodyEquipment = null;
            }

            if (WorldItemDatabase.Instance.GetLegEquipmentByID(currentCharacterData.legEquipment))
            {
                LegEquipmentItem LegEquipment = Instantiate(WorldItemDatabase.Instance.GetLegEquipmentByID(currentCharacterData.legEquipment));
                playerInventoryManager.legEquipment = LegEquipment;
            }
            else
            {
                playerInventoryManager.legEquipment = null;
            }

            if (WorldItemDatabase.Instance.GetHandEquipmentByID(currentCharacterData.handEquipment))
            {
                HandEquipmentItem handEquipment = Instantiate(WorldItemDatabase.Instance.GetHandEquipmentByID(currentCharacterData.handEquipment));
                playerInventoryManager.handEquipment = handEquipment;
            }
            else
            {
                playerInventoryManager.handEquipment = null;
            }

            if (WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.rightWeapon01))
            {
                WeaponItem rightWeapon01 = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.rightWeapon01));
                playerInventoryManager.weaponsInRightHandSlots[0] = rightWeapon01;
            }
            else
            {
                playerInventoryManager.weaponsInLeftHandSlots[0] = null;
            }

            if (WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.rightWeapon02))
            {
                WeaponItem rightWeapon02 = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.rightWeapon02));
                playerInventoryManager.weaponsInRightHandSlots[1] = rightWeapon02;
            }
            else
            {
                playerInventoryManager.weaponsInRightHandSlots[1] = null;
            }

            if (WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.rightWeapon03))
            {
                WeaponItem rightWeapon03 = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.rightWeapon03));
                playerInventoryManager.weaponsInRightHandSlots[2] = rightWeapon03;
            }
            else
            {
                playerInventoryManager.weaponsInRightHandSlots[2] = null;
            }

            if (WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.leftWeapon01))
            {
                WeaponItem leftWeapon01 = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.leftWeapon01));
                playerInventoryManager.weaponsInLeftHandSlots[0] = leftWeapon01;
            }
            else
            {
                playerInventoryManager.weaponsInLeftHandSlots[0] = null;
            }

            if (WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.leftWeapon02))
            {
                WeaponItem leftWeapon02 = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.leftWeapon02));
                playerInventoryManager.weaponsInLeftHandSlots[1] = leftWeapon02;
            }
            else
            {
                playerInventoryManager.weaponsInLeftHandSlots[1] = null;
            }

            if (WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.leftWeapon03))
            {
                WeaponItem leftWeapon03 = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(currentCharacterData.leftWeapon03));
                playerInventoryManager.weaponsInLeftHandSlots[2] = leftWeapon03;
            }
            else
            {
                playerInventoryManager.weaponsInLeftHandSlots[2] = null;
            }

            playerEquipmentManager.EquipArmor();

            playerInventoryManager.rightHandWeaponIndex = currentCharacterData.rightWeaponIndex;
            playerNetworkManager.currentRightHandWeaponID.Value = playerInventoryManager.weaponsInRightHandSlots[currentCharacterData.rightWeaponIndex].itemID;
            playerInventoryManager.leftHandWeaponIndex = currentCharacterData.leftWeaponIndex;
            playerNetworkManager.currentLeftHandWeaponID.Value = playerInventoryManager.weaponsInLeftHandSlots[currentCharacterData.leftWeaponIndex].itemID;
        }

        public void LoadOtherPlayerCharacterWhenJoiningServer()
        {
            // sync weapons
            playerNetworkManager.OnCurrentRightHandWeaponIDChange(0, playerNetworkManager.currentRightHandWeaponID.Value);
            playerNetworkManager.OnCurrentLeftHandWeaponIDChange(0, playerNetworkManager.currentLeftHandWeaponID.Value);

            // sync armor
            playerNetworkManager.OnHeadEquipmentChanged(0, playerNetworkManager.headEquipmentID.Value);
            playerNetworkManager.OnBodyEquipmentChanged(0, playerNetworkManager.bodyEquipmentID.Value);
            playerNetworkManager.OnLegEquipmentChanged(0, playerNetworkManager.legEquipmentID.Value);
            playerNetworkManager.OnHandEquipmentChanged(0, playerNetworkManager.handEquipmentID.Value);

            // sync two hand status
            playerNetworkManager.OnIsTwoHandingRightWeaponChanged(false, playerNetworkManager.isTwoHandingRightWeapon.Value);
            playerNetworkManager.OnIsTwoHandingLeftWeaponChanged(false, playerNetworkManager.isTwoHandingLeftWeapon.Value);

            // sync block status
            playerNetworkManager.OnIsBlockingChanged(false, playerNetworkManager.isBlocking.Value);

            // armor

            // lock on
            if (playerNetworkManager.isLockedOn.Value)
            {
                playerNetworkManager.OnLockOnTargetIDChange(0, playerNetworkManager.currentTargetNetworkObjectID.Value);
            }
        }
    }
}
