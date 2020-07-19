namespace PartyPlaylist.Spotify
{
    using Func;
    using System.Threading.Tasks;

    public interface ISpotify
    {
        Task<Result<SearchResult>> Search(string query);
    }
}