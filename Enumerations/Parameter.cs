using System.ComponentModel;

namespace WindowCreator.Enumerations
{
    /// <summary>
    /// ��������� ������.
    /// </summary>
    public enum Parameter
    {
        [Description("������ ����")]
        BodyWidth,

        [Description("������ ����")]
        BodyHeight,

        [Description("������� ����")]
        BodyLength,

        [Description("������� �����")]
        WallThickness,

        [Description("������� �����������")]
        IsNightStand,

        [Description("����� �����������")]
        NightStandHeight,

        [Description("������� �������")]
        IsHanger,

        [Description("������� �����")]
        IsShelf,

        [Description("���������� �����")]
        ShelfCount,

        [Description("������ �����")]
        ShelfHeight,

        [Description("������� �����������")]
        NightStandThic,

        [Description("����������� ����������")]
        OpenDirection,

        [Description("���������� �������")]
        IsCut,

        [Description("����� � ������������")]
        GlassCount,
    }
}