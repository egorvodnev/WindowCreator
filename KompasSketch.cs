using Kompas6API5;
using System.Drawing;
using Kompas6Constants3D;
using System.Collections.Generic;
using WindowCreator.Enumerations;

namespace WindowCreator
{
    /// <summary>
    /// Свойства эскиза.
    /// </summary>
    public class KompasSketch
    {
       #region - Конструктор -

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public KompasSketch()
        {
            Initialize();
        }

        #endregion // Конструктор.
        
        #region - Инициализация -

        /// <summary>
        /// Инициализирует переменные.
        /// </summary>
        private void Initialize()
        {
            SketchName = string.Empty;
            PointsList = new List<PointF>();
            
        }

        #endregion // Инициализация.
        
        #region - Свойства -

        /// <summary>
        /// Название эскиза.
        /// </summary>
        public string SketchName { get; set; }

        /// <summary>
        /// Величина угла
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Величина операции
        /// </summary>
        public double NormalValue { get; set; }

        /// <summary>
        /// Значение свойства.
        /// </summary>
        public double ReverseValue { get; set; }

        /// <summary>
        /// Примитив.
        /// </summary>
        public ShapeType Shape { get; set; }

        /// <summary>
        /// Плоскость для рисования.
        /// </summary>
        public PlaneType Plane { get; set; }

        /// <summary>
        /// Радиус окружности.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// True - если необходимо сместить плоскость.
        /// </summary>
        public bool IsOffsetPlane { get; set; }

        /// <summary>
        /// Величина смещения плоскости.
        /// </summary>
        public double OffsetPlaneValue { get; set; }

        /// <summary>
        /// Направление спещения плоскости.
        /// </summary>
        public bool PlaneDirectionUp { get; set; }

        /// <summary>
        /// Цвет операции.
        /// </summary>
        public Color OperationColor { get; set; }

        /// <summary>
        /// Направление каманды.
        /// </summary>
        public Direction_Type DirectionType { get; set; }

        /// <summary>
        /// Список координат фигуры.
        /// </summary>
        public List<PointF> PointsList { get; set; }

        /// <summary>
        /// Список операций.
        /// </summary>
        public OperationType Operation { get; set; }

        #endregion // Свойства.

        #region - Public методы -
        /// <summary>
        /// Создает новый эскиз.
        /// </summary>
        /// <param name="part">Новая деталь.</param>
        public void CreateNewSketch(ksPart part)
        {
            var entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);

            if (entitySketch == null) return;

            // Интерфейс свойств эскиза.
            var sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
            if (sketchDef != null)
            {
                // Получим интерфейс базовой плоскости.
                var basePlane = IsOffsetPlane
                                    ? CreateOffsetPlane(part, ActivePlane, OffsetPlaneValue, PlaneDirectionUp)
                                    : (ksEntity)part.GetDefaultEntity(ActivePlane);

                // Установим плоскость базовой для эскиза.
                sketchDef.SetPlane(basePlane);

                // 
                if (!string.IsNullOrEmpty(SketchName))
                    entitySketch.name = SketchName;

                // Создадим эскиз.
                entitySketch.Create();

                // Интерфейс редактора эскиза.
                var sketchEdit = (ksDocument2D)sketchDef.BeginEdit();

                switch (Shape)
                {
                    case ShapeType.Line:
                        {
                            DrawLine(sketchEdit);
                        }
                        break;

                    case ShapeType.Circle:
                        {
                            DrawCircle(sketchEdit);
                        }
                        break;
                }

                // Завершение редактирования эскиза.
                sketchDef.EndEdit();

                switch (Operation)
                {
                    case OperationType.BaseExtrusion:
                        {
                            BaseExtrusion(part, entitySketch);
                        }
                        break;

                    case OperationType.BaseCutExtrusion:
                        {
                            BaseCutExtrusion(part, entitySketch);
                        }
                        break;
                }
            }
        }

