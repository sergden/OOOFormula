using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Tests.MockData
{
    public class MockPages
    {
        public static List<_Pages> GetOnePageTest(int id = 1)
        {
            var _Pages = new List<_Pages>
            {
                new _Pages
                {
                    Id=id,
                    Name="Contacts",
                    Title="TITLE",
                    Body="BODY"
                }
            };

            return _Pages;
        }


        public static IQueryable<_Pages> GetAllPagesTest()
        {
            return GetOnePageTest().AsQueryable();
        }
    }
}
