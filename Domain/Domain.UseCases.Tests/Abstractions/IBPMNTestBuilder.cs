using System;

namespace Domain.UseCases.Tests.Abstractions
{
    public interface IBPMNTestBuilder
    {
        IBPMNTestBuilderStep1 FromDefinition(string defintion);
        IBPMNTestBuilderStep1 FromUri(Uri defintion);
    }
}