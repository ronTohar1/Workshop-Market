using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.BusinessLayer.Market;

namespace MarketBackend.BusinessLayer.Buyers.Members;

public class MembersController : IBuyersController
{
    private readonly ConcurrentDictionary<int, Member> members;
    private readonly Security security;
    //private const int invalidId = -1;

    private Mutex mutex;
    public MembersController()
    {
        members = new ConcurrentDictionary<int, Member>();
        this.security = new Security();
        this.mutex = new Mutex();
    }

    public Buyer? GetBuyer(int buyerId)
    {
        return GetMember(buyerId);
    }

    public int Register(string username, string password)
    {   
        if (this.security.CheckUsername(username) && this.security.CheckPassword(password))
        {
            lock (mutex)
            {
                if (!this.IsUsernameExists(username))
                {
                    Member member = new Member(username, this.security.HashPassword(password));
                    if (!this.AddMember(member))
                        throw new Exception("Could not add valid member to the members controller");
                    return member.Id;
                }
            }
        }
        throw new MarketException("Username or Password are not valid!");
    }

    public Member? GetMember(int memberId)
    {
        Member member;
        if (members.TryGetValue(memberId, out member))
        {
            return member;
        }
        return null;
    }

    private bool IsUsernameExists(string username)
    {
        foreach (Member member in members.Values)
            if (member.Username == username)
                return true;
        return false;
    }

    private bool AddMember(Member member)
    {
        return this.members.TryAdd(member.Id, member);
    }
}
