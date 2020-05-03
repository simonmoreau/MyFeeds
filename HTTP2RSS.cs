using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Xml;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http;

namespace HTTP2RSS
{
    public static class HTTP2RSS
    {
        public static string BlobBaseUrl = "https://helloworldfromb809c.blob.core.windows.net/rssfeeds/";

        [FunctionName("HTTP2RSS")]
        public static void Run([TimerTrigger("0 0 */6 * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            try
            {
                IConfigurationRoot configRoot = new ConfigurationBuilder()
.SetBasePath(context.FunctionAppDirectory)
.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
.AddEnvironmentVariables()
.Build();

                List<WebsiteFeed> feeds = new List<WebsiteFeed>();
                // feeds.Add(new LeVestiaireDuRenard());
                feeds.Add(new PermanentStyle());
                feeds.Add(new Batirama());
                feeds.Add(new BonneGueule());
                // feeds.Add(new PhilippeSilberzahn());
                feeds.Add(new TheSocialiteFamily());

                foreach (WebsiteFeed feed in feeds)
                {
                    string textFeed = BuildXmlFeed(feed);

                    string blobUIR = UploadXmlFeedFile(textFeed, feed.FeedId, configRoot).Result;
                }

                log.LogInformation($"RSSFeed function executed at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                log.LogInformation($"{DateTime.Now} : Error : {ex.ToString()}");
                throw ex;
            }
        }

        private static string BuildXmlFeed(WebsiteFeed feed)
        {
            List<Article> articles = feed.Articles;

            StringWriter parent = new StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(parent))
            {
                writer.WriteStartElement("feed");
                writer.WriteAttributeString("xmlns", "http://www.w3.org/2005/Atom");

                writer.WriteStartElement("link");
                writer.WriteAttributeString("href", feed.FeedLink);
                writer.WriteAttributeString("rel", "self");
                writer.WriteAttributeString("type", "application/atom+xml");
                writer.WriteEndElement();

                writer.WriteStartElement("link");
                writer.WriteAttributeString("href", feed.WebLink);
                writer.WriteAttributeString("rel", "alternate");
                writer.WriteAttributeString("type", "text/html");
                writer.WriteEndElement();

                // write out -level elements
                writer.WriteElementString("updated", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", System.Globalization.DateTimeFormatInfo.InvariantInfo));
                writer.WriteElementString("id", feed.FeedLink);

                writer.WriteStartElement("title");
                writer.WriteAttributeString("type", "html");
                writer.WriteString(feed.Title);
                writer.WriteEndElement();

                writer.WriteStartElement("subtitle");
                writer.WriteString(feed.Subtitle);
                writer.WriteEndElement();

                int i = 0;

                if (articles != null)
                {
                    foreach (var article in articles)
                    {
                        writer.WriteStartElement("entry");

                        writer.WriteStartElement("title");
                        writer.WriteAttributeString("type", "html");
                        writer.WriteString(article.Title);
                        writer.WriteEndElement();

                        writer.WriteStartElement("link");
                        writer.WriteAttributeString("href", article.Link); // todo build article path
                        writer.WriteAttributeString("type", "text/html");
                        writer.WriteAttributeString("title", article.Title);
                        writer.WriteEndElement();

                        writer.WriteElementString("published", article.Updated.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", System.Globalization.DateTimeFormatInfo.InvariantInfo));
                        writer.WriteElementString("updated", article.Updated.ToString("yyyy-MM-dd'T'HH:mm:ss.fffzzz", System.Globalization.DateTimeFormatInfo.InvariantInfo));

                        writer.WriteStartElement("content");
                        writer.WriteAttributeString("type", "html");
                        writer.WriteString(article.Content);
                        writer.WriteEndElement();

                        writer.WriteStartElement("category");
                        writer.WriteAttributeString("term", "Clothe");
                        writer.WriteEndElement();

                        writer.WriteStartElement("summary");
                        writer.WriteAttributeString("type", "html");
                        writer.WriteString(article.Summary);
                        writer.WriteEndElement();

                        if (article.MediaLink != "")
                        {
                            writer.WriteStartElement("media:thumbnail");
                            writer.WriteAttributeString("xmlns:media", "http://search.yahoo.com/mrss/");
                            writer.WriteAttributeString("url", article.MediaLink);
                            writer.WriteEndElement();
                        }

                        if (article.Author != "")
                        {
                            writer.WriteStartElement("author");
                            writer.WriteElementString("name", article.Author);
                            writer.WriteEndElement();
                        }
                        else
                        {
                            writer.WriteStartElement("author");
                            writer.WriteElementString("name", "Simon Moreau");
                            writer.WriteEndElement();
                        }

                        writer.WriteElementString("id", article.Id);

                        writer.WriteEndElement();
                        i++;
                    }
                }

                // write out 
                writer.WriteEndElement();
            }

            return parent.ToString();
        }

        private async static Task<string> UploadXmlFeedFile(string xmlFile, string feedId, IConfigurationRoot configRoot)
        {
            string accessKey;
            string accountName;
            string connectionString;
            CloudStorageAccount storageAccount;
            CloudBlobClient client;
            CloudBlobContainer container;

            accessKey = configRoot["BlobStorageAccessKey"];
            accountName = configRoot["BlobStorageAccountName"];
            connectionString = "DefaultEndpointsProtocol=https;AccountName=" + accountName + ";AccountKey=" + accessKey + ";EndpointSuffix=core.windows.net";
            storageAccount = CloudStorageAccount.Parse(connectionString);

            client = storageAccount.CreateCloudBlobClient();

            container = client.GetContainerReference("rssfeeds");

            await container.CreateIfNotExistsAsync();

            CloudBlockBlob blob = container.GetBlockBlobReference(feedId + ".xml");
            blob.Properties.ContentType = "application/atom+xml";

            using (Stream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlFile)))
            {
                await blob.UploadFromStreamAsync(stream);
            }

            return blob.Uri.ToString();
        }
    }
}
