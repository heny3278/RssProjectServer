using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSSProject;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class News : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRssFeed()
        {
            List<NewsTopic> rssFeed = RSSProject.RssFeedReader.GetNewsTopics();
            return Ok(rssFeed);
        }
    }
}
