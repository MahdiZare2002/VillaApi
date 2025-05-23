﻿using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Villa;

namespace OnlineShop.Controllers.V2
{
    [ApiVersion(2.0)]
    [Route("api/v{version:apiVersion}/villa")]
    [ApiController]
    public class VillaV2Controller : ControllerBase
    {
        private readonly IVillaService _villaService;
        public VillaV2Controller(IVillaService villaService)
        {
            _villaService = villaService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> getDataAsync(int pageId = 1, string filter = "", int take = 2)
        {
            if (pageId < 1 || take < 1) return BadRequest();
            var data = await _villaService.SerachVillaAsync(pageId, filter, take);
            return Ok(data);
        }
    }
}
