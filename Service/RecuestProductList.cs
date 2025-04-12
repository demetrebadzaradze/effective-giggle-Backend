namespace AMPL_Backend.Service
{
    public class RecuestProductList
    {
        public double TotalTime { get; set; }
        public double TotalMaterial { get; set; }
        public string[] Name {get; set; }
        public double[] Material { get; set; }
        public double[] Time { get; set; }
        public double[] Value { get; set; }
        public RecuestProductList(string[] name, double[] material, double[]time, double[] value)
        {
            Name = name;
            Material = material;
            Time = time;
            Value = value;
        }
        public Product[] IntoProductList()
        {
            Product[] result = new Product[Name.Length];
            for (int i = 0; i < Name.Length; i++)
            {
                result[i] = new Product(Name[i], Material[i], Time[i], Value[i]);
            }
            return result;
        }
        public double IntoTimeExeption()
        {   
            return TotalTime;
        }
        public double IntoMaterialExeption()
        {
            return TotalMaterial;
        }
    }
}


//{"name[]":["Chair","Table"],"material[]":["3","4"],"time[]":["0.2","0.5"],"value[]":["20","30"]}