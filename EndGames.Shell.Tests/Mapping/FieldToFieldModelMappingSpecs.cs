using EndGames.Phutball;
using EndGames.Shell.Mapping;
using EndGames.Shell.Models;
using NUnit.Framework;
using Rhino.Mocks;
using EndGames.Shell.Tests.SpecificationExtensions;

namespace EndGames.Shell.Tests.Mapping
{
    public abstract class observations_for_mapping_field_to_field_model : observations_for_mapping<Field, FieldModel>
    {
        protected readonly int ColumnCount = 10;
        protected readonly int RowCount = 10;

        protected override void EstablishContext()
        {
            ConfigureMappings();
            Dependency<IPhutballOptions>().Stub(options => options.ColumnCount).Return(ColumnCount).Repeat.Any();
            Dependency<IPhutballOptions>().Stub(options => options.RowCount).Return(RowCount).Repeat.Any();
        }

        private void ConfigureMappings()
        {
            IncludeMapping<FieldToFieldModelMapping>();
            IncludeMapping<SystemToWpfTypeMapping>();
            IncludeMapping<StoneToStoneModelMapping>();
        }
    }

    public class when_mapping_first_field_to_model : observations_for_mapping_field_to_field_model
    {
        protected override Field GetSource()
        {
            return new Field(0,0,0);
        }

        [Test]
        public void should_horizontal_up_part_to_not_visible()
        {
            Mapped.Lines.Up.ShouldNotBeVisible();
        }

        [Test]
        public void should_map_horizontal_down_part_to_not_visible()
        {
            Mapped.Lines.Down.ShouldNotBeVisible();
        }

        [Test]
        public void should_map_vertical_left_part_to_not_visible()
        {
            Mapped.Lines.Left.ShouldNotBeVisible();
        }

        [Test]
        public void should_map_vertical_right_part_to_not_visible()
        {
            Mapped.Lines.Right.ShouldNotBeVisible();
        }


        [Test]
        public void should_not_throw_any_exception()
        {
            assert_no_exception_was_thrown_during_matching();
        }

        [Test]
        public void should_have_proper_mapper_configuration()
        {
            assert_mapper_configuration_is_valid();
        }
    }

    public class when_mapping_left_upper_field_to_model : observations_for_mapping_field_to_field_model
    {
        protected override Field GetSource()
        {
            return new Field(0, 1,1);
        }

        [Test]
        public void should_map_left_line_to_not_visible()
        {
            Mapped.Lines.Left.ShouldNotBeVisible();
        }

        [Test]
        public void should_map_vertical_right_line_to_visible()
        {
            Mapped.Lines.Right.ShouldBeVisible();
        }

        [Test]
        public void should_map_horizontal_up_line_to_not_visible()
        {
            Mapped.Lines.Up.ShouldNotBeVisible();
        }

        [Test]
        public void should_map_horizontal_down_line_to_be_visible()
        {
            Mapped.Lines.Down.ShouldBeVisible();
        }
    }

    public class when_mapping_right_bottom_field_to_model : observations_for_mapping_field_to_field_model
    {
        protected override Field GetSource()
        {
            return new Field(1, RowCount - 2, ColumnCount - 2);
        }

        [Test]
        public void should_map_vertical_left_line_to_visible()
        {
            Mapped.Lines.Left.ShouldBeVisible();
        }

        [Test]
        public void should_map_vertical_right_line_to_hidden()
        {
            Mapped.Lines.Right.ShouldNotBeVisible();
        }

        [Test]
        public void should_map_horizontal_up_line_to_visible()
        {
            Mapped.Lines.Up.ShouldBeVisible();
        }

        [Test]
        public void should_map_horizontal_down_to_hidden()
        {
            Mapped.Lines.Down.ShouldNotBeVisible();
        }
    }
}