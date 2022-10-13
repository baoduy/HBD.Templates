
using HBDStack.EfCore.Extensions.Configurations;

namespace TEMP.Infra.Share;

internal class DefaultMapper<T> : EntityTypeConfiguration<T> where T : class
{
    //public override void Configure(EntityTypeBuilder<T> builder)
    //{
    //    base.Configure(builder);
    //}
}