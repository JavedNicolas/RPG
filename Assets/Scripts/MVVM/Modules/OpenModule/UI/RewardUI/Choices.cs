using RPG.DataModule;

namespace RPG.OpenModule.View
{
    public struct Choices
    {
        public string playerElement { get; private set; }
        public string reward { get; private set; }

        public Choices(string playerElement, string choice)
        {
            this.playerElement = playerElement;
            this.reward = choice;
        }
    }
}