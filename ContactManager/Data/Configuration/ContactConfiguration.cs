using ContactManager.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManager.Data.Configuration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.MobilePhone)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(x => x.JobTitle)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.BirthDate)
            .IsRequired()
            .HasColumnType("datetime");
    }
}