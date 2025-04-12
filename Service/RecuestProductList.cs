namespace AMPL_Backend.Service
{
    public class RecuestProductList
    {
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
    }
}


//{"name[]":["Chair","Table"],"material[]":["3","4"],"time[]":["0.2","0.5"],"value[]":["20","30"]}