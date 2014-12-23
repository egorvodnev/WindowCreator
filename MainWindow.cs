﻿using System;
using System.Linq;
using Kompas6API5;
using System.Windows.Forms;
using System.Globalization;
using WindowCreator.Properties;
using WindowCreator.Converters;
using System.Collections.Generic;
using WindowCreator.Enumerations;
using System.Text.RegularExpressions;

namespace WindowCreator
{
    //TODO:
    /// <summary>
    /// Содержит методы инициализации элементов формы
    /// </summary>
    public partial class MainWindow : Form
    {
        #region - Переменные -

        /// <summary>
        /// Содержит методы для построения модели.
        /// </summary>
        private readonly Manager _manager;

        /// <summary>
        /// Содержит методы для работы с параметрами модели.
        /// </summary>
        private ModelParameters _modelParameters;

        /// <summary>
        /// Список Control'ов.
        /// </summary>
        private Dictionary<Parameter, Control> _controlsDictionary;
        
        #endregion // Переменные.
        
        #region - Конструктор -

        /// <summary>
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="kompas">Интерфейс объекта КОМПАС.</param>
        public MainWindow(KompasObject kompas)
        {
            InitializeComponent();
            InitializeVariables();
            
            _manager = new Manager(kompas);
        }

        #endregion // Конструктор.
        
        #region - Инициализация -

        /// <summary>
        ///  Инициализирует переменные.
        /// </summary>
        private void InitializeVariables()
        {
            Text = Resources.MainWindowTitle;

            _modelParameters = new ModelParameters();
            _controlsDictionary = new Dictionary<Parameter, Control>();
            
            //TODO:
            /*var labels =
                Controls.Cast<object>().Where(control => control.GetType().Name == "Label").Cast<Label>().ToList();
            labels = new List<Label>(labels.OrderBy(label => label.Tag));*/

            var labels =
               Controls.OfType<Label>().OrderBy(label => label.Tag).ToList();
            /*
            var textBoxes =
                Controls.Cast<object>().Where(control => control.GetType().Name == "TextBox").Cast<TextBox>().ToList();
            textBoxes = new List<TextBox>(textBoxes.OrderBy(textBox => textBox.Tag));*/

            var textBoxes =
              Controls.OfType<TextBox>().OrderBy(textBox => textBox.Tag).ToList();

            var comboBoxes =
                Controls.Cast<object>().Where(control => control.GetType().Name == "ComboBox").Cast<ComboBox>().ToList();
            comboBoxes = new List<ComboBox>(comboBoxes.OrderBy(comboBox => comboBox.Tag));

            var parameters = _modelParameters.Parameters.Keys.ToList();
            var parametersData = _modelParameters.Parameters.Values.ToList();

            int textBoxesIndex = 0;
            int comboBoxesIndex = 0;

            for (int i = 0; i < parametersData.Count; i++)
            {
                if (i < labels.Count)
                {
                    labels[i].Text = parameters[i].GetDescription();
                }

                Control control;


                
                if (parameters[i] == Parameter.IsNightStand ||
                    parameters[i] == Parameter.IsHanger ||
                    parameters[i] == Parameter.IsShelf ||
                    parameters[i] == Parameter.IsCut
                    )
                {
                   
                    control = comboBoxes[comboBoxesIndex];
                    comboBoxes[comboBoxesIndex].Items.Add("Да");
                    comboBoxes[comboBoxesIndex].Items.Add("Нет");
                    comboBoxes[comboBoxesIndex].SelectedIndex = 0;

                    comboBoxesIndex++;
                }
                    
                     else if (parameters[i] == Parameter.OpenDirection)
                     {

                         control = comboBoxes[comboBoxesIndex];
                         comboBoxes[comboBoxesIndex].Items.Add("Правое");
                         comboBoxes[comboBoxesIndex].Items.Add("Левое");
                         comboBoxes[comboBoxesIndex].SelectedIndex = 0;

                         comboBoxesIndex++;

                     }


                     else
                     {
                         control = textBoxes[textBoxesIndex];
                         textBoxes[textBoxesIndex].Text =
                             parametersData[i].Value.ToString(CultureInfo.InvariantCulture).Replace(".", ",");

                         textBoxesIndex++;

                     }
                
                _controlsDictionary.Add(parameters[i], control);
            }
        }

