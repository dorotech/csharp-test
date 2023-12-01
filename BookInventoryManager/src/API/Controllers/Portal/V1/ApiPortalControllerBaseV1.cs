using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Portal.V1;

[AllowAnonymous]
[Route("api/portal/v1/[controller]")]
public class ApiPortalControllerBaseV1 : ApiControllerBase
{
    
}