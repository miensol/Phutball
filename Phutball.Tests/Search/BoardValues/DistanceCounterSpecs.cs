using ForTesting;
using NUnit.Framework;
using Phutball.Search.BoardValues;

namespace Phutball.Tests.Search.BoardValues
{
    public abstract class observations_for_raw_distance_counting<TCOunter> : observations_for_auto_created_sut_of_type<TCOunter>
        where TCOunter : class, IDistanceCounter
    {
        protected int _distance;
        private Field _toField;

        protected override void Because()
        {
            _distance = Sut.Distance(_toField);
        }

        protected override void EstablishContext()
        {
            ProvideImplementationOf<IFieldsGraph>(new FieldsGraph(new PhutballOptions()
            {
                RowCount = 7,
                ColumnCount = 5
            }));
            _toField = WhiteField();
        }

        protected abstract Field WhiteField();
    }

    public class when_getting_distance_from_upper_border_to_field_one_row_from_lossing : observations_for_raw_distance_counting<DistanceToUpperBorderCounter>
    {
        protected override Field WhiteField()
        {
            return new Field(1, 4,3);
        }

        [Test]
        public void should_return_raw_distance()
        {
            _distance.ShouldEqual(3);
        }
    }

    public class when_getting_raw_distance_from_upper_border_to_field_on_lossing_position : observations_for_raw_distance_counting<DistanceToUpperBorderCounter>
    {
        protected override Field WhiteField()
        {
            return new Field(1, 6, 3);
        }

        [Test]
        public void should_return_distance_between_borders()
        {
            _distance.ShouldEqual(4);
        }
    }

    public class when_getting_raw_distance_from_bottom_border_to_field_one_row_from_winning_border : observations_for_raw_distance_counting<DistanceToBottomBorderCounter>
    {
        protected override Field WhiteField()
        {
            return new Field(1, 4, 3);
        }

        [Test]
        public void should_return_one()
        {
            _distance.ShouldEqual(1);
        }
    }

    public class when_getting_raw_distance_from_bottom_border_to_field_on_bottom_border : observations_for_raw_distance_counting<DistanceToBottomBorderCounter>
    {
        protected override Field WhiteField()
        {
            return new Field(1,5,3);
        }

        [Test]
        public void should_return_zero()
        {
            _distance.ShouldEqual(0);
        }
    }
}