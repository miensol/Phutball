using System;
using EndGames.Games;
using EndGames.Mapping;
using EndGames.Shell.Models;
using EndGames.Shell.Presenters;
using ForTesting;
using NUnit.Framework;
using Rhino.Mocks;

namespace EndGames.Shell.Tests.Presenters
{
    public class when_initalizing_checkers_board_presenter : observations_for_auto_created_sut_of_type<CheckersBoardPresenter>
    {
        protected override void Because()
        {
            Sut.Initialize();
        }

        [Test]
        public void should_initialize_checkers_game()
        {
            Dependency<ICheckersGame>().AssertWasCalled(game=> game.BuildInitialGraphGame());
        }
    }

    public class when_activating_checkers_board_presenter : observations_for_auto_created_sut_of_type<CheckersBoardPresenter>
    {
        private CheckersBoardModel _expectedBoardModel;

        protected override void Because()
        {
            Sut.Activate();
        }

        protected override void EstablishContext()
        {
            _expectedBoardModel = new CheckersBoardModel();
            Dependency<IDoMapper>().Stub(mapper => mapper.DoMap<ICheckersGame,CheckersBoardModel>(null))
                .IgnoreArguments().Return(_expectedBoardModel);
        }

        [Test]
        public void should_map_checkers_board_model_from_game()
        {
            Sut.Board.ShouldEqual(_expectedBoardModel);
        }
    }
}