using System;
using System.Collections.Generic;
using StIves.Interfaces;
using Utils;

namespace StIvesLib.Inns
{
    public class InnFactory : IInnFactory
    {
        public InnFactory(int numberOfPlayers)
        {
            _numberOfPlayers = numberOfPlayers;
            switch (numberOfPlayers)
            {
                case 2:
                    BedTable = new Dictionary<BedNumberType, int>()
                        {
                            {BedNumberType.A, 5},
                            {BedNumberType.B, 7},
                            {BedNumberType.C, 10},
                            {BedNumberType.D, 13}
                        };
                    break;
                case 3:
                    BedTable = new Dictionary<BedNumberType, int>()
                        {
                            {BedNumberType.A, 9},
                            {BedNumberType.B, 11},
                            {BedNumberType.C, 13},
                            {BedNumberType.D, 16}
                        };
                    break;
                case 4:
                    BedTable = new Dictionary<BedNumberType, int>()
                        {
                            {BedNumberType.A, 12},
                            {BedNumberType.B, 17},
                            {BedNumberType.C, 20},
                            {BedNumberType.D, 23}
                        };
                    break;
                case 5:
                    BedTable = new Dictionary<BedNumberType, int>()
                        {
                            {BedNumberType.A, 14},
                            {BedNumberType.B, 18},
                            {BedNumberType.C, 21},
                            {BedNumberType.D, 24},
                        };
                    break;
            }
        }

        public IEnumerable<IInnOption> InnSet
        {
            get
            {
                var result = new List<InnOption>(_numberOfPlayers * 24);
                foreach (BedNumberType bedNumberType in Enum.GetValues(typeof(BedNumberType)))
                {
                    result.AddRange(AddInnOption(1, bedNumberType, 3));
                    result.AddRange(AddInnOption(2, bedNumberType, 3));
                    result.AddRange(AddInnOption(3, bedNumberType, 3));
                    result.AddRange(AddInnOption(4, bedNumberType, 2));
                    result.AddRange(AddInnOption(5, bedNumberType, 1));
                }
                if (_numberOfPlayers > 2)
                {
                    foreach (BedNumberType bedNumberType in Enum.GetValues(typeof(BedNumberType)))
                    {
                        result.AddRange(AddInnOption(1, bedNumberType, 1));
                        result.AddRange(AddInnOption(2, bedNumberType, 2));
                        result.AddRange(AddInnOption(3, bedNumberType, 2));
                        result.AddRange(AddInnOption(4, bedNumberType, 1));
                    }
                }
                if (_numberOfPlayers > 3)
                {
                    foreach (BedNumberType bedNumberType in Enum.GetValues(typeof(BedNumberType)))
                    {
                        result.AddRange(AddInnOption(1, bedNumberType, 1));
                        result.AddRange(AddInnOption(2, bedNumberType, 2));
                        result.AddRange(AddInnOption(3, bedNumberType, 2));
                        result.AddRange(AddInnOption(4, bedNumberType, 1));
                    }
                }
                if (_numberOfPlayers > 4)
                {
                    foreach (BedNumberType bedNumberType in Enum.GetValues(typeof(BedNumberType)))
                    {
                        result.AddRange(AddInnOption(1, bedNumberType, 1));
                        result.AddRange(AddInnOption(2, bedNumberType, 1));
                        result.AddRange(AddInnOption(3, bedNumberType, 2));
                        result.AddRange(AddInnOption(4, bedNumberType, 1));
                        result.AddRange(AddInnOption(5, bedNumberType, 1));
                    }
                }
                result.Shuffle();
                return result;
            }
        }

        private IEnumerable<InnOption> AddInnOption(int minimumPrice, BedNumberType bedNumberType, int count)
        {
            var nrBeds = BedTable[bedNumberType];
            for (var i = 0; i < count; i++)
            {
                yield return new InnOption(minimumPrice, nrBeds);
            }
        }


        private readonly IDictionary<BedNumberType, int> BedTable;
        private readonly int _numberOfPlayers;
    }
}
