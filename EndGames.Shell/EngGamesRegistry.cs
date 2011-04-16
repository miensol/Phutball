using System;
using EndGames.Phutball;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;
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
            SelectConstructor(()=> new PlayersState(null));
            SelectConstructor(()=> new PerformMoves(null, null));
            For<IEventPublisher>().Singleton().Use<EventPublisher>();
            For<IPhutballOptions>().Singleton().Use<PhutballOptions>();
            For<IPlayersState>().Singleton();
            For<IPlayersSwapper>().Use(ctx => ctx.GetInstance<IPlayersState>());
            For<IFieldsGraph>().Singleton();
            For<MovesHistory>().Singleton();
            For<Func<IHandlePlayerMoves>>().Use(ctx => ctx.GetInstance<HandlePlayerMoves>);
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