using System;
using EndGames.Phutball;
using EndGames.Phutball.PlayerMoves;
using ForTesting;
using NUnit.Framework;
using Rhino.Mocks;

namespace EndGames.Tests.Phutball
{
    public abstract class observations_for_clicking_field_in_waiting_for_player_move_state : observations_for_auto_created_sut_of_type<WaitingForPlayerMoveState>
    {
        protected Field _clickedField;
        protected int RowCount;
        protected int ColumnCount;

      
        protected override void Because()
        {
            Sut.PlayerClickedField(_clickedField);
        }

        protected override void EstablishContext()
        {
            RowCount = 5;
            ColumnCount = 5;
            Dependency<IPhutballOptions>().Stub(options => options.RowCount).Return(RowCount).Repeat.Any();
            Dependency<IPhutballOptions>().Stub(options => options.ColumnCount).Return(ColumnCount).Repeat.Any();
            _clickedField = GetClickedField();
        }

        protected abstract Field GetClickedField();
    }

    public class when_clicking_empty_field_in_middle_rows : observations_for_clicking_field_in_waiting_for_player_move_state
    {
        [Test]
        public void should_place_black_stone_on_field()
        {
            _clickedField.Stone.ShouldBeOfType<BlackStone>();
        }

        [Test]
        public void should_wait_for_next_player_move()
        {
            Sut.GetNextState().ShouldBeOfType<WaitingForPlayerMoveState>();
        }

        protected override Field GetClickedField()
        {
            return new Field(1,1,1);
        }
    }

    public class when_clicking_empty_field_on_the_board_edge : observations_for_clicking_field_in_waiting_for_player_move_state
    {
        protected override Field GetClickedField()
        {
            return new Field(0, RowCount - 1, ColumnCount - 1);
        }

        [Test]
        public void should_not_place_stone_on_field()
        {
            _clickedField.HasStone.ShouldBeFalse();
        }
    }

    public class when_clicking_field_with_white_stone : observations_for_clicking_field_in_waiting_for_player_move_state
    {
        protected override Field GetClickedField()
        {
            var field = new Field(1, 1, 1);
            field.PlaceWhiteStone();
            return field;
        }

        [Test]
        public void should_make_field_selectd()
        {
            _clickedField.Selected.ShouldBeTrue();
        }

        [Test]
        public void should_move_to_player_selected_field_state()
        {
            Sut.GetNextState().ShouldBeOfType<PlayerSelectedFieldStateMove>();
        }
    }
}