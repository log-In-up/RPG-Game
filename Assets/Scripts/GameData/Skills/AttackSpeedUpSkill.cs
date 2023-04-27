using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Attack Speed Up", menuName = "Game Data/Player/Skill/AttackSpeedUp", order = 1)]
    public sealed class AttackSpeedUpSkill : SkillData
    {
        [field: SerializeField, Min(1.0f)] public float AttackSpeedMultiplier { get; private set; }
    }
}