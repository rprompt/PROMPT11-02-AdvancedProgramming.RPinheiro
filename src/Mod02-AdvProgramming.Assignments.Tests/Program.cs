using System;
//using System.Linq;
using System.Linq;
using Mod02_AdvProgramming.Data;
using Mod02_AdvProgramming.Utils;
using Mod02_AdvProgramming.Assignments;

namespace Mod02_AdvProgramming.Assignments.Tests {
    class Program {
        static void Main(string[] args)
        {
            //var countries = Ex5.CustomerCountriesSorted();
            //ObjectDumper.Write(countries);
            //Console.WriteLine("Total countries: {0}", countries.Count());

            //var citiesByCountry = Ex5.CustomerCountriesWithCitiesSortedByCountry();
            //ObjectDumper.Write(citiesByCountry, 2);
            //Console.WriteLine("Total countries: {0}", countries.Count());

            //var customerOrders = Ex5.CustomerWithNumOrdersSortedByNumOrdersDescending();
            //ObjectDumper.Write(customerOrders, 2);
            //Console.WriteLine("Total customers: {0}", customerOrders.Count());

            //var totalsByCountry = Ex5.TotalsByCountrySortedByCountry();
            //ObjectDumper.Write(totalsByCountry);

            //first
            //var cust = SampleData.LoadCustomersFromXML().Where(c => c.Country == "Portugal").Select(c => c);
            //var cust1 = SampleData.LoadCustomersFromXML().Where(c => c.Country == "France" || c.Country == "Italy");
            //var cust2 = cust.Concat(cust1).First();

            int[] grades = { 59, 82, 70, 56, 92, 98, 85 };
            string[] chares = { "um","dois","tres" };
            
            //last => var last = grades.Last( x => x < 60);

            //var twhile = grades.TakeWhile(x => x < 83);
            //ObjectDumper.Write(twhile, 0);

            //var take = grades.Take(3);
            //ObjectDumper.Write(take, 0);

            //var skip = grades.Skip(4);
            //ObjectDumper.Write(skip, 0);

            //var skipwhile = grades.SkipWhile(x => x < 92);
            //ObjectDumper.Write(skipwhile, 0);

            //var skipwhile = grades.SkipWhile(x => x < 92);
            //ObjectDumper.Write(skipwhile, 0);

            var mergeequals = grades.Zip(chares,(first,second) => first + " " + second);
            ObjectDumper.Write(mergeequals, 0);

            //var cities = SampleData.LoadCustomersFromXML().Select(c => new { c.City, c.Country }).OrderBy(cc => cc.City);
            //ObjectDumper.Write(cities);

            //var cities = Ex5.TotalsByCountryByPeriodSortedByCountry(Ex5.PeriodRange.Year);
            //ObjectDumper.Write(cities);

        }
    }
}
