using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SimCovidAPI
{
    public abstract class ResourceLoader
    {
        public string Name { get; }
        public long Operations { get; protected set; }
        public long DoneOperations { get; protected set; }
        protected List<ILoadOperation> OperationsList = new List<ILoadOperation>();

        protected ResourceLoader(string name, long operations)
        {
            Name = name;
            Operations = operations;
            DoneOperations = 0;
        }

        public virtual Task LoadAll()
        {
            foreach (ILoadOperation loadOperation in OperationsList)
            {
                Task task = loadOperation.Load();
                task.Wait();
            }

            return Task.CompletedTask;
        }


        public virtual async Task LoadAllAsync()
        {
            List<Task> tasks = new List<Task>();
            foreach (ILoadOperation loadOperation in OperationsList)
            {
                tasks.Add(Task.Run(loadOperation.Load));
            }
            
            while (DoneOperations < Operations)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                ++DoneOperations;
                await finishedTask;
                tasks.Remove(finishedTask);
            }
        }

        public void AddILoadOperation(ILoadOperation loadOperation)
        {
            OperationsList.Add(loadOperation);  
            SetOperations();
        }
        public void SetOperations()
        {
            Operations = OperationsList.Count;
        }
    }
}
