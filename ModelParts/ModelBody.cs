using Kompas6API5;
using System.Drawing;
using Kompas6Constants3D;
using WindowCreator.Interfaces;
using System.Collections.Generic;
using WindowCreator.Enumerations;

namespace WindowCreator.ModelParts
{
    /// <summary>
    /// Несущая конструкция (каркас рамы).
    /// </summary>
    public class ModelBody : IModelPart
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

                #region - Построение основоной рамы с одной перегородкой -

                sketchProperty.PointsList.Add(new PointF(-bodyWidth, 0));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth, bodyHeight));
                sketchProperty.PointsList.Add(new PointF(30, bodyHeight));
                sketchProperty.PointsList.Add(new PointF(30, 0));
                sketchProperty.PointsList.Add(new PointF(30 - thickness, 0));
                sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, bodyHeight - thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0));
                sketchProperty.SketchName = "Несущая конструкция 1";
                sketchProperty.CreateNewSketch(part);
                sketchProperty.PointsList.Clear();
               
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f*thickness, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f*thickness, bodyHeight - thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness, bodyHeight - thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness, thickness));
                sketchProperty.PointsList.Add(new PointF(30 - thickness, thickness));
                sketchProperty.PointsList.Add(new PointF(30 - thickness, 0));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0));
                sketchProperty.SketchName = "Несущая конструкция 2";
                sketchProperty.CreateNewSketch(part);

                #endregion // Построение основоной рамы с одной перегородкой

                #region - Построение внешнего бортика у левой половины -

                sketchProperty.PointsList.Clear();
                sketchProperty.OperationColor = Color.DarkRed;
                
                sketchProperty.OffsetPlaneValue = additional;
                sketchProperty.NormalValue = additional;

                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness-additional, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness, bodyHeight - thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, bodyHeight - thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0+thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness+additional, 0 + thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness+additional, bodyHeight - thickness -additional));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness-additional, bodyHeight - thickness-additional));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness-additional, thickness+additional));          
                sketchProperty.SketchName = "Рамка внешняя 1";
                sketchProperty.CreateNewSketch(part);
                sketchProperty.PointsList.Clear();

                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness + additional, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness - additional, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness - additional, thickness+additional));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness + additional, thickness+additional));
                sketchProperty.SketchName = "Рамка внешняя 2";
                sketchProperty.CreateNewSketch(part);
                sketchProperty.PointsList.Clear();

                #endregion // Построение внешнего бортика у левой половины 

                #region - Построение внутренней рамки-стопора для стекла -

                sketchProperty.PointsList.Clear();
                sketchProperty.IsOffsetPlane = true;
                sketchProperty.OffsetPlaneValue = additional + (bodyLength - 2 * additional);
                sketchProperty.NormalValue = additional ;
                sketchProperty.PlaneDirectionUp = true;
                sketchProperty.OperationColor = Color.DarkRed;


             
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness - additional, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness, bodyHeight - thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, bodyHeight - thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness, 0 + thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness + additional, 0 + thickness));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness + additional, bodyHeight - thickness - additional));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness - additional, bodyHeight - thickness - additional));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness - additional, thickness + additional));
                sketchProperty.SketchName = "Рамка внутренняя 1";
                sketchProperty.CreateNewSketch(part);
                sketchProperty.PointsList.Clear();

                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness + additional, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness - additional, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 - 0.5f * thickness - additional, thickness + additional));
                sketchProperty.PointsList.Add(new PointF(-bodyWidth + thickness + additional, thickness + additional));
                sketchProperty.SketchName = "Рамка внутренняя 2";
                sketchProperty.CreateNewSketch(part);
                sketchProperty.PointsList.Clear();

                #endregion  // Построение внутренней рамки-стопора для стекла
                
                #region - Построение внешнего бортика у правой половины -
               
                sketchProperty.PointsList.Clear();
                sketchProperty.IsOffsetPlane = true;
                sketchProperty.OffsetPlaneValue = 0;
                sketchProperty.NormalValue = additional;
                sketchProperty.OperationColor = Color.DarkRed;
                sketchProperty.PlaneDirectionUp = true;
            
                 sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + additional, thickness));
                 sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness, thickness));
                 sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness, bodyHeight - thickness));
                 sketchProperty.PointsList.Add(new PointF(30 - thickness, bodyHeight - thickness));
                 sketchProperty.PointsList.Add(new PointF(30- thickness, 0 + thickness));
                 sketchProperty.PointsList.Add(new PointF(30 - thickness - additional, 0 + thickness));
                 sketchProperty.PointsList.Add(new PointF(30 - thickness - additional, bodyHeight - thickness - additional));
                 sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + additional, bodyHeight - thickness - additional));
                 sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness + additional, thickness + additional));
                 sketchProperty.SketchName = "Рамка внешняя 1";
                 sketchProperty.CreateNewSketch(part);
                 sketchProperty.PointsList.Clear();

                sketchProperty.PointsList.Add(new PointF(30 - thickness - additional, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness - additional, thickness));
                sketchProperty.PointsList.Add(new PointF((30 - bodyWidth) / 2 + 0.5f * thickness - additional, thickness + additional));
                sketchProperty.PointsList.Add(new PointF(30 - thickness - additional, thickness + additional));
                sketchProperty.SketchName = "Рамка внешняя 2";
                sketchProperty.CreateNewSketch(part);
                sketchProperty.PointsList.Clear();
                
                #endregion // Построение внешнего бортика у левой половины 

               
              }
         }
      }
    }
  
