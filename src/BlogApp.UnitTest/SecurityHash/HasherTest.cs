using BlogApp.Api.Commons.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlogApp.UnitTest.Security
{
    public class HasherTest
    {
        [Fact]
        public void ReturnValidHash()
        {
            string password = "A07102002a!";
            string oldHash = "$2a$11$U/jBuvLCClrwyHFbhIa/0OiJ3m1ZopW4U9mRPjAOAyO6a/jjZ6ImS";
            var isCompleted = PasswordHasher.Verify(password, "47f1eb8f-6450-4da9-9bde-6859056d0a35", oldHash);


            
        }
    }
}
