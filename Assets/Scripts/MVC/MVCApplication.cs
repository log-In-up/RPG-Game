using GameData;
using UnityEngine;
using Zenject;

namespace MVC
{
    [DisallowMultipleComponent]
    public sealed class MVCApplication : MonoBehaviour
    {
        #region Fields
        private GameModel _model = null;
        private GameView _view = null;
        private GameController _controller = null;

        private PerksModel _perksModel = null;
        private PerksView _perksView = null;
        private PerksController _perksController = null;

        private SkillList _skillList;
        #endregion

        #region Properties
        public PerksModel PerksModel => _perksModel;
        public PerksView PerksView => _perksView;
        #endregion

        #region Zenject
        [Inject]
        private void Constructor(SkillList skillList)
        {
            _skillList = skillList;
        }
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _model = new GameModel();
            _view = new GameView();
            _controller = new GameController();

            _perksModel = new PerksModel(_skillList);
            _perksController = new PerksController(_perksModel);
            _perksView = new PerksView(_perksController, _perksModel);
        }
        #endregion
    }
}