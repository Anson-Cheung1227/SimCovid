using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;
using SimCovidAPI;
using UnityEngine;

namespace SimCovid.Tests.EditMode
{
    public class ResourceLoaderTest
    {
        //Operations
        private class OperationA : ILoadOperation
        {
            public string Name { get; set; }
            public float Operations { get; set; }
            public float DoneOperations { get; set; }
            public MonoBehaviour Operator { get; set; }

            public OperationA()
            {
                Operations = 1;
                DoneOperations = 0;
            }
            public Task Load()
            {
                for (int i = 1; i <= 2; ++i)
                {
                    Task.Delay(1000).Wait();
                    Debug.Log(Name + i * 1000);
                }

                ++DoneOperations;
                return Task.CompletedTask;
            }
        }

        private class TestResourceLoader : ResourceLoader
        {
            public TestResourceLoader(string name, long operations, List<ILoadOperation> operationsList) : base(name, operations)
            {
                OperationsList = operationsList;
            }
        }
        [Test]
        public async Task ResourceLoaderLoadAsync()
        {
            List<ILoadOperation> operations = new List<ILoadOperation>();
            operations.Add(new OperationA{Name = "Operation A"});
            operations.Add(new OperationA{Name = "Operation B"});
            TestResourceLoader loader = new TestResourceLoader("Async Loader", 2, operations);
            LogAssert.Expect(LogType.Log, "Operation A1000");
            LogAssert.Expect(LogType.Log, "Operation B1000");
            LogAssert.Expect(LogType.Log, "Operation A2000");
            LogAssert.Expect(LogType.Log, "Operation B2000");
            await loader.LoadAllAsync();
            LogAssert.Expect(LogType.Log, "Done");
            Debug.Log("Done");
            foreach (OperationA operationA in operations)
            {
                Assert.AreEqual(1, operationA.DoneOperations);
            }
        }
    }
}