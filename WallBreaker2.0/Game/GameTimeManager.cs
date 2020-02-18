using System;
using System.Windows.Threading;

namespace WallBreaker2.GameData
{
    public static class GameTimeManager
    {
        public static DispatcherTimer timer;
        private static DispatcherTimer gameloopTimer;
        private static DispatcherTimer slowMotionCooldownTimer;
        private static DispatcherTimer slowMotionTimer;
        private static DispatcherTimer gameStartDelay;
        private static readonly double SlowTimeAmmount = 0.25;
        private static readonly double SlowMotionCooldown = 2;

        internal static void StartGame(Action<object, EventArgs> gameLoop)
        {
            gameloopTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 8)
            };
            gameloopTimer.Tick += new EventHandler(gameLoop);
            gameloopTimer.Start();
        }
        public static void StopGame()
        {
            gameloopTimer.Stop();
            timer.Stop();
        }

        internal static void GameTime(Action<object, EventArgs> dispatcherTimer_Tick)
        {
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Start();
        }

        internal static void SlowMotionCooldownStart(Action<object, EventArgs> slowMotion_tick)
        {
            GameStatusEffect.SlowMotionIsReady = true;
            slowMotionCooldownTimer = new DispatcherTimer();
            slowMotionCooldownTimer.Interval = TimeSpan.FromSeconds(SlowMotionCooldown);
            slowMotionCooldownTimer.Tick += new EventHandler(slowMotion_tick);
            slowMotionCooldownTimer.Start();
        }
        internal static void SlowMotionCoolDownStop()
        {
            slowMotionCooldownTimer.Stop();
            GameStatusEffect.SlowMotionIsReady = true;
        }

        internal static void SlowMotionTimeStart(Action<object, EventArgs> slowMotionTimer_Tick)
        {
            slowMotionTimer = new DispatcherTimer();
            slowMotionTimer.Interval = TimeSpan.FromSeconds(SlowTimeAmmount);
            slowMotionTimer.Tick += new EventHandler(slowMotionTimer_Tick);
            slowMotionTimer.Start();
            Console.WriteLine($"{SlowTimeAmmount} sec");
        }
        internal static void SlowMotionTimeStop()
        {
            slowMotionTimer.Stop();
        }
    }
}
