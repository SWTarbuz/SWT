using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATMPart1
{
    class Program
    {
        static void Main(string[] args)
        {

        //Arrange
        string[] formats = { "yyyyMMddHHmmfff" };

        var time1 = DateTime.ParseExact("201712122000250", formats[0], CultureInfo.CurrentCulture);
        var time2 = DateTime.ParseExact("201712122021250", formats[0], CultureInfo.CurrentCulture);

        var UUT = new Track("", 2000, 2000, 0, time1);

        UUT.ChangePosition(5000, 3000, 0, time2); //act
        }
    }
}
