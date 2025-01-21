using TMPro;
using UnityEngine;

namespace UIReward
{
    public abstract class RewardUI<T> : MonoBehaviour where T : IReward
    {
        [SerializeField] protected TMP_Text _text;
        [SerializeField] protected T _reward;

        protected virtual void OnEnable()
        {
            _reward.ValueChanged += OnSetValue;
        }

        protected virtual void OnDisable()
        {
            _reward.ValueChanged -= OnSetValue;
        }

        protected void OnSetValue(int value)
        {
            _text.text = value.ToString();
        }
    }
}