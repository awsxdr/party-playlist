namespace PartyPlaylist.Spotify
{
    public class AuthorizationConfiguration
    {
        public string AuthorizationHeader { get; }

        public AuthorizationConfiguration(string authorizationHeader)
        {
            AuthorizationHeader = authorizationHeader;
        }
    }
}