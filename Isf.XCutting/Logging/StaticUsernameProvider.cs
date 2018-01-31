using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Logging
{
    public class StaticUsernameProvider : IUsernameProvider
    {
        private readonly string username;

        public StaticUsernameProvider(string username)
        {
            this.username = username;
        }

        public string Username { get { return username; } }
    }
}
