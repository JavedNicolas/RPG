using RPG.Data;

namespace RPG.UI
{
    using RPG.Battle.UI;

    public class ActionMenuItem : MenuItem<Action>
    {
        /// <summary> set the display elements </summary>
        public void set()
        {
            gameObject.GetComponent<ActionMenuItemSetter>().set(element.cost, element.getLocalisationKey());
        }
    }
}

