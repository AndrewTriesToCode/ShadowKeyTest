// Copyright Finbuckle LLC, Andrew White, and Contributors.
// Refer to the solution LICENSE file for more inforation.

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ShadowKeyTest;

public class ShadowKeyGenerator : ValueGenerator<string?>
{
    public override string? Next(EntityEntry entry)
    {
        return ((MyDbContext)entry.Context).ShadowKeyValueOnGenerate;
    }

    public override bool GeneratesTemporaryValues => false;
}