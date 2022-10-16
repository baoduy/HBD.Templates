using Mapster;

namespace MediatR.AppServices;

public class CodeGenerationRegister : ICodeGenerationRegister
{
    public void Register(CodeGenerationConfig config)
    {
        config.AdaptTo("[name]Dto", MapType.Map | MapType.Projection | MapType.MapToTarget)
            .ForAllTypesInNamespace(typeof(Domains.Share.DomainEntity).Assembly, "MediatR.Domains.Features")
            .ExcludeTypes(t => t.IsInterface || t.IsAbstract || t.IsEnum)
            .PreserveReference(true)
            .ShallowCopyForSameType(true);

        //config.GenerateMapper("[name]Mapper")
        //    .ForAllTypesInNamespace(typeof(Domains.Share.DomainEntity).Assembly, "MediatR.Domains.Features");
    }
}