using EndGames.Phutball;
using EndGames.Shell.Presenters;
using EndGames.Shell.Presenters.Interfaces;
using EndGames.Utils;
using StructureMap.Configuration.DSL;

namespace EndGames.Shell
{
    public class EngGamesRegistry : Registry
    {
        public EngGamesRegistry()
        {
            For<IShellPresenter>().Singleton().Use<ShellPresenter>();
            For<IPhutballBoard>().Singleton().Use<PhutballBoard>();
            For<PhutballGameState>().Singleton().Use<PhutballGameState>();
            For<IEventPublisher>().Singleton().Use<EventPublisher>();
            For<Log>().Singleton().Use<Log>();
            Scan(scanner=>
                     {
                         scanner.AssemblyContainingType<ShellPresenter>();
                         scanner.AssemblyContainingType<Field>();
                         scanner.WithDefaultConventions();
                     });
            IncludeRegistry<StartupTaskRegistry>();
        }
    }

    public class StartupTaskRegistry : Registry
    {
        public StartupTaskRegistry()
        {
            Scan(scanner => 
            {
                scanner.AssemblyContainingType<ShellPresenter>();
                scanner.AssemblyContainingType<Field>();
                scanner.AddAllTypesOf<IStartupTask>(); 
            });
        }
    }
}