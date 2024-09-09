using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{

}

// used for character date saving
public enum CharacterSlot
{
    CharacterSlot_01,
    CharacterSlot_02,
    CharacterSlot_03,
    CharacterSlot_04,
    CharacterSlot_05,
    CharacterSlot_06,
    CharacterSlot_07,
    CharacterSlot_08,
    CharacterSlot_09,
    CharacterSlot_10,
    NO_SLOT
}

// used for to process damage, and character targeting
public enum CharacterGroup
{
    Team01,
    Team02,
}

// used as a tag for each weapon model instantiation slot
public enum WeaponModelSlot
{
    RightHand,
    LeftHandWeaponSlot,
    LeftHandShieldSlot,
    BackSlot,
    //Right Hips
    //Left Hips
}

// used to know where to instantiate the weapon model based on model type
public enum WeaponModelType
{
    Weapon,
    Shield
}

// used for any information specific to a weapon class, such as being able to riposte ect
public enum WeaponClass
{
    StraightSword,
    Spear,
    MediumShield,
    Fist
}

//  used to tag equipment model with specific bady parts that they will cover
public enum EquipmentModelType
{
    FullHelmet,
    Hat,
    Hood,
    HemletAcessories,
    FaceCover,
    Torso,
    Back,
    RightShoulder,
    RightUpperArm,
    RightElbow,
    RightLowerArm,
    RightHand,
    LeftShoulder,
    LeftUpperArm,
    LeftElbow,
    LeftLowerArm,
    LeftHand,
    Hips,
    HipsAttachment,
    RightLeg,
    RightKnee,
    LeftLeg,
    LeftKnee
}

// sued to determine with equipment slot is current selected
public enum EquipmentType
{
    RightWeapon01, // 0
    RightWeapon02, // 1
    RightWeapon03, // 2
    LeftWeapon01,  // 3
    LeftWeapon02,  // 4
    LeftWeapon03,  // 5
    Head,          // 6
    Body,          // 7
    Legs,          // 8
    Hands,         // 9
}

// used to tag helmet type, so specific head portions can be hidden during equip process (hair, beard,ect)
public enum HeadEquipmentType
{
    FullHelmet,    // hide entier head + features
    Hat,           // does not hide anything
    Hood,          // hides hair
    FaceCover      // hides deard
}

// this is used to calculate damage bades on attack type 
public enum AttackType
{
    LightAttack01,
    LightAttack02,
    HeavyAttack01,
    HeavyAttack02,
    ChargedAttack01,
    ChargedAttack02,
    RunningAttack01,
    RollingAttack01,
    BackstepAttack01,
}

// used to calculate damage animation intensity
public enum DamageIntensity
{
    Ping,
    Light,
    Medium,
    Heavy,
    Colossal
}

// used to determine item pickup type
public enum ItemPickUpType
{
    WorldSpawn,
    CharacterDrop,
}