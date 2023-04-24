using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Life Steal", menuName = "Game Data/Player/Skill/LifeSteal", order = 1)]
    public sealed class LifeStealSkill : SkillData
    {
        [field: SerializeField, Range(0.0f, 100.0f)] public float LifeStealFromDamageRatio { get; private set; }
    }
}