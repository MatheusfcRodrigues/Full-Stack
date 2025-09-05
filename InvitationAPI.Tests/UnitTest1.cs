using InvitationAPI.Models;
using Xunit;

namespace InvitationAPI.Tests;

public class InvitationTests
{
    [Fact]
    public void DefaultStatus_ShouldBeInvited()
    {
        var invite = new Invitation { Name = "Matheus" };
        Assert.Equal("Invited", invite.Status);
    }
}
