namespace PartyPlaylist.Spotify
{
    using Newtonsoft.Json;

    public class SearchResult
    {
        [JsonProperty("tracks")]
        public Tracks Tracks { get; set; }
    }

    public class Tracks
    {
        [JsonProperty("items")]
        public Track[] Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("total")]
        public int Count { get; set; }
    }

    public class Track
    {
        [JsonProperty("album")]
        public Album Album { get; set; }

        [JsonProperty("artists")]
        public Artist[] Artists { get; set; }

        [JsonProperty("duration_ms")]
        public int DurationInMilliseconds { get; set; }

        [JsonProperty("explicit")]
        public bool IsExplicit { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class Album
    {
        [JsonProperty("images")]
        public Image[] Images { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }

    public class Artist
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Image
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}