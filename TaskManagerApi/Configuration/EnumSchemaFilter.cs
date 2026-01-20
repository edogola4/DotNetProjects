using System.ComponentModel;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskManagerApi.Configuration;

/// <summary>
/// Schema filter to provide better enum documentation in Swagger.
/// </summary>
/// <remarks>
/// This class is currently disabled due to compatibility issues with Swashbuckle 10 and .NET 10.
/// The ISchemaFilter interface requires different parameter types in the newer version.
/// TODO: Update to use IOpenApiSchema when Swashbuckle/OpenAPI fully support .NET 10.
/// </remarks>
/*
public class EnumSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Applies enum descriptions to OpenAPI schema.
    /// </summary>
    /// <param name="schema">The schema to modify.</param>
    /// <param name="context">The schema filter context.</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            var enumValues = new List<IOpenApiAny>();
            var enumDescriptions = new List<string>();

            foreach (var enumValue in Enum.GetValues(context.Type))
            {
                var enumName = enumValue.ToString();
                var enumMember = context.Type.GetMember(enumName!).FirstOrDefault();
                var descriptionAttribute = enumMember?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .Cast<DescriptionAttribute>()
                    .FirstOrDefault();

                enumValues.Add(new OpenApiString(enumName));
                enumDescriptions.Add(descriptionAttribute?.Description ?? enumName!);
            }

            schema.Enum = enumValues;
            schema.Description = string.Join(", ", enumDescriptions.Select((desc, i) => 
                $"{enumValues[i]}: {desc}"));
        }
    }
}
*/