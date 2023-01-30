namespace Server
{
    internal class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Admin(int id, string username, string password) {
            this.Id = id;
            this.Username = username;
            this.Password = password;
        }
    }
}
