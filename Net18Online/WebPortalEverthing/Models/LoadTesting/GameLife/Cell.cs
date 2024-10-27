namespace WebPortalEverthing.Models.LoadTesting.GameLife
{
    public class Cell
    {
        /* представляет клетку и её состояние. */


        public bool IsAlive { get; private set; }

        public Cell(bool isAlive = false)
        {
            IsAlive = isAlive;
        }

        public void SetAlive(bool state)
        {
            IsAlive = state;
        }

        public void ToggleState()
        {
            IsAlive = !IsAlive;
        }
    }
}
