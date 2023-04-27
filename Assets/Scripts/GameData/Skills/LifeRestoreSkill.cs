using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Life Restore", menuName = "Game Data/Player/Skill/LifeRestore", order = 1)]
    public sealed class LifeRestoreSkill : SkillData
    {
        [field: SerializeField, Min(0.0f)] public float LifeRestore { get; private set; }
    }
}