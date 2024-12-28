using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.Components
{
    public class TimerProgressBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;

        public void SetActive(in bool isActive) => gameObject.SetActive(isActive);

        public void SetTimerText(in string text) => _text.text = text;

        public void SetTimerPercent(in float percent) => _image.fillAmount = percent;
    }
}