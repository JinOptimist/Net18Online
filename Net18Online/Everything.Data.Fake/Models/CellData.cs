using Everything.Data.Fake.Models;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Models;

namespace WebPortalEverthing.Models.LoadTesting
{
    public class CellData : BaseModel, ICellData
    {
        /* представляет клетку и её состояние. */


        public bool IsAlive { get; set; }

        public CellData(bool isAlive = false)
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
