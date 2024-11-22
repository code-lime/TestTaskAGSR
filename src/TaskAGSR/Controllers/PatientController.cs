using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskAGSR.Application.Common.VM;
using TaskAGSR.Application.Data.Queries;
using TaskAGSR.Models.Response;

namespace TaskAGSR.Controllers;

[ApiController]
[Route("patient")]
public class PatientController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        [FromBody] PatientVM<PatientIdentityVM> patient,
        CancellationToken cancellationToken)
        => Ok(SuccessResponse.Create(await mediator.Send(new PatientCreateQuery(patient), cancellationToken)));

    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<PatientVM<PatientIndexIdentityVM>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(
        [FromQuery] Guid id,
        CancellationToken cancellationToken)
        => await mediator.Send(new PatientGetQuery(id), cancellationToken) is PatientVM<PatientIndexIdentityVM> value
        ? Ok(SuccessResponse.Create(value))
        : NotFound(ErrorResponse.Create($"Patient with ' {id} ' not found"));

    [HttpPut]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        [FromQuery] Guid id,
        [FromBody] PatientVM<PatientIdentityVM> patient,
        CancellationToken cancellationToken)
        => await mediator.Send(new PatientUpdateQuery(id, patient), cancellationToken)
        ? Ok(SuccessResponse.Create())
        : NotFound(ErrorResponse.Create($"Patient with '{id}' not found"));

    [HttpDelete]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(
        [FromQuery] Guid id,
        CancellationToken cancellationToken)
        => await mediator.Send(new PatientDeleteQuery(id), cancellationToken)
        ? Ok(SuccessResponse.Create())
        : NotFound(ErrorResponse.Create($"Patient with '{id}' not found"));
}
