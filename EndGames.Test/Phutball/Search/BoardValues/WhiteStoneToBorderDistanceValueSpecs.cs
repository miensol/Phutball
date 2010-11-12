using System;
using EndGames.Phutball;
using EndGames.Phutball.Search.BoardValues;
using ForTesting;
using NUnit.Framework;

namespace EndGames.Tests.Phutball.Search.BoardValues
{
    public class when_getting_distace_to_field_on_loosing_border : observations_for_distance_to_border
    {
        protected override TargetBorder GetTargetBorder()
        {
            return TargetBorderEnum.Bottom;
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            var field = new Field(0, 1, 0);
            field.PlaceWhiteStone();
            _fieldsGraph.UpdateFields(field);
        }

        [Test]
        public void should_return_0()
        {
            _distance.ShouldEqual(0);
        }
        
    }

    public class when_getting_distance_to_field_in_middle_board : observations_for_distance_to_border
    {
        protected override TargetBorder GetTargetBorder()
        {
            return TargetBorderEnum.Upper;
        }

        [Test]
        public void should_return_distance_from_white_field()
        {
            _distance.ShouldEqual(int.MaxValue - 2);
        }

    }

    public class when_getting_distace_to_field_behind_winning_board : observations_for_distance_to_border
    {
        protected override TargetBorder GetTargetBorder()
        {
            return TargetBorderEnum.Upper;
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            var whiteStone = new Field(0,0,0);
            whiteStone.PlaceWhiteStone();
            _fieldsGraph.UpdateFields(new[]{whiteStone });
        }

        [Test]
        public void should_return_max_value()
        {
            _distance.ShouldEqual(int.MaxValue);
        }
    }

    public class when_getting_distance_to_field_on_bottom_board : observations_for_distance_to_border
    {
        protected override TargetBorder GetTargetBorder()
        {
            return TargetBorderEnum.Bottom;
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            var whiteField = new Field(34, 5, 0);
            whiteField.PlaceWhiteStone();
            _fieldsGraph.UpdateFields(new[]{whiteField});
        }

        [Test]
        public void should_return_max_value()
        {
            _distance.ShouldEqual(int.MaxValue);
        }
    }

    public abstract class observations_for_distance_to_border : observations_for_auto_created_sut_of_type<WhiteStoneToBorderDistanceValue>
    {
        public TargetBorder _targetBorder;
        protected IFieldsGraph _fieldsGraph;
        protected int _distance;

        protected override WhiteStoneToBorderDistanceValue CreateSut()
        {
            _targetBorder = GetTargetBorder();
            return new WhiteStoneToBorderDistanceValue(_targetBorder);
        }

        protected abstract TargetBorder GetTargetBorder();

        protected override void EstablishContext()
        {
            ProvideImplementationOf<IPhutballOptions>(new TestPhutballOptions
                                                          {
                                                              RowCount = 7,
                                                              ColumnCount = 5
                                                          });
            _fieldsGraph = new FieldsGraph(Dependency<IPhutballOptions>());
        }

        protected override void Because()
        {
            _distance = Sut.GetValue(_fieldsGraph);
        }
    }
}