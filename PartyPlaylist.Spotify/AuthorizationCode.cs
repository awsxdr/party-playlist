namespace PartyPlaylist.Spotify
{
  public class AuthorizationCode
  {
    private readonly string _value;

    private AuthorizationCode(string value) => _value = value;

    public static implicit operator string(AuthorizationCode value) => value._value;
    public static implicit operator AuthorizationCode(string value) => new AuthorizationCode(value);
  }
}