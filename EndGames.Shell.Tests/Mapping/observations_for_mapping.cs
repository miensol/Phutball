using Phutball.Utils;

namespace Phutball.Shell.Tests.Mapping
{
    public abstract class observations_for_mapping<TSource,TDestination> : ForTesting.observations_for_mapping<TSource,TDestination>
    {
        protected void IncludeMapping<TMappingTask>()
            where TMappingTask :  IStartupTask
        {
            StartupTask.Run(Container.GetInstance<TMappingTask>());
        }
    }
}