using UnityEngine;

namespace TurretScripts
{
    public abstract class TurretStats : MonoBehaviour
    {
        [SerializeField] protected float StartValue;
        [SerializeField] protected float UpdateValue;

        protected int Level;
        protected float Value;
        protected int MaxLevel = 10;

        private void OnEnable()
        {
            Value = StartValue;
            Level = 0;
        }

        public abstract void Upgrade();

        public int GetLevel()
        {
            return Level;
        }
    }
}