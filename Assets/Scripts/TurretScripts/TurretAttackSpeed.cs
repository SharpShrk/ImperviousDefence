namespace TurretScripts
{
    public class TurretAttackSpeed : TurretStats
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
            Value = 1 - (Level * UpdateValue);
            Level++;

            _turret.SetAttackSpeed(Value);

            if (Level == MaxLevel)
            {
                _isMaxLevel = true;
            }
        }
    }
}