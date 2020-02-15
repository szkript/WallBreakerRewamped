using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallBreaker2.GameData
{
    class GameStatusEffect
    {
        public static bool NitroIsOn { get; set; } = false;
        public static readonly int NitroSpeed = 10;
        public static readonly int SlowMotionSpeed = 1;

        public GameStatusEffect() 
        { 
        
        }
    }
}
