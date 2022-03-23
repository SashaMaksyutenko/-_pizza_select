using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace JsonWork
{
    public class PizzaIngredients : IEquatable<PizzaIngredients?>
    {
        public string[] toppings { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as PizzaIngredients);
        }

        public bool Equals(PizzaIngredients? other)
        {
            IStructuralEquatable se1 = toppings;
            return other != null &&
                   se1.Equals(other.toppings, StructuralComparisons.StructuralEqualityComparer);
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();

            foreach (object o in toppings)
            {
                sb.Append(o.GetHashCode());
                sb.Append(";");
            }

            return sb.ToString().GetHashCode();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "Pizzas.json";
            string jsonString = File.ReadAllText(fileName);

            List<PizzaIngredients> pizzass = JsonSerializer.Deserialize<List<PizzaIngredients>>(jsonString)!;

            Dictionary<PizzaIngredients, int> pizzasCount = new Dictionary<PizzaIngredients, int>();

            foreach (var item in pizzass)
            {
                if(pizzasCount.Keys.Contains(item))
                {
                    pizzasCount[item]++;
                }
                else
                {
                    pizzasCount[item] = 1;
                }
            }

            var SortedPizzas = pizzasCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            int cnt = 0;
            foreach (var item in SortedPizzas)
            {
                Console.Write("pizza ingredients: ");
                foreach (var it in item.Key.toppings)
                {
                    Console.Write(it + " ");
                }
                Console.WriteLine();
                Console.WriteLine($"the quantity of orders: {item.Value};");
                cnt++;
                if (cnt == 20)
                    break;
            }
        }
    }
}