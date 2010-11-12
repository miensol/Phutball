using System;
using AutoMapper;
using EndGames.Games;
using EndGames.Shell.Mapping;
using EndGames.Shell.Models;
using ForTesting;
using NUnit.Framework;

namespace EndGames.Shell.Tests.Mapping
{
    public abstract class observations_for_mapping_defined_in<TMapping> : observations_for_static_sut where TMapping : class, IBoostrapperTask, new()
    {
        protected override void EstablishContext()
        {
            BoostrapperTask.Run<TMapping>();
        }

        protected override void AfterEachObservation()
        {
            Mapper.Reset();
            base.AfterEachObservation();
        }

    }

    public class when_maping_checker_field_to_field_model : observations_for_mapping_defined_in<BaseCheckerFieldToFieldModelMapping>
    {
        private IField _source;
        private BlackFieldModel _mapped;

        protected override void Because()
        {
            _mapped = (BlackFieldModel) Mapper.Map<IField, FieldModel>(_source);
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            _source = new BlackField(New.Int())
                          {
                              Pawn = new BlackPawn(null)
                          };
        }

        [Test]
        public void should_have_proper_mapper_configuration()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void should_map_has_pawn_from_field_pawn_property()
        {
            _mapped.HasPawn.ShouldBeTrue();
        }

        [Test]
        public void should_map_color_according_to_pawn_type()
        {
            _mapped.Color.ShouldEqual(BlackFieldModel.BlackPawnColor);
        }

    }
}