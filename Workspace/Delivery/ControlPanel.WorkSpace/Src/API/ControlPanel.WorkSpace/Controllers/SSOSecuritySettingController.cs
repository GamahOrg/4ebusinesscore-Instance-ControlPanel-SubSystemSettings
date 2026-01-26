using _4EPlatform.Application.Features.SSOSecuritySetting.Command.Delete;
using _4EPlatform.Application.Features.SSOSecuritySetting.Command.Post;
using _4EPlatform.Application.Features.SSOSecuritySetting.Command.Put;
using _4EPlatform.Application.Features.SSOSecuritySetting.Query.GetAllSSOSecuritySetting;


namespace ControlPanel.WorkSpace.Controllers
{
    [Authorize(Policy = "ResourceOwnerPolicy")]
    [ApiController]
    public class SSOSecuritySettingController : ApiBaseController
    {
        private readonly IMediator _mediator;
        public SSOSecuritySettingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpPost]
        [Route("PostSSOSecuritySetting")]

        public async Task<ResponseDTO> PostSSOSecuritySetting([FromQuery] PostSSOSecuritySettingCommand command)
        {
            return await _mediator.Send(command);

        }
        [HttpGet]
        [Route("GetAllSSOSecuritySetting")]

        public async Task<ResponseDTO> GetAllSSOSecuritySetting()
        {
            return await _mediator.Send(new GetAllSSOSecuritySettingQuery());

        }

        [HttpPut]
        [Route("PutSSOSecuritySetting")]

        public async Task<ResponseDTO> PutSSOSecuritySetting([FromQuery] PutSSoSecurtiySettingCommand command)
        {
            return await _mediator.Send(command);

        }
        [HttpDelete]
        [Route("DeleteSSOSecuritySetting")]

        public async Task<ResponseDTO> DeleteSSOSecuritySetting([FromQuery] DeleteSSoSecurtiySeetingCommand command)
        {
            return await _mediator.Send(command);

        }
    }
    }
