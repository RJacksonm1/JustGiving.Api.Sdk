﻿using System;
using JustGiving.Api.Sdk.Http.DataPackets;

namespace JustGiving.Api.Sdk.Http
{
    public interface IHttpClient : IDisposable
    {
        HttpResponseMessage Get(string uri);
        HttpResponseMessage Get(string uri, string contentType);
        HttpResponseMessage Post(string url, string contentType, HttpContent body);
        HttpResponseMessage Delete(string url);
        void SendAsync(HttpRequestMessage httpRequestMessage);
        HttpResponseMessage Send(HttpRequestMessage httpRequestMessage);
        void Put(string url, string contentType, HttpContent body);
        void AddHeader(string key, string value);
        HttpResponseMessage Send(string method, Uri uri, byte[] postData, string contentType);
        HttpResponseMessage Send(string method, Uri uri, Payload postData);
    }
}