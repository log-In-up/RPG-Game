using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Archery Skill", menuName = "Game Data/Player/Skill/Archery", order = 1)]
    public sealed class KnifeThrowingSkill : SkillData
    {
        [SerializeField, Min(0.0f)] private float _attacksPerMinute = 40.0f;
        [field: SerializeField] public GameObject Knife { get; private set; }
        public float AttackInterval => _attacksPerMinute / 60.0f;
    }
}