using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "Equipment Model")]
    public class EquipmentModel : ScriptableObject
    {
        public EquipmentModelType equipmentModelType;
        public string maleEquipmentName;
        public string femaleEquipmentName;

        public void LoadModel(PlayerManager player, bool isMale)
        {
            if (isMale)
            {
                loadMaleModel(player);
            }
            else
            {
                loadFemaleModel(player);
            }
        }

        private void loadMaleModel(PlayerManager player)
        {
            switch (equipmentModelType)
            {
                case EquipmentModelType.FullHelmet:

                    foreach (var model in player.playerEquipmentManager.maleHeadFullHelmets)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Hat:
                    foreach (var model in player.playerEquipmentManager.hats)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hood:
                    foreach (var model in player.playerEquipmentManager.hoods)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HemletAcessories:
                    foreach (var model in player.playerEquipmentManager.helmetAccessories)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.FaceCover:
                    foreach (var model in player.playerEquipmentManager.faceCover)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Torso:
                    foreach (var model in player.playerEquipmentManager.maleBodies)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Back:
                    foreach (var model in player.playerEquipmentManager.backAccessories)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightShoulder:
                    foreach (var model in player.playerEquipmentManager.rightShoulder)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightUpperArm:
                    foreach (var model in player.playerEquipmentManager.maleRightUpperArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightElbow:
                    foreach (var model in player.playerEquipmentManager.rightElbow)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLowerArm:
                    foreach (var model in player.playerEquipmentManager.maleRightLowerArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightHand:
                    foreach (var model in player.playerEquipmentManager.maleRightHands)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftShoulder:
                    foreach (var model in player.playerEquipmentManager.leftShoulder)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftUpperArm:
                    foreach (var model in player.playerEquipmentManager.maleLeftUpperArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftElbow:
                    foreach (var model in player.playerEquipmentManager.leftElbow)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLowerArm:
                    foreach (var model in player.playerEquipmentManager.maleLeftLowerArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftHand:
                    foreach (var model in player.playerEquipmentManager.maleLeftHands)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hips:
                    foreach (var model in player.playerEquipmentManager.maleHips)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HipsAttachment:
                    foreach (var model in player.playerEquipmentManager.maleHips)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLeg:
                    foreach (var model in player.playerEquipmentManager.maleRightLeg)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightKnee:
                    foreach (var model in player.playerEquipmentManager.rightKnee)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLeg:
                    foreach (var model in player.playerEquipmentManager.maleLeftLeg)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftKnee:
                    foreach (var model in player.playerEquipmentManager.leftKnee)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
            }
        }

        private void loadFemaleModel(PlayerManager player)
        {
            switch (equipmentModelType)
            {
                case EquipmentModelType.FullHelmet:

                    foreach (var model in player.playerEquipmentManager.femaleHeadFullHelmets)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;

                case EquipmentModelType.Hat:
                    foreach (var model in player.playerEquipmentManager.hats)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hood:
                    foreach (var model in player.playerEquipmentManager.hoods)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HemletAcessories:
                    foreach (var model in player.playerEquipmentManager.helmetAccessories)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.FaceCover:
                    foreach (var model in player.playerEquipmentManager.faceCover)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Torso:
                    foreach (var model in player.playerEquipmentManager.femaleBodies)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Back:
                    foreach (var model in player.playerEquipmentManager.backAccessories)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightShoulder:
                    foreach (var model in player.playerEquipmentManager.rightShoulder)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightUpperArm:
                    foreach (var model in player.playerEquipmentManager.femaleRightUpperArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightElbow:
                    foreach (var model in player.playerEquipmentManager.rightElbow)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLowerArm:
                    foreach (var model in player.playerEquipmentManager.femaleRightLowerArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightHand:
                    foreach (var model in player.playerEquipmentManager.femaleRightHands)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftShoulder:
                    foreach (var model in player.playerEquipmentManager.leftShoulder)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftUpperArm:
                    foreach (var model in player.playerEquipmentManager.femaleLeftUpperArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftElbow:
                    foreach (var model in player.playerEquipmentManager.leftElbow)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLowerArm:
                    foreach (var model in player.playerEquipmentManager.femaleLeftLowerArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftHand:
                    foreach (var model in player.playerEquipmentManager.femaleLeftHands)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hips:
                    foreach (var model in player.playerEquipmentManager.maleHips)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HipsAttachment:
                    foreach (var model in player.playerEquipmentManager.maleHips)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLeg:
                    foreach (var model in player.playerEquipmentManager.femaleRightLeg)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightKnee:
                    foreach (var model in player.playerEquipmentManager.rightKnee)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLeg:
                    foreach (var model in player.playerEquipmentManager.femaleLeftLeg)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftKnee:
                    foreach (var model in player.playerEquipmentManager.leftKnee)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
            }
        }
    }
}
