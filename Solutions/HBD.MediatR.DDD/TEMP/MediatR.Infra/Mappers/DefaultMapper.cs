﻿
using HBD.EfCore.Extensions.Configurations;

namespace MediatR.Infra.Mappers;

internal class DefaultMapper<T> : EntityTypeConfiguration<T> where T : class
{
    //public override void Configure(EntityTypeBuilder<T> builder)
    //{
    //    base.Configure(builder);
    //}
}