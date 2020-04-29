using RPG.Data;

namespace RPG.DungeonMode.UI
{
    public struct Choices<T> where T : DatabaseElement
    {
        public Reward<T> playerElement { get; private set; }
        public Reward<T> reward { get; private set; }

        public Choices(Reward<T> playerElement, Reward<T> choice)
        {
            this.playerElement = playerElement;
            this.reward = choice;
        }
    }
}