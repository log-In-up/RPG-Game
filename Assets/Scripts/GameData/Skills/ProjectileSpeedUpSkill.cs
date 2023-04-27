using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Projectile Speed Up", menuName = "Game Data/Player/Skill/ProjectileSpeedUp", order = 1)]
    public sealed class ProjectileSpeedUpSkill : SkillData
    {
        [field: SerializeField, Min(1.0f)] public float ProjectileSpeedMultiplier { get; private set; }
    }
}