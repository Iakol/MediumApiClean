using Microsoft.IdentityModel.Tokens;

namespace UserDomain.Domain
{
    public class UserWrapper
    {
        public string UserWrapperId { get; private set; }


        private string _Tag
        {
            get; set;
        }

        public string Tag { get => _Tag; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Tag can`t be empty");
                }
                _Tag = value;
            }
        }

        public UserWrapper() { }   

        public UserWrapper(string UserId,string Tag){
            this.UserWrapperId = UserId;
            this.Tag = Tag;
            }


    }
}
