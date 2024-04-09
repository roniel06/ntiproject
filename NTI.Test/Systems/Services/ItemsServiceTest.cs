using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using NTI.Application.InputModels.Items;
using NTI.Application.Services;

namespace NTI.Test.Systems.Services
{
    public class ItemsServiceTest
    {
        
        private readonly ItemsService _sut;

        public ItemsServiceTest()
        {
            
        }

        [Fact]
        public async Task Create_Item_ShouldReturnCreatedEntityAsync()
        {
            // Arrange 
            var fixture = new Fixture();
            var item = fixture.Build<ItemInputModel>().With(x => x.Id, 0).Create();

            // Act

            

            // Assert
            Assert.NotNull(null);

        }
    }
}