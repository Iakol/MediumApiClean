using Microsoft.IdentityModel.Tokens;

namespace UserDomain.Domain
{
    public class UserWrapper
    {
        public string UserId { get; private set; }
        public string Tag { get; private set; } 

        public UserWrapper(string UserId,string Tag){
            this.UserId = UserId;
            SetTag(Tag);
            }
        public void SetTag(string Tag) 
        {
            if (string.IsNullOrEmpty( Tag)) 
            {
                throw new ArgumentNullException("Tag can`t be empty");
            }
            this.Tag = Tag.Trim();
        }


    }
}
