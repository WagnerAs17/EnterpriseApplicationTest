using Volvo.Frota.API;
using Xunit;

namespace Volvo.Frota.Test.Fixture
{
    [CollectionDefinition(nameof(CaminhaoFixtureCollection))]
    public class CaminhaoFixtureCollection : ICollectionFixture<CaminhaoTestFixture> { }
    public class CaminhaoTestFixture : IntegrationFixture<StartupTest>
    {
        public CaminhaoTestFixture()
        {}
    }
}
