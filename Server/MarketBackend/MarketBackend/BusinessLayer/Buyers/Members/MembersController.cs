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
    private Mutex mutex;

    public MembersController()
    {
        members = new ConcurrentDictionary<int, Member>();
        this.mutex = new Mutex();
    }

    public Buyer? GetBuyer(int buyerId)
    {
        return GetMember(buyerId);
    }


    /*  Registers a new member to the controller.
    *   First, validates the username and password.
    *   Second, checks the username is unique.
    *   If conditions met then adding a new member.
    *   Returns: ID of new member.
    *   Throws: 
    *       - MarketException when username or password not valid or taken
    *       - Exception otherwise.
    */
    public int Register(string username, string password)
    {   
        if (this.CheckUsername(username) && this.CheckPassword(password))
        {
            lock (mutex)
            {
                if (!this.IsUsernameExists(username))
                {
                    Member member = new Member(username, password);
                    if (!this.AddMember(member))
                        throw new Exception("Could not add valid member to the members controller");
                    return member.Id;
                }
                else
                    throw new MarketException("Username already exists");
            }
        }
        throw new MarketException("Username or Password are not valid!");
    }

    public Member? GetMember(int memberId)
    {
        members.TryGetValue(memberId, out Member? member);
        return member;
    }

    public Member? GetMember(string username)
    {
        foreach (Member member in members.Values)
            if (member.Username == username)
                return member;
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

    private bool CheckUsername(string username)
    {
        return true;
    }

    private bool CheckPassword(string username)
    {
        return true;
    }
}
