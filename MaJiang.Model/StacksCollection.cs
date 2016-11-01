using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Extention;
using MaJiang.Model.EventArgs;
using MaJiang.Model.Interfaces;

namespace MaJiang.Model
{
    public class StacksCollection: ICountableTileCollection, ICountableStackCollection, ISelectableCollection
    {
        private readonly TilesCollection _tilesCollection;

        public TilesCollection TilesCollection
        {
            get { return _tilesCollection; }
        } 

        public StacksCollection()
        {
            _tilesCollection = new TilesCollection();
            _tilesCollection.TileRemoved += TilesCollectionOnTileRemoved;
            _tilesCollection.TileShuffled += TilesCollectionOnTileShuffled;
        }

        private void TilesCollectionOnTileShuffled(object sender, System.EventArgs e)
        {
            Stacks = _tilesCollection.GetStacks();
        }

        private void TilesCollectionOnTileRemoved(object sender, TileRemovedEventArgs e)
        {
            var count = 0;
            foreach (var stack in Stacks)
            {
                if (e.Index >= count && e.Index < count + stack.Count)
                {
                    stack.RemoveTileAt(e.Index - count);
                    break;
                }
                count += stack.Count;
            }

            for (var i = 0; i < Stacks.Count; i++)
            {
                if (Stacks[i].IsEmpty)
                {
                    Stacks.RemoveAt(i);
                    break;
                }
            }
        }

        private List<Stack> _stacks;

        public List<Stack> Stacks
        {
            get
            {
                if (_stacks == null)
                {
                    _stacks = new List<Stack>();
                }
                return _stacks;
            }
            set { _stacks = value; }
        }

        public void Shuffle()
        {
            _tilesCollection.Shuffle();
        }

        public int Count
        {
            get { return _tilesCollection.Count; }
        }

        public int StackCount
        {
            get { return Stacks.Count; }
        }

        public Tile GetNextTile()
        {
            return _tilesCollection.GetTileAt(0);
        }

        public Tile GetTileFromLastStack()
        {
            var count = 0;
            if (StackCount > 1)
            {
                for (int i = 0; i < Stacks.Count - 1; i++)
                {
                    count += Stacks[i].Count;
                }
            }
            else if (StackCount < 1)
            {
                throw new Exception("There is no stack available");
            }

            var tile = _tilesCollection.GetTileAt(count);

            return tile;
        }

        public Tile GetTileAt(int index)
        {
            var tile = _tilesCollection.GetTileAt(index);

            return tile;
        }

        public List<Tile> GetNextTiles(int numberOfTiles)
        {
            var output = new List<Tile>();
            for (int i = 0; i < numberOfTiles; i++)
            {
                output.Add(GetNextTile());
            }
            return output;
        }

        public List<Tile> GetKongTiles()
        {
            var output = new List<Tile>();


            var dice1 = RandomHelper.GetRandomDice();
            Console.WriteLine(dice1);

            var dice2 = RandomHelper.GetRandomDice();
            Console.WriteLine(dice2);
            var dice = dice1 + dice2;

            if (StackCount >= 1)
            {
                if (StackCount < dice)
                {
                    if (dice1 > dice2)
                    {
                        dice = dice2;
                    }
                    else
                    {
                        dice = dice1;
                    }

                    while (StackCount < dice)
                    {
                        dice = RandomHelper.GetRandomDice();
                    }
                }

                var count = 0;
                var stackIndex = StackCount - dice;
                if (StackCount > 1)
                {
                    for (int i = 0; i <= stackIndex; i++)
                    {


                        if (i < stackIndex - 1)
                        {
                            count += Stacks[i].Count;
                        }
                        else
                        {
                            var stackCount = Stacks[i].Count;

                            for (int j = 0; j < stackCount; j++)
                            {
                                output.Add(_tilesCollection.GetTileAt(count));
                            }
                        }
                    }
                }
                else if (StackCount < 1)
                {
                    throw new Exception("There is no stack available");
                }



            }
            else
            {
                throw new Exception("No available stack for kong");
            }


            return output;
        }

        public override string ToString()
        {
            var output = string.Empty;

            foreach (var stack in Stacks)
            {
                if (output != string.Empty)
                {
                    output += "\r\n";
                }
                output += stack.ToString();
            }

            return output;
        }
    }
}
