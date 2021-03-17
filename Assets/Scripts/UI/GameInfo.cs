using TMPro;
using UnityEngine;

namespace UI
{
    public class GameInfo : MonoBehaviour
    {
        public TextMeshProUGUI CountTurnText;
        public TextMeshProUGUI TurnFractionText;

        public void SetCountTurnText(string value) =>
            CountTurnText.text = "Ход #" + value;

        public void SetTurnFractionText(string value) =>
            TurnFractionText.text = value;
    }
}
