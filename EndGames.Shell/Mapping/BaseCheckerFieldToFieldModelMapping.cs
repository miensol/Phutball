using System;
using System.Collections;
using AutoMapper;
using EndGames.Games;

namespace EndGames.Shell.Mapping
{
    public class BaseCheckerFieldToFieldModelMapping : BoostrapperTask
    {
        public override void Execute()
        {
            Mapper.CreateMap<IField, FieldModel>()
                .Include<BlackField, BlackFieldModel>();


            Mapper.CreateMap<BlackField, BlackFieldModel>()
                .CreateWpfCollectionMapping()
                .ForMember(dst => dst.Color, opt => opt.MapFrom(src => GetColorByPawnType(src.Pawn)));
        }

        private string GetColorByPawnType(IPawn pawn)
        {
            return pawn is BlackPawn ? BlackFieldModel.BlackPawnColor : BlackFieldModel.WhitePawnColor;
        }
    }
}