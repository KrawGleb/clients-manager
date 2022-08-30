using System.ComponentModel;

namespace ClientsManager.Domain.Enums;

public enum SearchOptions
{
    [Description("---")]
    None = 0,

    [Description("ФИО")]
    ByFullName,
    
    [Description("Телефон")]
    ByPhone,
    
    [Description("Модель машины")]
    ByCarModel,

    [Description("Номер машины")]
    ByCarNumber
}
