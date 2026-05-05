using ScottPlot;

namespace CryptoArbitrageLibrary.Charts
{
    public static class BarChartRepository
    {
        private const string FontName = "Roboto";

        public static string GetHorizontalBarChart(List<Position> values)
        {
            ScottPlot.Plot myPlot = new();
            ScottPlot.Bar[] bars = new Bar[values.Count];

            myPlot.Font.Set(FontName);
            myPlot.Legend.FontName = FontName;

            myPlot.Grid.LineColor = Colors.Gray.WithAlpha(.1);
            myPlot.Axes.Hairline(true);

            myPlot.Axes.Bottom.Label.Text = $"Time  ({values.Select(x => x.Y).Min():hh:mm:ss fff}) - ({values.Select(x => x.Y).Max():hh:mm:ss fff})";
            myPlot.Axes.Bottom.Label.Bold = false;
            myPlot.Axes.Bottom.Label.FontSize = 10;

            myPlot.Axes.Left.Label.Text = "Price (usd)";
            myPlot.Axes.Left.Label.Bold = false;
            myPlot.Axes.Left.Label.FontSize = 10;

            myPlot.Title($"Prices per market on date {values.Select(x => x.Y).Min():dd/MM/yyyy hh:mm:ss fff}", 10);

            var mediana = GetMedian(values.Select(y => y.Y.ToOADate()).ToList());
            
            var lastLeftAlignment = Alignment.UpperLeft;
            var lastRightAlignment = Alignment.LowerRight;

            foreach (var value in values)
            {
                myPlot.Add.Marker(value.Y.ToOADate(), value.X);
                
                if (value.Y.ToOADate() <= mediana)
                {
                    var plotLeft = myPlot.Add.Text(value.Caption, new Coordinates(value.Y.ToOADate(), value.X ) );
                    
                    plotLeft.LabelAlignment = lastLeftAlignment;
                    lastLeftAlignment = Alignment.LowerLeft;

                    plotLeft.LabelFontColor = GetColor(value.Color);
                    plotLeft.LabelFontSize = 9;
                }
                else
                {
                    var plotRight = myPlot.Add.Text(value.Caption, new Coordinates(value.Y.ToOADate(), value.X));
                    
                    plotRight.LabelAlignment = lastRightAlignment;
                    lastRightAlignment = Alignment.UpperRight;

                    plotRight.LabelFontColor = GetColor(value.Color);
                    plotRight.LabelFontSize = 9;
                }
            }

            return myPlot.GetSvgXml(400, 300);
        }



        private static Color GetColor(string valueColor)
        {
            return Color.FromHex(valueColor);
        }
        


        private static double GetMedian(IEnumerable<double> source)
        {
            var enumerable = source as double[] ?? source.ToArray();
            if (source == null || !enumerable.Any())
            {
                throw new Exception("Empty source");
            }

            var minimal = source.Min();
            var maximal = source.Max();

            return (maximal + minimal) / 2;
        }
    }
}
