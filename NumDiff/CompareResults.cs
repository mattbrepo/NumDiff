using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumDiff
{
    class DifferentCells
    {
        public long Col { get; set; }
        public long Row { get; set; }
    }

    class CompareResults
    {
        public string[] Separators { get; set; }
        public double Tollerance { get; set; }
        public string FilePath1 { get; set; }
        public string FilePath2 { get; set; }
        public long CountLines1 { get; set; }
        public long CountLines2 { get; set; }

        public ConcurrentBag<DifferentCells> Differences { get; set; }
        public ConcurrentBag<string> Errors { get; set; }

        public CompareResults()
        {
            Differences = new ConcurrentBag<DifferentCells>();
            Errors = new ConcurrentBag<string>();
        }
    }
}
