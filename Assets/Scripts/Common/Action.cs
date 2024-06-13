namespace Common
{
    public static class Action
    {
        public const int MoveLeft = 0;
        public const int MoveRight = 1;
        public const int MoveUp = 2;
        public const int MoveDown = 3;

        public static int FlipAction(int action)
        {
            return action switch
            {
                MoveLeft => MoveRight,
                MoveRight => MoveLeft,
                MoveUp => MoveDown,
                MoveDown => MoveUp,
                _ => throw new System.Exception("Invalid action to flip. Expected an Move action.")
            };
        }
    }
}