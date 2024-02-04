using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AsyncWpfDEMO_Net8.Tasks
{
    public class AwaitDemoTaskList
    {
        private readonly IList<AwaitDemoTaskInfo> _tasks;
        public AwaitDemoTaskList()
        {
            _tasks = InitializeTasks();
        }

        protected IList<AwaitDemoTaskInfo> InitializeTasks()
        {
            var tasks = new List<AwaitDemoTaskInfo>
            {
                new(AwaitDemoTaskId.HeatPan, 3000, 1, Brushes.MediumVioletRed),
                new(AwaitDemoTaskId.FryEggs, 3000, 1, Brushes.Firebrick),
                new(AwaitDemoTaskId.FryBacon, 5000, 1, Brushes.Tomato),
                new(AwaitDemoTaskId.MakeCoffee, 4000, 1, Brushes.Orange),
                new(AwaitDemoTaskId.MakeToast, 2000, 1, Brushes.Yellow),
                new(AwaitDemoTaskId.SpreadButter, 1000, 1, Brushes.Cyan),
                new(AwaitDemoTaskId.SpreadJam, 1000, 1, Brushes.DarkCyan),
                new(AwaitDemoTaskId.PourJuice, 500, 1, Brushes.RoyalBlue),
                new(AwaitDemoTaskId.MakeBreakfast, 0, 0, Brushes.DarkGray),
                new(AwaitDemoTaskId.OtherWork, 5000, 0, Brushes.Green),
                new(AwaitDemoTaskId.BreakfastCaller, 0, 0, Brushes.White)
            };

            var breakfastTaskIds = new AwaitDemoTaskId[] { AwaitDemoTaskId.HeatPan,
                                                            AwaitDemoTaskId.FryEggs,
                                                            AwaitDemoTaskId.FryBacon,
                                                            AwaitDemoTaskId.MakeCoffee,
                                                            AwaitDemoTaskId.MakeToast,
                                                            AwaitDemoTaskId.SpreadButter,
                                                            AwaitDemoTaskId.SpreadJam,
                                                            AwaitDemoTaskId.PourJuice
                                                        };

            foreach (var task in tasks)
            {
                if (breakfastTaskIds.Contains(task.Id))
                {
                    task.Tags.Add(TaskTag.BreakfastTask);
                }
            }

            return tasks;
        }

        public IList<AwaitDemoTaskInfo> GetTasks()
        {
            return _tasks;
        }

        public AwaitDemoTaskInfo GetTask(AwaitDemoTaskId id)
        {
            return _tasks.First(t => EqualityComparer<AwaitDemoTaskId>.Default.Equals(t.Id, id));
        }
    }
}
