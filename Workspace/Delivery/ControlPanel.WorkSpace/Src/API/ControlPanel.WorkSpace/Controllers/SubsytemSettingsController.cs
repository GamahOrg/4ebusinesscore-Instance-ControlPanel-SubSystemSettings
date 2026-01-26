

using _4EPlatform.Application.Features.Query.GetAllSubsytemSettingsMainClassification;
using _4EPlatform.Application.Features.Query.GetComponentData;
using _4EPlatform.Application.Features.Query.SubsytemMainLookup;
using Domain.Enums;

namespace ControlPanel.WorkSpace.Controllers
{
    [Authorize(Policy = "ResourceOwnerPolicy")]
    [ApiController]
    public class SubsytemSettingsController : ApiBaseController
    {
        private readonly IMediator _mediator;
        public SubsytemSettingsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet]
        [Route("GetAllsubsystem")]
        public async Task<ResponseDTO> GetAllsubsystem(SubsystemType? subtemType)

        {
            return await _mediator.Send(new GetAllWorkSpaceSubSystemQuery { SubsystemType = subtemType });

        }
        //[HttpGet("GetAllComponent/{workSpaceId}")]
        //public async Task<ResponseDTO> GetAllComponent(Guid workSpaceId, string? searhTerm)
        //{
        //    return await _mediator.Send(new GetComponentBySubsystemQuery {WorkSpaceId = workSpaceId , SearchTerm = searhTerm }); ;
        //}
        //[HttpPost]
        //public async Task<ResponseDTO> PostWorkSpace([FromBody] PostWorkSpaceCommand command)
        //{

        //    return await _mediator.Send(command);
        //}

        [HttpPut]
        [Route("PutSubsystemSquence")]
        public async Task<ResponseDTO> PutSubsystemSquence(PutSubsystemSquenceCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDTO> DeleteWorkSpace(Guid id)
        {
            return await _mediator.Send(new DeleteWorkSpaceCommand { Id = id });
        }
        [HttpPut]
        [Route("PutWorkSpaceActivaion/{id}")]
        public async Task<ResponseDTO> PutWorkSpaceActivaion(Guid id, [FromBody] PutWorkSpaceActivationCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }
        [HttpGet]
        [Route("GetAllsubsystemfilter")]
        public async Task<ResponseDTO> GetAllsubsystemfilter(string? searhTerm, DateTime? date, int? numberofservices, string? component, SubsystemType SubsystemType, int pagesize, int pagenumber)

        {
            return await _mediator.Send(new GetAllSubsystemQuery
            {
                SearchTerm = searhTerm,
                Date = date,
                NumberOfServices = numberofservices,
                Component = component,
                SubsystemType = SubsystemType,
                PaginatedInputModel = new _4EPlatform.Application.Paginations.PaginatedInputModel
                {
                    PageNumber = pagenumber,
                    PageSize = pagesize
                }
            });

        }
        [HttpGet]
        [Route("GetAllSubsystemAndComponent")]
        public async Task<ResponseDTO> GetAllSubsystemAndComponent(string? searhTerm)

        {
            return await _mediator.Send(new GetAllSubsystemAndComponentQuery
            {
                SearchTerm = searhTerm
            });

        }
        [HttpGet]
        [Route("GetAllSubsystemAndComponentTree")]
        public async Task<ResponseDTO> GetAllSubsystemAndComponentTree(string? searhTerm)

        {
            return await _mediator.Send(new GetAllSubsystemAndComponentTreeQuery
            {
                SearchTerm = searhTerm
            });

        }

        //#region GetAll
        //[HttpGet]
        //public async Task<ResponseDTO> GetAll(NoteTypeId? NoteTypeId,string? searhTerm)
        //{
        //    return await _mediator.Send(new GetAllWorkSpaceQuery { NoteTypeId= NoteTypeId,SearchTerm=searhTerm });
        //}
        //#endregion
        //#region GetAllParts
        //[HttpGet("GetAllParts/{workSpaceId}")]
        //public async Task<ResponseDTO> GetAllParts(Guid workSpaceId, string? searhTerm)
        //{
        //    return await _mediator.Send(new GetAllWorkSpacePartsQuery { WorkSpaceId = workSpaceId, SearchTerm = searhTerm }); ;
        //}
        //#endregion
        //#region GetById
        //[HttpGet("GetById/{workSpaceId}")]
        //public async Task<ResponseDTO> GetById(Guid workSpaceId)
        //{
        //    return await _mediator.Send(new GetAllWorkSpaceByIdQuery { WorkSpaceId = workSpaceId }); ;
        //}
        //#endregion
        #region UpdateVisitDateToWorkSpace
        [HttpPost("UpdateVisitDateToWorkSpace")]
        public async Task<ResponseDTO> UpdateVisitDateToWorkSpace(UpdateVisitDateToWorkSpaceCommand request)
        {
            return await _mediator.Send(request);
        }
        #endregion

        [HttpGet]
        [Route("GetAllLookUp")]
        public async Task<ResponseDTO> GetAllLookUp()

        {
            return await _mediator.Send(new GetAllLookUpQuery());

        }
        [HttpGet]
        [Route("GetLookUpById/{lookupId}")]
        public async Task<ResponseDTO> GetLookUpById(Guid lookupId)
        {
            return await _mediator.Send(new GetLookUpByIdQuery() { LooKUpId = lookupId });
        }

        [HttpPost]
        [Route("PostLookUp")]
        public async Task<ResponseDTO> PostLookUp(PostLookUpCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [Route("PutLookUp/{LookUpId}")]
        public async Task<ResponseDTO> PutLookUp(Guid LookUpId, PutLookUpCommand command)
        {
            command.Id = LookUpId;
            return await _mediator.Send(command);
        }
        [HttpDelete]
        [Route("DeleteLookUp/{LookUpId}")]
        public async Task<ResponseDTO> DeleteLookUp(Guid LookUpId)
        {
            return await _mediator.Send(new DeleteLookUpCommand() { Id = LookUpId });
        }
        [HttpGet]
        [Route("GetAllTypeWithListOfWorkspaceParents")]
        public async Task<ResponseDTO> GetAllTypeWithListOfWorkspaceParents()
        {
            return await _mediator.Send(new GetAllTypeWithListOfWorkspaceParentsQuery());
        }

        [HttpGet]
        [Route("GetAllSubsytemSettingsMainClassification")]
        public async Task<ResponseDTO> GetAllSubsytemSettingsMainClassification()

        {
            return await _mediator.Send(new GetAllSubsytemSettingsMainClassificationQuery());

        }
        [HttpGet]
        [Route("GetAllSubsytemMainLookup")]
        public async Task<ResponseDTO> GetAllSubsytemMainLookup()

        {
            return await _mediator.Send(new GetAllSubsytemMainLookupQuery());

        }
        [HttpGet]
        [Route("GetComponentData")]
        public async Task<ResponseDTO> GetComponentData(string route)

        {
            return await _mediator.Send(new GetComponentDataCommand { Route = route});

        }
    }

}