using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EndGames.Utils
{
    public interface IStartupTask
    {
        void Execute();
    }

    public abstract class StartupTask : IStartupTask
    {
        #region IStartupTask Members

        public abstract void Execute();

        #endregion

        public static void Run<TTask>() where TTask : class, IStartupTask, new()
        {
            Run(typeof (TTask));
        }

        private static void Run(Type boostrappTask)
        {
            Run((IStartupTask) Activator.CreateInstance(boostrappTask));
        }

        public static void Run(IStartupTask task)
        {
            task.Execute();
        }

        public static void RunAllFrom(Assembly assemblty)
        {
            assemblty.GetTypes().Where(IsStartupTask())
                .Each(Run);
        }

        public static void RunAll(IEnumerable<IStartupTask> startupTasks)
        {
            startupTasks.Each(Run);
        }

        private static Func<Type, bool> IsStartupTask()
        {
            return t => t.IsPublic && t.IsClass && t.IsAbstract == false && typeof (IStartupTask).IsAssignableFrom(t);
        }
    }
}