
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]

    public class MembersController(IMemberRepository memberRepository) : BaseApiController
    {
        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            var members = await memberRepository.GetMembersAsync();
            return Ok(members);
        }

        [HttpGet("{id}")] // localhost:5001/api/members/bob-id
        public async Task<ActionResult<Member>> GetMember(string id)
        {
            var member = await memberRepository.GetMemberByIdAsync(id);
            if (member == null)
                return NotFound();
            return Ok(member);
        }

        [HttpGet("{id}/photos")]

        public async Task<ActionResult<IReadOnlyList<Photo>>> GetPhotosForMember(string id)
        {
            return Ok(await memberRepository.GetPhotosByMemberIdAsync(id));
        }
    }
}