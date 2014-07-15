using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Balanced;
using System.Collections.Generic;

namespace BalancedTests
{
    public class BaseTest
    {
        protected Marketplace mp;

        /*[TestInitialize()]
        public void setUp()
        {
            ApiKey key = new ApiKey();
            key.save();
            Balanced.Balanced.configure(key.secret);
            Marketplace marketplace = new Marketplace();
            marketplace.save();
            this.mp = marketplace;
        }*/
        /*
        protected Marketplace createMarketplace()
        {
            ApiKey key = new ApiKey();
            key.save();
            Balanced.configure(key.secret);

            Marketplace mp = new Marketplace();
            mp.save();
            return mp;
        }

        protected Card createCard()
        {

            Map<String, Object> addressPayload = new HashMap<String, Object>();
            addressPayload.put("line1", "123 Fake Street");
            addressPayload.put("city", "Jollywood");
            addressPayload.put("postal_code", "90210");

            Map<String, Object> payload = new HashMap<String, Object>();
            payload.put("name", "Homer Jay");
            payload.put("number", "4112344112344113");
            payload.put("cvv", "123");
            payload.put("expiration_month", 12);
            payload.put("expiration_year", 2016);
            payload.put("address", addressPayload);

            Card card = new Card(payload);
            card.save();

            return card;
        }

        protected Card createCreditableCard() throws HTTPError {
            Map<String, Object> payload = new HashMap<String, Object>();
            payload.put("name", "Johannes Bach");
            payload.put("number", "4342561111111118");
            payload.put("expiration_month", 05);
            payload.put("expiration_year", 2016);

            Card card = new Card(payload);
            card.save();

            return card;
        }

        protected Card createNonCreditableCard() throws HTTPError {
            Map<String, Object> payload = new HashMap<String, Object>();
            payload.put("name", "Georg Telemann");
            payload.put("number", "4111111111111111");
            payload.put("expiration_month", 12);
            payload.put("expiration_year", 2016);

            Card card = new Card(payload);
            card.save();

            return card;
        }
        */
        protected BankAccount createBankAccount()
        {
            BankAccount bankAccount = new BankAccount();
            bankAccount.name = "Johann Bernoulli";
            bankAccount.routing_number = "121000358";
            bankAccount.account_number = "9900000001";
            bankAccount.account_type = "checking";
            bankAccount.save();

            return bankAccount;
        }
        
        protected Customer createPersonCustomer()
        {
            Dictionary<string, string> meta = new Dictionary<string, string>();
            meta.Add("meta can store", "any flat key/value data you like");
            meta.Add("github", "https://github.com/balanced");
            meta.Add("more_additional_data", "54.8");

            Dictionary<string, string> address = new Dictionary<string, string>();
            address.Add("city", "San Francisco");
            address.Add("state", "CA");
            address.Add("postal_code", "94103");
            address.Add("line1", "965 Mission St");
            address.Add("country_code", "US");

            Customer customer = new Customer();
            customer.name = "John Lee Hooker";
            customer.phone = "(904) 555-1796";
            customer.dob_month = 1;
            customer.dob_year = 1980;
            customer.meta = meta;
            customer.address = address;
            customer.ssn_last4 = "3209";
            customer.save();

            return customer;
        }
        /*
        protected Customer createBusinessCustomer() throws HTTPError {
            Customer customer = new Customer(businessCustomerPayload());
            customer.save();
            return customer;
        }

        protected Map<String, Object> businessCustomerPayload() {
            Map<String, Object> payload = new HashMap<String, Object>();

            payload.put("name", "John Lee Hooker");
            payload.put("phone", "(904) 555-1796");
            payload.put("business_name", "Balanced");
            payload.put("ein", "123456789");

            Map<String, String> meta = new HashMap<String, String>();
            meta.put("meta can store", "any flat key/value data you like");
            meta.put("github", "https://github.com/balanced");
            meta.put("more_additional_data", "54.8");
            payload.put("meta", meta);

            Map<String, String> address = new HashMap<String, String>();
            address.put("city", "San Francisco");
            address.put("state", "CA");
            address.put("postal_code", "94103");
            address.put("line1", "965 Mission St");
            address.put("country_code", "USA");
            payload.put("address", address);

            return payload;
        }

        public void TestMethod1()
        {
        }*/
    }
}
