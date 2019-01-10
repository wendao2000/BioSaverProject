using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Character/Skill")]
public class ScriptSkill : ScriptableObject
{
    public int skillID = -1;
    public string skillName = "Skill";
    public Animation animationClip = null;
    public int manaUsage = 0;
    public float damageMultiplier = 0;
}

[Serializable]
public class SkillList
{
    public ScriptSkill armor;
}