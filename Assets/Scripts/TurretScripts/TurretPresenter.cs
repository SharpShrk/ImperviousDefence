using TurretsUI;
using UnityEngine;

namespace TurretScripts
{
    public class TurretPresenter : MonoBehaviour
    {
        [SerializeField] private Turret _turret;
        [SerializeField] private TurretAttackSpeed _turretAttackSpeed;
        [SerializeField] private TurretDamage _turretDamage;
        [SerializeField] private ColorChangeButtonUpgrade _colorChanger;
        [SerializeField] private TurretUpgradeView _upgradeMenu;
        [SerializeField] private UpgradeService _upgradeService;

        private void OnEnable()
        {
            _colorChanger.ButtonFullPressed += OnOpenShop;
        }

        private void OnDisable()
        {
            _colorChanger.ButtonFullPressed -= OnOpenShop;
        }

        public void TurretPlace()
        {
            if (_turret.IsPlaced == true)
                return;

            _turret.Place();
        }

        public int GetLevelAttackSpeed()
        {
            return _turretAttackSpeed.GetLevel();
        }

        public int GetLevelDamage()
        {
            return _turretDamage.GetLevel();
        }

        public void UpgradeAttackSpeed()
        {
            _turretAttackSpeed.Upgrade();
        }

        public void UpgradeDamage()
        {
            _turretDamage.Upgrade();
        }

        private void OnOpenShop()
        {
            if (_turret.IsPlaced)
            {
                _upgradeMenu.OpenUpgradeTurretMenu(this);
                _upgradeService.SetCurrentTurretUpdates(this);
            }
            else
            {
                _upgradeMenu.OpenBuyTurretMenu(this);
            }
        }
    }
}