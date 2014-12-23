using Kompas6API5;
using System.Drawing;
using Kompas6Constants3D;
using WindowCreator.Interfaces;
using System.Collections.Generic;
using WindowCreator.Enumerations;

namespace WindowCreator.ModelParts
{
    /// <summary>
    /// Антресоль.
    /// </summary>
    public class WindowSill : IModelPart
    {
        /// <summary>
        /// Строит часть модели.
        /// </summary>
        /// <param name="document3D">3D документ.</param>
        /// <param name="parameters">Параметры модели.</param>
        public void Create(ksDocument3D document3D, Dictionary<Parameter, ParameterData> parameters)
        {
           var isNightStand = (int)parameters[Parameter.IsNightStand].Value;
           if (isNightStand == 1) return;
           var isCut = (int)parameters[Parameter.IsCut].Value;
           #region - Построение подоконника -
           var bodyWidth = parameters[Parameter.BodyWidth].Value;          // Ширина окна
           var bodyHeight = parameters[Parameter.BodyHeight].Value;          // Ширина окна
           var bodyLength = parameters[Parameter.BodyLength].Value;        // Глубина окна
           var thickness = parameters[Parameter.NightStandThic].Value;      // Толщина стенки
           var nightStandHeight = parameters[Parameter.NightStandHeight].Value; //Вылет подоконника

            var part = (ksPart)document3D.GetPart((short)Part_Type.pNew_Part);
            if (part != null)
            {
                var sketchProperty = new KompasSketch
                {
                    Shape = ShapeType.Line, // Чем рисовать
                    Plane = PlaneType.PlaneXOY, // Плоскость для рисования
                    NormalValue = nightStandHeight,
                    Operation = OperationType.BaseExtrusion,
                    DirectionType = Direction_Type.dtNormal,
                    OperationColor = Color.Cornsilk
                };

                
                sketchProperty.PointsList.Add(new PointF(-bodyWidth, 0));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth, -thickness));
                sketchProperty.PointsList.Add(new PointF(30, -thickness));
                sketchProperty.PointsList.Add(new PointF(30, 0));
                sketchProperty.SketchName = "Подоконник";
                sketchProperty.CreateNewSketch(part);
                sketchProperty.PointsList.Clear();
                
                sketchProperty.IsOffsetPlane = true;
                sketchProperty.OffsetPlaneValue = nightStandHeight;
                sketchProperty.PlaneDirectionUp = true;
                sketchProperty.NormalValue = thickness * 2;
                sketchProperty.PlaneDirectionUp = true;

                sketchProperty.PointsList.Add(new PointF(-bodyWidth, 0));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth, -thickness*2));
                sketchProperty.PointsList.Add(new PointF(30, -thickness*2));
                sketchProperty.PointsList.Add(new PointF(30, 0));
                sketchProperty.SketchName = "ПодоконникБортик";
                sketchProperty.CreateNewSketch(part);
                sketchProperty.PointsList.Clear();
                #endregion //Построение стекла в левой части -

                if (isCut != 1)
                {
                    #region - Построение сечения -

                    sketchProperty.IsOffsetPlane = true;
                    sketchProperty.OffsetPlaneValue = 5;
                    sketchProperty.NormalValue = 30;
                    sketchProperty.DirectionType = Direction_Type.dtBoth;
                    sketchProperty.Operation = OperationType.BaseCutExtrusion;

                    sketchProperty.PointsList.Add(new PointF(-bodyWidth, bodyHeight));
                    sketchProperty.PointsList.Add(new PointF(-bodyWidth, bodyHeight / 2));
                    sketchProperty.PointsList.Add(new PointF(30, bodyHeight / 2));
                    sketchProperty.PointsList.Add(new PointF(30, bodyHeight));

                    sketchProperty.SketchName = "Секущая";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();

                    #endregion // Построение сечения -
                }

            }
          }
    }
}