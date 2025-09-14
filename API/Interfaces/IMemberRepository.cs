
using API.Entities;

namespace API.Interfaces;

public interface IMemberRepository
{
    void UpdateMember(Member member);

    Task<bool> SaveAllAsync();

    Task<IReadOnlyList<Member>> GetMembersAsync();
    Task<Member?> GetMemberByIdAsync(string id);

    Task<IReadOnlyList<Photo>> GetPhotosByMemberIdAsync(string memberId);
}
