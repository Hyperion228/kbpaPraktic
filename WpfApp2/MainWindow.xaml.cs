using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PlotNyquist()
        {
            // Очищаем график перед перерисовкой
            nyquistCanvas.Children.Clear();

            // Получаем значения параметров K, T3 и T4 из текстовых полей
            if (!double.TryParse(txtK.Text, out double K) ||
                !double.TryParse(txtT3.Text, out double T3) ||
                !double.TryParse(txtT4.Text, out double T4))
            {
                MessageBox.Show("Введите числовые значения для K, T3 и T4.");
                return;
            }

            // Проверяем, что значения не равны нулю
            if (K == 0 || T3 == 0 || T4 == 0)
            {
                MessageBox.Show("Коэффициенты не могут быть равны нулю.");
                return;
            }

            // Расчет и отображение годографа Найквиста
            for (double w = 0.01; w <= 100; w += 0.1)
            {
                // Расчет вещественной и мнимой частей передаточной функции
                double realPart = K / Math.Sqrt(Math.Pow(1 - Math.Pow(w * T3, 2), 2) * (1 + Math.Pow(w * T4, 2)));
                double imagPart = -w * T3 * realPart + w * T4 * K / Math.Sqrt(Math.Pow(1 - Math.Pow(w * T3, 2), 2) * (1 + Math.Pow(w * T4, 2)));

                // Отображение точки на графике
                Ellipse point = new Ellipse
                {
                    Width = 2,
                    Height = 2,
                    Fill = Brushes.Red,
                    Margin = new Thickness(200 + realPart * 50, 200 - imagPart * 50, 0, 0)
                };
                nyquistCanvas.Children.Add(point);
            }
        }

        private void btnPlot_Click(object sender, RoutedEventArgs e)
        {
            PlotNyquist();
        }
    }
}
