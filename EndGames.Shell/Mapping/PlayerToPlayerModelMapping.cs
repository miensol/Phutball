using System;
using AutoMapper;
using Phutball.Shell.Models;
using Phutball.Utils;

namespace Phutball.Shell.Mapping
{
    public class PlayerToPlayerModelMapping : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<Player, PlayerModel>();
            Mapper.CreateMap<PlayerOnBoardInfo, PlayerOnBoardModel>();
            Mapper.CreateMap<TimeSpan, string>()
                .ConvertUsing(ts=> ts.ToMinutesAndSeconds());
        }
    }
}