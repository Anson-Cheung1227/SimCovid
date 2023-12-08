namespace SimCovidAPI.Infection.Generators
{
    public interface ISpreadableGenerationManager
    {
        /// <summary>
        /// Called when each day passes
        /// </summary>
        public void OnGenerate();
    }
}