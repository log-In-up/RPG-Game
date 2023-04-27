using SkillTree;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Skills", menuName = "Game Data/Player/Skills", order = 1)]
    public sealed class SkillList : ScriptableObject
    {
        [field: SerializeField] public List<SkillData> Skills { get; private set; }

        [field: SerializeField, Range(ushort.MinValue, ushort.MaxValue)] public ushort GraphVertices { get; private set; }

        public SkillData GetSkill(SkillType skillType)
        {
            return Skills.Find(skill => skill.SkillType.Equals(skillType));
        }
    }
}