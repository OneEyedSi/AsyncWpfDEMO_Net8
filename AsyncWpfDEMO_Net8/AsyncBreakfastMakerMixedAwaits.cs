using AsyncWpfDEMO_Net8.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AsyncWpfDEMO_Net8
{
    public class AsyncBreakfastMakerMixedAwaits
    {
        protected readonly AwaitDemoTaskList _taskList;
        protected readonly Action<string, SolidColorBrush> _wpfLineWriter;

        public AsyncBreakfastMakerMixedAwaits(AwaitDemoTaskList taskList,
            Action<string, SolidColorBrush> wpfLineWriter)
        {
            _taskList = taskList;
            _wpfLineWriter = wpfLineWriter;
        }

        public async Task MakeBreakfastAsync(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeBreakfast);
            var taskWriter = new WpfTaskWriter(taskInfo, _wpfLineWriter);

            taskWriter.Write("Preparing breakfast:");

            // Use immediate awaits for heating the pan and frying the eggs: 
            // This will ensure the pan finishes heating before starting to fry the eggs, and the 
            // eggs finish frying before starting to fry the bacon.  
            await HeatFryingPanAsync(startTime);

            int numberOfEggs = 2;
            List<Egg> eggs = await FryEggsAsync(numberOfEggs, startTime);

            // Defer the await for frying the bacon: We're happy for it to run in parallel with 
            // making the coffee and preparing the toast.  Running the three tasks in parallel 
            // reduces execution time.
            int numberOfBaconSlices = 3;
            Task<List<Bacon>> baconSlicesTask = FryBaconAsync(numberOfBaconSlices, startTime);

            Task<Coffee> cupTask = MakeCoffeeAsync(startTime);

            // The PrepareToastAsync method fixes the second problem: Now spreading butter and jam 
            // on the toast only has to wait for the toast to finish, it no longer has to wait 
            // until the bacon frying and the coffee making have finished.
            int numberOfToastSlices = 2;
            Task<List<Toast>> toastSlicesTask = PrepareToastAsync(numberOfToastSlices, startTime);

            var baconSlices = await baconSlicesTask;
            var cup = await cupTask;
            var toastSlices = await toastSlicesTask;

            Juice juice = PourJuice(startTime);

            taskWriter.WriteWithElapsedTime("Breakfast is ready!", startTime);
        }

        private async Task<List<Toast>> PrepareToastAsync(int numberOfSlices, DateTime startTime)
        {
            List<Toast> toastSlices = await MakeToastAsync(numberOfSlices, startTime);

            SpreadButterOnToast(toastSlices, startTime);

            SpreadJamOnToast(toastSlices, startTime);

            return toastSlices;
        }

        protected async Task HeatFryingPanAsync(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.HeatPan);
            var taskWriter = new WpfTaskWriter(taskInfo, _wpfLineWriter);

            taskWriter.Write("Heating frying pan...");

            await Task.Delay(taskInfo.Duration);

            taskWriter.WriteWithElapsedTime("Frying pan is hot", startTime);
        }

        protected async Task<List<Egg>> FryEggsAsync(int numberOfEggs, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.FryEggs);
            var taskWriter = new WpfTaskWriter(taskInfo, _wpfLineWriter);

            taskWriter.Write($"Frying {numberOfEggs} eggs...");

            List<Egg> eggs = new();

            await Task.Delay(taskInfo.Duration);

            for (int i = 0; i < numberOfEggs; i++)
            {
                eggs.Add(new Egg());
            }

            taskWriter.WriteWithElapsedTime("Eggs are ready", startTime);

            return eggs;
        }

        protected async Task<List<Bacon>> FryBaconAsync(int numberOfSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.FryBacon);
            var taskWriter = new WpfTaskWriter(taskInfo, _wpfLineWriter);

            taskWriter.Write($"Frying {numberOfSlices} slices of bacon...");

            List<Bacon> baconSlices = new();

            await Task.Delay(taskInfo.Duration);

            for (int i = 0; i < numberOfSlices; i++)
            {
                baconSlices.Add(new Bacon());
            }

            taskWriter.WriteWithElapsedTime("Bacon is ready", startTime);

            return baconSlices;
        }

        protected async Task<Coffee> MakeCoffeeAsync(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeCoffee);
            var taskWriter = new WpfTaskWriter(taskInfo, _wpfLineWriter);

            taskWriter.Write("Making coffee...");

            await Task.Delay(taskInfo.Duration);

            taskWriter.WriteWithElapsedTime("Coffee is ready", startTime);

            return new Coffee();
        }

        protected async Task<List<Toast>> MakeToastAsync(int numberOfSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.MakeToast);
            var taskWriter = new WpfTaskWriter(taskInfo, _wpfLineWriter);

            taskWriter.Write($"Toasting {numberOfSlices} slices of bread...");

            List<Toast> toastSlices = new();

            await Task.Delay(taskInfo.Duration);

            for (int i = 0; i < numberOfSlices; i++)
            {
                toastSlices.Add(new Toast());
            }

            taskWriter.WriteWithElapsedTime("Toast has popped", startTime);

            return toastSlices;
        }

        protected void SpreadButterOnToast(List<Toast> toastSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.SpreadButter);
            var taskWriter = new WpfTaskWriter(taskInfo, _wpfLineWriter);

            taskWriter.Write($"Spreading butter on {toastSlices.Count} slices of toast...");

            Task.Delay(taskInfo.Duration).Wait();

            taskWriter.WriteWithElapsedTime("Butter has been spread", startTime);
        }

        protected void SpreadJamOnToast(List<Toast> toastSlices, DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.SpreadJam);
            var taskWriter = new WpfTaskWriter(taskInfo, _wpfLineWriter);

            taskWriter.Write($"Spreading jam on {toastSlices.Count} slices of toast...");

            Task.Delay(taskInfo.Duration).Wait();

            taskWriter.WriteWithElapsedTime("Jam has been spread", startTime);
        }

        protected Juice PourJuice(DateTime startTime)
        {
            var taskInfo = _taskList.GetTask(AwaitDemoTaskId.PourJuice);
            var taskWriter = new WpfTaskWriter(taskInfo, _wpfLineWriter);

            taskWriter.Write("Pouring orange juice...");

            Task.Delay(taskInfo.Duration).Wait();

            taskWriter.WriteWithElapsedTime("Juice is ready", startTime);

            return new Juice();
        }

    }
}
