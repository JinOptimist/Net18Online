using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class MetricData : BaseModel, IMetricData
    {
        // Id - это свойство, которое реализует интерфейс IBaseModel и предоставляет доступ к идентификатору модели.
        public Guid Guid { get; set; } = Guid.NewGuid(); // Инициализация Guid при создании новой метрики, запись вместо описания в конструкторе, тоже самое д.б.
        public string Name { get; set; }
        public decimal Throughput { get; set; }
        public decimal Average { get; set; }

        public virtual LoadUserData? LoadUserDataCreator { get; set; }
        public virtual LoadVolumeTestingData? LoadVolumeTesting { get; set; }
    }
}