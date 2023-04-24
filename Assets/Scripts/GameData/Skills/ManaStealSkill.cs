using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Mana Steal", menuName = "Game Data/Player/Skill/ManaSteal", order = 1)]
    public sealed class ManaStealSkill : SkillData
    {
        [field: SerializeField, Range(0.0f, 100.0f)] public float ManaStealFromDamageRatio { get; private set; }
    }
}