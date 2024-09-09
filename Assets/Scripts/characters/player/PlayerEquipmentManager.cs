using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerEquipmentManager : CharaterEquipmentManager
    {
        PlayerManager player;

        [Header("Weapon Model Instantiation Slots")]
        [HideInInspector] public WeaponModelInstantiationSlot rightHandWeaponSlot;
        [HideInInspector] public WeaponModelInstantiationSlot leftHandWeaponSlot;
        [HideInInspector] public WeaponModelInstantiationSlot leftHandShieldSlot;
        [HideInInspector] public WeaponModelInstantiationSlot BackSlot;

        [Header("Weapon Models")]
        [HideInInspector] public GameObject rightHandWeaponModel;
        [HideInInspector] public GameObject leftHandWeaponModel;

        [Header("Weapon Managers")]
        WeaponManager rightWeaponManger;
        WeaponManager leftWeaponManger;

        [Header("General Equipment Models")]
        public GameObject hatObject;
        [HideInInspector] public GameObject[] hats;
        public GameObject hoodsObject;
        [HideInInspector] public GameObject[] hoods;
        public GameObject faceCoversObject;
        [HideInInspector] public GameObject[] faceCover;
        public GameObject helmetAccessoriesObject;
        [HideInInspector] public GameObject[] helmetAccessories;
        public GameObject backAccessoriesObject;
        [HideInInspector] public GameObject[] backAccessories;
        public GameObject hipsAccessoriesObject;
        [HideInInspector] public GameObject[] hipsAccessories;
        public GameObject rightShoulderObject;
        [HideInInspector] public GameObject[] rightShoulder;
        public GameObject rightElbowObject;
        [HideInInspector] public GameObject[] rightElbow;
        public GameObject rightKneeObject;
        [HideInInspector] public GameObject[] rightKnee;
        public GameObject leftShoulderObject;
        [HideInInspector] public GameObject[] leftShoulder;
        public GameObject leftElbowObject;
        [HideInInspector] public GameObject[] leftElbow;
        public GameObject leftKneeObject;
        [HideInInspector] public GameObject[] leftKnee;

        [Header("Male Equipment Models")]
        public GameObject maleFullHelmetObject;
        [HideInInspector] public GameObject[] maleHeadFullHelmets;
        public GameObject maleFullBodyObject;
        [HideInInspector]public GameObject[] maleBodies;
        public GameObject maleRightUpperArmObject;
        [HideInInspector] public GameObject[] maleRightUpperArms;
        public GameObject maleRightLowerArmObject;
        [HideInInspector] public GameObject[] maleRightLowerArms;
        public GameObject maleRightHandObject;
        [HideInInspector] public GameObject[] maleRightHands;
        public GameObject maleLeftUpperArmObject;
        [HideInInspector] public GameObject[] maleLeftUpperArms;
        public GameObject maleLeftLowerArmObject;
        [HideInInspector] public GameObject[] maleLeftLowerArms;
        public GameObject maleLefHandObject;
        [HideInInspector] public GameObject[] maleLeftHands;
        public GameObject maleHipsObject;
        [HideInInspector] public GameObject[] maleHips;
        public GameObject maleRightLegObject;
        [HideInInspector] public GameObject[] maleRightLeg;
        public GameObject maleLeftLegObject;
        [HideInInspector] public GameObject[] maleLeftLeg;

        [Header("Female Equipment Models")]
        public GameObject femaleFullHelmetObject;
        [HideInInspector] public GameObject[] femaleHeadFullHelmets;
        public GameObject femaleFullBodyObject;
        [HideInInspector] public GameObject[] femaleBodies;
        public GameObject femaleRightUpperArmObject;
        [HideInInspector] public GameObject[] femaleRightUpperArms;
        public GameObject femaleRightLowerArmObject;
        [HideInInspector] public GameObject[] femaleRightLowerArms;
        public GameObject femaleRightHandObject;
        [HideInInspector] public GameObject[] femaleRightHands;
        public GameObject femaleLeftUpperArmObject;
        [HideInInspector] public GameObject[] femaleLeftUpperArms;
        public GameObject femaleLeftLowerArmObject;
        [HideInInspector] public GameObject[] femaleLeftLowerArms;
        public GameObject femaleLefHandObject;
        [HideInInspector] public GameObject[] femaleLeftHands;
        public GameObject femaleHipsObject;
        [HideInInspector] public GameObject[] femaleHips;
        public GameObject femaleRightLegObject;
        [HideInInspector] public GameObject[] femaleRightLeg;
        public GameObject femaleLeftLegObject;
        [HideInInspector] public GameObject[] femaleLeftLeg;

        [Header("debug delete later")]
        [SerializeField] bool equipNewItems = false;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();

            InitializeWeaponSlots();

            InitializeArmorModels();

            EquipArmor();
        }



        protected override void Start()
        {
            base.Start();

            LoadWeaponsOnBothHands();
        }

        private void Update()
        {
            if (equipNewItems)
            {
                equipNewItems = false;
                EquipArmor();
            }
        }

        public void EquipArmor()
        {
            LoadHeadEquipment(player.playerInventoryManager.headEquipment);
            LoadBodyEquipment(player.playerInventoryManager.bodyEquipment);
            LoadLegEquipment(player.playerInventoryManager.legEquipment);
            LoadHandEquipment(player.playerInventoryManager.handEquipment);
        }

        // equipment
        private void InitializeArmorModels()
        {
            // hats
            List<GameObject> hatsList = new List<GameObject>();

            foreach (Transform child in hatObject.transform)
            {
                hatsList.Add(child.gameObject);
            }

            hats = hatsList.ToArray();

            // full helmates
            List<GameObject> maleFullHelmetsList = new List<GameObject>();

            foreach (Transform child in maleFullHelmetObject.transform)
            {
                maleFullHelmetsList.Add(child.gameObject);
            }

            maleHeadFullHelmets = maleFullHelmetsList.ToArray();

            List<GameObject> femaleFullHelmetsList = new List<GameObject>();

            foreach (Transform child in femaleFullHelmetObject.transform)
            {
                femaleFullHelmetsList.Add(child.gameObject);
            }

            femaleHeadFullHelmets = femaleFullHelmetsList.ToArray();

            // helmet accessories
            List<GameObject> helmetAccessoriesList = new List<GameObject>();

            foreach (Transform child in helmetAccessoriesObject.transform)
            {
                helmetAccessoriesList.Add(child.gameObject);
            }

            helmetAccessories = helmetAccessoriesList.ToArray();

            // hoods
            List<GameObject> hoodsList = new List<GameObject>();

            foreach (Transform child in hoodsObject.transform)
            {
                hoodsList.Add(child.gameObject);
            }

            hoods = hoodsList.ToArray();

            // face covers
            List<GameObject> faceCoversList = new List<GameObject>();

            foreach (Transform child in faceCoversObject.transform)
            {
                faceCoversList.Add(child.gameObject);
            }

            faceCover = faceCoversList.ToArray();

            // back accessories
            List<GameObject> backAccessoriesList = new List<GameObject>();

            foreach (Transform child in backAccessoriesObject.transform)
            {
                backAccessoriesList.Add(child.gameObject);
            }

            backAccessories = backAccessoriesList.ToArray();

            //bodies
            List<GameObject> maleBodiesList = new List<GameObject>();

            foreach (Transform child in maleFullBodyObject.transform)
            {
                maleBodiesList.Add(child.gameObject);
            }

            maleBodies = maleBodiesList.ToArray();

            List<GameObject> femaleBodiesList = new List<GameObject>();

            foreach (Transform child in femaleFullBodyObject.transform)
            {
                femaleBodiesList.Add(child.gameObject);
            }

            femaleBodies = femaleBodiesList.ToArray();

            // hips accessories
            List<GameObject> hipsAccessoriesList = new List<GameObject>();

            foreach (Transform child in hipsAccessoriesObject.transform)
            {
                hipsAccessoriesList.Add(child.gameObject);
            }

            hipsAccessories = hipsAccessoriesList.ToArray();

            // right shoulder
            List<GameObject> rightShoulderList = new List<GameObject>();

            foreach (Transform child in rightShoulderObject.transform)
            {
                rightShoulderList.Add(child.gameObject);
            }

            rightShoulder = rightShoulderList.ToArray();

            // right upper arm
            List<GameObject> maleRightUpperArmList = new List<GameObject>();

            foreach (Transform child in maleRightUpperArmObject.transform)
            {
                maleRightUpperArmList.Add(child.gameObject);
            }

            maleRightUpperArms = maleRightUpperArmList.ToArray();

            // right upper arm
            List<GameObject> femaleRightUpperArmList = new List<GameObject>();

            foreach (Transform child in femaleRightUpperArmObject.transform)
            {
                femaleRightUpperArmList.Add(child.gameObject);
            }

            femaleRightUpperArms = femaleRightUpperArmList.ToArray();

            // right elbow
            List<GameObject> rightElbowList = new List<GameObject>();

            foreach (Transform child in rightElbowObject.transform)
            {
                rightElbowList.Add(child.gameObject);
            }

            rightElbow = rightElbowList.ToArray();

            // right lower arm
            List<GameObject> maleRightLowerArmList = new List<GameObject>();

            foreach (Transform child in maleRightLowerArmObject.transform)
            {
                maleRightLowerArmList.Add(child.gameObject);
            }

            maleRightLowerArms = maleRightLowerArmList.ToArray();

            // right lower arm
            List<GameObject> femaleRightLowerArmList = new List<GameObject>();

            foreach (Transform child in femaleRightLowerArmObject.transform)
            {
                femaleRightLowerArmList.Add(child.gameObject);
            }

            femaleRightLowerArms = femaleRightLowerArmList.ToArray();

            // right hands
            List<GameObject> maleRightHandsList = new List<GameObject>();

            foreach (Transform child in maleRightHandObject.transform)
            {
                maleRightHandsList.Add(child.gameObject);
            }

            maleRightHands = maleRightHandsList.ToArray();

            // right hands
            List<GameObject> femaleRightHandsList = new List<GameObject>();

            foreach (Transform child in femaleRightHandObject.transform)
            {
                femaleRightHandsList.Add(child.gameObject);
            }

            femaleRightHands = femaleRightHandsList.ToArray();

            // left shoulder
            List<GameObject> leftShoulderList = new List<GameObject>();

            foreach (Transform child in leftShoulderObject.transform)
            {
                leftShoulderList.Add(child.gameObject);
            }

            leftShoulder = leftShoulderList.ToArray();

            // left upper arm
            List<GameObject> maleLeftUpperArmList = new List<GameObject>();

            foreach (Transform child in maleLeftUpperArmObject.transform)
            {
                maleLeftUpperArmList.Add(child.gameObject);
            }

            maleLeftUpperArms = maleLeftUpperArmList.ToArray();

            // left upper arm
            List<GameObject> femaleLeftUpperArmList = new List<GameObject>();

            foreach (Transform child in maleLeftUpperArmObject.transform)
            {
                femaleLeftUpperArmList.Add(child.gameObject);
            }

            femaleLeftUpperArms = femaleLeftUpperArmList.ToArray();

            // left elbow
            List<GameObject> leftElbowList = new List<GameObject>();

            foreach (Transform child in leftElbowObject.transform)
            {
                leftElbowList.Add(child.gameObject);
            }

            leftElbow = leftElbowList.ToArray();

            // left lower arm
            List<GameObject> maleLeftLowerArmList = new List<GameObject>();

            foreach (Transform child in maleLeftLowerArmObject.transform)
            {
                maleLeftLowerArmList.Add(child.gameObject);
            }

            maleLeftLowerArms = maleLeftLowerArmList.ToArray();

            // left lower arm
            List<GameObject> femaleLeftLowerArmList = new List<GameObject>();

            foreach (Transform child in femaleLeftLowerArmObject.transform)
            {
                femaleLeftLowerArmList.Add(child.gameObject);
            }

            femaleLeftLowerArms = femaleLeftLowerArmList.ToArray();

            // male right hands
            List<GameObject> maleLeftHandsList = new List<GameObject>();

            foreach (Transform child in maleLefHandObject.transform)
            {
                maleLeftHandsList.Add(child.gameObject);
            }

            maleLeftHands = maleLeftHandsList.ToArray();

            //right hands
            List<GameObject> femaleLeftHandsList = new List<GameObject>();

            foreach (Transform child in femaleLefHandObject.transform)
            {
                femaleLeftHandsList.Add(child.gameObject);
            }

            femaleLeftHands = femaleLeftHandsList.ToArray();

            //hips
            List<GameObject> maleHipsList = new List<GameObject>();

            foreach (Transform child in maleHipsObject.transform)
            {
                maleHipsList.Add(child.gameObject);
            }

            maleHips = maleHipsList.ToArray();

            //hips
            List<GameObject> femaleHipsList = new List<GameObject>();

            foreach (Transform child in femaleHipsObject.transform)
            {
                femaleHipsList.Add(child.gameObject);
            }

            femaleHips = femaleHipsList.ToArray();

            //right leg
            List<GameObject> maleRightLegList = new List<GameObject>();

            foreach (Transform child in maleRightLegObject.transform)
            {
                maleRightLegList.Add(child.gameObject);
            }

            maleRightLeg = maleRightLegList.ToArray();

            //right leg
            List<GameObject> femaleRightLegList = new List<GameObject>();

            foreach (Transform child in femaleRightLegObject.transform)
            {
                femaleRightLegList.Add(child.gameObject);
            }

            femaleRightLeg = femaleRightLegList.ToArray();

            // right knee
            List<GameObject> rightKneeList = new List<GameObject>();

            foreach (Transform child in rightKneeObject.transform)
            {
                rightKneeList.Add(child.gameObject);
            }

            rightKnee = rightKneeList.ToArray();

            // left leg
            List<GameObject> maleLeftLegList = new List<GameObject>();

            foreach (Transform child in maleLeftLegObject.transform)
            {
                maleLeftLegList.Add(child.gameObject);
            }

            maleLeftLeg = maleLeftLegList.ToArray();

            // left leg
            List<GameObject> femaleLeftLegList = new List<GameObject>();

            foreach (Transform child in femaleLeftLegObject.transform)
            {
                femaleLeftLegList.Add(child.gameObject);
            }

            femaleLeftLeg = femaleLeftLegList.ToArray();

            // left knee
            List<GameObject> leftKneeList = new List<GameObject>();

            foreach (Transform child in leftKneeObject.transform)
            {
                leftKneeList.Add(child.gameObject);
            }

            leftKnee = leftKneeList.ToArray();
        }

        public void LoadHeadEquipment(HeadEquipmentItem equipmenet)
        {
            // 1. unload old head equipment models
            UnloadHeadEquipmentModels();
            // 2. if equipment is null set equipment in inverntroy to null and return
            if (equipmenet == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.headEquipmentID.Value = -1; // -1 will never be an item id, so it will always be null

                player.playerInventoryManager.headEquipment = null;
                return;
            }
            // 3. if an "onitemequipped" call on equipment, run it now
            // 4. set current equipment in player inventory to the equipment that is passed to this funcation 
            player.playerInventoryManager.headEquipment = equipmenet;
            // 5. if need to check for equipment type to disable certainbody features do it now

            switch (equipmenet.headEquipmentType)
            {
                case HeadEquipmentType.FullHelmet:
                    player.playerBodyManager.DisableHair();
                    player.playerBodyManager.DisableHead();
                    break;
                case HeadEquipmentType.Hat:
                    break;
                case HeadEquipmentType.Hood:
                    player.playerBodyManager.DisableHair();
                    break;
                case HeadEquipmentType.FaceCover:
                    player.playerBodyManager.DisableFacialHair();
                    break;
                default:
                    break;
            }
            // 6. load head equipment models
            foreach (var model in equipmenet.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }
            // 7. calculate total equipment load (weight of all worn equipment. this impacts roll speeds and at extreme weights, movement speeds)
            // 8. calculate total armor absorption
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.headEquipmentID.Value = equipmenet.itemID;
        }

        private void UnloadHeadEquipmentModels()
        {
            foreach (var model in maleHeadFullHelmets)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleHeadFullHelmets)
            {
                model.SetActive(false);
            }

            foreach (var model in hats)
            {
                model.SetActive(false);
            }

            foreach (var model in faceCover)
            {
                model.SetActive(false);
            }

            foreach (var model in hoods)
            {
                model.SetActive(false);
            }

            foreach (var model in helmetAccessories)
            {
                model.SetActive(false);
            }

            player.playerBodyManager.EnableHead();
            player.playerBodyManager.EnableHair();
        }

        public void LoadBodyEquipment(BodyEquipmentItem equipmenet)
        {
            // 1. unload old body equipment models
            UnloadBodyEquipmentModels();
            // 2. if equipment is null set equipment in inverntroy to null and return
            if (equipmenet == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.bodyEquipmentID.Value = -1; // -1 will never be an item id, so it will always be null

                player.playerInventoryManager.bodyEquipment = null;
                return;
            }
            // 3. if an "onitemequipped" call on equipment, run it now
            // 4. set current equipment in player inventory to the equipment that is passed to this funcation 
            player.playerInventoryManager.bodyEquipment = equipmenet;
            // 5. if need to check for equipment type to disable certainbody features do it now
            player.playerBodyManager.DisableBody();

            // 6. load body equipment models
            foreach (var model in equipmenet.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }
            // 7. calculate total equipment load (weight of all worn equipment. this impacts roll speeds and at extreme weights, movement speeds)
            // 8. calculate total armor absorption
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.bodyEquipmentID.Value = equipmenet.itemID;
        }

        private void UnloadBodyEquipmentModels()
        {
            foreach (var model in rightShoulder)
            {
                model.SetActive(false);
            }

            foreach (var model in rightElbow)
            {
                model.SetActive(false);
            }

            foreach (var model in leftShoulder)
            {
                model.SetActive(false);
            }

            foreach (var model in leftElbow)
            {
                model.SetActive(false);
            }

            foreach (var model in backAccessories)
            {
                model.SetActive(false);
            }

            foreach (var model in maleBodies)
            {
                model.SetActive(false);
            }

            foreach (var model in maleRightUpperArms)
            {
                model.SetActive(false);
            }

            foreach (var model in maleLeftUpperArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleBodies)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleRightUpperArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLeftUpperArms)
            {
                model.SetActive(false);
            }

            player.playerBodyManager.EnableBody();
        }

        public void LoadLegEquipment(LegEquipmentItem equipmenet)
        {
            // 1. unload old head equipment models
            UnloadLegEquipmentModels();
            // 2. if equipment is null set equipment in inverntroy to null and return
            if (equipmenet == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.legEquipmentID.Value = -1; // -1 will never be an item id, so it will always be null

                player.playerInventoryManager.legEquipment = null;
                return;
            }
            // 3. if an "onitemequipped" call on equipment, run it now

            // 4. set current equipment in player inventory to the equipment that is passed to this funcation 
            player.playerInventoryManager.legEquipment = equipmenet;
            // 5. if need to check for equipment type to disable certainbody features do it now
            player.playerBodyManager.DisableLowerBody();

            // 6. load body equipment models
            foreach (var model in equipmenet.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }
            // 7. calculate total equipment load (weight of all worn equipment. this impacts roll speeds and at extreme weights, movement speeds)
            // 8. calculate total armor absorption
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.legEquipmentID.Value = equipmenet.itemID;
        }

        private void UnloadLegEquipmentModels()
        {
            foreach (var model in maleHips)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleHips)
            {
                model.SetActive(false);
            }

            foreach (var model in leftKnee)
            {
                model.SetActive(false);
            }

            foreach (var model in rightKnee)
            {
                model.SetActive(false);
            }

            foreach (var model in maleLeftLeg)
            {
                model.SetActive(false);
            }

            foreach (var model in maleRightLeg)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLeftLeg)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleRightLeg)
            {
                model.SetActive(false);
            }

            player.playerBodyManager.EnableLowerBody();
        }

        public void LoadHandEquipment(HandEquipmentItem equipmenet)
        {
            // 1. unload old head equipment models
            UnloadHandEquipmentModels();
            // 2. if equipment is null set equipment in inverntroy to null and return
            if (equipmenet == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.handEquipmentID.Value = -1; // -1 will never be an item id, so it will always be null

                player.playerInventoryManager.handEquipment = null;
                return;
            }
            // 3. if an "onitemequipped" call on equipment, run it now

            // 4. set current equipment in player inventory to the equipment that is passed to this funcation 
            player.playerInventoryManager.handEquipment = equipmenet;
            // 5. if need to check for equipment type to disable certainbody features do it now
            player.playerBodyManager.DisableArms();

            // 6. load body equipment models
            foreach (var model in equipmenet.equipmentModels)
            {
                model.LoadModel(player, player.playerNetworkManager.isMale.Value);
            }
            // 7. calculate total equipment load (weight of all worn equipment. this impacts roll speeds and at extreme weights, movement speeds)
            // 8. calculate total armor absorption
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.handEquipmentID.Value = equipmenet.itemID;
        }

        private void UnloadHandEquipmentModels()
        {
            foreach (var model in maleLeftLowerArms)
            {
                model.SetActive(false);
            }

            foreach (var model in maleRightLowerArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLeftLowerArms)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleRightLowerArms)
            {
                model.SetActive(false);
            }

            foreach (var model in maleLeftHands)
            {
                model.SetActive(false);
            }

            foreach (var model in maleRightHands)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleLeftHands)
            {
                model.SetActive(false);
            }

            foreach (var model in femaleRightHands)
            {
                model.SetActive(false);
            }

            player.playerBodyManager.EnableArms();
        }

        private void InitializeWeaponSlots()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

            foreach (var weaponSlot in weaponSlots)
            {
                if (weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
                {
                    rightHandWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHandWeaponSlot)
                {
                    leftHandWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHandShieldSlot)
                {
                    leftHandShieldSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.BackSlot)
                {
                    BackSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponsOnBothHands()
        {
            LoadRightWeapon();
            LoadLeftWeapon();
        }
        
        // right weapon

        public void SwitchRightWeapon()
        {
            if (!player.IsOwner)
                return;

            player.playerAnimatorManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, false, true, true);

            WeaponItem selectedWeapon = null;

            // disable two handing if two handing

            // add one to index to switch to the next potential weapon
            player.playerInventoryManager.rightHandWeaponIndex += 1;

            // if index is out of bounds, reset it to position #1 (0)
            if (player.playerInventoryManager.rightHandWeaponIndex < 0 || player.playerInventoryManager.rightHandWeaponIndex > 2)
            {
                player.playerInventoryManager.rightHandWeaponIndex = 0;

                // check if holding more then one weapon
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponPosition = 0;

                for (int i = 0; i < player.playerInventoryManager.weaponsInRightHandSlots.Length; i++)
                {
                    if (player.playerInventoryManager.weaponsInRightHandSlots[i].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        weaponCount += 1;

                        if (firstWeapon == null)
                        {
                            firstWeapon = player.playerInventoryManager.weaponsInRightHandSlots[i];
                            firstWeaponPosition = i;
                        }
                    }
                }

                if (weaponCount <= 1)
                {
                    player.playerInventoryManager.rightHandWeaponIndex = -1;
                    selectedWeapon = WorldItemDatabase.Instance.unarmedWeapon;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = selectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.rightHandWeaponIndex = firstWeaponPosition;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = firstWeapon.itemID;
                }

                return;
            }

            foreach (WeaponItem weapon in player.playerInventoryManager.weaponsInRightHandSlots)
            {
                // if the next potential weapon does not equal then unarmed weapon 
                if (player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex];
                    // assign the network weapon id so it switches for all connected clients
                    player.playerNetworkManager.currentRightHandWeaponID.Value = player.playerInventoryManager.weaponsInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID;
                    return;
                }
            }

            if (selectedWeapon == null && player.playerInventoryManager.rightHandWeaponIndex <= 2)
            {
                SwitchRightWeapon();
            }
        }

        public void LoadRightWeapon()
        {
            if (player.playerInventoryManager.currentRightHandWeapon != null)
            {
                // remove the old weapon
                rightHandWeaponSlot.UnloadWeapon();

                // bring in the new weapon
                rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
                rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);
                rightWeaponManger = rightHandWeaponModel.GetComponent<WeaponManager>();
                rightWeaponManger.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
                player.playerAnimatorManager.UpdatedAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);
            }
        }

        // left weapon

        public void SwitchLeftWeapon()
        {

            if (!player.IsOwner)
                return;

            player.playerAnimatorManager.PlayTargetActionAnimation("Swap_Left_Weapon_01", false, false, true, true);

            WeaponItem selectedWeapon = null;

            // disable two handing if two handing

            // add one to index to switch to the next potential weapon
            player.playerInventoryManager.leftHandWeaponIndex += 1;

            // if index is out of bounds, reset it to position #1 (0)
            if (player.playerInventoryManager.leftHandWeaponIndex < 0 || player.playerInventoryManager.leftHandWeaponIndex > 2)
            {
                player.playerInventoryManager.leftHandWeaponIndex = 0;

                // check if holding more then one weapon
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponPosition = 0;

                for (int i = 0; i < player.playerInventoryManager.weaponsInLeftHandSlots.Length; i++)
                {
                    if (player.playerInventoryManager.weaponsInLeftHandSlots[i].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                    {
                        weaponCount += 1;

                        if (firstWeapon == null)
                        {
                            firstWeapon = player.playerInventoryManager.weaponsInLeftHandSlots[i];
                            firstWeaponPosition = i;
                        }
                    }
                }

                if (weaponCount <= 1)
                {
                    player.playerInventoryManager.leftHandWeaponIndex = -1;
                    selectedWeapon = WorldItemDatabase.Instance.unarmedWeapon;
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = selectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.leftHandWeaponIndex = firstWeaponPosition;
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = firstWeapon.itemID;
                }

                return;
            }

            foreach (WeaponItem weapon in player.playerInventoryManager.weaponsInLeftHandSlots)
            {
                // if the next potential weapon does not equal then unarmed weapon 
                if (player.playerInventoryManager.weaponsInLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex].itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = player.playerInventoryManager.weaponsInLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex];
                    // assign the network weapon id so it switches for all connected clients
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = player.playerInventoryManager.weaponsInLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex].itemID;
                    return;
                }
            }

            if (selectedWeapon == null && player.playerInventoryManager.leftHandWeaponIndex <= 2)
            {
                SwitchLeftWeapon();
            }
        }

        public void LoadLeftWeapon()
        {
            if (player.playerInventoryManager.currentLeftHandWeapon != null)
            {
                // remove the old weapon
                if (leftHandWeaponSlot.currentWeaponModel != null)
                    leftHandWeaponSlot.UnloadWeapon();

                if (leftHandShieldSlot.currentWeaponModel != null)
                    leftHandShieldSlot.UnloadWeapon();

                // bring in the new weapon
                leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);

                switch (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType)
                {
                    case WeaponModelType.Weapon:
                        leftHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
                        break;
                    case WeaponModelType.Shield:
                        leftHandShieldSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
                        break;
                    default:
                        break;
                }

                leftWeaponManger = leftHandWeaponModel.GetComponent<WeaponManager>();
                leftWeaponManger.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
            }
        }

        // two hand

        public void UnTwoHandWeapon()
        {
            // update animator controller to current main hand wepaom
            player.playerAnimatorManager.UpdatedAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);
            // remove the strength bonus (two handing a weapon gives make the player strength level (strength + (strength * 0.5)

            // un-two hand the model and move the model that isnt being two handed back to its hand (if there is any)
            
            // left hand 
            if (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType == WeaponModelType.Weapon)
            {
                leftHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
            }
            else if (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType == WeaponModelType.Shield)
            {
                leftHandShieldSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
            }

            // right hand
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);

            // refresh the damage collider calculations (strenght scaling would be effected since the strength bonus was removed)
            rightWeaponManger.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManger.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }

        public void TwoHandRightWeapon()
        {
            // check for untwohandable item (like unarmed) if attempting to two hand unarmed, return
            if (player.playerInventoryManager.currentRightHandWeapon == WorldItemDatabase.Instance.unarmedWeapon)
            {
                // if returning and not two handing the weapon, reset bool status's
                if (player.IsOwner)
                {
                    player.playerNetworkManager.isTwoHandingRightWeapon.Value = false;
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                }

                return;
            }

            // update animator
            player.playerAnimatorManager.UpdatedAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);

            // place the non-two handed weapon model in the back slot or hip slot
            BackSlot.PlaceWeaponModelInUnequippedSlot(leftHandWeaponModel, player.playerInventoryManager.currentLeftHandWeapon.weaponClass, player);

            // add two hand strenght bonus

            // place the two handed weapon model in the main (right hand)
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);

            rightWeaponManger.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManger.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }

        public void TwoHandLeftWeapon()
        {
            // check for untwohandable item (like unarmed) if attempting to two hand unarmed, return
            if (player.playerInventoryManager.currentLeftHandWeapon == WorldItemDatabase.Instance.unarmedWeapon)
            {
                // if returning and not two handing the weapon, reset bool status's
                if (player.IsOwner)
                {
                    player.playerNetworkManager.isTwoHandingLeftWeapon.Value = false;
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                }

                return;
            }

            // update animator
            player.playerAnimatorManager.UpdatedAnimatorController(player.playerInventoryManager.currentLeftHandWeapon.weaponAnimator);

            // place the non-two handed weapon model in the back slot or hip slot
            BackSlot.PlaceWeaponModelInUnequippedSlot(rightHandWeaponModel, player.playerInventoryManager.currentRightHandWeapon.weaponClass, player);

            // add two hand strenght bonus

            // place the two handed weapon model in the main (right hand)
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);

            rightWeaponManger.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManger.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }

        // damage colliders
        public void OpenDamageCollider()
        {
            //open right weapon damage collider
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                rightWeaponManger.meleeDamageCollider.EnableDamageCollider();
                player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.Instance.ChooseRandomSFXFromArray(player.playerInventoryManager.currentRightHandWeapon.whooshes));
            }
            //open left weapon damage collider
            else if (player.playerNetworkManager.isUsingLeftHand.Value)
            {
                leftWeaponManger.meleeDamageCollider.EnableDamageCollider();
                player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.Instance.ChooseRandomSFXFromArray(player.playerInventoryManager.currentLeftHandWeapon.whooshes));
            }

            //play whoosh sfx
        }

        public void CloseDamageCollider()
        {
            //open right weapon damage collider
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                rightWeaponManger.meleeDamageCollider.DisableDamageCollider();
            }
            //open left weapon damage collider
            else if (player.playerNetworkManager.isUsingLeftHand.Value)
            {
                leftWeaponManger.meleeDamageCollider.DisableDamageCollider();
            }
        }
    }
}
