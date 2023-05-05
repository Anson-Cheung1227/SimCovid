using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SimCovidAPI
{
    public abstract class ResourceLoader
    {
        public string Name { get; }
        public long Operations { get; }
        public long DoneOperations { get; protected set; }
        protected List<ILoadOperation> OperationsList = new List<ILoadOperation>();

        protected ResourceLoader(string name, long operations)
        {
            Name = name;
            Operations = operations;
            DoneOperations = 0;
        }

        public virtual void LoadAll()
        {
            foreach (ILoadOperation loadOperation in OperationsList)
            {
                loadOperation.Load();
            }
        }

        public virtual async Task LoadAllAsync()
        {
            List<Task> tasks = new List<Task>();
            foreach (ILoadOperation loadOperation in OperationsList)
            {
                tasks.Add(loadOperation.LoadAsync());
            }

            while (DoneOperations < Operations)
            {
                Task finishedTask = await Task.WhenAny(tasks);
                ++DoneOperations;
                await finishedTask;
                tasks.Remove(finishedTask);
            }
        }
    }
}
