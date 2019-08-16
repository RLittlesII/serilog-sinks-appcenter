using System;
namespace LoginSample
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("Wrong User and/or Password")
        {
        }
    }
}
