using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UserInterface
{
    public abstract class BasePauseCanvas : MonoBehaviour
    {
        [SerializeField] protected Canvas[] _guiCanvases;
        [SerializeField] protected GamePauseHandler _gamePauseHandler;

        protected void HideGUICanvas()
        {
            foreach (var canvas in _guiCanvases)
            {
                canvas.enabled = false;
            }
        }

        protected void OpenGUICanvas()
        {
            foreach (var canvas in _guiCanvases)
            {
                canvas.enabled = true;
            }
        }

        protected void PauseGameIfNotPaused()
        {
            if (!_gamePauseHandler.IsPaused)
            {
                _gamePauseHandler.PauseGame();
            }
        }

        protected void ResumeGameIfPaused()
        {
            if (_gamePauseHandler.IsPaused)
            {
                _gamePauseHandler.ResumeGame();
            }
        }
    }
}