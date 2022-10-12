using System.ComponentModel;

namespace ClientsManager.Domain.Enums;

public enum OrderType
{
    [Description("Автомойка")]
    CarWash = 0,

    [Description("СТО")]
    CarService = 1
}
