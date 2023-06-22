using UnityEngine;
using SimCovidAPI;
using System.Threading.Tasks;

namespace SimCovid.Core.GameManagement
{
    public partial class GameManager : MonoBehaviour
    {
        private class InitializationResourceLoader : ResourceLoader
        {
            public InitializationResourceLoader(string name, long operations) : base(name, operations)
            {
            }

            public override async Task LoadAllAsync()
            {
                await base.LoadAllAsync();
                Debug.Log("Loading Resources: " + DoneOperations + "/" + Operations);
            }
        }
    }
}