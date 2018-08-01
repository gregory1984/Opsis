using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opsis.Database.Context;

namespace Opsis.Database.Tests
{
    [TestClass]
    public class CommonTests
    {
        [TestMethod]
        public void ConnectivityTest()
        {
            using (var db = new OpsisContext())
            {
                Assert.AreEqual(true, db.Database.Exists());
            }
        }
    }
}
