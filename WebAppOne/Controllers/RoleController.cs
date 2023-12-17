namespace NewsPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService) 
        { 
            _roleService = roleService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthorizationPolicies.AdminPolicy)]
        public async Task<IActionResult> CreateRole(RoleModel model)
        {
            return GetResponse(await _roleService.CreateRole(model));
        }

        [HttpPut]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthorizationPolicies.AdminPolicy)]
        public async Task<IActionResult> UpdateRoleById(Guid roleId, RoleModel model)
        {
            return GetResponse(await _roleService.UpdateById(roleId, model));
        }

        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [Authorize(AuthorizationPolicies.AdminPolicy)]
        public async Task<IActionResult> Get()
        {
            return GetResponse(await _roleService.GetAll());
        }

        [HttpGet("{roleId:guid}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [Authorize(AuthorizationPolicies.AdminPolicy)]
        public async Task<IActionResult> GetById(Guid roleId)
        {
            return GetResponse(await _roleService.GetById(roleId));
        }

        [HttpDelete("{roleId:guid}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthorizationPolicies.AdminPolicy)]
        public async Task<IActionResult> Delete(Guid roleId)
        {
            return GetResponse(await _roleService.DeleteById(roleId));
        }

    }
}
