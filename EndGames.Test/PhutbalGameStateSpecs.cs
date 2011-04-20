using ForTesting;
using NUnit.Framework;

namespace Phutball.Tests
{
    public class when_creating_game_state : observations_for_auto_created_sut_of_type<PhutballGameState>
    {
        private PhutballGameStateEnum _curretnState;

        protected override void Because()
        {
            _curretnState = Sut.CurrentState;
        }

        [Test]
        public void should_set_state_to_not_started()
        {
            _curretnState.ShouldEqual(PhutballGameStateEnum.NotStarted);
        }
    }
}