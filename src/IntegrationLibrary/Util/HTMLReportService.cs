namespace IntegrationLibrary.Util
{
    using IntegrationLibrary.Util.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    public class HTMLReportService : IHTMLReportService
    {
        private static readonly string BODY_WILDCARD = "<!--BODY_WILDCARD-->";
        private static readonly string DATE_SPAN_WILDCARD = "<!--DATE_SPAN_WILDCARD-->";
        private static readonly string SCRIPT_WILDCARD = "//SCRIPT_WILDCARD";
        private int barChartCnt;
        private int pieChartCnt;
        private readonly string _templateFile;
        public string OutputFile { get; set; }
        public HTMLReportService()
        {
            var currentPath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            currentPath = Path.Combine(new string[] { currentPath, "IntegrationLibrary", "Util", "Report-Templates", "template.html" });
            _templateFile = File.ReadAllText(currentPath);
            OutputFile = _templateFile;
        }

        public void AddBarChart(List<string> lables, List<double> data)
        {
            if (lables == null || data == null || lables.Count != data.Count)
            {
                throw new ArgumentException("Lables and values array don't have the same number of values or one of them is null.");
            }

            string uniqueId = "bar_chart_id_" + ++barChartCnt;
            string htmlPart = "<div class=\"chart\">\r\n<canvas id=\"" + uniqueId + "\"></canvas>\r\n</div>";
            string jsPart = GetChartJSElement(lables, data, uniqueId, "bar", "Amount");
            AddHTMLElement(htmlPart);
            AddJSElement(jsPart);
        }

        public void AddPieChart(List<string> lables, List<double> data)
        {
            if (lables == null || data == null || lables.Count != data.Count)
            {
                throw new ArgumentException("Lables and values array don't have the same number of values or one of them is null.");
            }
            string uniqueId = "pie_chart_id_" + ++pieChartCnt;
            string htmlPart = "<div class=\"chart\">\r\n<canvas id=\"" + uniqueId + "\"></canvas>\r\n</div>";
            string jsPart = GetChartJSElement(lables, data, uniqueId, "doughnut", "Share");
            AddHTMLElement(htmlPart);
            AddJSElement(jsPart);
        }

        public void AddTable(List<string> header, List<List<string>> input)
        {
            string html =
                "<table class=\"table\">\n";
            html += "\n<tr>";
            foreach (var data in header)
            {
                html += "\n<th>" + data + "</th>";
            }
            html += "\n</tr>";
            foreach (var row in input)
            {
                html += "\n<tr>";
                foreach (var data in row)
                {
                    html += "\n<td>" + data + "</td>";
                }
                html += "\n</tr>";
            }
            html += "\n</table>";
            AddHTMLElement(html);
        }

        private static string GetChartJSElement(List<string> lables, List<double> data, string identifier, string type, string label)
        {
            return
                "const " + identifier + " = document.getElementById('" + identifier + "');\r\n" +
                "        new Chart(" + identifier + ", {\r\n" +
                "            type: '" + type + "',\r\n" +
                "            data: {\r\n" +
                "                labels: " + ListToJSArrayString(lables) + ",\r\n" +
                "                datasets: [{\r\n" +
                "                    label: '" + label + "',\r\n" +
                "                    data: " + ListToJSArrayString(data) + ",\r\n" +
                "                    borderWidth: 1\r\n" +
                "                }]\r\n" +
                "            },\r\n" +
                "            options: { animation: false, }" +
                "        });";
        }

        private static string ListToJSArrayString(List<double> list)
        {
            if (list == null || list.Count == 0)
            {
                return "[]";
            }
            string output = "[";
            foreach (double item in list)
            {
                output += item.ToString() + ", ";
            }
            return output.Substring(0, output.Length - 2) + "]";
        }

        private static string ListToJSArrayString(List<string> list)
        {
            if (list == null || list.Count == 0)
            {
                return "[]";    
            }
            string output = "[";
            foreach (string item in list)
            {
                output += "'" + item.ToString() + "'" + ", ";
            }
            return output.Substring(0, output.Length - 2) + "]";
        }

        private void AddHTMLElement(string element)
        {
            OutputFile = OutputFile.Replace(BODY_WILDCARD, element + "\n" + BODY_WILDCARD);
        }

        private void AddJSElement(string element)
        {
            OutputFile = OutputFile.Replace(SCRIPT_WILDCARD, element + "\n" + SCRIPT_WILDCARD);
        }

        public void AddTimestamp(DateTime from, DateTime to)
        {
            OutputFile = OutputFile.Replace(DATE_SPAN_WILDCARD, from.ToShortDateString() + " and " + to.ToShortDateString());
        }
    }
}
