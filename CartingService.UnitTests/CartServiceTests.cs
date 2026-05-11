using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using CartingService.BLL.Services;
using CartingService.DAL.Interfaces;
using CartingService.Domain.Entities;
using Xunit;

namespace CartingService.UnitTests;

public class CartServiceTests
{
    private readonly Mock<ICartRepository> _repositoryMock;
    private readonly CartService _sut;

    public CartServiceTests()
    {
        _repositoryMock = new Mock<ICartRepository>();
        _sut = new CartService(_repositoryMock.Object);
    }

    [Fact]
    public void AddItem_ShouldCallUpdate_WhenItemIsNew()
    {
        var cartId = "test-cart";
        var newItem = new CartItem { Id = 1, Name = "Test Product", Quantity = 1 };
        // We simulate that the repository returns an empty cart
        _repositoryMock.Setup(r => r.GetById(cartId))
                       .Returns(new Cart { Id = cartId, Items = new List<CartItem>() });

        _sut.AddItem(cartId, newItem);

        // We check if the repository's Update method was called exactly once
        _repositoryMock.Verify(r => r.Update(It.IsAny<Cart>()), Times.Once);
    }

    [Fact]
    public void GetItems_ShouldReturnCorrectCount()
    {
        var cartId = "test-cart";
        var mockCart = new Cart
        {
            Id = cartId,
            Items = new List<CartItem> { new CartItem { Id = 1 }, new CartItem { Id = 2 } }
        };

        _repositoryMock.Setup(r => r.GetById(cartId)).Returns(mockCart);

        var result = _sut.GetItems(cartId);

        result.Should().HaveCount(2);
        _repositoryMock.Verify(r => r.GetById(cartId), Times.Once);
    }
}
