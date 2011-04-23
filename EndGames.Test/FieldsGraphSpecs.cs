using ForTesting;
using NUnit.Framework;

namespace Phutball.Tests
{
    public class when_cloning_fields_graph : observations_for_sut_of_type<FieldsGraph>
    {
        private IFieldsGraph _clonedGraph;

        protected override void Because()
        {
            _clonedGraph = (IFieldsGraph) Sut.Clone();
        }

        protected override FieldsGraph CreateSut()
        {
            return new FieldsGraph(new PhutballOptions
                                       {
                                           RowCount = 7,
                                           ColumnCount = 5
                                       });
        }

        [Test]
        public void should_have_all_fields_copied()
        {
            _clonedGraph.GetFields().ShouldHaveCount(35);
        }

        [Test]
        public void should_have_white_field_on_the_same_index_as_original()
        {
            _clonedGraph.GetWhiteField().Id.ShouldEqual(Sut.GetWhiteField().Id);
        }
    }

    public class when_creating_fields_graph : observations_for_auto_created_sut_of_type<FieldsGraph>
    {
        protected override void Because()
        {            
        }
        protected override void EstablishContext()
        {
            ProvideImplementationOf<IPhutballOptions>(new PhutballOptions
                                                          {
                                                              RowCount = 7,
                                                              ColumnCount = 5
                                                          });
        }

        [Test]
        public void should_have_white_field_set_to_proper_value()
        {
            Sut.GetWhiteField().Stone.ShouldBeOfType<WhiteStone>();
        }

        [Test]
        public void should_put_white_stone_on_middle_row()
        {
            Sut.GetWhiteField().RowIndex.ShouldEqual(3);
        }

        [Test]
        public void should_put_white_stone_on_middle_column()
        {
            Sut.GetWhiteField().ColumnIndex.ShouldEqual(2);
        }
    }
}