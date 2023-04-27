using GameData;
using SkillTree;
using System;
using System.Collections.Generic;
using System.Linq;
using UserInterface;
using Utills;

namespace MVC
{
    public sealed class PerksController
    {
        #region Fields
        private readonly PerksModel _perksModel;
        private SkillNode _selectedNode;
        #endregion

        public PerksController(PerksModel perksModel)
        {
            _perksModel = perksModel;
        }

        #region Public methods
        internal void AddOneSkillPoint() => _perksModel.AddOneSkillPoint();

        internal void RestoreAllSkillPoints() => _perksModel.RestoreAllSkillPoints();

        internal void ForgetAllSkills() => _perksModel.ClearSkills();

        internal void SetSelectedNode(SkillNode skillNode) => _selectedNode = skillNode;

        internal void LearnSkill()
        {
            if (_selectedNode == null) return;
            if (_selectedNode.SkillType.Equals(SkillType.Base)) return;

            SkillType skillType = _selectedNode.SkillType;

            if (_perksModel.IsSkillUnlocked(skillType)) return;

            SkillData skillData = _perksModel.SkillList.GetSkill(skillType);

            if (!_perksModel.SkillCanBeAdded(skillData.PreviousRequiredSkills)) return;

            ushort price = skillData.UnlockCost;

            if (!_perksModel.IsSkillUnlocked(skillType) && _perksModel.SkillPointsCanWithdrawn(price))
            {
                _perksModel.UnlockSkill(skillType);
                _perksModel.WithdrawSkillPoints(price);
            }
        }

        internal void ForgetSkill()
        {
            if (_selectedNode == null) return;
            if (_selectedNode.SkillType.Equals(SkillType.Base)) return;

            SkillType skillType = _selectedNode.SkillType;

            if (!_perksModel.IsSkillUnlocked(skillType)) return;

            if (!SkillCanBeForgotten(skillType)) return;

            _perksModel.LockSkill(skillType);

            SkillData skillData = _perksModel.SkillList.GetSkill(skillType);
            ushort price = skillData.UnlockCost;

            _perksModel.ReturnSkillPoints(price);
        }
        #endregion

        #region Methods
        private bool SkillCanBeForgotten(SkillType skillType)
        {
            List<SkillType> tempSkillList = new List<SkillType>(_perksModel.UnlockedSkills);
            tempSkillList.Remove(skillType);

            HashSet<Tuple<int, int>> edges = new HashSet<Tuple<int, int>>();
            HashSet<int> nodes = tempSkillList.Cast<int>().ToHashSet();

            foreach (SkillType skill in tempSkillList)
            {
                SkillData skillData = _perksModel.SkillList.GetSkill(skill);

                foreach (SkillType requiredSkill in skillData.PreviousRequiredSkills)
                {
                    if (tempSkillList.Contains(requiredSkill))
                    {
                        Tuple<int, int> edge = Tuple.Create((int)skill, (int)requiredSkill);

                        edges.Add(edge);
                    }
                }
            }

            HashSet<Tuple<int, int>> tempEdges = new HashSet<Tuple<int, int>>(edges);

            List<SkillType> sortedSkillList = Sorting.TopologicalSort(nodes, edges).Cast<SkillType>().ToList();

            return Result(tempEdges, sortedSkillList);
        }

        private bool Result(HashSet<Tuple<int, int>> edges, List<SkillType> skillList)
        {
            DFSGraph graph = new DFSGraph(_perksModel.SkillList.GraphVertices);

            graph.AddEdges(edges);

            List<bool> result = new List<bool>();
            int source = (int)SkillType.Base;

            foreach (SkillType skill in skillList)
            {
                bool item = graph.IsReachable((int)skill, source);
                result.Add(item);
            }

            return result.All(x => x.Equals(true));
        }
        #endregion
    }
}