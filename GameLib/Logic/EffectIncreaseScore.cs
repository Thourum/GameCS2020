using System;
using System.Collections.Generic;
using System.Text;
using GameLib.Interface;

namespace GameLib.Logic
{
    public class EffectIncreaseScore : Effect
    {
        /// <summary>
        /// Increase player score by 100
        /// </summary>
        public override void Exec() => GetPlayer().Score += 100;


    }
}
