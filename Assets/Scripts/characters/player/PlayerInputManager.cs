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
        [SerializeField] Vector2 cameraInput;
        public float cameraVericalInput;
        public float cameraHorizontalInput;

        [Header("player movement input")]
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;
        [SerializeField] public float moveAmount;

        [Header("Player Action Input")]
        [SerializeField] bool dodgeInput = false;
        [SerializeField] bool sprintInput = false;
        [SerializeField] bool jumpInput = false;

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

                playerControles.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControles.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControles.PlayerAction.Dodge.performed += i => dodgeInput = true;
                playerControles.PlayerAction.Jump.performed += i => jumpInput = true;

                // holding the input, sets the bool to true
                playerControles.PlayerAction.Sprint.performed += i => sprintInput = true;
                // releasing the input, sets the bool to false
                playerControles.PlayerAction.Sprint.canceled += i => sprintInput = false;
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
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleJumpInput();
        }

        // Movemt

        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            
            // RETURNS THE ABSOLUTE NUMBER, (Meaning number without the nagative sign, so its always positive)
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            //WE CLAMP THE VALUES,so they are 0, 0.5 or 1
            if (moveAmount <= 0.5 && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                moveAmount = 1;
            }

            if (player == null)
                return;

            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);
        }

        private void HandleCameraMovementInput()
        {
            cameraVericalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;
        }

        // Action

        private void HandleDodgeInput()
        {
            if (dodgeInput)
            {
                dodgeInput = false;

                // furter note: return (do nothing) if menu or ui windows is open

                player.playerLocomotionManager.AttemptToPerformDodge();
            }
        }

        private void HandleSprintInput()
        {
            if (sprintInput)
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
            if (jumpInput)
            {
                jumpInput = false;

                // if ui window open, simply return without doing anything

                // attempt to preform jump
                player.playerLocomotionManager.AttemptToPerformJump();
            }
        }
    }
}
