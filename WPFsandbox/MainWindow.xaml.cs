using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// 以下を追加
using LiveCharts;
using LiveCharts.Wpf;

namespace WPFsandbox
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void B_draw_Click(object sender, RoutedEventArgs e)
        {
            GraphData gd = new GraphData();

            var sc = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 3, 1, 4, 1 }
                },
                new ColumnSeries
                {
                    Values = new ChartValues<double> { 2, 7, 1, 8 }
                }
            };
            gd.seriesCollection = sc;
            //Live_1.DataContext = gd;
        }

        public class GraphData
        {
            public SeriesCollection seriesCollection { get; set; }
        }

        private void B_add_Click(object sender, RoutedEventArgs e)
        {
            CartesianChart cc = new CartesianChart();
            cc.HorizontalAlignment = HorizontalAlignment.Stretch;
            cc.VerticalAlignment = VerticalAlignment.Stretch;
            //cc.Width = 800;
            cc.Height = 240;
            //stack1.Children.Add(cc);
        }
    }

}
