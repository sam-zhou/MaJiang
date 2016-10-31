using System.Collections.Generic;
using System.Linq;
using MaJiang.Model;
using MaJiang.Model.Enums;

namespace MaJiang.Core.WinProcessors
{
    public abstract class WinProcessor : IWinProcessor
    {
        private List<MeldCollection> _possibleSets;

        private List<MeldCollection> _winningMelds;

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

        protected WinProcessor(IEnumerable<Tile> tilesOnHand, IEnumerable<Tile> draws)
        {
            TilesOnHand = tilesOnHand;
            Draws = draws;
        }



        public abstract IEnumerable<MeldCollection> Validate();

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

        private IEnumerable<MeldCollection> GetTripletMeldCollections(MeldCollection meldCollection)
        {
            var output = new List<MeldCollection>
            {
                meldCollection
            };


            var tilesWithTripletOccurance = meldCollection.TilesLeft.Where(q => meldCollection.TilesLeft.Count(p => p.Rank.Equals(q.Rank) && p.Suit.Equals(q.Suit)) >= 3);

            foreach (var tile in tilesWithTripletOccurance)
            {
                var newMeldCollection = new MeldCollection(meldCollection.Melds.ToList(), meldCollection.TilesLeft.ToList(), meldCollection.Draw);

                newMeldCollection.CreateMeld(new Meld
                {
                    Tiles = new List<Tile>
                        {
                            tile, tile, tile
                        },
                    Type = MeldType.Triplet
                });

                output.Add(newMeldCollection);
                var possbileMeldCollectionList = GetTripletMeldCollections(newMeldCollection);
                output.AddRange(possbileMeldCollectionList);

            }

            return output;
        }

        private IEnumerable<MeldCollection> GetSequanceMeldCollections(MeldCollection meldCollection)
        {
            var output = new List<MeldCollection>
            {
                meldCollection
            };


            return output;
        }

        private IEnumerable<MeldCollection> GetEyeMeldCollections(MeldCollection meldCollection)
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
    }
}
