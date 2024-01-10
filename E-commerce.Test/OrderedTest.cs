using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace E_commerce.Test
{
    public class OrderedTest : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var sortedlist = new SortedDictionary<int, List<ITestCase>>();

            foreach (var testCase in testCases)
            {
                int priority = 0;

                foreach (var Attribute in testCase.TestMethod.Method.GetCustomAttributes(typeof(AttributePriority).AssemblyQualifiedName))
                {
                    priority = Attribute.GetNamedArgument<int>("Priority");

                    GetOrCreate(sortedlist, priority).Add(testCase);
                }
            }

            foreach (var List in sortedlist.Keys.Select(Priority => sortedlist[Priority]))
            {
                List.Sort((x,y) => StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name,y.TestMethod.Method.Name));
                foreach (TTestCase testCase in List)
                {
                    yield return testCase;
                }
            }
        }

         static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new ()
        {

            TValue Value;

            if(dictionary.TryGetValue(key, out Value)) { return Value; }

            Value = new TValue();

            dictionary[key] = Value;

            return Value;


        
        }
    }
}
