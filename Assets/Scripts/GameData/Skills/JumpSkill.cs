using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Jump", menuName = "Game Data/Player/Skill/Jump", order = 1)]
    public sealed class JumpSkill : SkillData
    {
        [field: SerializeField, Min(0.0f)] public float JumpPower { get; private set; }
    }
}