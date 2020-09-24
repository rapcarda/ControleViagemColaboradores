using System.Collections.Generic;

namespace API.ViewModel.TokenControl
{
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}
