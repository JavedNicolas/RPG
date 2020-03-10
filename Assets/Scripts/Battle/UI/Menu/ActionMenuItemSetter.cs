using UnityEngine;

namespace RPG.Battle.UI
{
    using TMPro;

    public class ActionMenuItemSetter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _apCost;
        [SerializeField] LocalizeText _actionNameLocalization;

        public void set(int apCost, string nameKey)
        {
            _apCost.text = apCost.ToString();
            _actionNameLocalization.key = nameKey;
        }
    }
}