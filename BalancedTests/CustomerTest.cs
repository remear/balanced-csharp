using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using Balanced;
using Balanced.Exceptions;

namespace BalancedTests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestCreateCustomer()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");
            Customer customer = Balanced.Customer.Fetch("/customers/CU6Gju1EVhTsSi66zBavYqSV");
            Assert.AreEqual("Henry Ford", customer.name);
            Assert.AreEqual("henry@email.com", customer.email);
            Assert.AreEqual("Seattle", customer.address["city"]);
        }

        [TestMethod]
        public void TestPostCustomer()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");
            //Customer customer = Balanced.Customer.fetch("/customers/CU6Gju1EVhTsSi66zBavYqSV");

            Dictionary<string, string>address = new Dictionary<string, string>();
            address.Add("city", "Seattle");
            address.Add("line1", "12345 PL SE");
            address.Add("line2", "Suite 201");
            address.Add("state", "WA");
            address.Add("postal_code", "48120");
            address.Add("country_code", "US");

            Dictionary<string, string>meta = new Dictionary<string, string>();
            meta.Add("internal-id", "12134");
            meta.Add("satisfaction", "good");

            Customer customer = new Customer();
            
            customer.address = address;
            customer.business_name = "Happy Fun Time Land";
            customer.dob_month = 7;
            customer.dob_year = 1963;
            customer.ein = "123456789";
            customer.email = "henry@email.com";
            customer.meta = meta;
            customer.name = "Henry Ford";
            customer.phone = "123-456-7890";

            customer.save();
            Assert.AreEqual("Henry Ford", customer.name);

            Customer customerA = new Customer() { name = "Mike Jones" };
            customerA.save();
        }

        [TestMethod]
        public void TestUpdateCustomer()
        {
            Balanced.Balanced.configure("8f7b42ba043211e3bd9e026ba7cd33d0");

            Customer customer = new Customer() { name = "Mike Jones" };
            customer.save();

            customer.name = "Mike Richard Jones";
            customer.save();

            Assert.AreEqual("Mike Richard Jones", customer.name);
        }

        [TestMethod]
        public void TestCustomerCollection()
        {
            int count = 0;
            Customer.Collection customers = new Customer.Collection();

            foreach (Customer c in customers)
            {
                var foo = c.name;
                count += 1;
            }

            Assert.AreEqual(customers.Total(), count);
        }
    }
}
