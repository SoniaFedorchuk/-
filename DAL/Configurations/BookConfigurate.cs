using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class BookConfigurate : EntityTypeConfiguration<Models.Books>
    {
        public BookConfigurate()
        {
            this.HasKey(b => b.Id);
            this.Property(b => b.Name).IsRequired().HasMaxLength(50);
            this.Property(b => b.Publisher).IsRequired().HasMaxLength(50);
            this.Property(b => b.Author).IsRequired().HasMaxLength(50);
            this.Property(b => b.Genre).IsRequired().HasMaxLength(50);
            this.Property(b => b.Amount);
            this.Property(b => b.Price);
            this.Property(b => b.Year);
        }
    }
}
