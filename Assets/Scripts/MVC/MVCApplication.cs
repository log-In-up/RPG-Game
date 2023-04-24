using UnityEngine;

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
        #endregion

        #region Properties
        public GameModel Model => _model;
        public GameController Controller => _controller;
        public GameView View => _view;
        public PerksModel PerksModel => _perksModel;
        public PerksController PerksController => _perksController;
        public PerksView PerksView => _perksView;
        #endregion

        #region MonoBehaviour API
        private void Awake()
        {
            _model = new GameModel();
            _view = new GameView();
            _controller = new GameController();

            _perksModel = new PerksModel();
            _perksController = new PerksController(_perksModel);
            _perksView = new PerksView(_perksController, _perksModel);
        }
        #endregion
    }
}