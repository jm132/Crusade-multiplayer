using PsychoticLab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerBodyManager : MonoBehaviour
    {
        PlayerManager player;

        [Header("hair Object")]
        [SerializeField] public GameObject hair;
        [SerializeField] public GameObject faiclHair;

        [Header("Male")]
        [SerializeField] public GameObject maleObject;       // the master male gameobject parent
        [SerializeField] public GameObject maleHead;         // default head model when unequipping armor
        [SerializeField] public GameObject[] maleBody;       // default upperbody models when unequipping armor (chest, upper right arm,  upper left arm)
        [SerializeField] public GameObject[] maleArms;       // default upperbody models when unequipping armor (lower right arm, right hand, lower left arm, left hand)
        [SerializeField] public GameObject[] maleLegs;       // default upperbody models when unequipping armor (hips, right leg, left leg)
        [SerializeField] public GameObject maleEyebrows;     // facial feature
        [SerializeField] public GameObject maleFacialHair;   // facial feature 

        [Header("Female")]
        [SerializeField] public GameObject femaleObject;     // the master female gameobject parent
        [SerializeField] public GameObject femaleHead;
        [SerializeField] public GameObject[] femaleBody;
        [SerializeField] public GameObject[] femaleArms;
        [SerializeField] public GameObject[] femaleLegs;
        [SerializeField] public GameObject femaleEyebrows;

        private void Awake()
        {
            player = GetComponent<PlayerManager>();
        }

        //enable body features
        public void EnableHead()
        {
            // enable head object
            maleHead.SetActive(true);
            femaleHead.SetActive(true);

            // enable any facial object (eyebrows, lips, nose ect)
            maleEyebrows.SetActive(true);
            femaleEyebrows.SetActive(true);
        }

        public void DisableHead()
        {
            // disable head object
            maleHead.SetActive(false);
            femaleHead.SetActive(false);

            // disable any facial object (eyebrows, lips, nose ect)
            maleEyebrows.SetActive(false);
            femaleEyebrows.SetActive(false);
        }

        public void EnableHair()
        {
            hair.SetActive(true);
        }

        public void DisableHair()
        {
            hair.SetActive(false);
        }

        public void EnableFacialHair()
        {
            faiclHair.SetActive(true);
        }

        public void DisableFacialHair()
        {
            faiclHair.SetActive(false);
        }

        public void EnableBody()
        {
            foreach (var model in maleBody)
            {
                model.SetActive(true);
            }

            //foreach (var model in femaleBody)
            //{
                //model.SetActive(true);
            //}
        }

        public void EnableArms()
        {
            foreach (var model in maleArms)
            {
                model.SetActive(true);
            }

            foreach (var model in femaleArms)
            {
                model.SetActive(true);
            }
        }

        public void EnableLowerBody()
        {
            foreach (var model in maleLegs)
            {
                model.SetActive(true);
            }

            foreach (var model in femaleLegs)
            {
                model.SetActive(true);
            }
        }

        public void DisableArms()
        {
            foreach (var model in maleArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleArms)
            {
                model.SetActive(false);
            }
        }

        public void DisableBody()
        {
            foreach (var model in maleBody)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleBody)
            {
            model.SetActive(false);
            }
        }

        public void DisableLowerBody()
        {
            foreach (var model in maleLegs)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLegs)
            {
                model.SetActive(false);
            }
        }

        public void ToggleBodyType(bool isMale)
        {
            if (isMale)
            {
                maleObject.SetActive(true);
                femaleObject.SetActive(false);
            }
            else
            {
                maleObject.SetActive(false);
                femaleObject.SetActive(true);
            }
            
            player.playerEquipmentManager.EquipArmor();
        }
    }
}
