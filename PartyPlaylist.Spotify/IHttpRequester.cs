namespace PartyPlaylist.Spotify
{
    using Func;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHttpRequester
    {
        Task<Result<string>> Get(Uri uri, HttpRequestInfo requestInfo);
        Task<Result<TResult>> Get<TResult>(Uri uri, HttpRequestInfo requestInfo);
        Task<Result<string>> PostFormUrlEncoded(Uri uri, IEnumerable<(string Key, string Value)> data, HttpRequestInfo requestInfo);
        Task<Result<TResult>> PostFormUrlEncoded<TResult>(Uri uri, IEnumerable<(string Key, string Value)> data, HttpRequestInfo requestInfo);
    }
}