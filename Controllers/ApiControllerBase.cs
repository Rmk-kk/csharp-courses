using System.Net.Mime;
namespace NetCoreCourse.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("[controller]s")]
public abstract class ApiControllerBase : ControllerBase 
{

}