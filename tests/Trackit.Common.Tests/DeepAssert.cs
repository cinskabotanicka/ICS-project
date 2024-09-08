using KellermanSoftware.CompareNetObjects;
using Xunit.Sdk;

namespace Trackit.Common.Tests
{
    public static class DeepAssert
    {
        public static void Equal<T>(T? expected, T? actual, params string[] propertiesToIgnore)
        {
            CompareLogic compareLogic = new()
            {
                Config =
                {
                    MembersToIgnore = new List<string>{ "Start", "End" },
                    IgnoreCollectionOrder = true,
                    IgnoreObjectTypes = true,
                    CompareStaticProperties = false,
                    CompareStaticFields = false
                }
            };

            foreach (var str in propertiesToIgnore) 
                compareLogic.Config.MembersToIgnore.Add(str);

            var comparisonResult = compareLogic.Compare((object)expected!, (object)actual!);
            if (!comparisonResult.AreEqual)
                throw new ObjectEqualException((object)expected!, (object)actual!, comparisonResult.DifferencesString);
        }

        public static void Contains<T>(T? expected, IEnumerable<T>? collection, params string[] propertiesToIgnore)
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));

            CompareLogic compareLogic = new()
            {
                Config =
                {
                    MembersToIgnore = propertiesToIgnore.ToList(),
                    IgnoreCollectionOrder = true,
                    IgnoreObjectTypes = true,
                    CompareStaticProperties = false,
                    CompareStaticFields = false
                }
            };

            if (!collection.Any(item => compareLogic.Compare(expected!, item).AreEqual))
            {
                throw new ContainsException(expected!, collection);
            }
        }
    }
}