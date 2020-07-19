namespace PartyPlaylist.Spotify
{
    using Func;
    using System.Collections.Generic;
    using System.Net.Http.Headers;

    public class HttpRequestInfo
    {
        public IEnumerable<(string Key, string Value)> Headers { get; set; } = new (string, string)[0];
        public Option<AuthenticationHeaderValue> Authentication { get; set; }
    }
}
