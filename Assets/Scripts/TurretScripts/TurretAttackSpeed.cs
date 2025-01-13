namespace TurretScripts
{
    public class TurretAttackSpeed : TurretStats
    {
        private Turret _turret;

        private void OnEnable()
        {
            _turret = GetComponent<Turret>();
        }

        public override void Upgrade()
        {
            Value = 1 - (Level * UpdateValue);
            Level++;

            _turret.SetAttackSpeed(Value);
        }
    }
}