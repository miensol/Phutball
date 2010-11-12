using System;
using System.Collections.Generic;
using QuickGraph;

namespace EndGames.Games
{
    public class CheckersGame : ICheckersGame
    {
        public const int BoardSize = 8;
        public const int FieldCount = BoardSize*BoardSize;
        private IMutableUndirectedGraph<IField, Edge<IField>> _gameGraph = new UndirectedGraph<IField,Edge<IField>>();
        private readonly IPlayer _firstPlayer;
        private readonly IPlayer _secondPlayer;
        private CheckersFieldsFactory _checkersFieldFactory;

        public CheckersGame(IPlayer firstPlayer, IPlayer secondPlayer)
        {
            _firstPlayer = firstPlayer;
            _secondPlayer = secondPlayer;
            _checkersFieldFactory = new CheckersFieldsFactory(BoardSize,_firstPlayer,_secondPlayer);
        }

        private void BuildInitialGameGraph()
        {
            _gameGraph = new UndirectedGraph<IField, Edge<IField>>();
            _gameGraph.AddVertexRange(_checkersFieldFactory.CreateInitialFields());
           
        }

   

     

        protected IPlayer SecondPlayer
        {
            get {
                return _secondPlayer;
            }
        }

        protected IPlayer FirstPlayer
        {
            get { return _firstPlayer; }
        }

       

        public void BuildInitialGraphGame()
        {
            BuildInitialGameGraph();
        }

        public IEnumerable<IField> Fields
        {
            get { return _gameGraph.Vertices; }
        }
    }
}