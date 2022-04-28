using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Buyers.Members;

namespace TestMarketBackend.BusinessLayer.Buyers.Members
{
    internal class MembersControllerTests
    {
        private MembersController membersController;
        
        private Mock<Member> mockMember = new Mock<Member>();
        private Member member;


        /*private Member[] SetUpMembersMocks()
        {

        }*/

        [SetUp]
        private void SetUp()
        {
            membersController = new MembersController();
            mockMember = new Mock<Member>();
        }


        [Test]
        private void TestRegister()
        {

        }


        [Test]
        private void TestRegisterExistingMember()
        {

        }
    }
}
