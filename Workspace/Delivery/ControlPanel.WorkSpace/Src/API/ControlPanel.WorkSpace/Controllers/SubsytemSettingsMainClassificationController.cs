
using _4EPlatform.Application.ApplicationDto.CommandDto.SubsytemSettingsMainClassification;
using _4EPlatform.Application.Features.Query.GetAllSubsytemSettingsMainClassification;
using Application.Features.CommandDto.SubsytemSettingsMainClassification;

namespace ControlPanel.SubsytemSettingsMainClassification.Controllers
{
    [Authorize(Policy = "ResourceOwnerPolicy")]
    [ApiController]
    public class SubsytemSettingsMainClassificationController : ApiBaseController
    {
        private readonly IMediator _mediator;
        public SubsytemSettingsMainClassificationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<ResponseDTO> PostSubsytemSettingsMainClassification([FromBody] PostSubsytemSettingsMainClassificationCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ResponseDTO> PutSubsytemSettingsMainClassification(Guid id, [FromBody] PutSubsytemSettingsMainClassificationCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDTO> DeleteSubsytemSettingsMainClassification(Guid id)
        {
            return await _mediator.Send(new DeleteSubsytemSettingsMainClassificationCommand { Id = id });
        }
        [HttpPut]
        [Route("PutSubsytemSettingsMainClassificationSquence")]
        public async Task<ResponseDTO> PutSubsytemSettingsMainClassificationSquence([FromBody] PutSubsytemSettingsMainClassificationSquenceCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpGet]
        [Route("GetAllSubsytemSettingsMainClassificationByRowCount")]
        public async Task<ResponseDTO> GetAllSubsytemSettingsMainClassificationByRowCount(string? searh, int pageSize, int pageNumber,bool?IsVisable)

        {
            return await _mediator.Send(new GetAllSubsytemMainByRowCountQuery
            {
                Search = searh,
                IsVisable = IsVisable,

                PaginatedInputModel = new _4EPlatform.Application.Paginations.PaginatedInputModel
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                }
            });

        }
        [HttpGet]
        [Route("GetAllSubsytemSettingsMainClassification")]
        public async Task<ResponseDTO> GetAllSubsytemSettingsMainClassification()

        {
            return await _mediator.Send(new GetAllSubsytemSettingsMainClassificationQuery());

        }
        [HttpGet("GetById/{id}")]
        public async Task<ResponseDTO> GetById(Guid id)
        {
            return await _mediator.Send(new GetSubsytemSettingsMainClassificationByIdQuery { Id = id });
        }
        [HttpGet("GetSubsytemSettingsByMainClassificationId/{id}")]
        public async Task<ResponseDTO> GetSubsytemSettingsByMainClassificationId(Guid id)
        {
            return await _mediator.Send(new GetSubsytemSettingsByMainClassificationIdQuery { Id = id });
        }
    }

}