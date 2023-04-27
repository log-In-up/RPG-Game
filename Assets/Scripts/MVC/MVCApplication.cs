using GameData;
using Player;
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
        private PlayerData _playerData;
        private PlayerBehaviour _playerBehaviour;
        #endregion

        #region Properties
        public GameView GameView => _view;
        public GameController Controller => _controller;
        public GameModel GameModel => _model;
        public PerksModel PerksModel => _perksModel;
        public PerksView PerksView => _perksView;
        #endregion

        #region Zenject
        [Inject]
        private void Constructor(SkillList skillList, PlayerData playerData, PlayerBehaviour playerBehaviour)
        {
            _skillList = skillList;
            _playerBehaviour = playerBehaviour;

            _playerData = playerData;
        }
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _model = new GameModel(_playerBehaviour, _playerData);
            _controller = new GameController(_model);
            _view = new GameView(_controller, _model);

            _perksModel = new PerksModel(_skillList);
            _perksController = new PerksController(_perksModel);
            _perksView = new PerksView(_perksController, _perksModel);
        }

        private void Start()
        {
            _playerBehaviour.EnableConnectionWithSkilltree();
        }

        private void OnApplicationQuit()
        {
            _playerBehaviour.DisableConnectionWithSkilltree();
        }
        #endregion
    }
}