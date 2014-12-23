using System.Runtime.InteropServices;
using Kompas6API5;
using System.Drawing;
using Kompas6Constants3D;
using WindowCreator.Interfaces;
using System.Collections.Generic;
using WindowCreator.Enumerations;

namespace WindowCreator.ModelParts
{
    /// <summary>
    /// Вешалка.
    /// </summary>
    public class Leaf : IModelPart
    {
        /// <summary>
        /// Строит часть модели.
        /// </summary>
        /// <param name="document3D">3D документ.</param>
        /// <param name="parameters">Параметры модели.</param>
        public void Create(ksDocument3D document3D, Dictionary<Parameter, ParameterData> parameters)
        {
            var OpenDirection = (int)parameters[Parameter.OpenDirection].Value;

            var bodyWidth = parameters[Parameter.BodyWidth].Value;          // Ширина окна
            var bodyHeight = parameters[Parameter.BodyHeight].Value;        // Высота окна
            var bodyLength = parameters[Parameter.BodyLength].Value;        // Глубина окна
            var thickness = parameters[Parameter.WallThickness].Value;      // Толщина стенки
            var additional = 0.5f;                                          // Толщина рамки стопора

            var isHanger = (int)parameters[Parameter.IsHanger].Value;

            var part = (ksPart)document3D.GetPart((short)Part_Type.pNew_Part);

            if (part != null)
            {
                var sketchProperty = new KompasSketch
                {
                    Shape = ShapeType.Line,
                    Plane = PlaneType.PlaneXOY,
                    NormalValue = bodyLength,
                    Operation = OperationType.BaseExtrusion,
                    DirectionType = Direction_Type.dtNormal,
                    OperationColor = Color.Cornsilk
                };

         

                if (isHanger != 1)
                {
                    #region - Правая открывающаяся створка-

                    sketchProperty.IsOffsetPlane = true;
                    sketchProperty.OffsetPlaneValue = additional;
                    sketchProperty.NormalValue = bodyLength - 0.4f;
                    sketchProperty.OperationColor = Color.Cornsilk;
                    sketchProperty.PlaneDirectionUp = true;

                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness, bodyHeight - thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness, 0 + thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness - thickness, 0 + thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness - thickness,
                        bodyHeight - thickness - thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness,
                        bodyHeight - thickness - thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness,
                        thickness + thickness));
                    sketchProperty.SketchName = "Створка";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();

                    sketchProperty.PointsList.Add(new PointF(30 - thickness, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness,
                        thickness + thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness, thickness + thickness));
                    sketchProperty.SketchName = "Створка";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();


                    #endregion // Правая открывающаяся створка

                    #region - Нахлёст на створку-

                    sketchProperty.IsOffsetPlane = true;
                    sketchProperty.OffsetPlaneValue = bodyLength + 0.1f;
                    sketchProperty.NormalValue = 0.15f;
                    sketchProperty.OperationColor = Color.Cornsilk;
                    sketchProperty.PlaneDirectionUp = true;

                    sketchProperty.PointsList.Add(
                        new PointF((30 - bodyWidth) / 2 + 0.5f * 2 * thickness + thickness + additional, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness - 0.4f, thickness));

                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness - 0.4f,
                        bodyHeight - thickness + 0.4f));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness + 0.4f, bodyHeight - thickness + 0.4f));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness + 0.4f, 0 + thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness - thickness, 0 + thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness - thickness,
                        bodyHeight - thickness - thickness));

                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness,
                        bodyHeight - thickness - thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness,
                        thickness + thickness));
                    sketchProperty.SketchName = "Створка";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();

                    sketchProperty.PointsList.Add(new PointF(30 - thickness + 0.4f, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness - 0.4f, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness - 0.4f,
                        thickness + thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness + 0.4f, thickness + thickness));
                    sketchProperty.SketchName = "Створка";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();

                    #endregion // Нахлёст на створку

                    #region - Внешняя рамка для створки-

                    sketchProperty.PointsList.Clear();
                    sketchProperty.IsOffsetPlane = true;
                    sketchProperty.OffsetPlaneValue = additional;
                    sketchProperty.NormalValue = additional;
                    sketchProperty.OperationColor = Color.DarkRed;
                    sketchProperty.PlaneDirectionUp = true;

                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness,
                        2 * thickness + additional));
                    sketchProperty.PointsList.Add(
                        new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness + additional,
                            2 * thickness + additional));
                    sketchProperty.PointsList.Add(
                        new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness + additional,
                            bodyHeight - 2 * thickness - additional));
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness - additional,
                        bodyHeight - 2 * thickness - additional));
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness - additional, 2 * thickness + additional));
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness, 2 * thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness, bodyHeight - 2 * thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness,
                        bodyHeight - 2 * thickness));
                    sketchProperty.SketchName = "Рамка внешняя 1";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();

                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness, 2 * thickness)); // Первая точка для стекла
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness, 2 * thickness + additional));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * 2 * thickness,
                        2 * thickness + additional));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * 2 * thickness, 2 * thickness));
                    sketchProperty.SketchName = "Рамка внешняя 2";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();

                    #endregion // Внешняя рамка для створки

                    #region - Внешняя рамка для створки-

                    sketchProperty.PointsList.Clear();
                    sketchProperty.IsOffsetPlane = true;
                    sketchProperty.OffsetPlaneValue = additional + bodyLength - 2 * additional + 0.25f;
                    sketchProperty.NormalValue = additional;
                    sketchProperty.OperationColor = Color.DarkRed;
                    sketchProperty.PlaneDirectionUp = true;

                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness,
                        2 * thickness + additional));
                    sketchProperty.PointsList.Add(
                        new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness + additional,
                            2 * thickness + additional));
                    sketchProperty.PointsList.Add(
                        new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness + additional,
                            bodyHeight - 2 * thickness - additional));
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness - additional,
                        bodyHeight - 2 * thickness - additional));
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness - additional, 2 * thickness + additional));
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness, 2 * thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness, bodyHeight - 2 * thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness,
                        bodyHeight - 2 * thickness));
                    sketchProperty.SketchName = "Рамка внешняя 1";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();

                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness, 2 * thickness)); // Первая точка для стекла
                    sketchProperty.PointsList.Add(new PointF(30 - 2 * thickness, 2 * thickness + additional));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * 2 * thickness,
                        2 * thickness + additional));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * 2 * thickness, 2 * thickness));
                    sketchProperty.SketchName = "Рамка внешняя 2";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();

                    #endregion // Внешняя рамка для створки

                    if (OpenDirection == 0)
                    {

                        #region - Построение ручки и навесов при правом открывании-

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = (2 * additional + bodyLength - 2 * additional + 0.25f);
                        sketchProperty.NormalValue = 0.25f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 + 0.4f,
                                bodyHeight / 2 + 0.7f));
                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 + 0.4f,
                                bodyHeight / 2 - 0.7f));
                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 - 0.4f,
                                bodyHeight / 2 - 0.7f));
                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 - 0.4f,
                                bodyHeight / 2 + 0.7f));
                        sketchProperty.SketchName = "Ручка";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = (2 * additional + bodyLength - 2 * additional + 0.5f);
                        sketchProperty.NormalValue = 0.5f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 + 0.3f,
                                bodyHeight / 2 + 0.3f));
                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 + 0.3f,
                                bodyHeight / 2 - 0.3f));
                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 - 0.3f,
                                bodyHeight / 2 - 0.3f));
                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 - 0.3f,
                                bodyHeight / 2 + 0.3f));
                        sketchProperty.SketchName = "Ручка";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = (2 * additional + bodyLength - 2 * additional + 1);
                        sketchProperty.NormalValue = 0.3f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 + 0.3f,
                                bodyHeight / 2 + 0.3f));
                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 + 0.3f,
                                bodyHeight / 2 - 3));
                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 - 0.3f,
                                bodyHeight / 2 - 3));
                        sketchProperty.PointsList.Add(
                            new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + thickness / 2 - 0.3f,
                                bodyHeight / 2 + 0.3f));

                        sketchProperty.SketchName = "Ручка";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();


                        // Навесы

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = 2 * additional + bodyLength - 2 * additional + 0.1f;
                        sketchProperty.NormalValue = 0.5f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(new PointF(30 - thickness / 2 - 0.5f + 0.3f,
                            bodyHeight - bodyHeight / 4 + 0.9f));
                        sketchProperty.PointsList.Add(new PointF(30 - thickness / 2 - 0.5f + 0.3f,
                            bodyHeight - bodyHeight / 4 - 0.9f));
                        sketchProperty.PointsList.Add(new PointF(30 - thickness / 2 - 0.5f - 0.3f,
                            bodyHeight - bodyHeight / 4 - 0.9f));
                        sketchProperty.PointsList.Add(new PointF(30 - thickness / 2 - 0.5f - 0.3f,
                            bodyHeight - bodyHeight / 4 + 0.9f));
                        sketchProperty.SketchName = "Навес 1";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = 2 * additional + bodyLength - 2 * additional + 0.1f;
                        sketchProperty.NormalValue = 0.5f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(new PointF(30 - thickness / 2 - 0.5f + 0.3f, 0 + bodyHeight / 4 + 0.9f));
                        sketchProperty.PointsList.Add(new PointF(30 - thickness / 2 - 0.5f + 0.3f, 0 + bodyHeight / 4 - 0.9f));
                        sketchProperty.PointsList.Add(new PointF(30 - thickness / 2 - 0.5f - 0.3f, 0 + bodyHeight / 4 - 0.9f));
                        sketchProperty.PointsList.Add(new PointF(30 - thickness / 2 - 0.5f - 0.3f, 0 + bodyHeight / 4 + 0.9f));
                        sketchProperty.SketchName = "Навес 2";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();


                        #endregion
                    }

                    else
                    {

                        #region - Построение ручки и навесов при левом открывании-

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = (2 * additional + bodyLength - 2 * additional + 0.25f);
                        sketchProperty.NormalValue = 0.25f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness + 0.4f, bodyHeight / 2 + 0.7f));
                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness + 0.4f, bodyHeight / 2 - 0.7f));
                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness - 0.4f, bodyHeight / 2 - 0.7f));
                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness - 0.4f, bodyHeight / 2 + 0.7f));
                        sketchProperty.SketchName = "Ручка";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = (2 * additional + bodyLength - 2 * additional + 0.5f);
                        sketchProperty.NormalValue = 0.5f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness + 0.3f,
                            bodyHeight / 2 + 0.3f));
                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness + 0.3f,
                            bodyHeight / 2 - 0.3f));
                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness - 0.3f,
                            bodyHeight / 2 - 0.3f));
                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness - 0.3f,
                            bodyHeight / 2 + 0.3f));
                        sketchProperty.SketchName = "Ручка";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = (2 * additional + bodyLength - 2 * additional + 1);
                        sketchProperty.NormalValue = 0.3f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness + 0.3f,
                            bodyHeight / 2 + 0.3f));
                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness + 0.3f,
                            bodyHeight / 2 - 3));
                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness - 0.3f,
                            bodyHeight / 2 - 3));
                        sketchProperty.PointsList.Add(new PointF(30 - 1.5f * thickness - 0.3f,
                            bodyHeight / 2 + 0.3f));

                        sketchProperty.SketchName = "Ручка";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();


                        // Навесы

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = 2 * additional + bodyLength - 2 * additional + 0.1f;
                        sketchProperty.NormalValue = 0.5f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.3f * thickness + 0.3f,
                            bodyHeight - bodyHeight / 4 + 0.9f));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.3f * thickness + 0.3f,
                            bodyHeight - bodyHeight / 4 - 0.9f));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.3f * thickness - 0.3f,
                            bodyHeight - bodyHeight / 4 - 0.9f));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.3f * thickness - 0.3f,
                            bodyHeight - bodyHeight / 4 + 0.9f));
                        sketchProperty.SketchName = "Навес 1";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();

                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = 2 * additional + bodyLength - 2 * additional + 0.1f;
                        sketchProperty.NormalValue = 0.5f;
                        sketchProperty.OperationColor = Color.Cornsilk;

                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.3f * thickness + 0.3f,
                            0 + bodyHeight / 4 + 0.9f));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.3f * thickness + 0.3f,
                            0 + bodyHeight / 4 - 0.9f));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.3f * thickness - 0.3f,
                            0 + bodyHeight / 4 - 0.9f));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.3f * thickness - 0.3f,
                            0 + bodyHeight / 4 + 0.9f));
                        sketchProperty.SketchName = "Навес 2";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();



                        #endregion
                    }
                }


                if (isHanger != 0)
                {
                    #region - Построение внешнего бортика у правой половины -

                    sketchProperty.PointsList.Clear();
                    sketchProperty.IsOffsetPlane = true;
                    sketchProperty.OffsetPlaneValue = additional + (bodyLength - 2 * additional);
                    sketchProperty.NormalValue = additional;
                    sketchProperty.PlaneDirectionUp = true;
                    sketchProperty.OperationColor = Color.DarkRed;

                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + additional, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness, bodyHeight - thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness, 0 + thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness - additional, 0 + thickness));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness - additional, bodyHeight - thickness - additional));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + additional, bodyHeight - thickness - additional));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + additional, thickness + additional));
                    sketchProperty.SketchName = "Рамка внешняя 9";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();


                    sketchProperty.PointsList.Add(new PointF(30 - thickness - additional, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness - additional, thickness));
                    sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness - additional, thickness + additional));
                    sketchProperty.PointsList.Add(new PointF(30 - thickness - additional, thickness + additional));
                    sketchProperty.SketchName = "Рамка внешняя 10";
                    sketchProperty.CreateNewSketch(part);
                    sketchProperty.PointsList.Clear();

                    #endregion // Построение внешнего бортика у левой половины
                }


            }
        }
    }
}