using System.ComponentModel;

namespace WindowCreator.Enumerations
{
    /// <summary>
    /// Параметры модели.
    /// </summary>
    public enum Parameter
    {
        [Description("Ширина окна")]
        BodyWidth,

        [Description("Высота окна")]
        BodyHeight,

        [Description("Глубина окна")]
        BodyLength,

        [Description("Толщина рамки")]
        WallThickness,

        [Description("Наличие подоконника")]
        IsNightStand,

        [Description("Вылет подоконника")]
        NightStandHeight,

        [Description("Наличие створки")]
        IsHanger,

        [Description("Наличие стёкол")]
        IsShelf,

        [Description("Количество полок")]
        ShelfCount,

        [Description("Высота полок")]
        ShelfHeight,

        [Description("Толщина подоконника")]
        NightStandThic,

        [Description("Направление открывания")]
        OpenDirection,

        [Description("Поперечное сечение")]
        IsCut,

        [Description("Стёкол в стеклопакете")]
        GlassCount,
    }
}