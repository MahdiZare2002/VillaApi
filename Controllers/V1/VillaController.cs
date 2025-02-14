using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Dtos;
using OnlineShop.Models;
using OnlineShop.Services.Villa;

namespace OnlineShop.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly IVillaService _service;
        private readonly IMapper _mapper;
        public VillaController(IVillaService service, IMapper mapper) 
        { 
            _service = service; 
            _mapper = mapper;
        }

        /// <summary>
        /// get all villa list
        /// </summary>
        /// <returns></returns>

        [HttpGet("[action]")]
        public async Task<IActionResult> getDataAsync()
        {
            var data = await _service.GetAll().ToListAsync();
            var mappedData = _mapper.Map<List<VillaDto>>(data);
            return Ok(new { mappedData });
        }

        /// <summary>
        /// get a villa with id
        /// </summary>
        /// <param name="villaId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{villaId:int}", Name = "GetDetails")]
        public async Task<IActionResult> GetDetailsAsync([FromRoute] int villaId)
        {
            var villa = await _service.GetById(villaId);
            if (villa == null) return NotFound();
            var model = _mapper.Map<VillaDto>(villa);
            return Ok(model);
        }

        /// <summary>
        /// create a new villa
        /// </summary>
        /// <param name="villaData"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateVilla([FromBody] Models.Villa villaData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.Create(villaData);
            if (result != null)
            {
                return CreatedAtRoute("GetDetails", new { villaId = result.Id }, result);
            }
            return Ok(result);
        }


        /// <summary>
        /// update exist villa
        /// </summary>
        /// <param name="villaId"></param>
        /// <param name="villaData"></param>
        /// <returns></returns>
        [HttpPatch("{villaId:int}")]
        public async Task<IActionResult> UpdateVilla(int villaId, Models.Villa villaData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (villaId != villaData.Id)
            {
                return NotFound();
            }

            var result = await _service.Update(villaData);
            if (result != null) return NoContent();

            ModelState.AddModelError("", "there is a problem in update Villa Details");
            return StatusCode(500, ModelState);
        }

        /// <summary>
        /// delete villa with id
        /// </summary>
        /// <param name="villaId"></param>
        /// <returns></returns>
        [HttpDelete("{villaId:int}")]
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var villa = await _service.GetById(villaId);
            if (villa == null)
            {
                return NotFound();
            }

            var result = await _service.Delete(villa);
            if (result != null) return NoContent();

            ModelState.AddModelError("", "there is a problem in deleting Villa");
            return StatusCode(500, ModelState);
        }
    }
}
