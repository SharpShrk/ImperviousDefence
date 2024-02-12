using TMPro;
using UnityEngine;

namespace UserInterface
{
    public abstract class RewardUI : MonoBehaviour
    {
        [SerializeField] protected TMP_Text _text;

        protected abstract void OnEnable();

        protected abstract void OnDisable();

        protected void SetValue(int value)
        {
            _text.text = value.ToString();
        }
    }
}