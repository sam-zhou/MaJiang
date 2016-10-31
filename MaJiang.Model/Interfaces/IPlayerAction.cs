using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Model.EventArgs;

namespace MaJiang.Model.Interfaces
{
    public interface IPlayerAction
    {
        void Draw(Tile tile);

        void Pong(Tile tile);

        void Kong(Tile tile);

        void Chow(Tile tile);

        void Discard(Tile tile);

        void DiscardByOther(Tile tile);

        event EventHandler<PlayerActionableEventArgs> PlayerActionable;

        event EventHandler<PlayerWinEventArgs> PlayerWin;

        event EventHandler<PlayerInitalWinEventArgs> PlayerInitalWin;

    }
}
