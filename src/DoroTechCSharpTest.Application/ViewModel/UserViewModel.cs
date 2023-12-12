namespace DoroTechCSharpTest.Application.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ManagerId { get; set; }
        public int RoleId { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
