using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Restore Effectiveness", menuName = "Game Data/Player/Skill/RestoreEffectiveness", order = 1)]
    public sealed class RestoreEffectivenessSkill : SkillData
    {
        [field: SerializeField, Min(1.0f)] public float LifeRestoreEffectiveness { get; private set; }
    }
}