using Kompas6API5;
using System.Drawing;
using Kompas6Constants3D;
using WindowCreator.Interfaces;
using System.Collections.Generic;
using WindowCreator.Enumerations;

namespace WindowCreator.ModelParts
{
    /// <summary>
    /// Стёкла
    /// </summary>
    public class Glazing : IModelPart
    {
        /// <summary>
        /// Строит часть модели.
        /// </summary>
        /// <param name="document3D">3D документ.</param>
        /// <param name="parameters">Параметры модели.</param>
        public void Create(ksDocument3D document3D, Dictionary<Parameter, ParameterData> parameters)
        {
            var isShelfs = (int)parameters[Parameter.IsShelf].Value;
            var isHanger = (int) parameters[Parameter.IsHanger].Value;
          


             var GlassCount = parameters[Parameter.GlassCount].Value;          // Ширина окна
            var bodyWidth = parameters[Parameter.BodyWidth].Value;          // Ширина окна
            var bodyHeight = parameters[Parameter.BodyHeight].Value;        // Высота окна
            var bodyLength = parameters[Parameter.BodyLength].Value;        // Глубина окна
            var thickness = parameters[Parameter.WallThickness].Value;      // Толщина стенки
            var additional = 0.5f;                                          // Толщина рамки стопора


            if (isShelfs != 1)
            {
                var part = (ksPart) document3D.GetPart((short) Part_Type.pNew_Part);

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

                    #region - Построение стекла в правой части -

                    if (isHanger != 1)
                    {

                        if (GlassCount == 1)
                        {
                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = 2*additional;
                            sketchProperty.NormalValue = bodyLength - 3*additional + 0.25f;
                            sketchProperty.PlaneDirectionUp = true;
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                2*thickness));

                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, 2*thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, bodyHeight - 2*thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                bodyHeight - 2*thickness));
                            sketchProperty.SketchName = "Стекло правое";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();
                        }

                        if (GlassCount == 2)
                        {
                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = 2*additional;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                2*thickness));

                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, 2*thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, bodyHeight - 2*thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                bodyHeight - 2*thickness));
                            sketchProperty.SketchName = "Стекло правое";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();

                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = bodyLength - 0.3f;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                2*thickness));

                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, 2*thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, bodyHeight - 2*thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                bodyHeight - 2*thickness));
                            sketchProperty.SketchName = "Стекло правое";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();



                        }

                        if (GlassCount == 3)
                        {
                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = 2*additional;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                2*thickness));

                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, 2*thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, bodyHeight - 2*thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                bodyHeight - 2*thickness));
                            sketchProperty.SketchName = "Стекло правое";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();

                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = bodyLength - 0.3f;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                2*thickness));

                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, 2*thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, bodyHeight - 2*thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                bodyHeight - 2*thickness));
                            sketchProperty.SketchName = "Стекло правое";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();


                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = additional + bodyLength/2 - 0.2f;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                2*thickness));

                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, 2*thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - 2*thickness, bodyHeight - 2*thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness + thickness,
                                bodyHeight - 2*thickness));
                            sketchProperty.SketchName = "Стекло правое";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();
                        }
                    }

                    else
                    {



                        if (GlassCount == 1)
                        {


                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = additional;
                            sketchProperty.NormalValue = bodyLength - 2*additional;
                            sketchProperty.PlaneDirectionUp = true;

                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness, thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness,
                                bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, 0 + thickness));
                            sketchProperty.SketchName = "Стекло";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();
                        }

                        if (GlassCount == 2)
                        {
                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = additional;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;

                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness, thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness,
                                bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, 0 + thickness));
                            sketchProperty.SketchName = "Стекло";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();

                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = bodyLength - additional - 0.15f;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;

                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness, thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness,
                                bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, 0 + thickness));
                            sketchProperty.SketchName = "Стекло";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();
                        }

                        if (GlassCount == 3)
                        {
                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = additional;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;

                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness, thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness,
                                bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, 0 + thickness));
                            sketchProperty.SketchName = "Стекло";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();



                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = bodyLength/2 - 0.075f;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;

                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness, thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness,
                                bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, 0 + thickness));
                            sketchProperty.SketchName = "Стекло";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();

                            sketchProperty.OperationColor = Color.LightSkyBlue;
                            sketchProperty.IsOffsetPlane = true;
                            sketchProperty.OffsetPlaneValue = bodyLength - additional - 0.15f;
                            sketchProperty.NormalValue = 0.15f;
                            sketchProperty.PlaneDirectionUp = true;

                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness, thickness));
                            sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 + 0.5f*thickness,
                                bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                            sketchProperty.PointsList.Add(new PointF(30 - thickness, 0 + thickness));
                            sketchProperty.SketchName = "Стекло";
                            sketchProperty.CreateNewSketch(part);
                            sketchProperty.PointsList.Clear();
                        }

                    }

                    #endregion // Построение стекла в правой части

                        #region - Построение стекла в левой части -

                    if (GlassCount == 1)
                    {


                        sketchProperty.OperationColor = Color.LightSkyBlue;
                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = additional;
                        sketchProperty.NormalValue = bodyLength - 2*additional;
                        sketchProperty.PlaneDirectionUp = true;

                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 - 0.5f*thickness, thickness));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth)/2 - 0.5f*thickness,
                            bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0 + thickness));
                        sketchProperty.SketchName = "Стекло";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();
                    }

                    if (GlassCount == 2)
                    {
                        sketchProperty.OperationColor = Color.LightSkyBlue;
                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = additional;
                        sketchProperty.NormalValue = 0.15f;
                        sketchProperty.PlaneDirectionUp = true;

                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness, thickness));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness,
                            bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0 + thickness));
                        sketchProperty.SketchName = "Стекло";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();

                        sketchProperty.OperationColor = Color.LightSkyBlue;
                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = bodyLength- additional-0.15f;
                        sketchProperty.NormalValue = 0.15f;
                        sketchProperty.PlaneDirectionUp = true;

                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness, thickness));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness,
                            bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0 + thickness));
                        sketchProperty.SketchName = "Стекло";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();
                    }

                    if (GlassCount == 3)
                    {
                        sketchProperty.OperationColor = Color.LightSkyBlue;
                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = additional;
                        sketchProperty.NormalValue = 0.15f;
                        sketchProperty.PlaneDirectionUp = true;

                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness, thickness));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness,
                            bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0 + thickness));
                        sketchProperty.SketchName = "Стекло";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();



                        sketchProperty.OperationColor = Color.LightSkyBlue;
                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = bodyLength/2 - 0.075f;
                        sketchProperty.NormalValue = 0.15f;
                        sketchProperty.PlaneDirectionUp = true;

                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness, thickness));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness,
                            bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0 + thickness));
                        sketchProperty.SketchName = "Стекло";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();

                        sketchProperty.OperationColor = Color.LightSkyBlue;
                        sketchProperty.IsOffsetPlane = true;
                        sketchProperty.OffsetPlaneValue = bodyLength - additional - 0.15f;
                        sketchProperty.NormalValue = 0.15f;
                        sketchProperty.PlaneDirectionUp = true;

                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness, thickness));
                        sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness,
                            bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, bodyHeight - thickness));
                        sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0 + thickness));
                        sketchProperty.SketchName = "Стекло";
                        sketchProperty.CreateNewSketch(part);
                        sketchProperty.PointsList.Clear();
                    }

                    #endregion // Построение стекла в левой части

                   

                }
            }
        }
    }
}