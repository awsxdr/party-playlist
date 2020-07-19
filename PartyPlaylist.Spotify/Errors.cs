namespace PartyPlaylist.Spotify.Errors
{
    using Func;

    public class NotLoggedInError : ResultError { }
    public class HttpError : ResultError
    {
        public int StatusCode { get; }
        public HttpError(int statusCode) => StatusCode = statusCode;
    }
}
