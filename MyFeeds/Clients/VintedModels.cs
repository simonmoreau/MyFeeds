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


    public class ItemSummary
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

    public class Item
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("brand_id")]
        public int BrandId { get; set; }

        [JsonPropertyName("size_id")]
        public int SizeId { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusId { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("country_id")]
        public int CountryId { get; set; }

        [JsonPropertyName("catalog_id")]
        public int CatalogId { get; set; }

        [JsonPropertyName("color1_id")]
        public int Color1Id { get; set; }

        [JsonPropertyName("color2_id")]
        public object Color2Id { get; set; }

        [JsonPropertyName("package_size_id")]
        public int PackageSizeId { get; set; }

        [JsonPropertyName("is_hidden")]
        public int IsHidden { get; set; }

        [JsonPropertyName("is_reserved")]
        public int IsReserved { get; set; }

        [JsonPropertyName("is_visible")]
        public int IsVisible { get; set; }

        [JsonPropertyName("is_unisex")]
        public int IsUnisex { get; set; }

        [JsonPropertyName("is_closed")]
        public int IsClosed { get; set; }

        [JsonPropertyName("moderation_status")]
        public int ModerationStatus { get; set; }

        [JsonPropertyName("favourite_count")]
        public int FavouriteCount { get; set; }

        [JsonPropertyName("active_bid_count")]
        public int ActiveBidCount { get; set; }

        [JsonPropertyName("favourite_count_new")]
        public int FavouriteCountNew { get; set; }

        [JsonPropertyName("active_bid_count_new")]
        public int ActiveBidCountNew { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("package_size_standard")]
        public bool PackageSizeStandard { get; set; }

        [JsonPropertyName("item_closing_action")]
        public object ItemClosingAction { get; set; }

        [JsonPropertyName("related_catalog_ids")]
        public List<object> RelatedCatalogIds { get; set; }

        [JsonPropertyName("related_catalogs_enabled")]
        public bool RelatedCatalogsEnabled { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        [JsonPropertyName("composition")]
        public string Composition { get; set; }

        [JsonPropertyName("extra_conditions")]
        public string ExtraConditions { get; set; }

        [JsonPropertyName("disposal_conditions")]
        public int DisposalConditions { get; set; }

        [JsonPropertyName("is_for_sell")]
        public bool IsForSell { get; set; }

        [JsonPropertyName("is_handicraft")]
        public bool IsHandicraft { get; set; }

        [JsonPropertyName("is_processing")]
        public bool IsProcessing { get; set; }

        [JsonPropertyName("is_draft")]
        public bool IsDraft { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("original_price_numeric")]
        public string OriginalPriceNumeric { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("price_numeric")]
        public string PriceNumeric { get; set; }

        [JsonPropertyName("last_push_up_at")]
        public DateTime LastPushUpAt { get; set; }

        [JsonPropertyName("last_push_up_at_new")]
        public DateTime LastPushUpAtNew { get; set; }

        [JsonPropertyName("created_at_ts")]
        public DateTime CreatedAtTs { get; set; }

        [JsonPropertyName("updated_at_ts")]
        public DateTime UpdatedAtTs { get; set; }

        [JsonPropertyName("user_updated_at_ts")]
        public DateTime UserUpdatedAtTs { get; set; }

        [JsonPropertyName("is_delayed_publication")]
        public bool IsDelayedPublication { get; set; }

        [JsonPropertyName("photos")]
        public List<Photo> Photos { get; set; }

        [JsonPropertyName("can_be_sold")]
        public bool CanBeSold { get; set; }

        [JsonPropertyName("can_feedback")]
        public bool CanFeedback { get; set; }

        [JsonPropertyName("item_reservation_id")]
        public object ItemReservationId { get; set; }

        [JsonPropertyName("promoted_until")]
        public object PromotedUntil { get; set; }

        [JsonPropertyName("promoted_internationally")]
        public object PromotedInternationally { get; set; }

        [JsonPropertyName("discount_price_numeric")]
        public object DiscountPriceNumeric { get; set; }

        [JsonPropertyName("author")]
        public object Author { get; set; }

        [JsonPropertyName("book_title")]
        public object BookTitle { get; set; }

        [JsonPropertyName("isbn")]
        public object Isbn { get; set; }

        [JsonPropertyName("measurement_width")]
        public object MeasurementWidth { get; set; }

        [JsonPropertyName("measurement_length")]
        public object MeasurementLength { get; set; }

        [JsonPropertyName("measurement_unit")]
        public object MeasurementUnit { get; set; }

        [JsonPropertyName("manufacturer")]
        public object Manufacturer { get; set; }

        [JsonPropertyName("manufacturer_labelling")]
        public object ManufacturerLabelling { get; set; }

        [JsonPropertyName("transaction_permitted")]
        public bool TransactionPermitted { get; set; }

        [JsonPropertyName("video_game_rating_id")]
        public object VideoGameRatingId { get; set; }

        [JsonPropertyName("item_attributes")]
        public List<ItemAttribute> ItemAttributes { get; set; }

        [JsonPropertyName("haov_item?")]
        public bool HaovItem { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("price")]
        public Price Price { get; set; }

        [JsonPropertyName("discount_price")]
        public object DiscountPrice { get; set; }

        [JsonPropertyName("service_fee")]
        public string ServiceFee { get; set; }

        [JsonPropertyName("total_item_price")]
        public string TotalItemPrice { get; set; }

        [JsonPropertyName("total_item_price_rounded")]
        public object TotalItemPriceRounded { get; set; }

        [JsonPropertyName("can_edit")]
        public bool CanEdit { get; set; }

        [JsonPropertyName("can_delete")]
        public bool CanDelete { get; set; }

        [JsonPropertyName("can_reserve")]
        public bool CanReserve { get; set; }

        [JsonPropertyName("can_mark_as_sold")]
        public bool CanMarkAsSold { get; set; }

        [JsonPropertyName("can_transfer")]
        public bool CanTransfer { get; set; }

        [JsonPropertyName("instant_buy")]
        public bool InstantBuy { get; set; }

        [JsonPropertyName("can_close")]
        public bool CanClose { get; set; }

        [JsonPropertyName("can_buy")]
        public bool CanBuy { get; set; }

        [JsonPropertyName("can_bundle")]
        public bool CanBundle { get; set; }

        [JsonPropertyName("can_ask_seller")]
        public bool CanAskSeller { get; set; }

        [JsonPropertyName("can_favourite")]
        public bool CanFavourite { get; set; }

        [JsonPropertyName("user_login")]
        public string UserLogin { get; set; }

        [JsonPropertyName("city_id")]
        public int CityId { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("promoted")]
        public bool Promoted { get; set; }

        [JsonPropertyName("is_mobile")]
        public bool IsMobile { get; set; }

        [JsonPropertyName("bump_badge_visible")]
        public bool BumpBadgeVisible { get; set; }

        [JsonPropertyName("brand_dto")]
        public BrandDto BrandDto { get; set; }

        [JsonPropertyName("catalog_branch_title")]
        public string CatalogBranchTitle { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("accepted_pay_in_methods")]
        public List<AcceptedPayInMethod> AcceptedPayInMethods { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("color1")]
        public string Color1 { get; set; }

        [JsonPropertyName("color2")]
        public object Color2 { get; set; }

        [JsonPropertyName("size_title")]
        public string SizeTitle { get; set; }

        [JsonPropertyName("description_attributes")]
        public List<DescriptionAttribute> DescriptionAttributes { get; set; }

        [JsonPropertyName("video_game_rating")]
        public object VideoGameRating { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("is_favourite")]
        public bool IsFavourite { get; set; }

        [JsonPropertyName("view_count")]
        public int ViewCount { get; set; }

        [JsonPropertyName("performance")]
        public object Performance { get; set; }

        [JsonPropertyName("stats_visible")]
        public bool StatsVisible { get; set; }

        [JsonPropertyName("can_push_up")]
        public bool CanPushUp { get; set; }

        [JsonPropertyName("badge")]
        public object Badge { get; set; }

        [JsonPropertyName("size_guide_faq_entry_id")]
        public int SizeGuideFaqEntryId { get; set; }

        [JsonPropertyName("localization")]
        public string Localization { get; set; }

        [JsonPropertyName("offline_verification")]
        public bool OfflineVerification { get; set; }

        [JsonPropertyName("offline_verification_fee")]
        public object OfflineVerificationFee { get; set; }

        [JsonPropertyName("icon_badges")]
        public List<object> IconBadges { get; set; }
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
        public List<ItemSummary> Items { get; set; }

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
        public bool? OriginalSize { get; set; }
    }

    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("anon_id")]
        public string AnonId { get; set; }

        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("real_name")]
        public object RealName { get; set; }

        [JsonPropertyName("email")]
        public object Email { get; set; }

        [JsonPropertyName("birthday")]
        public object Birthday { get; set; }

        [JsonPropertyName("item_count")]
        public int ItemCount { get; set; }

        [JsonPropertyName("given_item_count")]
        public int GivenItemCount { get; set; }

        [JsonPropertyName("taken_item_count")]
        public int TakenItemCount { get; set; }

        [JsonPropertyName("favourite_topic_count")]
        public int FavouriteTopicCount { get; set; }

        [JsonPropertyName("forum_msg_count")]
        public int ForumMsgCount { get; set; }

        [JsonPropertyName("forum_topic_count")]
        public int ForumTopicCount { get; set; }

        [JsonPropertyName("followers_count")]
        public int FollowersCount { get; set; }

        [JsonPropertyName("following_count")]
        public int FollowingCount { get; set; }

        [JsonPropertyName("following_brands_count")]
        public int FollowingBrandsCount { get; set; }

        [JsonPropertyName("positive_feedback_count")]
        public int PositiveFeedbackCount { get; set; }

        [JsonPropertyName("neutral_feedback_count")]
        public int NeutralFeedbackCount { get; set; }

        [JsonPropertyName("negative_feedback_count")]
        public int NegativeFeedbackCount { get; set; }

        [JsonPropertyName("meeting_transaction_count")]
        public int MeetingTransactionCount { get; set; }

        [JsonPropertyName("account_status")]
        public int AccountStatus { get; set; }

        [JsonPropertyName("email_bounces")]
        public object EmailBounces { get; set; }

        [JsonPropertyName("feedback_reputation")]
        public double FeedbackReputation { get; set; }

        [JsonPropertyName("feedback_count")]
        public int FeedbackCount { get; set; }

        [JsonPropertyName("account_ban_date")]
        public object AccountBanDate { get; set; }

        [JsonPropertyName("forum_ban_date")]
        public object ForumBanDate { get; set; }

        [JsonPropertyName("is_account_ban_permanent")]
        public object IsAccountBanPermanent { get; set; }

        [JsonPropertyName("is_forum_ban_permanent")]
        public object IsForumBanPermanent { get; set; }

        [JsonPropertyName("is_on_holiday")]
        public bool IsOnHoliday { get; set; }

        [JsonPropertyName("is_publish_photos_agreed")]
        public bool IsPublishPhotosAgreed { get; set; }

        [JsonPropertyName("expose_location")]
        public bool ExposeLocation { get; set; }

        [JsonPropertyName("third_party_tracking")]
        public bool ThirdPartyTracking { get; set; }

        [JsonPropertyName("default_address")]
        public object DefaultAddress { get; set; }

        [JsonPropertyName("last_loged_on_ts")]
        public DateTime LastLogedOnTs { get; set; }

        [JsonPropertyName("city_id")]
        public int CityId { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country_id")]
        public int CountryId { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_iso_code")]
        public string CountryIsoCode { get; set; }

        [JsonPropertyName("country_title")]
        public string CountryTitle { get; set; }

        [JsonPropertyName("contacts_permission")]
        public object ContactsPermission { get; set; }

        [JsonPropertyName("contacts")]
        public object Contacts { get; set; }

        [JsonPropertyName("photo")]
        public Photo Photo { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("moderator")]
        public bool Moderator { get; set; }

        [JsonPropertyName("is_catalog_moderator")]
        public bool IsCatalogModerator { get; set; }

        [JsonPropertyName("is_catalog_role_marketing_photos")]
        public bool IsCatalogRoleMarketingPhotos { get; set; }

        [JsonPropertyName("hide_feedback")]
        public bool HideFeedback { get; set; }

        [JsonPropertyName("can_post_big_forum_photos")]
        public bool CanPostBigForumPhotos { get; set; }

        [JsonPropertyName("allow_direct_messaging")]
        public bool AllowDirectMessaging { get; set; }

        [JsonPropertyName("bundle_discount")]
        public object BundleDiscount { get; set; }

        [JsonPropertyName("donation_configuration")]
        public object DonationConfiguration { get; set; }

        [JsonPropertyName("fundraiser")]
        public object Fundraiser { get; set; }

        [JsonPropertyName("business")]
        public bool Business { get; set; }

        [JsonPropertyName("business_account")]
        public object BusinessAccount { get; set; }

        [JsonPropertyName("has_ship_fast_badge")]
        public bool HasShipFastBadge { get; set; }

        [JsonPropertyName("total_items_count")]
        public int TotalItemsCount { get; set; }

        [JsonPropertyName("about")]
        public string About { get; set; }

        [JsonPropertyName("verification")]
        public Verification Verification { get; set; }

        [JsonPropertyName("avg_response_time")]
        public object AvgResponseTime { get; set; }

        [JsonPropertyName("carrier_ids")]
        public List<int> CarrierIds { get; set; }

        [JsonPropertyName("carriers_without_custom_ids")]
        public List<int> CarriersWithoutCustomIds { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("updated_on")]
        public int UpdatedOn { get; set; }

        [JsonPropertyName("is_hated")]
        public bool IsHated { get; set; }

        [JsonPropertyName("hates_you")]
        public bool HatesYou { get; set; }

        [JsonPropertyName("is_favourite")]
        public bool IsFavourite { get; set; }

        [JsonPropertyName("profile_url")]
        public string ProfileUrl { get; set; }

        [JsonPropertyName("share_profile_url")]
        public string ShareProfileUrl { get; set; }

        [JsonPropertyName("facebook_user_id")]
        public object FacebookUserId { get; set; }

        [JsonPropertyName("is_online")]
        public bool IsOnline { get; set; }

        [JsonPropertyName("can_view_profile")]
        public bool CanViewProfile { get; set; }

        [JsonPropertyName("can_bundle")]
        public bool CanBundle { get; set; }

        [JsonPropertyName("country_title_local")]
        public string CountryTitleLocal { get; set; }

        [JsonPropertyName("last_loged_on")]
        public string LastLogedOn { get; set; }

        [JsonPropertyName("accepted_pay_in_methods")]
        public List<AcceptedPayInMethod> AcceptedPayInMethods { get; set; }

        [JsonPropertyName("localization")]
        public string Localization { get; set; }

        [JsonPropertyName("is_bpf_price_prominence_applied")]
        public bool IsBpfPriceProminenceApplied { get; set; }

        [JsonPropertyName("msg_template_count")]
        public int MsgTemplateCount { get; set; }
    }


    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class AcceptedPayInMethod
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("requires_credit_card")]
        public bool RequiresCreditCard { get; set; }

        [JsonPropertyName("event_tracking_code")]
        public string EventTrackingCode { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("translated_name")]
        public string TranslatedName { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("method_change_possible")]
        public bool MethodChangePossible { get; set; }
    }

    public class BrandDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("favourite_count")]
        public int FavouriteCount { get; set; }

        [JsonPropertyName("pretty_favourite_count")]
        public string PrettyFavouriteCount { get; set; }

        [JsonPropertyName("item_count")]
        public int ItemCount { get; set; }

        [JsonPropertyName("pretty_item_count")]
        public string PrettyItemCount { get; set; }

        [JsonPropertyName("is_visible_in_listings")]
        public bool IsVisibleInListings { get; set; }

        [JsonPropertyName("requires_authenticity_check")]
        public bool RequiresAuthenticityCheck { get; set; }

        [JsonPropertyName("is_luxury")]
        public bool IsLuxury { get; set; }

        [JsonPropertyName("is_hvf")]
        public bool IsHvf { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("is_favourite")]
        public bool IsFavourite { get; set; }
    }

    public class DescriptionAttribute
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("faq_id")]
        public object FaqId { get; set; }
    }

    public class Email
    {
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }

        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }

    public class Facebook
    {
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }

        [JsonPropertyName("verified_at")]
        public object VerifiedAt { get; set; }

        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }

    public class Google
    {
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }

        [JsonPropertyName("verified_at")]
        public DateTime VerifiedAt { get; set; }

        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }


    public class ItemAttribute
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("ids")]
        public List<int> Ids { get; set; }
    }

    public class Phone
    {
        [JsonPropertyName("valid")]
        public bool Valid { get; set; }

        [JsonPropertyName("verified_at")]
        public DateTime VerifiedAt { get; set; }

        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }

    public class Photo2
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

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
    }

    public class Price
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class ItemDetail
    {
        [JsonPropertyName("item")]
        public Item Item { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class Verification
    {
        [JsonPropertyName("email")]
        public Email Email { get; set; }

        [JsonPropertyName("facebook")]
        public Facebook Facebook { get; set; }

        [JsonPropertyName("google")]
        public Google Google { get; set; }

        [JsonPropertyName("phone")]
        public Phone Phone { get; set; }
    }

}
