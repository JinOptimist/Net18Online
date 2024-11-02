namespace Everything.Data.Interface.Models
{
    public interface ICellData : IBaseModel
    {
        /* представляет клетку и её состояние. */

        public bool IsAlive { get; set; }

        public void SetAlive(bool state);

        public void ToggleState();
    }
}
