namespace IntegrationLibrary.Util.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IHTMLReportService
    {
        string OutputFile { get; set; }
        /// <summary>
        /// Creates a table with specified header and input data. 
        /// It does not check for length of both lists so it creates a table for any input thrown at it.
        /// </summary>
        /// <param name="header">List of headers</param>
        /// <param name="input">List of input data rows lists.</param>
        void AddTable(List<string> header, List<List<string>> input);

        /// <summary>
        /// Creates a Chart.js bar chart with given labels and data.
        /// Throws ArgumentException if labels and data are not of equal length or if they are null
        /// </summary>
        /// <param name="lables"></param>
        /// <param name="data"></param>
        void AddBarChart(List<string> lables, List<double> data);
        /// <summary>
        /// Creates a Chart.js pie chart with given labels and data.
        /// </summary>
        /// <param name="lables"></param>
        /// <param name="data"></param>
        void AddPieChart(List<string> lables, List<double> data);
    }
}
