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
            For<IFieldsUpdater>().Singleton().Use(ctx=> ctx.GetInstance<IPhutballBoard>());
            For<PhutballGameState>().Singleton().Use<PhutballGameState>();
            For<PlayersState>().Singleton();
            For<IEventPublisher>().Singleton().Use<EventPublisher>();
            For<IPhutballOptions>().Singleton().Use<PhutballOptions>();
            For<IPlayersState>().Singleton().Use(new PlayersState());
            For<IFieldsGraph>().Singleton();
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