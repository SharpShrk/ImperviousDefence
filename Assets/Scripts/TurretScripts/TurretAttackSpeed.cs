namespace TurretScripts
{
    public class TurretAttackSpeed : TurretStats
    {
        public override void Upgrade()
        {
            Value = 1 - (Level * UpdateValue);
            Level++;

            Turret.SetAttackSpeed(Value);
        }
    }
}