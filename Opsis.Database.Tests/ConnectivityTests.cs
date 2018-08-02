using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opsis.Database.Context;
using Opsis.Security;

namespace Opsis.Database.Tests
{
    [TestClass]
    public class ConnectivityTests
    {
        [TestMethod]
        public void EncryptConnectionStringsSection_CheckDbConnection()
        {
            AppConfig.EncryptSectionIfPlain("connectionStrings", "Opsis.Database.Tests.dll");

            using (var db = new OpsisContext())
            {
                Assert.AreEqual(true, db.Database.Exists());
            }
        }
    }
}
