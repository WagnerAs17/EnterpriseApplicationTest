using Bogus;
using System;
using Volvo.Frota.API.Dtos;
using Volvo.Frota.API.Utils.Enum;

namespace Volvo.Frota.Test.Stub
{
    public static class NewCaminhaoDtoStub
    {
        private static readonly Faker _faker = new Faker("pt_BR");
        public static NewCaminhaoDto Factory(int ano, ModeloCaminhao modeloCaminhao, string nome)
        {
            return new NewCaminhaoDto
            {
                AnoModelo = ano,
                Modelo = modeloCaminhao,
                Nome = nome
            };
        }

        public static NewCaminhaoDto CompletoModeloCaminhaoFH()
        {
            return Factory(DateTime.Now.Year, ModeloCaminhao.FH, _faker.Random.String2(20));
        }

        public static NewCaminhaoDto CompletoModeloCaminhaoFM()
        {
            return Factory(DateTime.Now.Year + 1, ModeloCaminhao.FM, _faker.Random.String2(20));
        }

        public static NewCaminhaoDto AnoModeloInvalido()
        {
            return Factory(DateTime.Now.Year - 1, ModeloCaminhao.FH, _faker.Random.String2(20));
        }

        public static NewCaminhaoDto SemNome()
        {
            return Factory(DateTime.Now.Year, ModeloCaminhao.FM, string.Empty);
        }

        public static NewCaminhaoDto ModeloCaminhaoInvalido()
        {
            return Factory(DateTime.Now.Year, 0, _faker.Random.String2(20));
        }
    }
}
