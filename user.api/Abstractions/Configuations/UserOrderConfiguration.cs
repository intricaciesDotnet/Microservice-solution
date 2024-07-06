using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace user.api.Abstractions.Configuations;

public class UserOrderConfiguration : IEntityTypeConfiguration<UserOrder>
{
    public void Configure(EntityTypeBuilder<UserOrder> builder)
    {
        builder.HasKey(x => new { x.UserId, x.OrderId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserOrders)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Order)
            .WithMany(x => x.UserOrders)
            .HasForeignKey(x => x.OrderId);
    }
}
