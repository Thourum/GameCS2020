namespace GameLib.Interface
{
    public interface IUnit : IObject
    {
        public int MaxHealth { get; set; }
        public double CurrentHealth { get; set; }
        public double Speed { get; set; }
    }
}
