﻿using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{

    private readonly IConfiguration _config;

    public DiscountRepository(IConfiguration config)
    {
        _config = config;
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString"));

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

        if (coupon is null)
            return new Coupon { ProductName = "No Discount", Description = "There is no discount", Amount = 0 };

        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString"));

        int affected = await connection.ExecuteAsync
            ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)", new { ProductName = coupon, Description = coupon.Description, Amount = coupon.Amount });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString"));

        int affected = await connection.ExecuteAsync
            ("UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id=@Id", new { Id = coupon.Id, ProductName = coupon, Description = coupon.Description, Amount = coupon.Amount });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString"));

        int affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName=@ProductName");

        if (affected == 0)
            return false;

        return true;
    }

}
