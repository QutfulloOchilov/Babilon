using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Babilon.Model;

namespace Babilon.TestModel
{
    [TestClass]
    public class EntityBaseTest
    {
        TestEntityBase testEntity;

        [TestInitialize]
        public void TestInitialize()
        {
            testEntity = new TestEntityBase();
        }

        [TestMethod]
        public void TestEntityBase_IdTest()
        {
            Assert.IsNotNull(testEntity.Id, "If we get id it should't not be null");
            Assert.AreNotEqual(testEntity.Id.ToString().Substring(0, 4), "0000");
        }

        private class TestEntityBase : EntityBase
        {

        }

    }

}
