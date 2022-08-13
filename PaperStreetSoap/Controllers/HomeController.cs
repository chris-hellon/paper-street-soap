using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Interfaces.Services;
using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Core.Interfaces.Repositories;
using PaperStreetSoap.Views.Home;
using System.Text;
using System.Xml.Serialization;
using AspNetCore.SEOHelper.Sitemap;
using ProjectBase.Core.Utils;

namespace PaperStreetSoap.Controllers;

public class HomeController : Controller
{
    public IEnumerable<PageContent> About { get; set; } = null;
    public IEnumerable<Chart> Charts { get; set; } = null;
    private readonly IPageContentRepository _pageContentRepository;
    private readonly IChartsRepository _chartsRepository;
    private readonly IVideosRepository _videosRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IErrorLoggerService _errorLoggerService;

    public HomeController(IErrorLoggerService errorLoggerService, IPageContentRepository pageContentRepository, IChartsRepository chartsRepository, IVideosRepository videosRepository, IUsersRepository usersRepository)
    {
        _errorLoggerService = errorLoggerService;
        _pageContentRepository = pageContentRepository;
        _chartsRepository = chartsRepository;
        _videosRepository = videosRepository;
        _usersRepository = usersRepository;
    }

    [Route("/")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var shortIntroSection = _pageContentRepository.GetPageSectionContent(6);
            var aboutSection = _pageContentRepository.GetPageSectionContent(1);
            var welcomeVideoSection = _pageContentRepository.GetPageSectionContent(2);
            var chartsSection = _chartsRepository.GetCharts(4);

            await Task.WhenAll(shortIntroSection, aboutSection, chartsSection, welcomeVideoSection);

            return View(new IndexModel(aboutSection.Result, chartsSection.Result, welcomeVideoSection.Result, shortIntroSection.Result));
        }
        catch (Exception e)
        {
            _errorLoggerService.LogError(e);
            return LocalRedirect("/error");
        }
    }

    [Route("/unsubscribe/{id}")]
    public async Task<IActionResult> Unsubscribe(string id)
    {
        try
        {
            await _usersRepository.UnsubscribeUser(id);
            return View();
        }
        catch (Exception e)
        {
            _errorLoggerService.LogError(e);
            return LocalRedirect("/error");
        }
    }

    [Route("/robots.txt")]
    public ContentResult RobotsTxt()
    {
        var sb = new StringBuilder();
        sb.AppendLine("User-agent: *")
            .AppendLine("Disallow:")
            .Append("sitemap: ")
            .Append(this.Request.Scheme)
            .Append("://")
            .Append(this.Request.Host)
            .AppendLine("/sitemap.xml");

        return this.Content(sb.ToString(), "text/plain", Encoding.UTF8);
    }

    [Route("/sitemap.xml")]
    public ContentResult Sitemap()
    {
        StringBuilder sb = new();
        sb.Append(GenerateSitemap());

        return new ContentResult
        {
            ContentType = "application/xml",
            Content = sb.ToString(),
            StatusCode = 200
        };
    }

    private string GenerateSitemap()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var sitemapNodes = new List<SitemapNode>()
            {
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = baseUrl, Frequency = SitemapFrequency.Weekly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.3, Url = baseUrl + "/about", Frequency = SitemapFrequency.Monthly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/disclaimer", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/terms-and-conditions", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/login", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/register", Frequency = SitemapFrequency.Yearly },
                new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.1, Url = baseUrl + "/forgot-password", Frequency = SitemapFrequency.Yearly }
            };

        var membersSections = new List<string>()
            {
                "latest-review",
                "market-reviews",
                "education",
                "resources",
                "live-trading",
                "my-account",
                "email",
                "change-password",
                "two-factor-authentication",
                "personal-data",
                "subscription"
            };

        membersSections.ForEach(async membersSection =>
        {
            sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = $"{baseUrl}/members/{membersSection}", Frequency = SitemapFrequency.Weekly });

            IEnumerable<Video> videos = null;

            switch (membersSection)
            {
                case "market-reviews":
                    videos = await _videosRepository.GetMarketReviews();
                    break;
                case "education":
                    videos = await _videosRepository.GetMembersEducation();
                    break;
                case "resources":
                    ViewData["Title"] = "Resources";
                    videos = await _videosRepository.GetResources();
                    break;
            }

            if (videos != null)
            {
                foreach (var video in videos)
                {
                    var url = $"members/{membersSection}/{video.Id}/{video.Title.UrlFriendly()}";

                    sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = $"{baseUrl}/{url}", Frequency = SitemapFrequency.Weekly });
                }
            }
        });


        var adminSections = new List<string>()
            {
                "videos",
                "charts",
                "packages",
                "live-trading",
                "users",
                "orders",
                "pages/home",
                "pages/about",
                "pages/terms-and-conditions",
                "pages/disclaimer"
            };

        adminSections.ForEach(adminSection =>
        {
            sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = $"{baseUrl}/admin/{adminSection}", Frequency = SitemapFrequency.Weekly });
        });

        var freeContentSections = new List<string>()
            {
                "gm-bitcoin",
                "education",
                "charts"
            };

        freeContentSections.ForEach(async freeContentSection =>
        {
            sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = $"{baseUrl}/free-content/{freeContentSection}", Frequency = SitemapFrequency.Weekly });

            IEnumerable<Video> videos = null;

            switch (freeContentSection)
            {
                case "education":
                    videos = await _videosRepository.GetFreeEducation();
                    break;
                case "gm-bitcoin":
                    videos = await _videosRepository.GetGmBitcoin();
                    break;
            }

            if (videos != null)
            {
                foreach (var video in videos)
                {
                    var url = $"free-content/{freeContentSection}/{video.Id}/{video.Title.UrlFriendly()}";

                    sitemapNodes.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.6, Url = $"{baseUrl}/{url}", Frequency = SitemapFrequency.Weekly });
                }
            }
        });

        XmlSerializer serializer = new(typeof(List<SitemapNode>));

        var stringwriter = new StringWriter();
        serializer.Serialize(stringwriter, sitemapNodes);

        return stringwriter.ToString();
    }
}

