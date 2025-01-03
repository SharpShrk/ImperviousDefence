namespace TurretScripts
{
    public class TurretDamage : TurretStats
    {
        private Turret _turret;
        private bool _isMaxLevel;

        public bool IsMaxLevel => _isMaxLevel;

        private void OnEnable()
        {
            _isMaxLevel = false;
            _turret = GetComponent<Turret>();
        }

        public override void Upgrade()
        {
            Value += UpdateValue;
            Level++;

            _turret.SetUpgradeDamage(Value);

            if (Level == MaxLevel)
            {
                _isMaxLevel = true;
            }
        }
    }
}