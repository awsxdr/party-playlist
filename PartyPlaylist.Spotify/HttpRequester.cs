namespace PartyPlaylist.Spotify
{
    using Func;
    using Newtonsoft.Json;
    using PartyPlaylist.Spotify.Errors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;
    using System.Threading.Tasks;

    using static Func.Result;

    public class HttpRequester : IHttpRequester
    {
        public Task<Result<string>> PostFormUrlEncoded(Uri uri, IEnumerable<(string Key, string Value)> data, HttpRequestInfo requestInfo) =>
            new HttpClient()
            .Tee(AddExtraInfo(requestInfo))
            .PostAsync(uri.AbsoluteUri, new FormUrlEncodedContent(data.Select(ToKeyValuePair)))
            .Map(ResponseToResult);

        public Task<Result<TResult>> PostFormUrlEncoded<TResult>(Uri uri, IEnumerable<(string Key, string Value)> data, HttpRequestInfo requestInfo) =>
            PostFormUrlEncoded(uri, data, requestInfo)
            .Then(DeserializeJson<TResult>);

        public Task<Result<string>> Get(Uri uri, HttpRequestInfo requestInfo) =>
            new HttpClient()
            .Tee(AddExtraInfo(requestInfo))
            .GetAsync(uri.AbsoluteUri)
            .Map(ResponseToResult);

        public Task<Result<TResult>> Get<TResult>(Uri uri, HttpRequestInfo requestInfo) =>
            Get(uri, requestInfo)
            .Then(DeserializeJson<TResult>);

        private static async Task<Result<string>> ResponseToResult(HttpResponseMessage response) =>
            response.IsSuccessStatusCode
            ? Succeed(await response.Content.ReadAsStringAsync())
            : Result<string>.Fail(new HttpError((int)response.StatusCode));

        private static Action<HttpClient> AddExtraInfo(HttpRequestInfo info) => client =>
        {
            if(info.Authentication is Some<AuthenticationHeaderValue> s)
                client.DefaultRequestHeaders.Authorization = s.Value;

            foreach (var (key, value) in info.Headers)
                client.DefaultRequestHeaders.Add(key, value);
        };

        private static Result<TResult> DeserializeJson<TResult>(string jsonData) =>
            Succeed(JsonConvert.DeserializeObject<TResult>(jsonData));

        private static KeyValuePair<string, string> ToKeyValuePair((string Key, string Value) pair) =>
            new KeyValuePair<string, string>(pair.Key, pair.Value);
    }
}
