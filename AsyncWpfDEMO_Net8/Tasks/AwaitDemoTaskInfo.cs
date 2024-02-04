using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AsyncWpfDEMO_Net8.Tasks
{
    public class AwaitDemoTaskInfo
    {
        public AwaitDemoTaskInfo(AwaitDemoTaskId id, int duration, int indentLevel, SolidColorBrush textColor) 
        {
            Id = id;
            Duration = duration;
            IndentLevel = indentLevel;
            TextColor = textColor;
        }

        public AwaitDemoTaskId Id { get; }

        public string Title => Regex.Replace(Id.ToString(), "(A-Z)", " $1");

        public int Duration { get; set; }

        public decimal DurationSeconds => decimal.Divide(Duration, 1000);

        public string DurationText =>
            $"{Title}: {DurationSeconds.ToString(Constant.SecondsFormat)} seconds";

        public int IndentLevel { get; set; }

        public IList<string> Tags { get; } = new List<string>();

        public SolidColorBrush TextColor { get; }
    }
}
