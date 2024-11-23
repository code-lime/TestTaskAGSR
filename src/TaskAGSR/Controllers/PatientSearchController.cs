using Hl7.Fhir.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskAGSR.Application.Common.VM;
using TaskAGSR.Application.Data.Queries;
using TaskAGSR.Models.Response;

namespace TaskAGSR.Controllers;

[ApiController]
[Route("patient/search")]
public class PatientSearchController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Get patients record by birth date with using format Hl7.Fhir.Search#DateTime
    /// </summary>
    /// <param name="birthDate">Search by birth date list with using format Hl7.Fhir.Search#DateTime</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <response code="200">Return patients record</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(SuccessResponse<IEnumerable<PatientVM<PatientIndexIdentityVM>>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBirthDateSearch(
        [FromQuery(Name = "birth_date")] IEnumerable<DateTimeSearch> birthDate,
        CancellationToken cancellationToken)
        => Ok(SuccessResponse.Create(await mediator.Send(new PatientSearchByBirthDateQuery(birthDate), cancellationToken)));
}
