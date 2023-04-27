using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Knife Throwing", menuName = "Game Data/Player/Skill/KnifeThrowing", order = 1)]
    public sealed class KnifeThrowingSkill : SkillData
    {
        [field: SerializeField] public GameObject Knife { get; private set; }
    }
}