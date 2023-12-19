using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class TableTests
    {
        [TestMethod()]
        public void Book_Date_TrueReturned()
        {
            //Arrange
            var date = new DateTime(2023, 12, 26);
            bool expected = true;
            //act
            Table tb = new Table();
            bool result = tb.Book(date);
            //assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void IsBooked_Date_FalseReturned()
        {
            //Arrange
            var date = new DateTime(2023, 12, 26);
            bool expected = false;
            //act
            Table tb = new Table();
            bool result = tb.IsBooked(date);
            //assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void FreeTables_Date_NullReturned()
        {
            //Arrange
            var date = new DateTime(2023, 12, 26);
            List<string>expected = new List<string>();
            //act
            
            ReservationManagerClass rm = new ReservationManagerClass();
            var result = rm.FreeTables(date);

            //assert
            Assert.AreEqual(result.Count, expected.Count);
        }

        [TestMethod()]
        public void BookTable_ADate6_NullReturned()
        {
            //Arrange
            string name = "A";
            int table = 6;
            var date = new DateTime(2023, 12, 26);
            bool expected = false;
            //act

            ReservationManagerClass rm = new ReservationManagerClass();
            var result = rm.BookTable(name, date, table);

            //assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod()]
        public void CountAvTable_ADate_NullReturned()
        {
            //Arrange
            Restaurant restaurant = new Restaurant();
            var date = new DateTime(2023, 12, 26);
            int expected = 0;
            //act

            ReservationManagerClass rm = new ReservationManagerClass();
            var result = rm.CountAvTable(restaurant, date);

            //assert
            Assert.AreEqual(result, expected);
        }



    }
}