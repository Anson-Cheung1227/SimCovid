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
        public List<ILoadOperation> OperationsList { get; protected set; }= new List<ILoadOperation>();

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
                ++DoneOperations;
                OperationsList.Remove(loadOperation);
            }
            
            return Task.CompletedTask;
        }


        public virtual async Task LoadAllAsync()
        {
            Dictionary<Task, ILoadOperation> dictionary = new Dictionary<Task, ILoadOperation>();
            foreach (ILoadOperation loadOperation in OperationsList)
            {
                dictionary.Add(loadOperation.Load(), loadOperation);
            }
            
            while (DoneOperations < Operations)
            {
                Task finishedTask = await Task.WhenAny(dictionary.Keys);
                ++DoneOperations;
                await finishedTask;
                OperationsList.Remove(dictionary[finishedTask]);
                dictionary.Remove(finishedTask);
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
