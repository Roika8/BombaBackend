using DATA;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDataContext : IdentityDbContext<User>
    {
        public UserDataContext([NotNull] DbContextOptions<UserDataContext> options) : base(options)
        {
        }
    }
}
