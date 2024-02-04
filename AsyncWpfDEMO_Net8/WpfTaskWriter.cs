using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;
using AsyncWpfDEMO_Net8.Tasks;

namespace AsyncWpfDEMO_Net8
{
    public class WpfTaskWriter
    {
        private readonly AwaitDemoTaskInfo _taskInfo;

        public WpfTaskWriter(AwaitDemoTaskInfo taskInfo,
            Action<string, SolidColorBrush> wpfLineWriter)
        {
            _taskInfo = taskInfo;
            MessageWriter = new WpfMessageWriter(taskInfo.TextColor, wpfLineWriter);
        }

        public void Write()
        {
            MessageWriter.Write("");
        }

        public void Write(string message)
        {
            MessageWriter.Write(message, _taskInfo.IndentLevel, includeThreadId: true);
        }

        public void WriteWithElapsedTime(string message, DateTime startTime)
        {
            MessageWriter.WriteWithElapsedTime(message, startTime, _taskInfo.IndentLevel, includeThreadId: true);
        }

        public void WriteTotalTimeTaken(DateTime startTime)
        {
            MessageWriter.WriteTotalTimeTaken(startTime, _taskInfo.IndentLevel, includeThreadId: true);
        }

        public WpfMessageWriter MessageWriter { get; }
    }
}
