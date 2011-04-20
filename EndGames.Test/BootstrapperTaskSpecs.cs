using System.Reflection;
using ForTesting;
using NUnit.Framework;
using Phutball.Utils;

namespace Phutball.Tests
{
    public class when_runing_task_using_boostrapper_class : observations_for_static_sut
    {
        protected override void Because()
        {
            StartupTask.Run<TestTask>();
        }

        [Test]
        public void should_execute_given_task()
        {
            TestTask.WasExecuted.ShouldBeTrue();
        }
    }

    public class when_executing_task_from_given_assembly : observations_for_static_sut
    {
        private Assembly _currentAssemblty;

        protected override void Because()
        {
            StartupTask.RunAllFrom(_currentAssemblty);
        }

        protected override void EstablishContext()
        {
            _currentAssemblty = Assembly.GetExecutingAssembly();
        }

        [Test]
        public void should_execute_all_taks_in_given_assembly()
        {
            TestTask.WasExecuted.ShouldBeTrue();
            TestTask2.WasExecuted.ShouldBeTrue();
        }
    }

    public interface ITestBoostraper : IStartupTask
    {}

    public class TestTask2 : IStartupTask
    {
        public static bool WasExecuted = false;

        public TestTask2()
        {
            WasExecuted = false;
        }
        public void Execute()
        {
            WasExecuted = true;
        }
    }

    public class TestTask : IStartupTask
    {
        public static bool WasExecuted = false;

        public TestTask()
        {
            WasExecuted = false;
        }


        public void Execute()
        {
            TestTask.WasExecuted = true;
        }
    }
}