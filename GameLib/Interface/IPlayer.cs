using GameLib.Model;
using GameLib.State;

namespace GameLib.Interface
{
    public interface IPlayer : IUnit
    {
        public Point Position { get; set; }
        public UnitStatus Status { get; set; }
        public PlayerType PlayerType { get; set; }
        public int Score { get; set; }
    }
}
