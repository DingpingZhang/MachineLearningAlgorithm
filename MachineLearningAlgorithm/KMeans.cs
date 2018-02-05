using System;
using System.Collections.Generic;
using System.Linq;

namespace SimdTestCore
{
    public class KMeans
    {
        private readonly double[][] _centralPoints;

        private int[] _labels;
        private bool _isComplete;

        public int K { get; }

        public KMeans(int k)
        {
            K = k;
            _centralPoints = new double[k][];
        }

        public int[] Calculate(IList<double[]> dataSet)
        {
            if (K > dataSet.Count) throw new ArgumentException($"k is {K}, the count of data set is {dataSet.Count}.");

            Initialize(dataSet);

            while (!_isComplete)
            {
                _isComplete = true;
                for (int i = 0; i < dataSet.Count; i++)
                {
                    RecordLabels(i, GetMinIndex(_centralPoints.Select(centralPoint => GetVectorDistance(centralPoint, dataSet[i]))));
                }
                for (int i = 0; i < K; i++)
                {
                    var vectors = from pair in _labels.Zip(dataSet, (label, vector) => (label, vector))
                                  where pair.label == i
                                  select pair.vector;
                    if (!vectors.Any()) continue;
                    _centralPoints[i] = GetMeanVector(vectors);
                }
            }
            return _labels;
        }

        private double[] GetMeanVector(IEnumerable<double[]> vectors)
        {
            var n = vectors.Count();
            return vectors.Aggregate((result, vector) => result.Select((item, i) => item + vector[i]).ToArray())
                          .Select(item => item / n)
                          .ToArray();
        }

        private void Initialize(IList<double[]> dataSet)
        {
            _isComplete = false;
            _labels = new int[dataSet.Count];
            for (int i = 0; i < K; i++)
            {
                _centralPoints[i] = dataSet[i];
            }
        }

        private double GetVectorDistance(double[] x, double[] y)
        {
            if (x.Length != y.Length) throw new ArgumentException($"{nameof(x)}.Length and {nameof(y)}.Length are different.");

            return x.Zip(y, (xi, yi) => (xi - yi) * (xi - yi)).Sum();
        }

        private int GetMinIndex(IEnumerable<double> array)
        {
            double min = double.MaxValue;
            int index = 0;
            int i = 0;
            foreach (var item in array)
            {
                if (item < min)
                {
                    min = item;
                    index = i;
                }
                i++;
            }
            return index;
        }

        private void RecordLabels(int index, int label)
        {
            if (_isComplete && _labels[index] != label) _isComplete = false;

            _labels[index] = label;
        }
    }
}
