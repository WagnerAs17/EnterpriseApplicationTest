using Bogus;
using System;
using Volvo.Frota.API.Models;
using Volvo.Frota.API.Utils.Enum;

namespace Volvo.Frota.Test.Stub
{
    public class CaminhaoStub
    {
        private static readonly Faker _faker = new Faker("pt_BR");

        public static Caminhao GetById(int id)
        {
            return new Caminhao
            {
                Id = id,
                Nome = _faker.Random.String2(15),
                AnoFabricacao = DateTime.Now.Year,
                AnoModelo = DateTime.Now.Year + 1,
                Modelo = ModeloCaminhao.FM
            };
        }
    }
}
