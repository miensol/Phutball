using System;
using AutoMapper;
using EndGames.Phutball;
using EndGames.Shell.Models;
using EndGames.Utils;

namespace EndGames.Shell.Mapping
{
    public class PlayerToPlayerModelMapping : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<Player, PlayerModel>();
            Mapper.CreateMap<PlayerOnBoardInfo, PlayerOnBoardModel>();
            Mapper.CreateMap<TimeSpan, string>()
                .ConvertUsing(ts=> "{0}:{1}".ToFormat(ts.Minutes, ts.Seconds));
        }
    }
}