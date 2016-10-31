using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaJiang.Model.Interfaces;

namespace MaJiang.Model
{
    public class Board: ICountableTileCollection, ICountableStackCollection, ISelectableCollection
    {
        private readonly StacksCollection _stacksCollection;

        public int Count
        {
            get { return _stacksCollection.Count; }
        }

        public int StackCount
        {
            get { return _stacksCollection.StackCount; }
        }

        public StacksCollection StacksCollection
        {
            get { return _stacksCollection; }
        }


        public Board()
        {
            _stacksCollection = new StacksCollection();
        }



        public Tile GetNextTile()
        {
            return _stacksCollection.GetNextTile();
        }

        public Tile GetTileAt(int index)
        {
            return _stacksCollection.GetTileAt(index);
        }

        public Tile GetTileFromLastStack()
        {
            return _stacksCollection.GetTileFromLastStack();
        }

       

        public void Shuffle()
        {
            _stacksCollection.Shuffle();
            
        }        

        public List<Tile> GetNextTiles(int numberOfTiles)
        {
            return _stacksCollection.GetNextTiles(numberOfTiles);
        }

        public List<Tile> GetKongTiles()
        {
            return _stacksCollection.GetKongTiles();
        }

        public override string ToString()
        {
            var output = StacksCollection.TilesCollection.ToString();


            return output;
        }
    }
}
