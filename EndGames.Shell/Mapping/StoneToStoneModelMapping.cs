using AutoMapper;
using EndGames.Phutball;
using EndGames.Shell.Models;
using EndGames.Utils;

namespace EndGames.Shell.Mapping
{
    public class StoneToStoneModelMapping : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<IStone, IStoneModel>()
                .Include<BlackStone, BlackStoneModel>()
                .Include<WhiteStone, WhiteStoneModel>();
            Mapper.CreateMap<BlackStone, BlackStoneModel>();
            Mapper.CreateMap<WhiteStone, WhiteStoneModel>();
        }
    }
}