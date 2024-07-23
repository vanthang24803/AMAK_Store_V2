using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Address;
using AMAK.Application.Services.Address.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize]
    public class AddressesController : BaseController {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService) {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BaseQuery query) {
            return Ok(await _addressService.GetAddressesAsync(User, query));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressRequest request) {
            return StatusCode(StatusCodes.Status201Created, await _addressService.CreateAddressAsync(User, request));
        }

        [HttpGet]
        [Route("Account/{id}")]
        [Authorize(Roles = $"{Role.MANAGER}, {Role.ADMIN}")]
        public async Task<IActionResult> GetAddresses([FromRoute] string id, [FromQuery] BaseQuery query) {
            return Ok(await _addressService.GetAddressesUserAsync(id, query));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id) {
            return Ok(await _addressService.GetAddressDetailAsync(id));
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AddressRequest request) {
            return Ok(await _addressService.UpdateAddressAsync(User, id, request));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id) {
            return Ok(await _addressService.RemoveAddressAsync(User, id));
        }

    }
}