namespace Common
{
    public static class ActionEvent
    {
        public const int MOVE_LEFT = 0;
        public const int MOVE_RIGHT = 1;
        public const int MOVE_UP = 2;
        public const int MOVE_DOWN = 3;
        public const int MOVE_UP_LEFT = 4;
        public const int MOVE_UP_RIGHT = 5;
        public const int MOVE_DOWN_LEFT = 6;
        public const int MOVE_DOWN_RIGHT = 7;

        public static int FlipAction(int action)
        {
            return action switch
            {
                MOVE_LEFT => MOVE_RIGHT,
                MOVE_RIGHT => MOVE_LEFT,
                MOVE_UP => MOVE_DOWN,
                MOVE_DOWN => MOVE_UP,
                MOVE_UP_LEFT => MOVE_DOWN_RIGHT,
                MOVE_UP_RIGHT => MOVE_DOWN_LEFT,
                MOVE_DOWN_LEFT => MOVE_UP_RIGHT,
                MOVE_DOWN_RIGHT => MOVE_UP_LEFT,
                _ => throw new System.Exception("Invalid action to flip. Expected an Move action.")
            };
        }
    }
}