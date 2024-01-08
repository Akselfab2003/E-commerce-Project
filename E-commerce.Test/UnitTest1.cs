using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;

namespace E_commerce.Test
{
    public class UnitTest1
    {
        private readonly IDataCollection dataCollection;

        public UnitTest1(IDataCollection collection)
        {
            dataCollection = collection;
        }

        [Fact]
        public void Test1()
        {
           Orders orders = new Orders();
        }
    }
}