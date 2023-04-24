using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Health Points Regeneration", menuName = "Game Data/Player/Skill/HealthRegeneration", order = 1)]
    public sealed class HealthPointsRegenerationSkill : SkillData
    {
        [field: SerializeField, Min(0.0f)] public float HealthPointsRegeneration { get; private set; }
    }
}