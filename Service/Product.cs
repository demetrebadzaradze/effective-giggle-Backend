namespace AMPL_Backend.Service
{
    enum ProductName
    {
        OakTable,
        PineChair,
        MahoganyDesk,
        WalnutShelf,
        CedarBench,
        BambooRack,
        MapleCabinet,
        TeakWardrobe,
        BirchDrawer,
        RosewoodCoffeetable
    }
    public class Product
    {
        public string Name { get; set; }
        public double Material { get; set; }
        public double Time { get; set; }
        public double Value { get; set; }

        public Product(string name, double material, double time, double value)
        {
            Name = name;
            Material = material;
            Time = time;
            Value = value;
        }

        public static Product MakeProduct()
        {
            return new Product(GetRandomEnumValue<ProductName>(),
                                Math.Round(new Random().NextDouble() * 10, 2),
                                Math.Round(new Random().NextDouble() * 10, 2),
                                Math.Round(new Random().NextDouble() * 100, 2));

        }
        public EquationResult GetEquationResult(int amountOfProducts)
        {
            return new EquationResult(Name, amountOfProducts, Value, Material, Time);
        }

        private static string GetRandomEnumValue<T>() where T : Enum
        {
            var v = Enum.GetValues(typeof(T));
            return v.GetValue(new Random().Next(v.Length)).ToString() ?? "";
        }
    }
}
