using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Balanced;
using Balanced.Exceptions;

namespace BalancedTests
{
    [TestClass]
    public class BankAccountVerificationTest : BaseTest
    {
        [TestMethod]
        public void TestBankAccountVerify()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");
            BankAccount ba = createBankAccount();
            BankAccountVerification bav = ba.verify();
            ba.reload();

            Assert.AreEqual(ba.verification.id, bav.id);
            bav.confirm(1, 1);
            Assert.AreEqual(bav.attempts, 1);
            Assert.AreEqual(bav.attempts_remaining, 2);
            Assert.AreEqual(bav.deposit_status, "succeeded");
            Assert.AreEqual(bav.verification_status, "succeeded");
        }

        [TestMethod]
        [ExpectedException(typeof(Balanced.Exceptions.APIException))]
        public void testFailedConfirm()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");
            BankAccount ba = createBankAccount();
            ba.verify();
            ba.reload();
            BankAccountVerification bav = ba.verification;
            bav.confirm(12, 13);
        }

        [TestMethod]
        [ExpectedException(typeof(Balanced.Exceptions.APIException))]
        public void testDoubleConfirm()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");
            BankAccount ba = createBankAccount();
            ba.verify();
            ba.reload();
            BankAccountVerification bav = ba.verification;
            bav.confirm(1, 1);
            bav.confirm(1, 1);
        }

        [TestMethod]
        public void testExhaustedConfirm()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");
            BankAccount ba = createBankAccount();
            ba.verify();
            ba.reload();
            BankAccountVerification bav = ba.verification;
            while (bav.attempts_remaining != 1) {
                try {
                    bav.confirm(12, 13);
                }
                catch (APIException e){
                    bav = BankAccountVerification.Fetch(bav.href);
                    Assert.AreEqual("pending", bav.deposit_status);
                }
            }
            try {
                bav.confirm(12, 13);
            }
            catch (APIException e)
            {
                bav = BankAccountVerification.Fetch(bav.href);
                Assert.AreEqual(bav.verification_status, "failed");
            }
            Assert.AreEqual(bav.attempts_remaining, 0);
            bav = ba.verify();
            bav.confirm(1, 1);
            Assert.AreEqual("succeeded", bav.verification_status);
            Assert.AreEqual(ba.href, bav.bank_account.href);
        }
    }
}
