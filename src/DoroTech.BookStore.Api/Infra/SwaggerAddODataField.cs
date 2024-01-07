using System.Reflection;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DoroTech.BookStore.Api.Infra;

public class SwaggerAddODataField : IOperationFilter
{
    private const string OPERATION_DESCRIPTION = @"We allow the following OData operations:
<a target='_blank' href='http://docs.oasis-open.org/odata/odata/v4.01/cs01/part1-protocol/odata-v4.01-cs01-part1-protocol.html#sec_SystemQueryOptiontop'>Top</a> |
<a target='_blank' href='http://docs.oasis-open.org/odata/odata/v4.01/cs01/part1-protocol/odata-v4.01-cs01-part1-protocol.html#sec_SystemQueryOptionskip'>Skip</a> |
<a target='_blank' href='http://docs.oasis-open.org/odata/odata/v4.01/cs01/part1-protocol/odata-v4.01-cs01-part1-protocol.html#sec_SystemQueryOptionorderby'>OrderBy</a> |
<a target='_blank' href='http://docs.oasis-open.org/odata/odata/v4.01/cs01/part1-protocol/odata-v4.01-cs01-part1-protocol.html#sec_SystemQueryOptionfilter'>Filter</a>";

    private static readonly OpenApiParameter _topParameter = CreateODataParameter("number", "$top", "OData: Field that allow you to set how many registers will be returned(max 10)", "10");
    private static readonly OpenApiParameter _skipParameter = CreateODataParameter("number", "$skip", "OData: Field that allow to skip registers", "0");
    private static readonly OpenApiParameter _orderByParameter = CreateODataParameter("string", "$orderBy", "OData: Field that allow to order data", "fieldName desc, fieldName2 asc");
    private static readonly OpenApiParameter _filterParameter = CreateODataParameter("string", "$filter", "OData: Field that allow to filter data", "fieldName eq 'Some string' and intField eq 2");
    private static readonly OpenApiParameter[] _oDataParameters = [_filterParameter, _orderByParameter, _skipParameter, _topParameter];

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.GetCustomAttribute<EnableQueryAttribute>() == null) return;

        operation.Description = OPERATION_DESCRIPTION;

        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>(_oDataParameters);
        else
            foreach (var parameter in _oDataParameters)
                operation.Parameters.Add(parameter);
    }

    private static OpenApiParameter CreateODataParameter(string type, string name, string description, string example)
        => new OpenApiParameter
        {
            Schema = new OpenApiSchema() { Type = type },
            Name = name,
            Description = description,
            Example = new OpenApiString(example),
            Required = false,
            In = ParameterLocation.Query,
        };
}
