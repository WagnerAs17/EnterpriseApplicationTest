using Bogus;
using System;
using Volvo.Frota.API.Dtos;
using Volvo.Frota.API.Utils.Enum;

namespace Volvo.Frota.Test.Stub
{
    public static class UpdateCaminhaoDtoStub
    {
        private static Faker _faker = new Faker("pt_BR");
        public static UpdateCaminhaoDto Factory(int id, string nome, ModeloCaminhao modelo, int anoModelo)
        {
            return new UpdateCaminhaoDto
            {
                Id = id,
                Nome = nome,
                Modelo = modelo,
                AnoModelo = anoModelo
            };
        }

        public static UpdateCaminhaoDto Completo(int id)
        {
            return Factory(id, _faker.Random.String2(15), ModeloCaminhao.FH, DateTime.Now.Year);
        }

        public static UpdateCaminhaoDto SemNome(int id)
        {
            return Factory(id, string.Empty, ModeloCaminhao.FM, DateTime.Now.Year + 1);
        }

        public static UpdateCaminhaoDto ModeloInvalido(int id)
        {
            return Factory(id, _faker.Random.String2(15), 0, DateTime.Now.Year);
        }

        public static UpdateCaminhaoDto AnoInvalido(int id)
        {
            return Factory(id, _faker.Random.String2(15), ModeloCaminhao.FH, DateTime.Now.Year - 1);
        }

        public static UpdateCaminhaoDto IdInvalido(int id)
        {
            return Factory(id, _faker.Random.String2(15), ModeloCaminhao.FM, DateTime.Now.Year + 1);
        }
    }
}
