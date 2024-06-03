using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyFeeds.Clients
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Extra
    {
    }

    public class HighResolution
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }

        [JsonPropertyName("orientation")]
        public object Orientation { get; set; }
    }

    public class IconBadge
    {
        [JsonPropertyName("icon_big")]
        public string IconBig { get; set; }

        [JsonPropertyName("icon_small")]
        public string IconSmall { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("is_visible")]
        public int IsVisible { get; set; }

        [JsonPropertyName("discount")]
        public object Discount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("brand_title")]
        public string BrandTitle { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("promoted")]
        public bool Promoted { get; set; }

        [JsonPropertyName("photo")]
        public Photo Photo { get; set; }

        [JsonPropertyName("favourite_count")]
        public int FavouriteCount { get; set; }

        [JsonPropertyName("is_favourite")]
        public bool IsFavourite { get; set; }

        [JsonPropertyName("badge")]
        public object Badge { get; set; }

        [JsonPropertyName("conversion")]
        public object Conversion { get; set; }

        [JsonPropertyName("service_fee")]
        public string ServiceFee { get; set; }

        [JsonPropertyName("total_item_price")]
        public string TotalItemPrice { get; set; }

        [JsonPropertyName("total_item_price_rounded")]
        public object TotalItemPriceRounded { get; set; }

        [JsonPropertyName("view_count")]
        public int ViewCount { get; set; }

        [JsonPropertyName("size_title")]
        public string SizeTitle { get; set; }

        [JsonPropertyName("content_source")]
        public string ContentSource { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("icon_badges")]
        public List<IconBadge> IconBadges { get; set; }

        [JsonPropertyName("search_tracking_params")]
        public SearchTrackingParams SearchTrackingParams { get; set; }
    }

    public class Pagination
    {
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_entries")]
        public int TotalEntries { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("time")]
        public int Time { get; set; }
    }

    public class Photo
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("temp_uuid")]
        public object TempUuid { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("dominant_color")]
        public string DominantColor { get; set; }

        [JsonPropertyName("dominant_color_opaque")]
        public string DominantColorOpaque { get; set; }

        [JsonPropertyName("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }

        [JsonPropertyName("is_suspicious")]
        public bool IsSuspicious { get; set; }

        [JsonPropertyName("orientation")]
        public object Orientation { get; set; }

        [JsonPropertyName("high_resolution")]
        public HighResolution HighResolution { get; set; }

        [JsonPropertyName("full_size_url")]
        public string FullSizeUrl { get; set; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("extra")]
        public Extra Extra { get; set; }

        [JsonPropertyName("image_no")]
        public int ImageNo { get; set; }

        [JsonPropertyName("is_main")]
        public bool IsMain { get; set; }
    }

    public class ItemRequest
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }

        [JsonPropertyName("dominant_brand")]
        public object DominantBrand { get; set; }

        [JsonPropertyName("search_tracking_params")]
        public SearchTrackingParams SearchTrackingParams { get; set; }

        [JsonPropertyName("pagination")]
        public Pagination Pagination { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class SearchTrackingParams
    {
        [JsonPropertyName("score")]
        public double Score { get; set; }

        [JsonPropertyName("matched_queries")]
        public List<string> MatchedQueries { get; set; }

        [JsonPropertyName("search_correlation_id")]
        public string SearchCorrelationId { get; set; }

        [JsonPropertyName("search_session_id")]
        public string SearchSessionId { get; set; }

        [JsonPropertyName("global_search_session_id")]
        public string GlobalSearchSessionId { get; set; }
    }

    public class Thumbnail
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("original_size")]
        public object OriginalSize { get; set; }
    }

    public class User
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("business")]
        public bool Business { get; set; }

        [JsonPropertyName("profile_url")]
        public string ProfileUrl { get; set; }

        [JsonPropertyName("photo")]
        public Photo Photo { get; set; }
    }


}
