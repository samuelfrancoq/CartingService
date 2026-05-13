using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartingService.BLL.Interfaces;
using CartingService.BLL.Services;
using CartingService.DAL.Interfaces;
using CartingService.Domain.Entities;
using FluentAssertions;
using Moq;
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
        _repositoryMock.Setup(r => r.GetById(cartId))
                       .Returns(new Cart { Id = cartId, Items = new List<CartItem>() });

        _sut.AddItem(cartId, newItem);

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

    [Fact]
    public void GetItems_ShouldReturnList_WhenCartExists()
    {
        var cartId = "test-cart";
        var mockCart = new Cart
        {
            Id = cartId,
            Items = new List<CartItem> { new CartItem { Id = 1, Name = "Product A" } }
        };
        _repositoryMock.Setup(r => r.GetById(cartId)).Returns(mockCart);

        var result = _sut.GetItems(cartId);

        result.Should().NotBeNull();
        result.Should().ContainSingle(item => item.Name == "Product A");
    }

    [Fact]
    public void AddItem_ShouldCallRepository_WhenItemIsValid()
    {
        var cartId = "new-cart";
        var newItem = new CartItem { Id = 2, Name = "Product B", Quantity = 1 };
        _repositoryMock.Setup(r => r.GetById(cartId))
                       .Returns(new Cart { Id = cartId, Items = new List<CartItem>() });

        // Act
        _sut.AddItem(cartId, newItem);

        _repositoryMock.Verify(r => r.Update(It.Is<Cart>(c => c.Id == cartId)), Times.Once);
    }

    [Fact]
    public void RemoveItem_ShouldInvokeUpdateOnRepository()
    {
        var cartId = "test-cart";
        var itemId = 1;
        var cartWithItem = new Cart
        {
            Id = cartId,
            Items = new List<CartItem> { new CartItem { Id = itemId } }
        };

        _repositoryMock.Setup(r => r.GetById(cartId)).Returns(cartWithItem);

        _sut.RemoveItem(cartId, itemId);

        _repositoryMock.Verify(r => r.Update(It.IsAny<Cart>()), Times.Once);
    }
}
