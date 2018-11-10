using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace timetracker
{
    public class viewUser
    {
        private User _currentUser;
        ServerProxy proxy = new ServerProxy();
        public User User {

            get { return _currentUser; }
            set { _currentUser = value; }


        }
        public viewUser() {

            _currentUser = new User();
        }
        public void startServer()
        {
            
            
            //proxy.sendRequest().Wait();

        }
        public void sendInfo(LoginData u) {

            //proxy.postLogin(u).Wait();


        }
    }
}
