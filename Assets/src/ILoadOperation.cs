using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ILoadOperation
{
    public string Name { get; set; }
    public float Operations { get; set; }
    public float DoneOperations { get; set; }
    public MonoBehaviour Operator { get; set; }
    public abstract void Load();
}
