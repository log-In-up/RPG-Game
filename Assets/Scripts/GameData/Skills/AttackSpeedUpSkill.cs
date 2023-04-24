using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Life Steal Effectiveness", menuName = "Game Data/Player/Skill/LifeStealEffectiveness", order = 1)]
    public sealed class AttackSpeedUpSkill : SkillData
    {
        [field: SerializeField, Min(1.0f)] public float AttackSpeedMultiplier { get; private set; }
    }
}