namespace Common
{
    public static class ActionEvent
    {
        public const int MoveLeft = 0;
        public const int MoveRight = 1;
        public const int MoveUp = 2;
        public const int MoveDown = 3;
        public const int MoveUpLeft = 4;
        public const int MoveUpRight = 5;
        public const int MoveDownLeft = 6;
        public const int MoveDownRight = 7;
        public const int CastSkill1 = 8;
        public const int CastSkill2 = 9;
        public const int CastSkill3 = 10;
        public const int CastSkill4 = 11;

        public static int FlipAction(int action)
        {
            return action switch
            {
                MoveLeft => MoveRight,
                MoveRight => MoveLeft,
                MoveUp => MoveDown,
                MoveDown => MoveUp,
                MoveUpLeft => MoveDownRight,
                MoveUpRight => MoveDownLeft,
                MoveDownLeft => MoveUpRight,
                MoveDownRight => MoveUpLeft,
                _ => throw new System.Exception("Invalid action to flip. Expected an Move action.")
            };
        }
    }
}