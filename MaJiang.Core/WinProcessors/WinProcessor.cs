using System;
using System.Collections.Generic;
using System.Linq;
using MaJiang.Extention;
using MaJiang.Model;
using MaJiang.Model.Enums;

namespace MaJiang.Core.WinProcessors
{
    public abstract class WinProcessor : IWinProcessor
    {
        private List<MeldCollection> _possibleSets;

        private List<MeldCollection> _winningMelds;

        public abstract WinType Type { get;}

        public IEnumerable<Tile> Draws { get; set; }

        public IEnumerable<Tile> TilesOnHand { get; set; }

        public IEnumerable<MeldCollection> PossibleSets
        {
            get
            {
                if (_possibleSets == null)
                {
                    _possibleSets = new List<MeldCollection>();

                    foreach (var draw in Draws)
                    {
                        var possibleSet = TilesOnHand.ToList();
                        possibleSet.Add(draw);
                        _possibleSets.Add(new MeldCollection(null, possibleSet, draw));
                    }

                    
                }
                return _possibleSets;
            }
        }

        public List<MeldCollection> WinningMelds
        {
            get
            {
                if (_winningMelds == null)
                {
                    _winningMelds = new List<MeldCollection>();
                }
                return null;
            }
        } 

        protected WinProcessor()
        {
            
        }

        protected abstract List<WinningTile> Process();


        public virtual List<WinningTile> Validate(IEnumerable<Tile> tilesOnHand, IEnumerable<Tile> draws)
        {
            TilesOnHand = tilesOnHand;
            Draws = draws;
            var time = DateTime.Now.Ticks;
            var result = Process();
            Console.WriteLine("Time Spent on {0} calculation was: {1}", Type, DateTime.Now.Ticks-time);
            return result;
        }
        

        protected IEnumerable<MeldCollection> GetMeldCollections(MeldCollection meldCollectionWithSameSuit)
        {
            var output = new List<MeldCollection>();

            var count = meldCollectionWithSameSuit.TilesLeft.Count;

            if (count%3 == 0)
            {
                var possibleMeldCollections = GetTripletMeldCollections(meldCollectionWithSameSuit);

                foreach (var possibleMeldCollection in possibleMeldCollections)
                {
                    if (possibleMeldCollection.Successful)
                    {
                        output.Add(possibleMeldCollection);
                    }
                    else
                    {
                        var meldCollections = GetSequanceMeldCollections(possibleMeldCollection);
                        foreach (var meldCollection in meldCollections)
                        {
                            if (meldCollection.Successful)
                            {
                                output.Add(meldCollection);
                            }
                        }
                    }
                }
            }
            else if (count%3 == 2)
            {
                var possibleMeldCollections = GetEyeMeldCollections(meldCollectionWithSameSuit);
                foreach (var possibleMeldCollection in possibleMeldCollections)
                {
                    if (possibleMeldCollection.Successful)
                    {
                        output.Add(possibleMeldCollection);
                    }
                    else
                    {
                        var meldCollections = GetMeldCollections(possibleMeldCollection);
                        foreach (var meldCollection in meldCollections)
                        {
                            if (meldCollection.Successful)
                            {
                                output.Add(meldCollection);
                            }
                        }
                    }
                }
            }
            return output;
        }

        protected IEnumerable<MeldCollection> GetTripletMeldCollections(MeldCollection meldCollection, IList<Tile> selectedTiles = null)
        {
            var output = new List<MeldCollection>();

            if (selectedTiles == null)
            {
                var tilesWithTripletOccurance =
                meldCollection.TilesLeft.Where(
                    q => meldCollection.TilesLeft.Count(p => p.GetHashCode() == q.GetHashCode()) >= 3)
                    .Distinct()
                    .ToList();

                var exceptionChildList = tilesWithTripletOccurance.GetChildSets();
                foreach (var list in exceptionChildList)
                {
                    output.AddRange(GetTripletMeldCollections(new MeldCollection(meldCollection.Melds.ToList(), meldCollection.TilesLeft.ToList(), meldCollection.Draw), list));
                }
            }
            else
            {
                var newMeldCollection = new MeldCollection(meldCollection.Melds.ToList(),
                        meldCollection.TilesLeft.ToList(), meldCollection.Draw);

                foreach (var tile in selectedTiles)
                {
                    

                    newMeldCollection.CreateMeld(new Meld
                    {
                        Tiles = new List<Tile>
                        {
                            tile,
                            tile,
                            tile
                        },
                        Type = MeldType.Triplet
                    });

                    
                }
                output.Add(newMeldCollection);
            }


            return output;
        }

        protected IEnumerable<MeldCollection> GetSequanceMeldCollections(MeldCollection meldCollection)
        {
            var output = new List<MeldCollection>
            {
                meldCollection
            };


            return output;
        }

        protected IEnumerable<MeldCollection> GetEyeMeldCollections(MeldCollection meldCollection)
        {
            var output = new List<MeldCollection>();

            var tilesWithDoubleOccurance = meldCollection.TilesLeft.Where(q => meldCollection.TilesLeft.Count(p => p.Rank.Equals(q.Rank) && p.Suit.Equals(q.Suit)) >= 2).Distinct();

            foreach (var tile in tilesWithDoubleOccurance)
            {
                var newMeldCollection = new MeldCollection(meldCollection.Melds.ToList(), meldCollection.TilesLeft.ToList(), meldCollection.Draw);

                newMeldCollection.CreateMeld(new Meld
                {
                    Tiles = new List<Tile>
                        {
                            tile, tile
                        },
                    Type = MeldType.Eye
                });

                output.Add(newMeldCollection);
            }

            return output;
        }

        protected List<MeldCollection> Join(List<MeldCollection> list1, List<MeldCollection> list2, Tile draw)
        {
            var output = new List<MeldCollection>();

            if (list1.Any() || list2.Any())
            {
                if (!list1.Any())
                {
                    return list2.ToList();
                }

                if (!list2.Any())
                {
                    return list1.ToList();
                }

                foreach (var meldCollection in list1)
                {
                    foreach (var collection in list2)
                    {
                        var mc = new MeldCollection(null, null, draw);
                        mc.Melds.AddRange(meldCollection.Melds);
                        mc.Melds.AddRange(collection.Melds);
                        output.Add(mc);
                    }
                }
            }



            return output;
        }
    }
}
