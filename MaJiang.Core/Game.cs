using System.Collections.Generic;
using MaJiang.Model;

namespace MaJiang.Core
{
    public class Game
    {
        private Board _board;

        private List<Player> _players; 

        public Board Board
        {
            get
            {
                if (_board == null)
                {
                    _board = new Board();
                }
                return _board;
            }
        }

        public List<Player> Players
        {
            get
            {
                if (_players == null)
                {
                    _players = new List<Player>();
                    AddPlayer(new Player("Player 1"));
                    AddPlayer(new Player("Player 2"));
                    AddPlayer(new Player("Player 3"));
                    AddPlayer(new Player("Player 4"));
                }
                return _players;
            }
        }

        private void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        private void DrawTilesForEachPlayer(int count)
        {
            foreach (var player in Players)
            {
                player.TilesOnHand.InitialDraw(Board.GetNextTiles(count));
            }
        }

        private void IntialiseTilesOnHand()
        {
            DrawTilesForEachPlayer(4);
            DrawTilesForEachPlayer(4);
            DrawTilesForEachPlayer(4);
            DrawTilesForEachPlayer(1);
        }

        public void Shuffle()
        {
            Board.Shuffle();
            
        }

        public void Initialise()
        {
            foreach (var player in Players)
            {
                player.Reset();
            }
            IntialiseTilesOnHand();
        }
    }
}
