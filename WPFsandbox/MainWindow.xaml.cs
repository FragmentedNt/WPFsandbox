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

// added
using LiveCharts;
using LiveCharts.Wpf;
using System.Diagnostics;
using LiveCharts.Configurations; // Mappersのため
using LiveCharts.Defaults; // ObservablePointのため

namespace WPFsandbox
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        List<DockPanel> dpList = new List<DockPanel>();

        public MainWindow()
        {
            InitializeComponent();
        }


        private void B_draw_Click(object sender, RoutedEventArgs e)
        {
            var gd = new GraphData();
            var r = new Random();

            foreach(var dp in dpList)
            {
                if(dp.Children.Count > 0)
                {
                    var x_ar = new List<int> { r.Next(-10, 10), r.Next(-10, 10), r.Next(-10, 10), r.Next(-10, 10) };
                    x_ar.Sort();
                    var sc = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Values = new ChartValues<ObservablePoint> {
                                new ObservablePoint(x_ar[0], r.Next(-10, 10)),
                                new ObservablePoint(x_ar[1], r.Next(-10, 10)),
                                new ObservablePoint(x_ar[2], r.Next(-10, 10)),
                                new ObservablePoint(x_ar[3], r.Next(-10, 10))
                            }
                        },
                        new ColumnSeries
                        {
                            Values = new ChartValues<ObservablePoint> {
                                new ObservablePoint(x_ar[0], r.Next(-10, 10)),
                                new ObservablePoint(x_ar[1], r.Next(-10, 10)),
                                new ObservablePoint(x_ar[2], r.Next(-10, 10)),
                                new ObservablePoint(x_ar[3], r.Next(-10, 10))
                            }
                        },
                        new ColumnSeries
                        {
                            Values = new ChartValues<ObservablePoint> {
                                new ObservablePoint(x_ar[0], r.Next(-10, 10)),
                                new ObservablePoint(x_ar[1], r.Next(-10, 10)),
                                new ObservablePoint(x_ar[2], r.Next(-10, 10)),
                                new ObservablePoint(x_ar[3], r.Next(-10, 10))
                            }
                        }
                    };
                    gd.SeriesCollection = sc;
                    ((CartesianChart)dp.Children[0]).DataContext = gd;
                }
            }
        }

        public class GraphData
        {
            public SeriesCollection SeriesCollection { get; set; }

            public GraphData() { }
        }

        private void B_add_Click(object sender, RoutedEventArgs e)
        {
            var cartesianCount = (int)(nudRowCount.Value * nudColumnCount.Value);
            var colmunCount = (int)nudColumnCount.Value;
            var rowCount = (int)nudRowCount.Value;

            cartesinGrid.Children.Clear();
            if (dpList.Count > cartesianCount)
                dpList.RemoveRange(cartesianCount - 1, dpList.Count - cartesianCount);

            var cgcd = cartesinGrid.ColumnDefinitions;
            if (cgcd.Count > colmunCount)
                cgcd.RemoveRange(colmunCount - 1, cgcd.Count - colmunCount);

            var cgrd = cartesinGrid.RowDefinitions;
            if (cgrd.Count > rowCount)
                cgrd.RemoveRange(rowCount - 1, cgrd.Count - rowCount);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colmunCount; j++)
                {
                    DockPanel dp;
                    if(dpList.Count > i * colmunCount + j)
                    {
                        dp = dpList[i * colmunCount + j];
                        Debug.WriteLine($"row{i} colmun{j} : dp reuse");
                    }
                    else
                    {
                        dp = new DockPanel();
                        var cc = new CartesianChart();
                        cc.SetBinding(CartesianChart.SeriesProperty, new Binding("SeriesCollection"));
                        dp.Children.Add(cc);
                        dpList.Add(dp);
                        Debug.WriteLine($"row{i} colmun{j} : dp new");
                    }

                    if (cartesinGrid.Children.IndexOf(dp) == -1)
                        cartesinGrid.Children.Add(dp);

                    if (cartesinGrid.RowDefinitions.Count <= i)
                    {
                        var rd =  new RowDefinition();
                        rd.Height = new GridLength(1.0, GridUnitType.Star);
                        cartesinGrid.RowDefinitions.Add(rd);
                    }
                    if (cartesinGrid.ColumnDefinitions.Count <= j)
                    {
                        var cd = new ColumnDefinition();
                        cd.Width = new GridLength(1.0, GridUnitType.Star);
                        cartesinGrid.ColumnDefinitions.Add(cd);
                    }
                    Grid.SetRow(dp, i);
                    Grid.SetColumn(dp, j);
                }
            }
            Debug.WriteLine("");
        }
    }
}
