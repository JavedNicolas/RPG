using RPG.Data;

namespace RPG.DungeonMode.UI
{
    public struct ChoiceElements<T> where T : DatabaseElement
    {
        public Choice<T> playerElement { get; private set; }
        public Choice<T> choice { get; private set; }

        public ChoiceElements(Choice<T> playerElement, Choice<T> choice)
        {
            this.playerElement = playerElement;
            this.choice = choice;
        }
    }
}