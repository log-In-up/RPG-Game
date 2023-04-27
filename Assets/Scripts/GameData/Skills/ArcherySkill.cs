using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Archery", menuName = "Game Data/Player/Skill/Archery", order = 1)]
    public sealed class ArcherySkill : SkillData
    {
        [field: SerializeField] public GameObject Arrow { get; private set; }
    }
}