using SkillTree;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    public abstract class SkillData : ScriptableObject
    {
        [field: SerializeField] public SkillType SkillType { get; private set; }
        [field: SerializeField, Range(0, 65535)] public ushort UnlockCost { get; private set; }
        [field: SerializeField] public List<SkillType> PreviousRequiredSkills { get; private set; }
    }
}