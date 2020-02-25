namespace WallBreaker2.GameData
{
    class GameStatusEffect
    {
        public static bool NitroIsOn { get; set; } = false;
        public static bool SlowMotionIsReady { get; set; } = true;
        public static int NitroSpeed = 10;
        public static readonly int SlowMotionSpeed = 1;

        public GameStatusEffect()
        {

        }
    }
}
