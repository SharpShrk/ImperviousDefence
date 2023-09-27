using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickProductionOrder : MonoBehaviour
{
    [SerializeField] private ButtonControlProdaction _buttonControlProdaction;
    [SerializeField] private GameObject _productionPanel;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private BrickFactory _brickFactory;
    [SerializeField] private InfoMessagePanel _infoPanel;
    [SerializeField] private int _costProductionBricks;
    [SerializeField] private int _valueBricks;

    private bool _isProductionPanelDisable = false;
    private ProductionPanel _productionPanelScript;

    private void OnEnable()
    {
        _buttonControlProdaction.ButtonPressed += OpenProductionPanel;
        _toggle.isOn = false;
        _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        _productionPanelScript = _productionPanel.transform.parent.GetComponent<ProductionPanel>();
        _productionPanelScript.OnButtonConfirmProductionClicked += HandleConfirmProduction;
    }

    private void OnDisable()
    {
        _buttonControlProdaction.ButtonPressed -= OpenProductionPanel;
        _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        _productionPanelScript.OnButtonConfirmProductionClicked -= HandleConfirmProduction;
    }

    private void OpenProductionPanel()
    {
        if (_isProductionPanelDisable == false)
        {
            Time.timeScale = 0;
            _productionPanel.SetActive(true);
        }
        else
        {
            HandleConfirmProduction();
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        _isProductionPanelDisable = isOn;
    }

    private void HandleConfirmProduction()
    {
        if (_wallet.SpendMoney(_costProductionBricks))
        {
            _brickFactory.AddToProductionQueue(_valueBricks);
        }
        else
        {
            _infoPanel.OpenMessagePanel("Недостаточно монет!");
        }
    }
}
