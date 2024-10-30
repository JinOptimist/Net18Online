namespace WebPortalEverthing.Models.LoadTesting.GameLife
{

    /* представляет клетку и её состояние. бизнес модель с данными находится в C:\Users\svetlana.terekhova\source\repos\Net18Online\Net18Online\LifeGame\Model\Cell.cs 
     это модель представления без методов, одни поля и пустой конструктор. При необходимости, перекладывать из View модели в дата модель (бизнес) */
    public class CellViewModel
    {
        /* представляет клетку и её состояние. */
        public bool IsAlive { get; set; }
    }
}
