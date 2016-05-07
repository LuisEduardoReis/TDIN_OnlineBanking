using System;


namespace RestService
{
    public class RestService : IRestService
    {
        static Users users = new Users();

        public Users GetUsers() {
            return users;            
        }
    }
}
