namespace AMPL_Backend.Service
{
    public class EquationResult
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Totalvalue { get; set; }
        public double TotalMaterial { get; set; }
        public double TotalTime { get; set; }

        public EquationResult(string name, int amount, double valuePerObj, double PerObjMaterial, double PerObjTime)
        {
            Name = name;
            Amount = amount;
            Totalvalue = amount * valuePerObj;
            TotalMaterial = amount * PerObjMaterial;
            TotalTime = amount * PerObjTime;
        }
    }
}
