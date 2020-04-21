using System;
using System.Collections.Generic;
using System.Text;
using GameLib.Interface;

namespace GameLib.Logic
{
    public abstract class Effect : IEffect
    {
        private Game GetGameInstance => GameSingleton.Instance;
        protected IGame GetGame() => GetGameInstance;
        protected IPlayer GetPlayer() => GetGameInstance.Player;
        protected IWorld GetWorld() => GetGameInstance.World;

        public abstract void Exec();
    }
}
