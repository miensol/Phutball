using System;
using EndGames.Phutball;
using ForTesting;
using NUnit.Framework;

namespace EndGames.Tests.Phutball
{
    public class when_current_player_wins_a_game : observations_for_auto_created_sut_of_type<PhutballGameState>
    {
        private CurrentPlayerWonEvent _playerWon;

        protected override void Because()
        {
            Sut.CurrentPlayerWon();
        }

        protected override void EstablishContext()
        {
            ProvideImplementationOf<IEventPublisher>(new EventPublisher());
            Dependency<IEventPublisher>().GetEvent<CurrentPlayerWonEvent>().Subscribe(cpwin => _playerWon = cpwin);
        }

        [Test]
        public void should_notify_of_game_end()
        {
            _playerWon.ShouldNotBeNull();
        }
    }
}