using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JM
{
    public class PlayerInputManager : MonoBehaviour
    {
        // Input controls
        public static PlayerInputManager instance;

        // Singleton
        public PlayerManager player;

        // Local Player
        PlayerControles playerControles;

        [Header("camera movement input")]
        [SerializeField] Vector2 camera_Input;
        public float cameraVerical_Input;
        public float cameraHorizontal_Input;

        [Header("LOCK ON INPUT")]
        [SerializeField] bool lockOn_Input;
        [SerializeField] bool lockOn_Left_Input;
        [SerializeField] bool lockOn_Right_Input;
        private Coroutine lockOnCoroutine;

        [Header("player movement input")]
        [SerializeField] Vector2 movement_Input;
        public float horizontal_Input;
        public float vertical_Input;
        public float moveAmount;

        [Header("Player Action Input")]
        [SerializeField] bool dodge_Input = false;
        [SerializeField] bool sprint_Input = false;
        [SerializeField] bool jump_Input = false;
        [SerializeField] bool switch_Right_Weapon_Input = false;
        [SerializeField] bool switch_Left_Weapon_Input = false;
        [SerializeField] bool interaction_Input = false;

        [Header("Bumper Input")]
        [SerializeField] bool RB_Input = false;
        [SerializeField] bool LB_Input = false;

        [Header("Trigger Input")]
        [SerializeField] bool RT_Input = false;
        [SerializeField] bool Hold_RT_Input = false;

        [Header("Two Hand Input")]
        [SerializeField] bool two_Hand_Input = false;
        [SerializeField] bool two_Hand_Right_Weapon_Input = false;
        [SerializeField] bool two_Hand_Left_Weapon_Input = false;

        [Header("Qued Inputs")]
        [SerializeField] private bool input_Que_Is_Active = false;
        [SerializeField] float default_Que_Input_Time = 0.35f;
        [SerializeField] float que_Input_Timer = 0;
        [SerializeField] bool que_RB_Input = false;
        [SerializeField] bool que_RT_Input = false;

        [Header("UI Input")]
        [SerializeField] bool openCharacterMenuInput = false;
        [SerializeField] bool closeMenuInput = false;


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
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            // WHEN THE SCENE CHANGES, RUN THIS LOGIC
            SceneManager.activeSceneChanged += OnSceneChange;

            instance.enabled = false;

            if(playerControles != null)
            {
                playerControles.Disable();
            }
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            //  IF WE ARE LOADING INTO THE WORLD SCENE, ENABLE THE PLAYER CONTROLS
            if(newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;

                if (playerControles != null)
                {
                    playerControles.Enable();
                }
            }
            // OTHERWISE WE MUST BE AT THE MAIN MENU, DISABLE OUR PLAYER CONTROLS
            // THIS IS SO OUR PLAYER CANT MOVE AROUND IF WE ENTER THINGS LIKE A CHARACTER CREATION MENU ECT
            else
            {
                instance.enabled = false;

                if (playerControles != null)
                {
                    playerControles.Disable();
                }
            }
        }

        private void OnEnable()
        {
            if (playerControles == null)
            {
                playerControles = new PlayerControles();

                playerControles.PlayerMovement.Movement.performed += i => movement_Input = i.ReadValue<Vector2>();
                playerControles.PlayerCamera.Movement.performed += i => camera_Input = i.ReadValue<Vector2>();

                // Action
                playerControles.PlayerAction.Dodge.performed += i => dodge_Input = true;
                playerControles.PlayerAction.Jump.performed += i => jump_Input = true;
                playerControles.PlayerAction.SwitchRightWeapon.performed += i => switch_Right_Weapon_Input = true;
                playerControles.PlayerAction.SwitchLeftWeapon.performed += i => switch_Left_Weapon_Input = true;
                playerControles.PlayerAction.Interact.performed += i => interaction_Input = true;

                //Bumpers
                playerControles.PlayerAction.RB.performed += i => RB_Input = true;
                playerControles.PlayerAction.LB.performed += i => LB_Input = true;
                playerControles.PlayerAction.LB.canceled += i => player.playerNetworkManager.isBlocking.Value = false;

                // Triggers
                playerControles.PlayerAction.RT.performed += i => RT_Input = true;
                playerControles.PlayerAction.HoldRT.performed += i => Hold_RT_Input = true;
                playerControles.PlayerAction.HoldRT.canceled += i => Hold_RT_Input = false;

                // Two Hand
                playerControles.PlayerAction.TwoHandWeapon.performed += i => two_Hand_Input = true;
                playerControles.PlayerAction.TwoHandWeapon.canceled += i => two_Hand_Input = false;
                playerControles.PlayerAction.TwoHandRightWeapon.performed += i => two_Hand_Right_Weapon_Input = true;
                playerControles.PlayerAction.TwoHandRightWeapon.canceled += i => two_Hand_Right_Weapon_Input = false;
                playerControles.PlayerAction.TwoHandLeftWeapon.performed += i => two_Hand_Left_Weapon_Input = true;
                playerControles.PlayerAction.TwoHandLeftWeapon.canceled += i => two_Hand_Left_Weapon_Input = false;

                // Lock on
                playerControles.PlayerAction.LockOn.performed += i => lockOn_Input = true;
                playerControles.PlayerAction.SeekLeftLockOnTarget.performed += i => lockOn_Left_Input = true;
                playerControles.PlayerAction.SeekRightLockOnTarget.performed += i => lockOn_Right_Input = true;

                // Holding the input, sets the bool to true
                playerControles.PlayerAction.Sprint.performed += i => sprint_Input = true;
                // Releasing the input, sets the bool to false
                playerControles.PlayerAction.Sprint.canceled += i => sprint_Input = false;

                // Qued Input
                playerControles.PlayerAction.QuedRB.performed += i => QueInput(ref que_RB_Input);
                playerControles.PlayerAction.QuedRT.performed += i => QueInput(ref que_RT_Input);

                // UI Inputs
                playerControles.PlayerAction.Dodge.performed += i => closeMenuInput = true;
                playerControles.PlayerAction.OpenCharacterMenu.performed += i => openCharacterMenuInput = true;
            }

            playerControles.Enable();
        }

        private void OnDestroy()
        {
            // IF WE DESTROY THUS OBJECT, UNSUBSCRIBE FROM THIS EVENT
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        // IF WE MINIMIZE OR LOWER THE WINDOW, STOP ADJUSTIONG INPUTS
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControles.Enable();
                }
                else
                {
                    playerControles.Disable();
                }
            }
        }

        private void Update()
        {
            HandleAllInputs();
        }

        private void HandleAllInputs()
        {
            HandleTwoHandInput();
            HandleLockOnInput();
            HandleLockOnSwitchTargetInput();
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleJumpInput();
            HandleRBInput();
            HandleLBInput();
            HandleRTInput();
            HandleChargeRTInput();
            HandleSwitchRightWeaponInput();
            HandleSwitchLeftWeaponInput();
            HandleQuedInputs();
            HandleInteractionInput();
            HandLeCloseUIInput();
            HandleOpenCharacterMenuInput();
        }

        // Two Hand
        private void HandleTwoHandInput()
        {
            // if ui window open, simply return without doing anything
            if (PlayerUIManager.instance.menuWindowIsOpen)
                return;


            if (!two_Hand_Input)
                return;

            if (two_Hand_Right_Weapon_Input)
            {
                // if using the two hand input and press the right two hand button want to stop the regular rb input (or else we would attack)
                RB_Input = false;
                two_Hand_Right_Weapon_Input = false;
                player.playerNetworkManager.isBlocking.Value = false;

                if (player.playerNetworkManager.isTwoHandingWeapon.Value)
                {
                    // if is two handing a weapon alreay, change the is twohanding bool to false which trigger an "onvaluechanged" function, which un-twohands current weapon
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                    return;
                }
                else
                {
                    // if not alreay two handing, change the right two hand bool to true, which triggers an onvaluechange function
                    // this function two hands the right weapon
                    player.playerNetworkManager.isTwoHandingRightWeapon.Value = true;
                    return;
                }
            }
            else if (two_Hand_Left_Weapon_Input)
            {
                // if using the two hand input and press the left two hand button want to stop the regular rb input (or else we would attack)
                LB_Input = false;
                two_Hand_Left_Weapon_Input = false;
                player.playerNetworkManager.isBlocking.Value = false;

                if (player.playerNetworkManager.isTwoHandingWeapon.Value)
                {
                    // if is two handing a weapon alreay, change the is twohanding bool to false which trigger an "onvaluechanged" function, which un-twohands current weapon
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                    return;
                }
                else
                {
                    // if not alreay two handing, change the left two hand bool to true, which triggers an onvaluechange function
                    // this function two hands the left weapon
                    player.playerNetworkManager.isTwoHandingLeftWeapon.Value = true;
                    return;
                }
            }
        }

        // Lock on
        private void HandleLockOnInput()
        {
            // if ui window open, simply return without doing anything
            if (PlayerUIManager.instance.menuWindowIsOpen)
                return;


            // check for dead target
            if (player.playerNetworkManager.isLockedOn.Value)
            {
                if (player.playerCombatManager.currentTarget == null)
                    return;

                if (player.playerCombatManager.currentTarget.isDead.Value)
                {
                    player.playerNetworkManager.isLockedOn.Value = false;
                }

                // attempt to find new target

                // this assures that the coroutine never runs muiltple times overlepping itself
                if (lockOnCoroutine != null)
                    StopCoroutine(lockOnCoroutine);

                lockOnCoroutine = StartCoroutine(PlayerCamera.instance.WaitthenFindNewTarget());
            }

            if (lockOn_Input && player.playerNetworkManager.isLockedOn.Value)
            {
                lockOn_Input = false;
                PlayerCamera.instance.ClearLockOnTargets();
                player.playerNetworkManager.isLockedOn.Value = false;
                // disable lock on 
                return;
            }

            if (lockOn_Input && !player.playerNetworkManager.isLockedOn.Value)
            {
                lockOn_Input = false;

                // if aiming using ranged weapons return (dont allow lock whilst aiming)

                PlayerCamera.instance.HandleLocatingLockOnTargets();

                if (PlayerCamera.instance.nearestLockOnTarget != null)
                {
                    player.playerCombatManager.SetTarget(PlayerCamera.instance.nearestLockOnTarget);
                    player.playerNetworkManager.isLockedOn.Value = true;

                }
            }
        }

        private void HandleLockOnSwitchTargetInput()
        {
            if (lockOn_Left_Input)
            {
                lockOn_Left_Input = false;

                if (player.playerNetworkManager.isLockedOn.Value)
                {
                    PlayerCamera.instance.HandleLocatingLockOnTargets();

                    if (PlayerCamera.instance.leftLockOnTarget != null)
                    {
                        player.playerCombatManager.SetTarget(PlayerCamera.instance.leftLockOnTarget);
                    }
                }
            }

            if (lockOn_Right_Input)
            {
                lockOn_Right_Input = false;

                if (player.playerNetworkManager.isLockedOn.Value)
                {
                    PlayerCamera.instance.HandleLocatingLockOnTargets();

                    if (PlayerCamera.instance.rightLockOnTarget != null)
                    {
                        player.playerCombatManager.SetTarget(PlayerCamera.instance.rightLockOnTarget);
                    }
                }
            }
        }

        // Movemt
        private void HandlePlayerMovementInput()
        {
            // if ui window open, simply return without doing anything
            if (PlayerUIManager.instance.menuWindowIsOpen)
                return;


            vertical_Input = movement_Input.y;
            horizontal_Input = movement_Input.x;
            
            // RETURNS THE ABSOLUTE NUMBER, (Meaning number without the nagative sign, so its always positive)
            moveAmount = Mathf.Clamp01(Mathf.Abs(vertical_Input) + Mathf.Abs(horizontal_Input));

            //WE CLAMP THE VALUES,so they are 0, 0.5 or 1
            if (moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            // pass 0  on the horizontal? because only when want non-strafing movement
            // used the horizontal when strafing or load on 

            if (player == null)
                return;

            if (moveAmount != 0)
            {
                player.playerNetworkManager.isMoving.Value = true;
            }
            else
            {
                player.playerNetworkManager.isMoving.Value = false;
            }

            // if not locked on, only used the move amount

            if (!player.playerNetworkManager.isLockedOn.Value || player.playerNetworkManager.isSprinting.Value)
            {
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);
            }
            else
            {
                player.playerAnimatorManager.UpdateAnimatorMovementParameters(horizontal_Input, vertical_Input, player.playerNetworkManager.isSprinting.Value);
            }

            // if locked on pass the horizontal movement as well
        }

        private void HandleCameraMovementInput()
        {
            // if ui window open, simply return without doing anything
            if (PlayerUIManager.instance.menuWindowIsOpen)
                return;


            cameraVerical_Input = camera_Input.y;
            cameraHorizontal_Input = camera_Input.x;
        }

        // Action
        private void HandleDodgeInput()
        {
            if (dodge_Input)
            {
                dodge_Input = false;

                // if ui window open, simply return without doing anything
                if (PlayerUIManager.instance.menuWindowIsOpen)
                    return;


                player.playerLocomotionManager.AttemptToPerformDodge();
            }
        }

        private void HandleSprintInput()
        {
            if (sprint_Input)
            {
                player.playerLocomotionManager.HandleSprinting();
            }
            else
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }
        }

        private void HandleJumpInput()
        {
            if (jump_Input)
            {
                jump_Input = false;

                // if ui window open, simply return without doing anything
                if (PlayerUIManager.instance.menuWindowIsOpen)
                    return;

                // attempt to preform jump
                player.playerLocomotionManager.AttemptToPerformJump();
            }
        }

        private void HandleRBInput()
        {
            if (two_Hand_Input)
                return;

            if (RB_Input)
            {
                RB_Input = false;

                // if ui window open, simply return without doing anything
                if (PlayerUIManager.instance.menuWindowIsOpen)
                    return;

                player.playerNetworkManager.SetCharacterActionHand(true);

                // todo: if two handing the weapon, use the two handed action

                player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentRightHandWeapon.oh_RB_Action, player.playerInventoryManager.currentRightHandWeapon);
            }
        }

        private void HandleLBInput()
        {
            if (two_Hand_Input)
                return;

            if (LB_Input)
            {
                LB_Input = false;

                // if ui window open, simply return without doing anything
                if (PlayerUIManager.instance.menuWindowIsOpen)
                    return;


                player.playerNetworkManager.SetCharacterActionHand(true);

                // todo: if two handing the weapon, use the two handed action

                player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentLeftHandWeapon.oh_LB_Action, player.playerInventoryManager.currentLeftHandWeapon);
            }
        }

        private void HandleRTInput()
        {
            if (RT_Input)
            {
                RT_Input = false;

                // if ui window open, simply return without doing anything
                if (PlayerUIManager.instance.menuWindowIsOpen)
                    return;


                player.playerNetworkManager.SetCharacterActionHand(true);

                // todo: if two handing the weapon, use the two handed action

                player.playerCombatManager.PerformWeaponBasedAction(player.playerInventoryManager.currentRightHandWeapon.oh_RT_Action, player.playerInventoryManager.currentRightHandWeapon);
            }
        }

        private void HandleChargeRTInput()
        {
            // only chech for a charge if player are in an action that requires in (attacking)
            if (player.isPerfromingAction)
            {
                if (player.playerNetworkManager.isUsingRightHand.Value)
                {
                    player.playerNetworkManager.isChargingAttack.Value = Hold_RT_Input;
                }
            }
        }

        private void HandleSwitchRightWeaponInput()
        {
            if (switch_Right_Weapon_Input)
            {
                switch_Right_Weapon_Input = false;

                if (PlayerUIManager.instance.menuWindowIsOpen)
                    return;

                player.playerEquipmentManager.SwitchRightWeapon();
            }
        }

        private void HandleSwitchLeftWeaponInput()
        {
            if (switch_Left_Weapon_Input)
            {
                switch_Left_Weapon_Input = false;

                if (PlayerUIManager.instance.menuWindowIsOpen)
                    return;

                player.playerEquipmentManager.SwitchLeftWeapon();
            }
        }

        private void HandleInteractionInput()
        {
            if (interaction_Input)
            {
                interaction_Input = false;

                player.playerInteractionManager.Interact();
            }
        }

        private void QueInput(ref bool quedInput) // passing a reference will pass a specific bool, and not the value of that bool (true or false)
        {
            // rest all qued inputs so only one can que at a time
            que_RB_Input = false;
            que_RT_Input = false;
            //que_LB_Input = false;
            //que_LT_Input = false;

            // check for ui windows bring open, if its open return

            if(player.isPerfromingAction || player.playerNetworkManager.isJumping.Value)
            {
                quedInput = true;
                que_Input_Timer = default_Que_Input_Time;
                input_Que_Is_Active = true;
            }
        }

        private void ProcessQuedInput()
        {
            if (player.isDead.Value)
                return;

            if(que_RB_Input)
                RB_Input = true;

            if (que_RT_Input)
                RT_Input = true;
        }

        private void HandleQuedInputs()
        {
            if (input_Que_Is_Active)
            {
                // while the timer is above 0, keep attempting press the input
                if (que_Input_Timer > 0)
                {
                    que_Input_Timer -= Time.deltaTime;
                    ProcessQuedInput();
                }
                else
                {
                    // reset all qued inputs
                    que_RB_Input = false;
                    que_RT_Input = false;

                    input_Que_Is_Active = false;
                    que_Input_Timer = 0;
                }
            }
        }

        private void HandleOpenCharacterMenuInput()
        {
            if (openCharacterMenuInput)
            {
                openCharacterMenuInput = false;

                PlayerUIManager.instance.playerUIPopUpManager.CloseAllPopUpWindows();
                PlayerUIManager.instance.CloseAllMenuWindows();
                PlayerUIManager.instance.playerUICharacterMenuManager.OpenCharacterMenu();
            }
        }

        private void HandLeCloseUIInput()
        {
            if (closeMenuInput)
            {
                closeMenuInput = false;

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    PlayerUIManager.instance.CloseAllMenuWindows();
                }
            }
        }
    }
}