        private void BaseCutExtrusion(ksPart part, ksEntity entitySketch)
        {
            // Построим выдавливанием.
            var entityExtr = (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            if (entityExtr != null)
            {
                // Интерфейс свойств базовой операции выдавливания.
                var extrusionDef = (ksCutExtrusionDefinition)entityExtr.GetDefinition();

                // Интерфейс базовой операции выдавливания.
                if (extrusionDef != null)
                {
                    // Направление выдавливания.
                    extrusionDef.directionType = (short)DirectionType;

                    // 1 - прямое направление,
                    // 2 - строго на глубину,
                    // 3 - расстояние.
                    switch (DirectionType)
                    {
                        case Direction_Type.dtNormal:
                            {
                                extrusionDef.SetSideParam(true, (short)End_Type.etBlind, NormalValue);
                            }
                            break;

                        case Direction_Type.dtReverse:
                            {
                                extrusionDef.SetSideParam(false, (short)End_Type.etBlind, ReverseValue);
                            }
                            break;

                        case Direction_Type.dtBoth:
                            {
                                extrusionDef.SetSideParam(true, (short)End_Type.etBlind, NormalValue);
                                extrusionDef.SetSideParam(false, (short)End_Type.etBlind, ReverseValue);
                            }
                            break;
                    }


                    var colorParam = (ksColorParam)entityExtr.ColorParam();

                    // Задаем цвет операции.
                    colorParam.color = GetKompasColor(OperationColor);

                    // Эскиз операции выдавливания.
                    extrusionDef.SetSketch(entitySketch);

                    // Создать операцию.
                    entityExtr.Create();

                    // Обновить параметры эскиза.
                    entitySketch.Update();

                    // Обновить параметры операции выдавливания.
                    entityExtr.Update();
                }
            }
        }

        #endregion // Public методы.

        #region - Операции 2D -

        /// <summary>
        /// Рисует линию.
        /// </summary>
        /// <param name="sketchEdit">Эскиз для рисования.</param>
        private void DrawLine(ksDocument2D sketchEdit)
        {
            if (PointsList.Count == 0) return;

            var pointList = new List<PointF>();

            for (int i = 0; i < PointsList.Count; i++)
            {   

                pointList.Add(PointsList[i]);
            }

            DrawLine(sketchEdit, pointList);
        }

        /// <summary>
        /// Рисует линию.
        /// </summary>
        /// <param name="sketchEdit">Эскиз для рисования.</param>
        /// <param name="pointsList">Координаты линии.</param>
        private void DrawLine(ksDocument2D sketchEdit, List<PointF> pointsList)
        {
            for (int i = 0; i < pointsList.Count - 1; i++)
            {
                sketchEdit.ksLineSeg(pointsList[i].X, pointsList[i].Y, pointsList[i + 1].X, pointsList[i + 1].Y, 1);
            }

            int index = pointsList.Count - 1;
            sketchEdit.ksLineSeg(pointsList[index].X, pointsList[index].Y, pointsList[0].X, pointsList[0].Y, 1);
        }

        /// <summary>
        /// Рисует окружность.
        /// </summary>
        /// <param name="sketchEdit">Эскиз для рисования.</param>
        public void DrawCircle(ksDocument2D sketchEdit)
        {
            foreach (PointF center in PointsList)
            {
                sketchEdit.ksCircle(center.X, center.Y, Radius, 1);
            }
        }

        #endregion // Операции 2D.

        #region - Операции 3D -

        /// <summary>
        /// Базовая операция выдавливания.
        /// </summary>
        /// <param name="part">Интерфейс детали.</param>
        /// <param name="entitySketch">Эскиз.</param>
        private void BaseExtrusion(ksPart part, ksEntity entitySketch)
        {
            // Построим выдавливанием.
            var entityExtr = (ksEntity)part.NewEntity((short)Obj3dType.o3d_baseExtrusion);
            if (entityExtr != null)
            {
                // Интерфейс свойств базовой операции выдавливания.
                var extrusionDef = (ksBaseExtrusionDefinition)entityExtr.GetDefinition();

                // Интерфейс базовой операции выдавливания.
                if (extrusionDef != null)
                {
                    // Направление выдавливания.
                    extrusionDef.directionType = (short)DirectionType;

                    // 1 - прямое направление,
                    // 2 - строго на глубину,
                    // 3 - расстояние.
                    switch (DirectionType)
                    {
                        case Direction_Type.dtNormal:
                            {
                                extrusionDef.SetSideParam(true, (short)End_Type.etBlind, NormalValue);
                            }
                            break;

                        case Direction_Type.dtReverse:
                            {
                                extrusionDef.SetSideParam(false, (short)End_Type.etBlind, ReverseValue);
                            }
                            break;

                        case Direction_Type.dtBoth:
                            {
                                extrusionDef.SetSideParam(true, (short)End_Type.etBlind, NormalValue);
                                extrusionDef.SetSideParam(false, (short)End_Type.etBlind, ReverseValue);
                            }
                            break;
                    }


                    var colorParam = (ksColorParam)entityExtr.ColorParam();

                    // Задаем цвет операции.
                    colorParam.color = GetKompasColor(OperationColor);

                    // Эскиз операции выдавливания.
                    extrusionDef.SetSketch(entitySketch);

                    // Создать операцию.
                    entityExtr.Create();

                    // Обновить параметры эскиза.
                    entitySketch.Update();

                    // Обновить параметры операции выдавливания.
                    entityExtr.Update();
                }
            }
        }

        #endregion // Операции 3D.

        #region - Private методы -

        /// <summary>
        /// Преобразует цвет модели в понятный для Компаса.
        /// </summary>
        /// <param name="color">Цвет модели.</param>
        /// <returns>Значение цвета.</returns>
        private int GetKompasColor(Color color)
        {
            return Color.FromArgb(color.B, color.G, color.R).ToArgb();
        }

        /// <summary>
        /// Возвращает текущую плоскость для рисования.
        /// </summary>
        private short ActivePlane
        {
            get
            {
                var plane = (short)Obj3dType.o3d_planeXOY;

                switch (Plane)
                {
                    case PlaneType.PlaneXOY:
                        plane = (short)Obj3dType.o3d_planeXOY;
                        break;

                    case PlaneType.PlaneXOZ:
                        plane = (short)Obj3dType.o3d_planeXOZ;
                        break;

                    case PlaneType.PlaneYOZ:
                        plane = (short)Obj3dType.o3d_planeYOZ;
                        break;
                }

                return plane;
            }
        }

        /// <summary>
        /// Создает смещенную плоскость.
        /// </summary>
        /// <param name="part">Новая деталь.</param>
        /// <param name="plane">Смещаемая плоскость.</param>
        /// <param name="offset">Величина смещения.</param>
        /// <param name="directionUp">Направление смещения.</param>
        /// <returns>Смещенная плоскость.</returns>
        private static ksEntity CreateOffsetPlane(ksPart part, short plane, double offset, bool directionUp)
        {
            var offsetPlane = (ksEntity)part.NewEntity((short)Obj3dType.o3d_planeOffset);

            // Интерфейс смещенной плоскости.
            var offsetDefinition = (ksPlaneOffsetDefinition)offsetPlane.GetDefinition();
            
            offsetDefinition.SetPlane(part.GetDefaultEntity(plane));
            offsetDefinition.offset = offset;
            offsetDefinition.direction = directionUp;

            offsetPlane.name = "Смещенная плоскость";
            offsetPlane.Create();

            return offsetPlane;
        }

        #endregion // Private методы.
    }
}