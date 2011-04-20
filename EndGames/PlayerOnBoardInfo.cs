using System;
using System.Diagnostics;

namespace Phutball
{
    public class PlayerOnBoardInfo 
    {
        private Stopwatch _timer;

        public PlayerOnBoardInfo(Player player)
        {
            Player = player;
            _timer = new Stopwatch();
        }

        public Player Player { get; set; }
        
        public TimeSpan TimeOnMoves 
        { 
            get { return _timer.Elapsed; }
        }

        public void StopMoving()
        {
            Player.IsOnTheMove = false;
            _timer.Stop();
        }

        public void StartMoving()
        {
            Player.IsOnTheMove = true;
            _timer.Start();
        }

        public void ClearTime()
        {
            _timer.Reset();
        }
    }
}