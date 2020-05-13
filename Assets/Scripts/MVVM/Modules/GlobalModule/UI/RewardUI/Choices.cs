using RPG.DataModule;

namespace RPG.GlobalModule.View
{
    public struct Choices
    {
        public string playerElement { get; private set; }
        public int playerElementIndex { get; private set; }
        public string reward { get; private set; }
        public int rewardIndex { get; private set; }

        public Choices(string playerElement, int playerElementIndex, string reward, int rewardIndex)
        {
            this.playerElement = playerElement;
            this.playerElementIndex = playerElementIndex;
            this.reward = reward;
            this.rewardIndex = rewardIndex;
        }
    }
}