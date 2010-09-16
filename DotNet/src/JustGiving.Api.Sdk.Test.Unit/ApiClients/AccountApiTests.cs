﻿using System;
using System.Net;
using JustGiving.Api.Sdk;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Model.Account;
using JustGiving.Api.Sdk.Model.Page;
using NUnit.Framework;

namespace GG.Api.Sdk.Test.Unit.ApiClients
{
    [TestFixture]
    public class AccountApiTests
    {
        [Test]
        public void Create_WhenProvidedWithNullRequest_ThrowsArgumentNullException()
        {
            var api = new AccountApi(new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion)));
            
            var exception = Assert.Throws<ArgumentNullException>(() => api.Create(null));

            Assert.That(exception.ParamName, Is.StringContaining("request"));
            Assert.That(exception.Message, Is.StringContaining("Request cannot be null."));
        }

        [Test]
        public void Create_WhenProvidedWithRequest_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<AccountRegistrationConfirmation>(HttpStatusCode.OK);
            var api = CreateAccountApiClient(httpClient);
            var request = new CreateAccountRequest();

            api.Create(request);

            Assert.That(httpClient.LastRequest.Uri.ToString(), Is.StringContaining(string.Format("{0}{1}/v{2}/account", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion)));
        }


        [TestCase("")]
        [TestCase(null)]
        public void ListAllPages_WhenProvidedWithNullOrEmptyEmail_ThrowsArgumentNullException(string email)
        {
            var api = new AccountApi(new JustGivingClient(new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion)));
            
            var exception = Assert.Throws<ArgumentNullException>(() => api.ListAllPages(email));

            Assert.That(exception.ParamName, Is.StringContaining("email"));
            Assert.That(exception.Message, Is.StringContaining("Email cannot be null or empty."));
        }

        [Test]
        public void ListAllPages_WhenProvidedWithEmail_CallsExpectedUrl()
        {
            var httpClient = new MockHttpClient<FundraisingPageSummarys>(HttpStatusCode.OK);
            var api = CreateAccountApiClient(httpClient);
            const string email = "some@email.com";

            api.ListAllPages(email);

            Assert.That(httpClient.LastRequest.Uri.ToString(), Is.StringContaining(string.Format("{0}{1}/v{2}/account/{3}/pages", TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion, email)));
        }

        private static AccountApi CreateAccountApiClient<T>(MockHttpClient<T> httpClient) where T: class, new()
        {
            var config = new ClientConfiguration(TestContext.ApiLocation, TestContext.ApiKey, TestContext.ApiVersion);
            var parent = new JustGivingClient(config, httpClient);
            return new AccountApi(parent);
        }
    }
}
