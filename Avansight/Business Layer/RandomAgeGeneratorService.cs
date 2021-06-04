using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avansight.Business_Layer
{
    public class RandomAgeGeneratorService
    {
        public List<int> RandomAgeGenerator(int minAge, int maxAge, int rndAgeCount)
        {
            var rnd = new Random();
            var intArr = new int[rndAgeCount];
            var ageList = new List<int>();

            for (var i = 0; i < intArr.Length; i++)
            {
                var num = rnd.Next(minAge, maxAge);
                intArr[i] = num;
                ageList.Add(num);
            }

            return ageList;
        }
    }
}
