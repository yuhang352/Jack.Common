using System.Linq;

namespace YH.Common
{
    public static class NPL
    {
        public static decimal CompareSimilarity(this string[] sourceWords, string[] targetWords)
        {
            var sameWords = sourceWords.Intersect(targetWords);
            var sameWordLen = sameWords.Sum(t => t.Length);
            if (sameWordLen == 0) return 0M;
            var sourceWordLen = sourceWords.Sum(t => t.Length);
            var targetWordLen = targetWords.Sum(t => t.Length);
            int sumWordLength = sourceWordLen + targetWordLen;
            if (sumWordLength == 0) return 0M;
            return (sameWordLen * 2M) / (sumWordLength);
        }
    }
}
