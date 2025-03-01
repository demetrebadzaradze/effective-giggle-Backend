using ampl;
using System.IO;

namespace AMPL_Backend.Service
{
    public class AMPLEquation
    {
        private AMPL a = new AMPL();

        public static string Run()
        {
            AMPL a = new AMPL();
            a.SetOption("solver", "minos");
            return a.GetOption("solver");
        }
        public static bool DefineModelFile(string path, Product[] products, double materialLimit, double timeLimit)      // this is the file that will be used to define the model  // limitation to add like ood  and time
        {
            Product p;
            string[] Params = new string[products.Length];
            for (int i = 0; i < products.Length; i++)       // put this in two loops for this material and price and time thingies.
            {
                p = products[i];
                Params[i] += $"var {p.Name} >= 0; \n";
                Params[i] += $"param {p.Name}material;  \n";
                Params[i] += $"param {p.Name}price;  \n";
                Params[i] += $"param {p.Name}time; \n";
            }

            File.WriteAllLines(path, Params);

            string[] constraints = new string[2];       // 2 cantraints so far material and price


            // put all of this  in a loop
            constraints[0] += $"subject to materialConstaint: ";
            for (int j = 0; j < products.Length; j++)
            {
                constraints[0] += $"{products[j].Name} * {products[j].Name}material ";
                if (j == products.Length - 1)
                {
                    constraints[0] += $" <= {materialLimit};  \n";
                }
                else
                {
                    constraints[0] += $" + ";
                }
            }
            constraints[0] += $"subject to timeConstaint: ";
            for (int j = 0; j < products.Length; j++)
            {
                constraints[0] += $"{products[j].Name} * {products[j].Name}time ";
                if (j == products.Length - 1)
                {
                    constraints[0] += $" <= {timeLimit};  \n";
                }
                else
                {
                    constraints[0] += $" + ";
                }
            }

            File.AppendAllLines(path, constraints);

            string function = "maximize profit: ";

            for (int i = 0; i < products.Length; i++)
            {
                function += $"{products[i].Name} * {products[i].Name}price ";
                if (i == products.Length - 1)
                {
                    function += ";  \n";
                }
                else
                {
                    function += " + ";
                }
            }
            File.AppendAllLines(path, new string[] { function });
            return true;
        }
        public static bool DefineDataFile(string path,Product[] products)
        {
            string[] paramValues = new string[products.Length];
            for(int i = 0; i < products.Length; i++)
            {
                paramValues[i] += $"param {products[i].Name}material := {products[i].Material.ToString().Replace(',','.')}; \n";
                paramValues[i] += $"param {products[i].Name}price := {products[i].Value.ToString().Replace(',', '.')}; \n" ;
                paramValues[i] += $"param {products[i].Name}time := {products[i].Time.ToString().Replace(',', '.')} ;  \n";
            }
            File.WriteAllLines(path, paramValues);
            return true;
        }
    }
}

