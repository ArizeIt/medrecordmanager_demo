using FizzWare.NBuilder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using UgentCareDate;
using UrgentCareData.Models;

namespace UrgentCareData.Migrations
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<UrgentCareContext>();
            context.Database.EnsureCreated();
            if (!context.Set<ClinicProfile>().Any())
            {

                var clinics = Builder<ClinicProfile>.CreateListOfSize(4).All()
                    .With(c => c.AmdcodeName = Faker.Company.Name())
                    .With(c => c.AmdcodePrefix = Faker.Lorem.Sentence(3))
                    .With(c => c.ClinicFullName = Faker.Company.Name())
                    .With(c => c.OfficeKey = Faker.RandomNumber.Next());
                    
            }
        }
    }
}
