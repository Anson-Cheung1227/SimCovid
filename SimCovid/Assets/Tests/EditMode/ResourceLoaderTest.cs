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
            public long Operations { get; set; }
            public long DoneOperations { get; set; }
            public MonoBehaviour Operator { get; set; }

            public OperationA()
            {
                Operations = 1;
                DoneOperations = 0;
            }
            public async Task Load()
            {
                for (int i = 1; i <= 2; ++i)
                {
                    await Task.Delay(1000);
                    Debug.Log(Name + i * 1000);
                }

                ++DoneOperations;
            }
        }
        private class OperationB : ILoadOperation
        {
            public string Name { get; set; }
            public long Operations { get; set; }
            public long DoneOperations { get; set; }
            public MonoBehaviour Operator { get; set; }
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
        public void ResourceLoaderLoadSync()
        {
            List<ILoadOperation> operations = new List<ILoadOperation>();
            operations.Add(new OperationB {Name = "Operation A"});
            operations.Add(new OperationB {Name = "Operation B"});
            TestResourceLoader loader = new TestResourceLoader("Sync Loader", 2, operations);
            LogAssert.Expect(LogType.Log, "Operation A1000");
            LogAssert.Expect(LogType.Log, "Operation A2000");
            LogAssert.Expect(LogType.Log, "Operation B1000");
            LogAssert.Expect(LogType.Log, "Operation B2000");
            loader.LoadAll();
            Debug.Log("Done");
            foreach (ILoadOperation loadOperation in operations)
            {
                Assert.AreEqual(1, loadOperation.DoneOperations);
            }
            Assert.AreEqual(2, loader.DoneOperations);
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
            Assert.AreEqual(2, loader.DoneOperations);
        }
    }
}