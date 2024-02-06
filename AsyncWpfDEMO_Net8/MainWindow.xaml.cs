using AsyncWpfDEMO_Net8.Tasks;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncWpfDEMO_Net8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected readonly AwaitDemoTaskList _taskList;

        public MainWindow()
        {
            InitializeComponent();
            _taskList = new AwaitDemoTaskList();
        }

        private async void btnMakeBreakfast_Click(object sender, RoutedEventArgs e)
        {
            await MakeBreakfastAsync();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            paraMessages.Inlines.Clear();
        }

        private void WriteWithLineBreaks(string text, SolidColorBrush brush)
        {
            // Buffer output
            paraMessages.Inlines.Add(new Run(text) { Foreground = brush });
            paraMessages.Inlines.Add(new LineBreak());

            // Always keep scrolled to the end
            rtbMessages.ScrollToEnd();
        }

        private async Task MakeBreakfastAsync()
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.BreakfastCaller);
            var taskWriter = new WpfTaskWriter(taskInfo, WriteWithLineBreaks);
            var messageWriter = taskWriter.MessageWriter;

            messageWriter.Write();
            taskWriter.Write("Making breakfast asynchronously:");
            messageWriter.Write();

            var startTime = DateTime.Now;

            var breakfastMaker = new AsyncBreakfastMakerMixedAwaits(_taskList, WriteWithLineBreaks);
            var task = breakfastMaker.MakeBreakfastAsync(startTime);

            DoOtherWork(startTime);

            await task;

            messageWriter.Write();

            taskWriter.WriteTotalTimeTaken(startTime);
        }

        private void DoOtherWork(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.OtherWork);
            var taskWriter = new WpfTaskWriter(taskInfo, WriteWithLineBreaks);

            taskWriter.Write("Caller doing other non-breakfast work...");

            Task.Delay(taskInfo.Duration).Wait();

            taskWriter.WriteWithElapsedTime("Other work is finished", startTime);
        }
    }
}