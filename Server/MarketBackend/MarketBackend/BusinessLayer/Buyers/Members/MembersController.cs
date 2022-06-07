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
    private readonly IDictionary<int, Member> members;
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
                    Member member = this.createNewMember(username,password);
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

    public virtual Member? GetMember(int memberId)
    {
        members.TryGetValue(memberId, out Member? member);
        return member;
    }

    public virtual Member? GetMember(string username)
    {
        foreach (Member member in members.Values)
            if (member.Username == username)
                return member;
        return null;
    }

    public void RemoveMember(int memberId) {
        lock (mutex)
        {
            if (!members.Keys.Contains(memberId))
                throw new MarketException($"Failed to remove, there isn't such member with id: {memberId}");
            members.Remove(memberId);
        }
    }
    public IDictionary<int, Member> GetLoggedInMembers()
       => members.Where(member =>member.Value.LoggedIn).ToDictionary(mem =>mem.Key,mem=>mem.Value);
    
    private Member createNewMember(string username,string password)
    {
        return new Member(username,password,new Security());
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
        return username.Length < 10;
    }

    private bool CheckPassword(string password)
    {
        return true;
    }
}
