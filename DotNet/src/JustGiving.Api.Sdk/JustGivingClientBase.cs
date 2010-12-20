﻿using System;
using JustGiving.Api.Sdk.ApiClients;
using JustGiving.Api.Sdk.Http;

namespace JustGiving.Api.Sdk
{
    public abstract class JustGivingClientBase
    {
        public IAccountApi Account { get; set; }
        public IDonationApi Donation { get; set; }
        public IPageApi Page { get; set; }
        public ISearchApi Search { get; set; }
        public ICharityApi Charity { get; set; }
        public IEventApi Event { get; set; }

        private readonly IHttpClient _httpClient;

        protected internal ClientConfiguration Configuration { get; private set; }
        internal HttpChannel HttpChannel { get; private set; }

        protected JustGivingClientBase(ClientConfiguration clientConfiguration, IHttpClient httpClient)
            : this(clientConfiguration, httpClient, null, null, null, null, null, null)
        {
        }

        protected JustGivingClientBase(ClientConfiguration clientConfiguration, IHttpClient httpClient, IAccountApi accountApi, IDonationApi donationApi, IPageApi pageApi, ISearchApi searchApi, ICharityApi charityApi, IEventApi eventApi)
        {
            if(httpClient == null)
            {
                throw new ArgumentNullException("httpClient", "httpClient must not be null to access the api.");
            }

            _httpClient = httpClient;

            Account = accountApi;
            Donation = donationApi;
            Page = pageApi;
            Search = searchApi;
            Charity = charityApi;
            Event = eventApi;

            Configuration = clientConfiguration;

            InitApis(_httpClient, clientConfiguration);
        }

        private void InitApis(IHttpClient httpClient, ClientConfiguration clientConfiguration)
        {
            HttpChannel = new HttpChannel(clientConfiguration, httpClient);

            if (Account == null)
            {
                Account = new AccountApi(this);
            }

            if (Donation == null)
            {
                Donation = new DonationApi(this);
            }

            if (Page == null)
            {
                Page = new PageApi(this);
            }

            if (Search == null)
            {
                Search = new SearchApi(this);
            }

            if(Charity == null)
            {
                Charity = new CharityApi(this);
            }

            if(Event == null)
            {
                Event = new EventApi(this);
            }
        }

        public void UpdateConfiguration(ClientConfiguration configuration)
        {
            Configuration = configuration;
            InitApis(_httpClient, configuration);
        }
    }
}
