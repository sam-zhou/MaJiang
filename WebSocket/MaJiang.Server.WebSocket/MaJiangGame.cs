using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MaJiang.Core;
using MaJiang.Extention;
using MaJiang.Model;
using MaJiang.Model.Enums;
using MaJiang.Model.EventArgs;
using MaJiang.WebSocket.Core;
using MaJiang.WebSocket.Core.Server;


namespace MaJiang.Server.WebSocket
{
    public class MaJiangGame : WebSocketBehavior
    {
        private string _name;
        private static int _number = 0;
        private readonly string _prefix;
        private string _room;
        private Player _player;
        private Game _game;


        public Dictionary<string, Game> Games { get; set; } 

        public MaJiangGame()
            : this(null)
        {
        }

        public MaJiangGame(string prefix)
        {
            _prefix = !prefix.IsNullOrEmpty() ? prefix : "anon#";
        }

        private string GetName()
        {
            var name = Context.QueryString["name"];
            return !name.IsNullOrEmpty() ? name : _prefix + GetNumber();
        }

        private string GetRoom()
        {
            var room = Context.QueryString["room"];
            return !room.IsNullOrEmpty() ? room : "1";
        }

        private static int GetNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Sessions.Broadcast(String.Format("{0} got logged off...", _name));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Initial")
            {
                Sessions.Broadcast(String.Format("{0}: connected to room {1}", _name, _room));
            }
            else
            {
                int rank;
                Suit suit = Suit.Bamboo;
                if (e.Data.ToLower()[0] == 'b')
                {
                    suit = Suit.Bamboo;
                }
                else if (e.Data.ToLower()[0] == 'c')
                {
                    suit = Suit.Character;
                }
                else if (e.Data.ToLower()[0] == 'd')
                {
                    suit = Suit.Dot;
                }

                switch (e.Data[1])
                {
                    case '1':
                        rank = 1;
                        break;
                    case '2':
                        rank = 2;
                        break;
                    case '3':
                        rank = 3;
                        break;
                    case '4':
                        rank = 4;
                        break;
                    case '5':
                        rank = 5;
                        break;
                    case '6':
                        rank = 6;
                        break;
                    case '7':
                        rank = 7;
                        break;
                    case '8':
                        rank = 8;
                        break;
                    case '9':
                        rank = 9;
                        break;
                    default:
                        rank = 1;
                        break;
                }
                var tile = new Tile(suit, rank);
                _player.Discard(tile);
                Sessions.Broadcast(string.Format("{0} discard: {1}", _name, tile));
            }
            
        }

        protected override void OnError(ErrorEventArgs e)
        {
            
        }

        protected override void OnOpen()
        {
            _name = GetName();
            _room = GetRoom();

            if (!Games.ContainsKey(_room))
            {
                _game = new Game();
                Games.Add(_room, _game);
            }
            else
            {
                _game = Games[_room];
            }

            _game.GameStarted += GameOnGameStarted;
            _game.PlayerInitalWin += GameOnPlayerInitalWin;

            _player = Games[_room].Players.FirstOrDefault(q => q.Id == ID);

            if (_player == null)
            {
                _player = new Player(_name, ID);
                _game.Join(_player);
                Sessions.SendTo(string.Format("Joining room {0}", _room), ID);
            }
        }

        private void GameOnPlayerInitalWin(object sender, PlayerInitialWinEventArgs e)
        {
            if (e.Type == InitialWinType.DaSiXi)
            {
                foreach (var meld in e.Melds)
                {
                    Sessions.Broadcast(_player.Name + " 大四喜: " + meld.Tiles.First());
                }
            }
            else if (e.Type == InitialWinType.LiuLiuShun)
            {
                Sessions.Broadcast(_player.Name + " 六六顺: " + String.Join(", ", e.Melds.Select(q => q.Tiles.First())));
            }
            else if (e.Type == InitialWinType.QueYiSe)
            {
                Sessions.Broadcast(_player.Name + " 缺一色: " + String.Join(", ", e.LackSuits.Select(q => q.GetAttribute<DescriptionAttribute>().Description)));
            }
            else if (e.Type == InitialWinType.BanBanHu)
            {
                Sessions.Broadcast(_player.Name + " 板板胡");
            }
        }

        private void GameOnGameStarted(object sender, EventArgs e)
        {
            Sessions.Broadcast("Game started");
            Sessions.SendTo(_player.ToString(), ID);
        }
    }
}
