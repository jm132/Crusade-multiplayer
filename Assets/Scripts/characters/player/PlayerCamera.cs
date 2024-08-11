using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        [SerializeField] private float cameraSmoothSpeed = 1; // the bigger this number, longer for the camera to reach its position during movement
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

        [Header("Lock On")]
        [SerializeField] float lockOnRadius = 20;
        [SerializeField] float minimumViewableAngle = -50;
        [SerializeField] float maxmumViewableAngle = 50;
        [SerializeField] float lockOnTargetFollowSpeed = 0.2f;
        [SerializeField] float setCameraHeightSpeed = 1;
        [SerializeField] float unLockedCameraHeight = 1.65f;
        [SerializeField] float lockedCameraHeight = 2.0f;
        private Coroutine cameraLockOnHightCoroutien;
        private List<CharaterManager> availableTargets = new List<CharaterManager>();
        public CharaterManager nearestLockOnTarget;
        public CharaterManager leftLockOnTarget;
        public CharaterManager rightLockOnTarget;

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
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed);
            transform.position = targetCameraPosition;
        }

        private void HanleRotations()
        {
            // if locked on force rotation towards targets
            if (player.playerNetworkManager.isLockedOn.Value)
            {
                //this roataes this gameobject
                Vector3 rotationDirection = player.playerCombatManager.currentTarget.characterCombatManager.lockOnTransform.position - transform.position;
                rotationDirection.Normalize();
                rotationDirection.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(rotationDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lockOnTargetFollowSpeed);

                // this rotates the pivot object
                rotationDirection = player.playerCombatManager.currentTarget.characterCombatManager.lockOnTransform.position - cameraPivotTransform.position;
                rotationDirection.Normalize();

                targetRotation = Quaternion.LookRotation(rotationDirection);
                cameraPivotTransform.transform.rotation = Quaternion.Slerp(cameraPivotTransform.rotation, targetRotation, lockOnTargetFollowSpeed);

                // save rotations to look angles
                leftAndRightLookAngle = transform.eulerAngles.y;
                upAndDownLookAngle = transform.eulerAngles.x;
            }
            // else rotate regularly
            else
            {
                // rotation left and right based on horizontal movement on the mouse
                leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontal_Input * leftAndRightRotationSpeed) * Time.deltaTime;
                // rotation left and right based on horizontal movement on the mouse
                upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerical_Input * upAndDownRotationSpeed) * Time.deltaTime;
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
            // else rotate regularly
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

        public void HandleLocatingLockOnTargets()
        {
            float shortestDistance = Mathf.Infinity;              // will be used to determine the target closest to the player
            float shortestDistanceOfRightTarget = Mathf.Infinity; // will be used to determine shortest distance on one axis to the right of current target (+)
            float shortestDistanceOfLeftTarget = -Mathf.Infinity; // will be used to determine shortest distance on one axis to the left of current target (-)

            // to do use a layermask 
            Collider[] collider = Physics.OverlapSphere(player.transform.position, lockOnRadius, WorldUtilityManager.instance.GetCharacterLayer());

            for (int i = 0; i < collider.Length; i++)
            {
                CharaterManager lockOnTarget = collider[i].GetComponent<CharaterManager>();

                if (lockOnTarget != null)
                {
                    // check if they are within the field of view
                    Vector3 lockOnTargetsDirection = lockOnTarget.transform.position - player.transform.position;
                    float distanceFromTarget = Vector3.Distance(player.transform.position, lockOnTarget.transform.position);
                    float viewableAngle = Vector3.Angle(lockOnTargetsDirection, cameraObject.transform.forward);

                    // if target is dead, check next potential target
                    if (lockOnTarget.isDead.Value)
                        continue;

                    // if target is player, check the next potential target
                    if (lockOnTarget.transform.root == player.transform.root)
                        continue;

                    // if the target is outside field of view or is blocked by enviroment, check next potential target
                    if (viewableAngle > minimumViewableAngle && viewableAngle < maxmumViewableAngle)
                    {
                        RaycastHit hit;

                        // todo add layer mask for enviro layers only
                        if (Physics.Linecast(player.playerCombatManager.lockOnTransform.position,
                            lockOnTarget.characterCombatManager.lockOnTransform.position,
                            out hit, WorldUtilityManager.instance.GetEnviroLayer()))
                        {
                            // if hit something, if cannt see lock on target
                            continue;
                        }
                        else
                        {
                            // otherwise, add them to potential target list
                           availableTargets.Add(lockOnTarget);
                        }
                    }
                }
            }

            // sort throught the potential target to see which one can lock onto first
            for (int k = 0; k < availableTargets.Count; k++)
            {
                if (availableTargets[k] != null)
                {
                    float diestaceFromTarget = Vector3.Distance(player.transform.position, availableTargets[k].transform.position);

                    if (diestaceFromTarget < shortestDistance)
                    {
                        shortestDistance = diestaceFromTarget;
                        nearestLockOnTarget = availableTargets[k];
                    }

                    // if already locked on when searching for a targets, search for the nearst left/right targets
                    if(player.playerNetworkManager.isLockedOn.Value)
                    {
                        Vector3 relativeEnemyPosition = player.transform.InverseTransformPoint(availableTargets[k].transform.position);
                        
                        var distanceFromLeftTarget = relativeEnemyPosition.x;
                        var distanceFromRightTarget = relativeEnemyPosition.y;

                        if (availableTargets[k] == player.playerCombatManager.currentTarget)
                            continue;
                       
                        // check the left side for targets
                        if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget > shortestDistanceOfLeftTarget)
                        {
                            shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                            leftLockOnTarget = availableTargets[k];
                        }
                        // check the right side for targets
                        else if (relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget)
                        {
                            shortestDistanceOfRightTarget = distanceFromRightTarget;
                            rightLockOnTarget = availableTargets[k];
                        }
                    }
                }
                else
                {
                    ClearLockOnTargets();
                    player.playerNetworkManager.isLockedOn.Value = false;
                }
            }
        }

        public void SetLockCamerHeight()
        {
            if (cameraLockOnHightCoroutien != null)
            {
                StopCoroutine(cameraLockOnHightCoroutien);
            }

            cameraLockOnHightCoroutien = StartCoroutine(SetCameraHight());
        }

        public void ClearLockOnTargets()
        {
            nearestLockOnTarget = null;
            leftLockOnTarget = null;
            rightLockOnTarget = null;
            availableTargets.Clear();
        }

        public IEnumerator WaitthenFindNewTarget()
        {
            while (player.isPerfromingAction)
            {
                yield return null;
            }

            ClearLockOnTargets();
            HandleLocatingLockOnTargets();

            if (nearestLockOnTarget != null)
            {
                player.playerCombatManager.SetTarget(nearestLockOnTarget);
                player.playerNetworkManager.isLockedOn.Value = true;
            }

            yield return null;
        }

        private IEnumerator SetCameraHight()
        {
            float duration = 1;
            float timer = 0;

            Vector3 velocity = Vector3.zero;
            Vector3 newLockedCameraheight = new Vector3(cameraPivotTransform.transform.localPosition.x, lockedCameraHeight);
            Vector3 newUnlockedCameraHeight = new Vector3(cameraPivotTransform.transform.localPosition.x, unLockedCameraHeight);

            while ( timer < duration )
            {
                timer += Time.deltaTime;

                if (player != null )
                {
                    if (player.playerCombatManager.currentTarget  != null)
                    {
                        cameraPivotTransform.transform.localPosition = 
                            Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newLockedCameraheight, ref velocity, setCameraHeightSpeed);
                        
                        cameraPivotTransform.transform.localRotation = 
                            Quaternion.Slerp(cameraPivotTransform.transform.localRotation, Quaternion.Euler(0, 0, 0), setCameraHeightSpeed);
                    }
                    else
                    {
                        cameraPivotTransform.transform.localPosition = 
                            Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition, newUnlockedCameraHeight, ref velocity, setCameraHeightSpeed);
                    }
                }

                yield return null;
            }

            if (player != null)
            {
                if (player.playerCombatManager.currentTarget != null)
                {
                    cameraPivotTransform.transform.localPosition = newLockedCameraheight;
                    cameraPivotTransform.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    cameraPivotTransform.transform.localPosition = newUnlockedCameraHeight;
                }
            }
            yield return null;
        }
    }
}