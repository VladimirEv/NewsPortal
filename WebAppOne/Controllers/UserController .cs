
namespace NewsPortal.Web.Controllers
{
    /// <summary>
    /// [ApiController] предоставляет несколько важных функциональных возможностей:
    /// 1.Модель привязки по умолчанию: Атрибут [ApiController] включает модель привязки параметров по умолчанию, 
    /// что означает, что ASP.NET Core будет автоматически разбирать запросы, поступающие на ваш контроллер, 
    /// и связывать их с параметрами методов действия. Это упрощает разработку API, поскольку вы можете избежать 
    /// явного кода для извлечения данных из запросов.
    ///2.Обработка ошибок: [ApiController] также включает обработку и преобразование исключений в ответы HTTP.
    ///Например, если в вашем методе действия происходит исключение, [ApiController] автоматически создаст 
    ///HTTP-ответ с соответствующим кодом состояния и сообщением об ошибке.
    ///3.Поддержка атрибутов для валидации: Вы можете использовать атрибуты данных, такие как[Required], 
    ///[MaxLength], [Range] и другие, чтобы определить правила валидации модели входных данных.Эти атрибуты 
    ///будут учитываться автоматически при разборе входных данных в методах действия.
    ///4.Поддержка конвенций и маршрутизации: [ApiController] также включает некоторые стандартные конвенции 
    ///маршрутизации и именования методов, что делает код более ясным и упрощает разработку API.
    ///5.Поддержка возвращаемых типов: [ApiController] позволяет возвращать объекты результатов, такие 
    ///как Ok, CreatedAtAction, BadRequest, и др., которые автоматически преобразуются в соответствующие 
    ///HTTP-ответы.
    ///6.Встроенная документация: [ApiController] также улучшает возможности автоматической документации 
    ///API с помощью инструментов, таких как Swagger.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(AuthorizationPolicies.AdminPolicy)]
        public async Task<IActionResult> Get()
        {
            return GetResponse(await _userService.GetAll());
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            return GetResponse(await _userService.GetById(id));
        }

        [HttpGet]
        [Route("{email}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthorizationPolicies.AdminPolicy)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return GetResponse(await _userService.GetByEmail(email));
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            return GetResponse(await _userService.Register(model));
        }

        [HttpPut("{userId:guid}")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> Update(Guid userId, UserModel model)
        {
            return GetResponse(await _userService.UpdateUserById(userId, model));
        }

        [HttpPut("{userId:guid}/changePassword")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> ChangePasswordById(Guid userId, ChangePasswordModel model)
        {
            return GetResponse(await _userService.ChangePasswordById(userId, model));
        }

        [HttpPost("{userId:guid}/generateChangeEmail")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateChangeEmailTokenById(Guid userId, string newEmail)
        {
            return GetResponse(await _userService.GenerateChangeEmailTokenById(userId, newEmail));
        }

        [HttpPut("{userId:guid}/changeEmail")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> ChangeEmailById(Guid userId, string newEmail, string token)
        {
            return GetResponse(await _userService.ChangeEmailById(userId, newEmail, token));
        }

        [HttpPut("{userId:guid}/blockUser")]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthorizationPolicies.AdminPolicy)]
        public async Task<IActionResult> BlockUserById(Guid userId, int hours)
        {
            return GetResponse(await _userService.BlockUserById(userId, hours));
        }
    }
}
