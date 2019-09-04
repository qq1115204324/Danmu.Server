using System.Linq;
using System.Net;
using Danmaku.Model;
using Danmaku.Utils.Dao;
using Microsoft.AspNetCore.Mvc;

namespace Danmaku.Controllers.Dplayer.V3
{
    [Route("api/dplayer/v3/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly IDanmakuDao _dd;

        public DefaultController(IDanmakuDao danmakuDao)
        {
            _dd = danmakuDao;
        }

        // GET: api/dplayer/v3/
        [HttpGet]
        public DanmakuWebResult Get()
        {
            string id = Request.Query["id"];
            if (string.IsNullOrEmpty(id)) return new DanmakuWebResult(1);
            var result = _dd.DanmakuQuery(id);
            return new DanmakuWebResult(result);
        }

        // POST: api/dplayer/v3/
        [HttpPost]
        public DanmakuWebResult Post([FromBody] DanmakuDataInsert data)
        {
            if (string.IsNullOrWhiteSpace(data.Id) || string.IsNullOrWhiteSpace(data.Text))
                return new DanmakuWebResult(1);
            data.Ip = IPAddress.TryParse(Request.Headers["X-Real-IP"], out var ip)
                ? ip
                : Request.HttpContext.Connection.RemoteIpAddress;
            data.Referer = Request.Headers["Referer"].FirstOrDefault();

            var result = _dd.DanmakuInsert(data);
            return result == 0 ? new DanmakuWebResult(1) : new DanmakuWebResult(0);
        }
    }
}