        #endregion // Инициализация.

        #region - Private методы -

        /// <summary>
        /// Строит модель.
        /// </summary>
        private void BuildModel()
        {
            var parameters = GetModelParameters();

            if (parameters == null) return;

            // Отправляем параметры на проверку.
            var errorsList = _modelParameters.CheckData(parameters);

            // Если все поля заполнены верно.
            if (errorsList.Count == 0)
            {
                _manager.BuildModel(parameters);
            }
            else
            {
                //TODO:
                var messageText = errorsList.Aggregate(string.Empty, (current, message) => current + (message + "\n"));
                MessageBox.Show(messageText, Resources.MainWindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Получает параметры модели.
        /// </summary>
        /// <returns>Параметры модели.</returns>
        private Dictionary<Parameter, ParameterData> GetModelParameters()
        {
            var parameters = new Dictionary<Parameter, ParameterData>();

            foreach (KeyValuePair<Parameter, Control> parameter in _controlsDictionary)
            {
                var comboBox = parameter.Value as ComboBox;
                //TODO:
                // ? nullable тип, помимо своих значений может быть null
                double? value = comboBox != null ? comboBox.SelectedIndex : GetParameterValue(parameter.Value.Text);

                if (value == null)
                {
                    MessageBox.Show(@"Поле '" + parameter.Key.GetDescription() + @"' имеет неверное значение.",
                                    Resources.MainWindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return null;
                }

                parameters.Add(parameter.Key,
                               new ParameterData(parameter.Key.ToString(), parameter.Key.GetDescription(),
                                                 (float) value));
            }

            return parameters;
        }

        /// <summary>
        /// Преобразует текстовое значение поля в числовое.
        /// </summary>
        /// <param name="text">Текстовое значение.</param>
        /// <returns>Числовое значение.</returns>
        private double? GetParameterValue(string text)
        {
            try
            {
                return Convert.ToDouble(text);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion // Private методы.
        
        #region - Методы контролов на форме -

        /// <summary>
        /// Возникает в момент нажатия на кнопку.
        /// </summary>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button == null) return;

            button.Enabled = false;
            BuildModel();
            button.Enabled = true;
        }

        /// <summary>
        /// Возникает в момент ввода значений.
        /// </summary>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            var regex = new Regex(@"[\b]|[0-9]|[,]|[.]");

            // Проверяем содержится ли введенный символ в регулярном выражении.
            bool isMatch = regex.IsMatch(e.KeyChar.ToString(CultureInfo.InvariantCulture));

            if (!isMatch) e.Handled = true;
        }

        /// <summary>
        /// Возникает в момент потери фокуса ввода.
        /// </summary>
        private void TextBox_Leave(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox == null) return;

            double result;
            textBox.Text = textBox.Text.Replace(".", ",");                            // Еще один этап проверки, если например введене несколько точек и запятых

            if (double.TryParse(textBox.Text, out result) == false)
            {
                MessageBox.Show(
                    @"Не удалось выполнить привидение типов. Убедитесь, что вы ввели корректное значение параметра.",
                    Resources.MainWindowTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                textBox.Focus();
            }
        }

        /// <summary>
        /// Возникает в момент изменения значения в ComboBox'е. Для того, чтобы задизаблить параметры зависимые от значения в поле 
        /// </summary>
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;

            if (comboBox != null)
            {
                var isEnabled = comboBox.SelectedIndex == 0;

                switch (comboBox.Tag.ToString())
                {
                    case "ParamE":
                        {
                            label6.Enabled = isEnabled;
                            textBox6.Enabled = isEnabled;
                            label11.Enabled = isEnabled;
                            textBox11.Enabled = isEnabled;
                        }
                        break;
                    case "ParamH":
                        {
                            label9.Enabled = isEnabled;
                            textBox9.Enabled = isEnabled;
                            label10.Enabled = isEnabled;
                            textBox5.Enabled = isEnabled;
                            label14.Enabled = isEnabled;
                            textBox7.Enabled = isEnabled;
                        }
                        break;

                    case "ParamG":
                        {
                            comboBox4.Enabled = isEnabled;
                            label12.Enabled = isEnabled;
                        }
                        break;
                }
            }
        }

        #endregion // Методы контролов на форме.
    }
}
