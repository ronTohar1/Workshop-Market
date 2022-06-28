using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;

using MarketBackend.BusinessLayer;
using MarketBackend.BusinessLayer.Market.StoreManagment;
using System.Threading;

namespace TestMarketBackend.BusinessLayer.Market.StoreManagment
{
    public class HierarchyTests
    {
        private Hierarchy<string> hierarchy;
        private String node1 = "David";
        
        [SetUp]
        public void Setup1() {
            DataManagersMock.InitMockDataManagers(); 

            hierarchy = new Hierarchy<string>(node1, (dataHierarchy, value) => { }); // the action here is not used because the dataManagers are mocked in the unit tests 
        }

        // AddToHierarchy tests
        [Test]
        [TestCase("Roi")]
        [TestCase("Amit")]
        public void AddToHierarchyFromFirstLevelSuccess(String node) {
            int sizeBefore = hierarchy.children.Count;
            hierarchy.AddToHierarchy(node1, node, new Action(() => Thread.Sleep(0)));
            int sizeAfter = hierarchy.children.Count;
            List<String> childrenValueSet = hierarchy.children.Select(h => h.value).ToList();
         
            Assert.IsTrue(childrenValueSet.Contains(node) && sizeAfter==sizeBefore+1);
        }

        private void BuildFirstLevel() {
            hierarchy.AddToHierarchy(node1, "Idan", new Action(() => Thread.Sleep(0)));
            hierarchy.AddToHierarchy(node1, "Nir", new Action(() => Thread.Sleep(0)));
        }

        [Test]
        [TestCase("David")]
        [TestCase("Idan")]
        public void AddToHierarchyFromFirstLevelFail(String node)
        {
            BuildFirstLevel();
            int sizeBefore = hierarchy.children.Count;
            Assert.Throws<MarketException>(()=>hierarchy.AddToHierarchy(node1, node, new Action(() => Thread.Sleep(0))));
            int sizeAfter = hierarchy.children.Count;
            Assert.IsTrue(sizeAfter == sizeBefore);
        }

        [Test]
        [TestCase("Nir", "Ron")]
        [TestCase("Idan","Ron" )]
        public void AddToHierarchyFromSecondLevelSuccess(String adder, String nodeToAdd)
        {
            BuildFirstLevel();
            Hierarchy<String> adderH = hierarchy.FindHierarchy(adder);
            int sizeBefore = adderH.children.Count;
            hierarchy.AddToHierarchy(adder, nodeToAdd, new Action(() => Thread.Sleep(0)));
            int sizeAfter = adderH.children.Count;
            List<String> childrenValueSet = adderH.children.Select(h => h.value).ToList();

            Assert.IsTrue(childrenValueSet.Contains(nodeToAdd) && sizeAfter == sizeBefore + 1);
        }


        [Test]
        [TestCase("Ron", "Tal")]   
        public void AddToHierarchyFromSecondLevelFail1(String adder, String nodeToAdd)
        {
            BuildFirstLevel();
            Assert.Throws<MarketException>(() => hierarchy.AddToHierarchy(adder, nodeToAdd, new Action(() => Thread.Sleep(0))));
        }
        
        [Test]//Cyclic check
        [TestCase("Idan", "David")]
        [TestCase("Nir", "David")]
        public void AddToHierarchyFromSecondLevelFail2(String adder, String nodeToAdd)
        {
            BuildFirstLevel();
            Hierarchy<String> adderH = hierarchy.FindHierarchy(adder);
            int sizeBefore = adderH.children.Count;
            Assert.Throws<MarketException>(() => hierarchy.AddToHierarchy(adder, nodeToAdd, new Action(() => Thread.Sleep(0))));
            int sizeAfter = adderH.children.Count;

            Assert.IsTrue(sizeAfter == sizeBefore);
        }


        // FindHierarchy tests
        [Test]
        public void SimpleFindHierarchySuccess1() {
            Assert.IsTrue(hierarchy.FindHierarchy("David") == hierarchy);
        }

        [Test]
        [TestCase("Idan")]
        [TestCase("Nir")]
        public void SimpleFindHierarchySuccess2(String node)
        {
            BuildFirstLevel();
            Assert.IsFalse(hierarchy.FindHierarchy(node) == null);
        }

        [Test]
        [TestCase("Nir")]
        [TestCase("Roi")]
        public void SimpleFindHierarchyFail(String node)
        {
            Assert.IsTrue(hierarchy.FindHierarchy(node) == null);
        }

        // RemoveFromHierarchy tests
        private void BuildAidHierarchy()
        {
            hierarchy.AddToHierarchy(node1, "Idan", new Action(() => Thread.Sleep(0)));
            hierarchy.AddToHierarchy(node1, "Nir", new Action(() => Thread.Sleep(0)));
            hierarchy.AddToHierarchy("Nir", "Roi", new Action(() => Thread.Sleep(0)));
            hierarchy.AddToHierarchy("Roi", "Ron", new Action(() => Thread.Sleep(0)));
        }
        
        [Test]
        [TestCase("David", "Ron")]
        [TestCase("Nir", "Ron")]
        [TestCase("Roi", "Ron")]
        public void RemoveFromHierarchySimpleSuccess(String remover, String nodeToRemove)
        {
            BuildAidHierarchy();
            Assert.IsNotNull(hierarchy.FindHierarchy(nodeToRemove));
            hierarchy.RemoveFromHierarchy(remover, nodeToRemove, new Action(() => Thread.Sleep(0)));
            Assert.IsNull(hierarchy.FindHierarchy(nodeToRemove));
        }

        [Test]
        [TestCase("David","Nir", "Ron")]
        [TestCase("Nir","Roi", "Ron")]
        public void RemoveFromHierarchyComplexSuccess(String remover, String nodeToRemove, String inNodeToRemoveHierarchy)
        {
            BuildAidHierarchy();
            Assert.IsNotNull(hierarchy.FindHierarchy(nodeToRemove));
            Assert.IsNotNull(hierarchy.FindHierarchy(inNodeToRemoveHierarchy));
            hierarchy.RemoveFromHierarchy(remover, nodeToRemove, new Action(() => Thread.Sleep(0)));
            Assert.IsNull(hierarchy.FindHierarchy(nodeToRemove));
            Assert.IsNull(hierarchy.FindHierarchy(inNodeToRemoveHierarchy));
        }

        [Test]
        [TestCase("Ron", "David")]
        [TestCase("Roi", "Nir")]
        [TestCase("David", "Tal")]
        public void RemoveFromHierarchyFail(String remover, String nodeToRemove)
        {
            BuildAidHierarchy();
            Assert.Throws<MarketException>(() => hierarchy.RemoveFromHierarchy(remover, nodeToRemove, new Action(() => Thread.Sleep(0))));
        }
    }
}
