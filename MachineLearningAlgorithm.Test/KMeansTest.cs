using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimdTestCore;

namespace MachineLearningAlgorithm.Test
{
    [TestClass]
    public class KMeansTest
    {
        [TestMethod]
        public void CalculateTest()
        {
            var array0 = new[] { 1.0, -1 };
            //var array1 = new[] { 1.0, -1 };
            var array4 = new[] { 1.0, 12 };
            var array2 = new[] { -1.1, 11 };
            var array3 = new[] { -1.2, -4 };
            var array1 = new[] { 1.3, -1 };

            var arraies = new[] { array0, array1, array2, array3, array4 };

            var kmeans = new KMeans(3);
            var result = kmeans.Calculate(arraies);
        }
    }
}
