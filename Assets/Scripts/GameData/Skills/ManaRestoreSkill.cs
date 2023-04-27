using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Mana Restore", menuName = "Game Data/Player/Skill/ManaRestore", order = 1)]
    public sealed class ManaRestoreSkill : SkillData
    {
        [field: SerializeField, Min(0.0f)] public float ManaRestore { get; private set; }
    }
}