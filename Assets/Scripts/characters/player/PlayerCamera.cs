using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        public PlayerManager player;
        public Camera cameraObject;
        [SerializeField] Transform cameraPivotTransform;

        // change these to tweak camera perfromance
        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 1; // the bigger this number, longer for the camera to reach its position during movement
        [SerializeField] float leftAndRightRotationSpeed = 220;
        [SerializeField] float upAndDownRotationSpeed = 220;
        [SerializeField] float minimumPivot = -30; // the lowest point able to look down
        [SerializeField] float maximumPivot = 60;  // the highest point able to look up
        [SerializeField] float cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask collideWithLayer;

        [Header("Camera Valuse")]
        private Vector3 cameraVelocity;
        private Vector3 CameraObjectsPosition; // used for camera collisions (move the camera object to this postioin upon colliding)
        [SerializeField] float leftAndRightLookAngle;
        [SerializeField] float upAndDownLookAngle;
        private float cameraZPosition; // values used for the camera collisions
        private float targetCameraZPosition; // values used for the camera collisions

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
            cameraZPosition = cameraObject.transform.position.z;
        }

        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                HandleFollowTarget();
                HanleRotations();
                HandleCollisions();
            }
           
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
            transform.position = targetCameraPosition;
        }

        private void HanleRotations()
        {
            // if locked on force rotation towards targets
            // else rotate regularly

            // normal rotations
            // rotation left and right based on horizontal movement on the mouse
            leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            // rotation left and right based on horizontal movement on the mouse
            upAndDownLookAngle -= (PlayerInputManager.instance.cameraVericalInput * upAndDownRotationSpeed) * Time.deltaTime;
            // clamp the up and down look angle to min and max values
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotion;

            cameraRotation.y = leftAndRightLookAngle;
            targetRotion = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotion;

            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotion = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotion;
        }

        private void HandleCollisions()
        {
            targetCameraZPosition = cameraZPosition;

            RaycastHit hit;
            // dirction for collision check
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            // we check if there is an object in front of our desired direction ^(see above)
            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayer))
            {
                // if there is, we get our distance from it
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                // we then Equate out target z position to the following
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            // our target position is ledd than our collision radius, we subtract our collision radius (making it snap back)
            if (Mathf.Abs(targetCameraZPosition) <cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            // we then apply our final position using lerap over a time of 0.2f
            CameraObjectsPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = CameraObjectsPosition;
        }
    }
}