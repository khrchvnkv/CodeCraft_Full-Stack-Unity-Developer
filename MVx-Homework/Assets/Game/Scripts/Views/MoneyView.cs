using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;

        public void SetMoneyText(in string text) => _moneyText.text = text;
    }
}