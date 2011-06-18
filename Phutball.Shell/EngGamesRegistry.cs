using System;
using Phutball.Moves;
using Phutball.PlayerMoves;
using Phutball.Shell.Presenters;
using Phutball.Shell.Presenters.Interfaces;
using Phutball.Utils;
using StructureMap.Configuration.DSL;

namespace Phutball.Shell
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
            SelectConstructor(()=> new PlayersState(null,null));
            SelectConstructor(()=> new PerformMoves(null, null));
            For<IEventPublisher>().Singleton().Use<EventPublisher>();
            For<PhutballOptions>().Singleton();
            For<IPhutballOptions>().Singleton().Use(ctx => ctx.GetInstance<PhutballOptions>());
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