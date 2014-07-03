using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Balanced;

namespace BalancedTests
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void TestCardCreate()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");

            Dictionary<string, string> meta = new Dictionary<string, string>();
            meta.Add("user_id", "0192837465");
            meta.Add("my-own-customer-id", "12345");

            Dictionary<string, string> address = new Dictionary<string, string>();
            address.Add("line1", "123 PL SE");
            address.Add("city", "San Francisco");
            address.Add("state", "CA");
            address.Add("postal_code", "98405");
            address.Add("country_code", "USA");

            Card card = new Card();

            card.name = "John Jameson";
            card.number = "5105105105105100";
            card.expiration_month = 12;
            card.expiration_year = 2020;
            card.cvv = "123";
            card.address = address;
            card.meta = meta;

            card.save();

            Assert.IsTrue(card.is_verified);
            Assert.AreEqual("MasterCard", card.brand);
            Assert.IsNull(card.customer);
            Assert.IsNotNull(card.number);
            Assert.AreEqual(12, card.expiration_month);
            Assert.AreEqual(2020, card.expiration_year);
            Assert.AreEqual("xxx", card.cvv);
            Assert.AreEqual("John Jameson", card.name);
            Assert.AreEqual("98405", card.address["postal_code"]);
            Assert.AreEqual("123 PL SE", card.address["line1"]);
            Assert.AreEqual("12345", card.meta["my-own-customer-id"]);
        }

        [TestMethod]
        public void TestCardDelete()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");

            Card card = new Card();
            card.name = "John Jameson";
            card.number = "5105105105105100";
            card.expiration_month = 12;
            card.expiration_year = 2020;
            card.cvv = "123";
            card.save();

            string href = card.href;

            card.unstore();

            Card deletedCard = Balanced.Card.Fetch(href);
            Assert.AreEqual(false, deletedCard.can_debit);
        }
    }
}
