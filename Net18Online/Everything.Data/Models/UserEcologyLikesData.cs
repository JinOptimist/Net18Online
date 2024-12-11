using Everything.Data.Models;

namespace Everything.Data.Models;

public class UserEcologyLikesData
{
    public int UserId { get; set; }
    public UserData User { get; set; }

    public int EcologyDataId { get; set; }
    public EcologyData EcologyData { get; set; }
}
    