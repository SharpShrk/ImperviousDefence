using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;
using UserInterface;
using Utilities;
using WalletAndScore;

namespace BrickFactories
{
    public class BrickProductionHandler : MonoBehaviour
    {
        private const string MessageErrorMoney = "Not_enough_money";

        [SerializeField] private ButtonControlProduction _buttonControlProduction;
        [SerializeField] private GameObject _productionPanel;
        [SerializeField] private ProductionPanel _productionPanelScript;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private BrickFactory _brickFactory;
        [SerializeField] private InfoMessagePanel _infoPanel;
        [SerializeField] private int _costProductionBricks;
        [SerializeField] private int _valueBricks;
        [SerializeField] private GamePauseHandler _gamePauseHandler;

        private bool _isProductionPanelDisable = false;

        private void OnEnable()
        {
            _buttonControlProduction.ButtonPressed += OnButtonPressedOpenProductionPanel;
            _toggle.isOn = false;
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
            _productionPanelScript.OnButtonConfirmProductionClick += OnButtonConfirmProductionClicked;
        }

        private void OnDisable()
        {
            _buttonControlProduction.ButtonPressed -= OnButtonPressedOpenProductionPanel;
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
            _productionPanelScript.OnButtonConfirmProductionClick -= OnButtonConfirmProductionClicked;
        }

        private void OnButtonPressedOpenProductionPanel()
        {
            if (_isProductionPanelDisable == false)
            {
                _gamePauseHandler.PauseGame();
                _productionPanel.SetActive(true);
            }
            else
            {
                OnButtonConfirmProductionClicked();
            }
        }

        private void CloseProductionPanel()
        {
            _gamePauseHandler.ResumeGame();
            _productionPanel.SetActive(false);
        }

        private void OnToggleValueChanged(bool isOn)
        {
            _isProductionPanelDisable = isOn;
        }

        private void OnButtonConfirmProductionClicked()
        {
            if (_wallet.SpendMoney(_costProductionBricks))
            {
                _brickFactory.AddToProductionQueue(_valueBricks);
                CloseProductionPanel();
            }
            else
            {
                string message = LeanLocalization.GetTranslationText(MessageErrorMoney);
                _infoPanel.OpenMessagePanel(message);
            }
        }
    }
}