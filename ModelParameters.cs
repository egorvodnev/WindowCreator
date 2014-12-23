using System.Drawing;
using System.Collections.Generic;
using WindowCreator.Enumerations;

namespace WindowCreator
{
    /// <summary>
    /// Содержит параметры модели.
    /// </summary>
    public class ModelParameters
    {
        /// <summary>
        /// Словарь параметров.
        /// </summary>
        public Dictionary<Parameter, ParameterData> Parameters { get; private set; }

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public ModelParameters()
        {
            Initialize();
        }

        /// <summary>
        /// Инициализирует переменные.
        /// </summary>
        private void Initialize()
        {
            Parameters = new Dictionary<Parameter, ParameterData>
                {
                    {Parameter.BodyWidth, new ParameterData(Parameter.BodyWidth.ToString(), 30, new PointF(30, 200))},
                    {Parameter.BodyHeight, new ParameterData(Parameter.BodyHeight.ToString(), 50, new PointF(30, 200))},
                    {Parameter.BodyLength, new ParameterData(Parameter.BodyLength.ToString(), 2, new PointF(2, 4))},
                    {
                        Parameter.WallThickness,
                        new ParameterData(Parameter.WallThickness.ToString(), 2, new PointF(2, 4))
                    },
                    {
                        Parameter.IsNightStand,
                        new ParameterData(Parameter.IsNightStand.ToString(), 0, new PointF(0, 1))
                    },
                    {
                        Parameter.NightStandHeight,
                        new ParameterData(Parameter.NightStandHeight.ToString(), 10, new PointF(8, 15))
                    },
                    {Parameter.IsHanger, new ParameterData(Parameter.IsHanger.ToString(), 0, new PointF(0, 1))},
                    {Parameter.IsShelf, new ParameterData(Parameter.IsShelf.ToString(), 0, new PointF(0, 1))},
                    {Parameter.ShelfCount, new ParameterData(Parameter.ShelfCount.ToString(), 5, new PointF(1, 200))},
                    {
                        Parameter.ShelfHeight, new ParameterData(Parameter.ShelfHeight.ToString(), 0.5f, new PointF(0.1f, 2))
                    },
                     {
                        Parameter.NightStandThic,
                        new ParameterData(Parameter.NightStandThic.ToString(), 0.5f, new PointF(0.5f, 2))
                    },
                    {Parameter.OpenDirection, new ParameterData(Parameter.OpenDirection.ToString(), 0, new PointF(0, 1))},
                    {Parameter.IsCut, new ParameterData(Parameter.IsCut.ToString(), 0, new PointF(0, 1))},
                    {Parameter.GlassCount, new ParameterData(Parameter.GlassCount.ToString(), 1, new PointF(1, 3))},
                };
        }

        /// <summary>
        /// Проверяет корректность введенных данных.
        /// </summary>
        /// <param name="parameters">Словарь параметров для проверки.</param>
        /// <returns>Список ошибок.</returns>
        public List<string> CheckData(Dictionary<Parameter, ParameterData> parameters)
        {
            var errorList = new List<string>();

            foreach (var parameter in parameters)
            {
                switch (parameter.Key)
                {
                    case Parameter.BodyHeight:
                        {
                           // SetMaxValue(Parameter.NightStandHeight, parameter.Value.Value / 3);
                        }
                        break;
                }

                var value = parameter.Value.Value;
                var validValue = GetValidValue(parameter.Key);

                if (validValue == null) continue;

                if (!(value >= validValue.RangeValue.X && value <= validValue.RangeValue.Y))
                {
                    errorList.Add("Значение параметра '" + parameter.Value.Description +
                                  "', должно лежать в диапазоне от " + validValue.RangeValue.X + " до " +
                                  validValue.RangeValue.Y + ".\n");
                }
            }

            return errorList;
        }

        /// <summary>
        /// Возвращает допустимые значения.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        /// <returns>Допустимое значение.</returns>
        private ParameterData GetValidValue(Parameter parameter)
        {
            if (Parameters.ContainsKey(parameter))
            {
                return Parameters[parameter];
            }

            return null;
        }
    }
}