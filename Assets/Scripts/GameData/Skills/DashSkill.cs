using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Dash", menuName = "Game Data/Player/Skill/Dash", order = 1)]
    public sealed class DashSkill : SkillData
    {
        [field: SerializeField, Min(0.0f)] public float DashDistance { get; private set; }
    }
}