using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class CharacterFootStepSFXMaker : MonoBehaviour
    {
        CharaterManager charater;

        AudioSource audioSource;
        GameObject SteppedOnObject;
        [SerializeField] float distanceToGround = 0.05f;

        private bool hasTouchedGround = false;
        private bool hasPlayedFootStepSFX = false;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            charater = GetComponentInParent<CharaterManager>();
        }

        private void FixedUpdate()
        {
            CheckForFootSteps();
        }

        private void CheckForFootSteps()
        {
            if(charater == null)
                return;

            if(!charater.characterNetworkManager.isMoving.Value)
                return;

            RaycastHit hit;

            if(Physics.Raycast(transform.position, charater.transform.TransformDirection(Vector3.down), out hit, distanceToGround, WorldUtilityManager.instance.GetEnviroLayer()))
            {
                hasTouchedGround = true;

                if(!hasPlayedFootStepSFX)
                    SteppedOnObject = hit.transform.gameObject;
            }
            else
            {
                hasTouchedGround = false;
                hasPlayedFootStepSFX = false;
                SteppedOnObject = null;
            }

            if (hasTouchedGround && !hasPlayedFootStepSFX)
            {
                hasPlayedFootStepSFX = true;
                PlayFootStepSoundFX();
            }
        }

        private void PlayFootStepSoundFX()
        {
            // play a differnet sfx depending on the later of the ground or tag (snow, wood, stone Ect)


            charater.characterSoundFXManager.PlayFootStepSoundFX();
        }
    }
}
