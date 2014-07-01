using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Balanced;

namespace BalancedTests
{
    [TestClass]
    public class MarketplaceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");
            Marketplace mp = Balanced.Marketplace.Mine();
        }
    }
}
