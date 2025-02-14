﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Dtos;
using OnlineShop.Models;
using OnlineShop.Services.Detail;
using OnlineShop.Services.Villa;

namespace OnlineShop.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        private readonly IDetailService _detailService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public DetailController(IDetailService detailService, IVillaService villaService, IMapper mapper)
        {
            _mapper = mapper;
            _detailService = detailService;
            _villaService = villaService;
        }

        /// <summary>
        /// get all details for a villa
        /// </summary>
        /// <param name="villaId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{villaId:int}")]
        public async Task<IActionResult> GetAllVillaDetails(int villaId)
        {
            var villa = await _villaService.GetById(villaId);
            if (villa == null) return NotFound();
            var data = await _detailService.GetAllVillaDetail(villaId);
            var MappedData = _mapper.Map<List<DetailDto>>(data);
            return Ok(MappedData);
        }

        /// <summary>
        /// get a detail with id
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{detailId:int}")]
        public async Task<IActionResult> GetDetailById(int detailId)
        {
            var detail = await _detailService.GetDetail(detailId);
            if (detail == null) return NotFound();
            var mappedData = _mapper.Map<DetailDto>(detail);
            return Ok(mappedData);
        }

        /// <summary>
        /// create a new detail for villa
        /// </summary>
        /// <param name="detailModel"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateVillaDetail([FromBody] DetailDto detailModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var mapperData = _mapper.Map<Detail>(detailModel);
            if (mapperData == null) return BadRequest();
            await _detailService.CreateDetail(mapperData);
            return Ok("Created Successfully");
        }

        /// <summary>
        /// update a villa using id
        /// </summary>
        /// <param name="detailDto"></param>
        /// <param name="DetailId"></param>
        /// <returns></returns>
        [HttpPatch("{DetailId:int}")]
        public async Task<IActionResult> UpdateDetail([FromBody] DetailDto detailDto, int DetailId)
        {
            if (detailDto.Detailid != DetailId) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var mappedData = _mapper.Map<Detail>(detailDto);
            await _detailService.UpdateDetail(mappedData);
            return Ok("Updated Successfully");
        }

        /// <summary>
        /// delete villa detail with id
        /// </summary>
        /// <param name="DetailId"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{DetailId:int}")]
        public async Task<IActionResult> DeleteDetail(int DetailId)
        {
            var detail = await _detailService.GetDetail(DetailId);
            if (detail == null) return NotFound();
            await _detailService.DeleteDetail(detail);
            return Ok("deleted successfully");
        }
    }
}
