namespace ExpressionConsole
{
    public class Girl
    {
        public int Age {  get; set; }
        public string? Name { get; set; }
        public bool IsAlive { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} ({Age}) {(IsAlive ? "+" : " - ")}";
        }
    }
}
