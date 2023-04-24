using SkillTree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [DisallowMultipleComponent]
    public sealed class SkillNode : MonoBehaviour
    {
        #region Editor fields
        [SerializeField] private Button _button = null;
        [SerializeField] private Color _activated, _deactivated;
        [SerializeField] private TextMeshProUGUI _requiredSkillpoints = null;
        [SerializeField] private SkillType _skillType;
        [SerializeField] private Image _image = null;
        #endregion

        #region Properties
        public Button Button => _button;
        public Image Image => _image;
        public SkillType SkillType => _skillType;
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }
        #endregion

        #region Public methods
        internal void UpdateLockStatus(bool value)
        {
            _image.color = value ? _activated : _deactivated;
        }
        #endregion
    }
}