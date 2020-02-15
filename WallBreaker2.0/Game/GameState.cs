using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallBreaker2.GameData
{
    public enum GameState
    {
        [Description("SimplePause")]
        SimplePause,
        [Description("GameOver")]
        GameOver,
        [Description("Exit")]
        Exit,
        [Description("Win")]
        Win,
        Restart
    }
}
