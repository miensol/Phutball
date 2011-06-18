using AutoMapper;
using Phutball.Shell.Models;
using Phutball.Utils;

namespace Phutball.Shell.Mapping
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