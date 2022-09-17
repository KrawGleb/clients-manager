using System.ComponentModel;

namespace ClientsManager.Domain.Enums;

public enum SearchOptions
{
    [Description("---")]
    None = 0,

    [Description("Заказчик")]
    ByCustomer,
    
    [Description("Телефон")]
    ByPhone,
    
    [Description("Модель машины")]
    ByCarModel,

    [Description("Номер машины")]
    ByCarNumber
}
