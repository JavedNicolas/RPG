using RPG.Data;

namespace RPG.UI
{
    using RPG.Battle.UI;

    public class ActionMenuItem : MenuItem<Action>
    {
        public void set()
        {
            gameObject.GetComponent<ActionMenuItemSetter>().set(element.cost, element.getLocalisationKey());
        }
    }
}

