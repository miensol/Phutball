using System;
using EndGames.Shell.Presenters;
using EndGames.Shell.Presenters.Interfaces;
using ForTesting;
using NUnit.Framework;
using Rhino.Mocks;

namespace EndGames.Shell.Tests.Presenters
{
    public class when_clicking_go_to_phutball : observations_for_auto_created_sut_of_type<HomePresenter>
    {
        protected override void Because()
        {
            Sut.GoToPhutball();
        }

        [Test]
        public void should_open_phutball_screen()
        {
            Dependency<IShellPresenter>().AssertWasCalled(shell => shell.Open<IPhutballPresenter>());
        }
    }

    
}