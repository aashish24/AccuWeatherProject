using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace AccuWeatherProject.Utilties
{
    public class ChartUtilities
    {
        /// <summary>
        /// These methods help make the chart
        /// </summary>
        /// <param name="datapoints"></param>
        /// <param name="chartType"></param>
        /// <param name="ChartTitle"></param>
        /// <returns></returns>
        public static Chart CreateChart(Dictionary<string, int> datapoints, SeriesChartType chartType, string ChartTitle)
        {
            Chart chart = new Chart();
            chart.Width = 900;
            chart.Height = 200;
            chart.BackColor = Color.LightGray;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackSecondaryColor = Color.LightGray;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BorderlineColor = Color.Gray;
            chart.RenderType = RenderType.BinaryStreaming;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;
            chart.Titles.Add(CreateTitle(ChartTitle));
            chart.Legends.Add(CreateLegend());
            chart.Series.Add(CreateSeries(datapoints, chartType));
            chart.ChartAreas.Add(CreateChartArea());
            return chart;
        }
        private static Title CreateTitle(string titleText)
        {
            Title title = new Title();
            title.Text = titleText;
            title.ShadowColor = Color.FromArgb(32, 0, 0, 0);
            title.Font = new Font("Trebuchet MS", 14F, FontStyle.Bold);
            title.ShadowOffset = 3;
            title.ForeColor = Color.FromArgb(26, 59, 105);
            return title;
        }
        private static Series CreateSeries(Dictionary<string, int> datapoints, SeriesChartType chartType)
        {
            Series seriesDetail = new Series();
            seriesDetail.Name = "Degrees";
            seriesDetail.IsValueShownAsLabel = false;
            seriesDetail.Color = Color.FromArgb(198, 99, 99);
            seriesDetail.ChartType = chartType;
            seriesDetail.IsValueShownAsLabel = true;
            seriesDetail.BorderWidth = 2;
            foreach (KeyValuePair<string, int> kvp in datapoints)
            {
                seriesDetail.Points.AddXY(kvp.Key, kvp.Value);
            }
            seriesDetail.ChartArea = "Result Chart";
            return seriesDetail;
        }
        private static ChartArea CreateChartArea()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.Name = "Result Chart";
            chartArea.BackColor = Color.Transparent;
            chartArea.AxisX.IsLabelAutoFit = false;
            chartArea.AxisY.IsLabelAutoFit = false;
            chartArea.AxisX.LabelStyle.Font =
               new Font("Verdana,Arial,Helvetica,sans-serif",
                        8F, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font =
               new Font("Verdana,Arial,Helvetica,sans-serif",
                        8F, FontStyle.Regular);
            chartArea.AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chartArea.AxisX.Interval = 1;
            return chartArea;
        }
        private static Legend CreateLegend()
        {
            Legend legend = new Legend();
            legend.Name = "Result Chart";
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.BackColor = Color.Transparent;
            legend.Font = new Font(new FontFamily("Trebuchet MS"), 9);
            legend.LegendStyle = LegendStyle.Row;
            return legend;
        }
    }
}
