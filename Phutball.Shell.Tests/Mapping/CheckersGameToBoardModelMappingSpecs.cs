using System;
using AutoMapper;
using EndGames.Games;
using EndGames.Shell.Mapping;
using EndGames.Shell.Models;
using NUnit.Framework;
using ForTesting;

namespace EndGames.Shell.Tests.Mapping
{
    public class when_mapping_checkers_game_to_board_model : observations_for_mapping_defined_in<CheckersGameToCheckersBoardModelMapping>
    {
        private CheckersBoardModel _mapped;
        private ICheckersGame _source;

        protected override void Because()
        {
            _mapped = Mapper.Map<ICheckersGame, CheckersBoardModel>(_source);
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();
            _source = new CheckersGame(null,null);
            BoostrapperTask.Run<BaseCheckerFieldToFieldModelMapping>();
        }

        [Test]
        public void should_have_proper_mapper_configuration()
        {
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void mapped_board_should_not_be_empty()
        {
            _mapped.ShouldNotBeNull();
        }
    }
}